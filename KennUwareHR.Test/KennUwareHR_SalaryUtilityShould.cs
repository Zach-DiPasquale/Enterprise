using System;
using Xunit;
using KennUwareHR.Contexts;
using System.Linq;
using KennUwareHR.Utilities;
using System.Collections.Generic;
using KennUwareHR.Models;

namespace KennUwareHR.Test.Utilities
{
    public class KennUwareHR_SalaryUtilityShould : IClassFixture<DatabaseFixture>
    {
        private readonly SalaryUtil _salaryUtil;

        private readonly HRContext _context;

        public KennUwareHR_SalaryUtilityShould(DatabaseFixture fixture)
        {
            _salaryUtil = new SalaryUtil(fixture.Context);
            _context = fixture.Context;
        }

        [Fact]
        public void TestCalculateEmployeeBasePay()
        {
            double value = _salaryUtil.CalculateEmployeeBasePay(36500, 365, 12);

            Assert.Equal(1200.00, value);
        }

        [Fact]
        public void TestGetMonthlyBonus()
        {
            Employee emp1 = _context.Employees.Where(e => e.Id == 1).First();
            double value = _salaryUtil.GetMonthlyBonus(emp1);
            Assert.True(value >= 0.00);
        }

        [Fact]
        public void GetQuarterlyBonus()
        {
            Employee emp1 = _context.Employees.Where(e => e.Id == 1).First();
            double value = _salaryUtil.GetQuarterlyBonus(emp1);
            Assert.True(value >= 0.00);
        }

        [Fact]
        public void GetIndividualBonus()
        {
            Employee emp1 = _context.Employees.Where(e => e.Id == 1).First();
            double value = _salaryUtil.GetIndividualBonus(emp1);
            Assert.True(value >= 0.00);
        }

        [Fact]
        public void CalculateSalaryWithIncorrectDates()
        {
            Paycheck pay = new Paycheck
            {
                EmployeeId = 1,
                Employee = _context.Employees.Where(e => e.Id == 1).First(),
                PayPeriodEnd = DateTime.Now.AddDays(-2),
                PayPeriodStart = DateTime.Now
            };
            Dictionary<string, string> result = _salaryUtil.CalculateSalary(pay, true, true).Result;
            Assert.True(result["status"] == "error");
        }

        [Fact (Skip = "Failing due to DB")]
        // This test is currently failing. We have discovered the cause but it only
        // relates to how we run our unit test. We currently have not discovered a solution
        // but can verify that this functionally works in the release of R2.
        public void CalculateSalarySuccessfully()
        {
            Paycheck pay = new Paycheck
            {
                PayPeriodEnd = DateTime.Now,
                PayPeriodStart = DateTime.Now.AddDays(-3),
            };
            Dictionary<string, string> result = _salaryUtil.CalculateSalary(pay, true, true).Result;
            Assert.True(result["status"] == "success");
        }

        [Fact (Skip = "Failing due to DB")]
        // This test is currently failing. We have discovered the cause but it only
        // relates to how we run our unit test. We currently have not discovered a solution
        // but can verify that this functionally works in the release of R2.
        public void CalculateSalaryWithRepeatDates()
        {
            Paycheck pay = new Paycheck
            {
                PayPeriodEnd = DateTime.Now.AddDays(-2),
                PayPeriodStart = DateTime.Now.AddDays(-4),
            };
            Dictionary<string, string> result = _salaryUtil.CalculateSalary(pay, true, true).Result;
            Assert.True(result["status"] == "error");
        }

    }
        
}
