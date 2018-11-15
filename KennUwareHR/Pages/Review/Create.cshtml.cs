using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Pages.Employee;
using System.Security.Claims;

namespace KennUwareHR.Pages_Review
{
    public class CreateModel : ReviewPageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public CreateModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }

        public IActionResult OnGet()
        {
            PopulateReviewedEmployeeDropDownList(_context);
            // PopulateReviewingEmployeeDropDownList(_context);
            var empId = Convert.ToInt32(
                                User.Claims
                                .Where(c => c.Type == ClaimTypes.PrimarySid)
                                .Select(c => c.Value)
                                .First()
                            );

            Employee = _context.Employees
                            .Where(e => e.Id == empId)
                            .First();
            
            return Page();
        }

        [BindProperty]
        public Review Review { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyReview = new Review();

            var empId = Convert.ToInt32(
                                User.Claims
                                .Where(c => c.Type == ClaimTypes.PrimarySid)
                                .Select(c => c.Value)
                                .First()
                            );

            if (await TryUpdateModelAsync<Review>(
                emptyReview,
                 "review",   // Prefix for form value.
                e => e.Comment, e => e.ReviewedEmployeeId))
            {
                emptyReview.ReviewerEmployeeId = empId;
                _context.Reviews.Add(emptyReview);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateReviewedEmployeeDropDownList(_context);
            PopulateReviewingEmployeeDropDownList(_context);
            return Page();
        }
    }
}
