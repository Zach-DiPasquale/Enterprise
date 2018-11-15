using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Pages.Leave;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace KennUwareHR.Pages_Leave
{
    [Authorize(Roles = "Employee, HR, Admin")]
    public class CreateModel : LeavePageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public CreateModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateEmployeeDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Leave Leave { get; set; }

        public int UserId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            UserId = Convert.ToInt32(User.Claims
                        .Where(c => c.Type == ClaimTypes.PrimarySid)
                        .Select(c => c.Value)
                        .First());

            var emptyLeave = new Leave();
            if (!ModelState.IsValid)
            {
                PopulateEmployeeDropDownList(_context);
                return Page();
            }

            Leave.Approved = false;
            if(await TryUpdateModelAsync<Leave>(emptyLeave, "leave", 
                l => l.Approved, l => l.StartTime, l => l.EndTime, l => l.EmployeeId))
            {
                _context.Leaves.Add(emptyLeave);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Calendar", new { id = UserId});
            }

            PopulateEmployeeDropDownList(_context, emptyLeave.Employee);

            return Page();
        }
    }
}