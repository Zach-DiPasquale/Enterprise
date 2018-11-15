using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace KennUwareHR.Pages_Paycheck
{
    [Authorize(Roles = "HR,Admin")]
    public class CreateModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public CreateModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Paycheck Paycheck { get; set; }

        public async Task<IActionResult> OnPostAsync(string monthlyBonus, string quarterlyBonus)
        {
            var SalaryUtilities = new SalaryUtil(_context);

            var returnJSON = await SalaryUtilities.CalculateSalary(Paycheck, Convert.ToBoolean(monthlyBonus), Convert.ToBoolean(quarterlyBonus));

            return RedirectToPage("./Index");
        }
    }
}