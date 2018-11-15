using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using KennUwareHR.Models;

namespace KennUwareHR.Pages_Leave
{
    public class DetailsModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public DetailsModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public Leave Leave { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Leave = await _context.Leaves.Include(l => l.Employee).SingleOrDefaultAsync(m => m.Id == id);

            if (Leave == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
