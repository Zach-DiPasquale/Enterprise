using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KennUwareHR.Models;
using KennUwareHR.Contexts;
using KennUwareHR.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace KennUwareHR.Utilities
{
    public class SalaryUtil
    {

        private static HRContext _context;
        private static APIWrapper _apiwrapper;
        
        public SalaryUtil(HRContext context)
        {
            _context = context;
            _apiwrapper = new APIWrapper();
        }

        public async Task<Dictionary<string,string>> CalculateSalary(Paycheck paystub, bool monthlyBonus, bool quarterlyBonus) {

            //Check to see if dates make a correct range
            int dateResult = DateTime.Compare(paystub.PayPeriodStart, paystub.PayPeriodEnd);
            if (dateResult > 0)
            {
                return new Dictionary<string, string>()
                {
                    { "status", "error" },
                    { "message", "Incorrect Dates" },
                };
            }

            var dates = new List<DateTime>();
            for (var dt = paystub.PayPeriodStart; dt <= paystub.PayPeriodEnd; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }
            var dates_copy = new List<DateTime>(dates);

            var result = true;
            foreach (DateTime date in dates)
            {
                result = result && !(_context.Paychecks.Any(paycheck => (date >= paycheck.PayPeriodStart && date <= paycheck.PayPeriodEnd)));
            }

            if (!result)
            {
                return new Dictionary<string, string>()
                {
                    { "status", "error" },
                    { "message", "Payment already calculated for dates" },
                };
            }

            var num_days = dates_copy.Count;
            int days_in_year = new DateTime(DateTime.Now.Year, 12, 31).DayOfYear - new DateTime(DateTime.Now.Year, 1, 1).DayOfYear;

            var lumpSum = 0.0;
            int salesRepId = _context.Positions.Where(e => e.Title.Equals("Sales Rep")).First().Id;

            foreach (Employee line in _context.Employees.ToList())
            {
                var base_pay = CalculateEmployeeBasePay(line.Salary, days_in_year, num_days);
                var bonus_pay = 0.0;
                var commission_pay = 0.0;
                
                if (salesRepId == line.PositionId)
                {
                    bonus_pay += GetIndividualBonus(line);
                    commission_pay = GetIndividualCommission(line);
                }

                if (monthlyBonus)
                {
                    bonus_pay += GetMonthlyBonus(line);
                }

                if (quarterlyBonus)
                {
                    bonus_pay += GetQuarterlyBonus(line);
                }

                //Make the paycheck object and Save
                var PayStubNew = new Paycheck();
                PayStubNew.EmployeeId = line.Id;
                PayStubNew.Employee = line;
                PayStubNew.IssueDate = paystub.PayPeriodEnd;
                PayStubNew.BasePay = base_pay;
                PayStubNew.BonusPay = bonus_pay;
                PayStubNew.Commission = commission_pay;
                PayStubNew.PayPeriodStart = dates_copy[0];
                PayStubNew.PayPeriodEnd = dates_copy[dates_copy.Count() - 1];

                lumpSum += base_pay + bonus_pay + commission_pay;

                _context.Paychecks.Add(PayStubNew);
                await _context.SaveChangesAsync();
            }

            var mapping = new Dictionary<string, string>()
            {
                { "lumpSum", lumpSum.ToString() },
                { "startDate", dates_copy[0].ToString() },
                { "endDate",  dates_copy[dates_copy.Count() - 1].ToString()},
                { "status", "success" }
            };

            return mapping;
        }

        public double GetIndividualCommission(Employee line)
        {
            var commission = 0.0;

            //Get individual sales
            var amountofSales = _apiwrapper.GetEmployeeSales(line);
            commission = amountofSales * (line.Commission / 100.0);

            return commission;
        }


        public double GetIndividualBonus(Employee line)
        {
            //get Individual Sales
            var amountofSales = _apiwrapper.GetEmployeeSales(line);
            var bonus = 0.0;

          
            if (amountofSales >= 10000000)
            {
                bonus = 1000;
            }

            return bonus;
        }

        public double GetQuarterlyBonus(Employee line)
        {
            //get Company sales
            var companySales = _apiwrapper.GetQuarterlyRevenue();
            var totalCompanyQuota = 750000000;
            var bonus = 0.0;

            if (companySales >= totalCompanyQuota)
            {
                bonus =  .02 * line.Salary;
            }

            return bonus;
        }

        public double GetMonthlyBonus(Employee line)
        {
            //get Regional sales
            var regionalSales = _apiwrapper.GetMonthlyRevenue(line.Region);
            var regionalQuota = 50000000;
            var bonus = 0.0;

            if (regionalSales >= regionalQuota)
            {
                var count = _context.Employees.Count(emp => emp.Region == line.Region);
                bonus = .01 * (regionalSales / count);
            }

            return bonus;
        }

        public double CalculateEmployeeBasePay(int salary, int days_in_year, int num_days) { 
            return Convert.ToDouble(num_days * (salary / days_in_year));
        }

    }
}
