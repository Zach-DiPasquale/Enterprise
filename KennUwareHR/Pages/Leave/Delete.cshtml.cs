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
    [Authorize(Roles = "Employee, HR, Admin")]
    public class DeleteModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public DeleteModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Leave Leave { get; set; }

        public string UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Leave = await _context.Leaves.SingleOrDefaultAsync(m => m.Id == id);

            UserId = User.Claims
                      .Where(c => c.Type == ClaimTypes.PrimarySid)
                      .Select(c => c.Value)
                      .First();

            if (Convert.ToInt32(UserId) != Leave.EmployeeId)
            {
                return RedirectToPage("/Leave/Index");
            }

            if (Leave == null)
            {
                return NotFound();
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

            return RedirectToPage("./Index");
        }
    }
}
