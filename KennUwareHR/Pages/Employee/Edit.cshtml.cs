using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Pages.Employee;
using System.Security.Claims;

namespace KennUwareHR.Pages_Employee
{
    public class EditModel : EmployeePageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public EditModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public int EmpId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                PopulateRegionDropDownList(_context);
                PopulateRoleDropDownList(_context);
                PopulatePositionDropDownList(_context);
                PopulateDepartmentDropDownList(_context);
                PopulateManagerDropDownList(_context);
                return NotFound();
            }

            Employee = await _context.Employees
                                     .Include(e => e.Region)
                                     .Include(e => e.Role)
                                     .FirstOrDefaultAsync(e => e.Id == id);

            EmpId = Convert.ToInt32(User.Claims
                        .Where(c => c.Type == ClaimTypes.PrimarySid)
                        .Select(c => c.Value)
                        .First());

            if (Employee == null)
            {
                return NotFound();
            }

            PopulateRegionDropDownList(_context, Employee.RegionId);
            PopulateRoleDropDownList(_context, Employee.RoleId);
            PopulatePositionDropDownList(_context, Employee.PositionId);
            PopulateDepartmentDropDownList(_context, Employee.DepartmentId);
            PopulateManagerDropDownList(_context, Employee.MyManagerId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var employeeToUpdate = await _context.Employees.FindAsync(id);

            if (await TryUpdateModelAsync<Employee>(
                employeeToUpdate,
                 "employee",   // Prefix for form value.
                e => e.FirstName, e => e.LastName, e => e.RegionId,
                e => e.DateOfBirth, e => e.Address, e => e.PhoneNumber,
                e => e.RoleId, e => e.DepartmentId, e => e.PositionId,
                e => e.Commission, e => e.Salary, e => e.MyManagerId,
                e => e.UserId))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateRegionDropDownList(_context, employeeToUpdate.Region);
            PopulateRoleDropDownList(_context, employeeToUpdate.Role);
            PopulatePositionDropDownList(_context);
            PopulateRoleDropDownList(_context);
            PopulateManagerDropDownList(_context, Employee.MyManagerId);
            return Page();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
