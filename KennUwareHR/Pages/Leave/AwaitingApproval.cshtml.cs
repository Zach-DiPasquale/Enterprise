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

namespace KennUwareHR.Pages.Leave
{
    [Authorize(Roles = "HR, Admin, Employee")]
    public class ApprovalModel : PageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public ApprovalModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public IList<KennUwareHR.Models.Leave> Leave { get;set; }
        public string EmpId { get; set; }

        public async Task OnGetAsync()
        {
            EmpId = User.Claims
                       .Where(c => c.Type == ClaimTypes.PrimarySid)
                       .Select(c => c.Value)
                       .First();

            Leave = await _context.Leaves
                .Include(l => l.Employee).ToListAsync();
        }
    }
}
