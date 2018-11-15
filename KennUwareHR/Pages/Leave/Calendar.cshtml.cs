using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KennUwareHR.Pages_Leave
{
    [Authorize(Roles = "Employee, HR, Admin")]
    public class CalendarModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public CalendarModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        
        public IList<Leave> Leave { get; set; }
        public Employee Employee { get; set; }
        public int UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            UserId = Convert.ToInt32(User.Claims
                            .Where(c => c.Type == ClaimTypes.PrimarySid)
                            .Select(c => c.Value)
                            .First());

            if (id != null)
            {
                Employee = await _context.Employees
                                     .Include(e => e.Region)
                                     .Include(e => e.Role)
                                     .Include(e => e.Department)
                                     .Include(e => e.Position)
                                     .SingleOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                return Redirect("/Leave/Calendar?id=" + UserId);
            }

            if (Employee == null)
            {
                return NotFound();
            }

            Leave = _context.Leaves.Where(e => e.EmployeeId == Employee.Id).ToList();

            
            return Page();

        }
    }
}