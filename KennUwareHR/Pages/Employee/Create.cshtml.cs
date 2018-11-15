using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Pages.Employee;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace KennUwareHR.Pages_Employee
{
    [Authorize(Roles = "Admin,HR")]
    public class CreateModel : EmployeePageModel
    {
        private readonly KennUwareHR.Contexts.HRContext _context;

        public CreateModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateRegionDropDownList(_context);
            PopulateRoleDropDownList(_context);
            PopulatePositionDropDownList(_context);
            PopulateDepartmentDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public String UserName { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public String Password { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateRegionDropDownList(_context);
                PopulateRoleDropDownList(_context);
                PopulatePositionDropDownList(_context);
                PopulateDepartmentDropDownList(_context);
                return Page();
            }

            var emptyEmployee = new Employee();

            NewUser newUser = await CreateUser(UserName, Password);

            if (await TryUpdateModelAsync<Employee>(
                emptyEmployee,
                "employee",   // Prefix for form value.
                e => e.FirstName, e => e.LastName, e => e.RegionId,
                e => e.DateOfBirth, e => e.Address, e => e.PhoneNumber,
                e => e.RoleId, e => e.DepartmentId, e => e.PositionId,
                e => e.Commission, e => e.Salary))
            {
                
                emptyEmployee.UserId = newUser.Id;
                _context.Employees.Add(emptyEmployee);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateRegionDropDownList(_context, emptyEmployee.Region);
            PopulateRoleDropDownList(_context, emptyEmployee.Role);
            PopulatePositionDropDownList(_context);
            PopulateDepartmentDropDownList(_context);
            return Page();
        }
    }
}