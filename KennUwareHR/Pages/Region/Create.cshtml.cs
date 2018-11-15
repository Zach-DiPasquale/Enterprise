using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KennUwareHR.Contexts;
using KennUwareHR.Models;

namespace KennUwareHR.Pages_Region
{
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
        public Region Region { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Regions.Add(Region);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}