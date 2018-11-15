using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KennUwareHR.Pages_Leave
{
    [Authorize(Roles = "HR, Employee, Admin")]
    public class DenyModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public DenyModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Leave Leave { get; set; }

        public string UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UserId = User.Claims
                       .Where(c => c.Type == ClaimTypes.PrimarySid)
                       .Select(c => c.Value)
                       .First();

            if (id == null)
            {
                return NotFound();
            }

            Leave = await _context.Leaves.Include(l => l.Employee).SingleOrDefaultAsync(m => m.Id == id);

            if (Leave == null)
            {
                return NotFound();
            }

            if (Leave.Employee.MyManagerId != Convert.ToInt32(UserId) && !User.IsInRole("HR"))
            {
                return RedirectToPage("/Leave/Index/");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Leave = await _context.Leaves.FindAsync(id);

            if (Leave != null)
            {
                _context.Leaves.Remove(Leave);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./AwaitingApproval");
        }
    }
}
