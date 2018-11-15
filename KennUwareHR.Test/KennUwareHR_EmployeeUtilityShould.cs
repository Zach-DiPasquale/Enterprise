using System;
using Xunit;
using KennUwareHR.Contexts;
using KennUwareHR.Utilities;
using KennUwareHR.Models;
using System.Collections.Generic;

namespace KennUwareHR.Test.Utilities
{
    public class KennUwareHR_EmployeeUtilityShould : IClassFixture<DatabaseFixture>
    {
        private readonly EmployeeUtil _employeeUtil;

        public KennUwareHR_EmployeeUtilityShould(DatabaseFixture fixture)
        {
            _employeeUtil = new EmployeeUtil(fixture.Context);
        }

        [Fact]
        public void ReturnTestEmp1GivenValueOf1()
        {
            EmployeeAPIDAO result = _employeeUtil.GetEmployee(1);
            Assert.Equal("Test", result.FirstName);
            Assert.Equal("User", result.LastName);
            Assert.Equal("Test position 2", result.PositionName);
            Assert.Equal("HR", result.DepartmentName);
            Assert.True(result.Id.Equals(1));
        }

        [Fact]
        public void ReturnAllTestEmps()
        {
            List<EmployeeAPIDAO> result = _employeeUtil.GetEmployees(null, null, null,
                                                                       0, 50);
            Assert.Equal("Test", result[0].FirstName);
            Assert.Equal("SpongeBob", result[1].FirstName);
        }

        [Fact]
        public void ReturnAllTestEmpsGivenRegionOf2()
        {
            List<EmployeeAPIDAO> result = _employeeUtil.GetEmployees("Texas", null, null,
                                                                       0, 50);
            Assert.Equal("SpongeBob", result[0].FirstName);
            Assert.Equal("Texas", result[0].RegionName);
            Assert.True(result.Count.Equals(1));
        }

        [Fact]
        public void ReturnAllTestEmpsGivenDepartmentOfHR()
        {
            List<EmployeeAPIDAO> result = _employeeUtil.GetEmployees(null, null, "HR",
                                                                       0, 50);
            Assert.True(result.Count.Equals(2));
            foreach (var value in result){
                Assert.Equal("HR", value.DepartmentName);
            }
        }

        [Fact]
        public void ReturnAllTestEmpsGivenPositionOfSales()
        {
            List<EmployeeAPIDAO> result = _employeeUtil.GetEmployees(null, "Sales Rep", null,
                                                                       0, 50);
            Assert.Equal("SpongeBob", result[0].FirstName);
            Assert.Equal("Sales Rep", result[0].PositionName);
            Assert.True(result.Count.Equals(1));
        }
    }
}
