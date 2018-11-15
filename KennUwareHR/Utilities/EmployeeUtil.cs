using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KennUwareHR.Models;
using KennUwareHR.Contexts;
using KennUwareHR.Controllers;
using Microsoft.EntityFrameworkCore;


namespace KennUwareHR.Utilities
{
    public class EmployeeUtil
    {

        private static HRContext _context;

        public EmployeeUtil(HRContext context)
        {
            _context = context;
        }

        public EmployeeAPIDAO GetEmployee(int Id)
        {
            EmployeeAPIDAO emp;

            try
            {
                emp = _context.Employees
                           .Where(e => e.Id == Id)
                           .Select(e => new EmployeeAPIDAO
                           {
                               Id = e.Id,
                               RoleName = e.Role.Title,
                               RegionName = e.Region.Name,
                               FirstName = e.FirstName,
                               LastName = e.LastName,
                               DateOfBirth = e.DateOfBirth,
                               DepartmentName = e.Department.Name,
                               PositionName = e.Position.Title
                           })
                          ?.First();
            } 
            catch
            {
                emp = null;
            }


            return emp;
        }

        public EmployeeAPIDAO GetEmployeeByUserId(int UserId)
        {
            EmployeeAPIDAO emp;

            try
            {
                emp = _context.Employees
                              .Where(e => e.UserId == UserId)
                              .Select(e => new EmployeeAPIDAO
                              {
                                  Id = e.Id,
                                  RoleName = e.Role.Title,
                                  RegionName = e.Region.Name,
                                  FirstName = e.FirstName,
                                  LastName = e.LastName,
                                  DateOfBirth = e.DateOfBirth,
                                  DepartmentName = e.Department.Name,
                                  PositionName = e.Position.Title
                              })
                              ?.First();
            }
            catch
            {
                emp = null;
            }


            return emp;
        }


        public List<EmployeeAPIDAO> GetEmployees(string region, string position,
                                                   string department, int offset,
                                                   int limit)
        {


            IQueryable<Employee> Employees = _context.Employees
                                                     .OrderBy(c => c.Id)
                                                     .Skip(offset)
                                                     .Take(limit);

            if (!String.IsNullOrEmpty(region))
            {
                Employees = Employees.Where(e => e.Region.Name == region);
            }

            if (!String.IsNullOrEmpty(position))
            {
                Employees = Employees.Where(e => e.Position.Title == position);
            }

            if (!String.IsNullOrEmpty(department))
            {
                Employees = Employees.Where(e => e.Department.Name == department);
            }

            List<EmployeeAPIDAO> Emp = Employees
                                                .Select(e => new EmployeeAPIDAO
                                                {
                                                    Id = e.Id,
                                                    RoleName = e.Role.Title,
                                                    RegionName = e.Region.Name,
                                                    FirstName = e.FirstName,
                                                    LastName = e.LastName,
                                                    DateOfBirth = e.DateOfBirth,
                                                    DepartmentName = e.Department.Name,
                                                    PositionName = e.Position.Title
                                                })
                                                .ToList();

                                               
            return Emp;
        }

    }


}