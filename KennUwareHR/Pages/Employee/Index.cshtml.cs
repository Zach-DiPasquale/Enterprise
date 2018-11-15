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

namespace KennUwareHR.Pages_Employee
{
    public class IndexModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public IndexModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; }

        public string EmpId { get; set; }

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

            if (role != "Admin" && role != "HR")
            {
                Employee = await _context.Employees
                                     .Where(e => e.MyManagerId == Convert.ToInt32(EmpId))
                                     .Include(e => e.Region)
                                     .Include(e => e.Department)
                                     .AsNoTracking()
                                     .ToListAsync();

                if(Employee.Count() == 0)
                {
                    return Redirect("/Employee/Details?id=" + EmpId);
                }
            } else {
                Employee = await _context.Employees
                                     .Include(e => e.Region)
                                     .Include(e => e.Department)
                                     .AsNoTracking()
                                     .ToListAsync();
            }
            
            return Page();
        }
    }
}
