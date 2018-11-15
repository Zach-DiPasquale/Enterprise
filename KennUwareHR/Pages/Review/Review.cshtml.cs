using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;

namespace KennUwareHR.Pages.Employee
{
    public class ReviewPageModel : PageModel
    {
        public SelectList ReviewedEmployeeNameSL { get; set; }

        public void PopulateReviewedEmployeeDropDownList(HRContext _context,
            object selectedEmployee = null)
        {
            var EmployeeQuery = from r in _context.Employees
                                                  orderby r.LastName // Sort by name.
                                                 select r;


            ReviewedEmployeeNameSL = new SelectList((from r in EmployeeQuery.ToList()
                                                     select new
                                                     {
                                                         Id = r.Id,
                                                         FullName = r.FirstName + " " + r.LastName
                                                     }), "Id", "FullName", selectedEmployee);
        }

        public SelectList ReviewingEmployeeNameSL { get; set; }

        public void PopulateReviewingEmployeeDropDownList(HRContext _context,
            object selectedEmployee = null)
        {
            var EmployeeQuery = from r in _context.Employees
                                orderby r.LastName // Sort by name.
                                select r;


            ReviewingEmployeeNameSL = new SelectList((from r in EmployeeQuery.ToList()
                                                      select new
                                                      {
                                                          Id = r.Id,
                                                          FullName = r.FirstName + " " + r.LastName
                                                      }), "Id", "FullName", selectedEmployee);

                //EmployeeQuery.AsNoTracking(),
                                            //"Id", "FirstName", selectedEmployee);
        }

       
    }
}
