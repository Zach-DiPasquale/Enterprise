using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using System.Security.Claims;

namespace KennUwareHR.Pages.Leave
{
    public class LeavePageModel : PageModel
    {
        public SelectList EmployeeSL { get; set; }
        public int UserId { get; set; }
        /*
        public void PopulateEmployeeDropDownList(HRContext _context, object selectedEmployee = null)
        {
            var EmployeeQuery = from e in _context.Employees orderby e.LastName select e;
            EmployeeSL = new SelectList(EmployeeQuery.AsNoTracking(), "Id", "FullName", selectedEmployee);
        }
        */
        public void PopulateEmployeeDropDownList(HRContext _context, object selectedEmployee = null)
        {
            UserId = Convert.ToInt32(User.Claims
                       .Where(c => c.Type == ClaimTypes.PrimarySid)
                       .Select(c => c.Value)
                       .First());
            //Rethink this. This will probably break when editing? Wait... Nevermind.
            var EmployeeQuery = from e in _context.Employees where e.Id == UserId orderby e.LastName select e;
            EmployeeSL = new SelectList(EmployeeQuery.AsNoTracking(), "Id", "FullName", selectedEmployee);
        }

    }
}
