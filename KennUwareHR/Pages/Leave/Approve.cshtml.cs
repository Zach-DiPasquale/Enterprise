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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KennUwareHR.Pages.Leave
{
    [Authorize(Roles = "HR, Employee, Admin")]
    public class ApproveModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public ApproveModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        [BindProperty]
        public KennUwareHR.Models.Leave Leave { get; set; }

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

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName");
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

            return RedirectToPage("./AwaitingApproval");
        }

        private bool LeaveExists(int id)
        {
            return _context.Leaves.Any(e => e.Id == id);
        }
    }
}
