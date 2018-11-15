using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using System.Security.Claims;

namespace KennUwareHR.Pages_Review
{
    public class IndexModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public IndexModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public IList<Review> Review { get;set; }

        public async Task OnGetAsync()
        {
            var empId = Convert.ToInt32(
                User.Claims
                    .Where(c => c.Type == ClaimTypes.PrimarySid)
                    .Select(c => c.Value)
                    .First()
            );

            var role = User.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
                            .First();

            if (role != "Admin" && role != "HR")
            {
            Review = await _context.Reviews
                            .Where(r => r.ReviewedEmployeeId == empId ||
                                        r.ReviewerEmployeeId == empId)
                            .Include(r => r.ReviewedEmployee)
                            .Include(r => r.ReviewerEmployee)
                            .ToListAsync();
            } else {
                Review = await _context.Reviews
                                .Include(r => r.ReviewedEmployee)
                                .Include(r => r.ReviewerEmployee)
                                .ToListAsync();
            }
        }
    }
}
