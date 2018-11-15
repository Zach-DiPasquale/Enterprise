using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Controllers;
using KennUwareHR.Utilities;

namespace KennUwareHR.Pages_Employee
{
    public class DetailsModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public DetailsModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }
        
        public Employee Manager { get; set; }

        public List<Paycheck> Paychecks { get; set; }

        public List<Review> Reviews { get; set; }

        public List<CSReviewsDAO> CSReviews { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employees
                                     .Include(e => e.Region)
                                     .Include(e => e.Role)
                                     .Include(e => e.Department)
                                     .Include(e => e.Position)
                                     .SingleOrDefaultAsync(m => m.Id == id);

            Manager = _context.Employees
                            .Where(e => e.Id == Employee.MyManagerId) 
                            .SingleOrDefault();

            Paychecks = _context.Paychecks
                                .Where(e => e.EmployeeId == Employee.Id)
                                .ToList();

            Reviews = _context.Reviews
                              .Include(e => e.ReviewerEmployee)
                              .Where(e => e.ReviewedEmployeeId == Employee.Id)
                              .ToList();

            CSReviews = await new ReviewUtil(_context).GetCSReviews(Employee.Id);


            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
