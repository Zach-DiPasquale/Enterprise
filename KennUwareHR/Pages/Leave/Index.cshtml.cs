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
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace KennUwareHR.Pages_Leave
{
    [Authorize(Roles = "Admin, Employee, HR")]
    public class IndexModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public IndexModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public IList<Leave> Leave { get;set; }

        public string EmpId { get; set; }

        public IList<Leave> myLeaves { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            EmpId = User.Claims
                       .Where(c => c.Type == ClaimTypes.PrimarySid)
                       .Select(c => c.Value)
                       .First();

            var role = User.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
                            .First();

            Leave = await _context.Leaves.Include(l => l.Employee).AsNoTracking().ToListAsync();
            myLeaves = await _context.Leaves.Include(l => l.Employee).Where(e => e.EmployeeId == Convert.ToInt32(EmpId)).ToListAsync();
            return Page();
        }
    }
}
