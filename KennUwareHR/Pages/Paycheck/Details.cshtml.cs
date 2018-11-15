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

namespace KennUwareHR.Pages_Paycheck
{
    [Authorize(Roles = "HR,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public DetailsModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public Paycheck Paycheck { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Paycheck = await _context.Paychecks.SingleOrDefaultAsync(m => m.Id == id);

            if (Paycheck == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
