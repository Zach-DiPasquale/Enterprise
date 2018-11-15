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

namespace KennUwareHR.Pages_Region
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public IndexModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public IList<Region> Region { get;set; }

        public async Task OnGetAsync()
        {
            Region = await _context.Regions.ToListAsync();
        }
    }
}
