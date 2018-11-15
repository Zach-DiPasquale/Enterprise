using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Pages.Leave;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KennUwareHR.Pages_Leave
{
    [Authorize(Roles = "Employee, HR, Admin")]
    public class EditModel : LeavePageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public EditModel(KennUwareHR.Contexts.HRContext context)
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

            Leave = await _context.Leaves.Include(l => l.Employee).SingleOrDefaultAsync(m => m.Id == id);

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

            PopulateEmployeeDropDownList(_context, Leave.EmployeeId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Leave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveExists(Leave.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LeaveExists(int id)
        {
            return _context.Leaves.Any(e => e.Id == id);
        }
    }
}
