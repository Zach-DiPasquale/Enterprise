using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using KennUwareHR.Models;

namespace KennUwareHR.Pages_Region
{
    public class DetailsModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public DetailsModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public Region Region { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Region = await _context.Regions.SingleOrDefaultAsync(m => m.Id == id);

            if (Region == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
