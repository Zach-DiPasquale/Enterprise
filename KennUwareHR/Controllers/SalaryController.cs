using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KennUwareHR.Models;
using Microsoft.AspNetCore.Mvc;
using KennUwareHR.Contexts;
using KennUwareHR.Utilities;

namespace KennUwareHR.Controllers
{
    [Route("api/[controller]")]
    public class SalaryController : Controller
    {
        private readonly HRContext _context;

        public SalaryController(HRContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public IActionResult GetSalary([FromForm] string monthlyBonus, [FromForm] string quarterlyBonus, [FromForm] string payStart, [FromForm] string payEnd)
        {
            if (!String.IsNullOrEmpty(monthlyBonus) && !String.IsNullOrEmpty(quarterlyBonus) && !String.IsNullOrEmpty(payStart) && !String.IsNullOrEmpty(payEnd))
            {
                var SalaryUtilities = new SalaryUtil(_context);

                //Make make shift Paycheck
                var paystub = new Paycheck();
                paystub.PayPeriodStart = DateTime.Parse(payStart);
                paystub.PayPeriodEnd = DateTime.Parse(payEnd);

                var returnDictionary = SalaryUtilities.CalculateSalary(paystub, Convert.ToBoolean(monthlyBonus), Convert.ToBoolean(quarterlyBonus)).Result;

                if(returnDictionary.ContainsKey("status") && returnDictionary["status"] == "error")
                {
                    return BadRequest(new
                    {
                        status = "error",
                        message = returnDictionary["message"]
                    });
                } else
                {
                    return Ok(new
                    {
                        lumpSum = returnDictionary["lumpSum"],
                        startDate = returnDictionary["startDate"],
                        endDate = returnDictionary["endDate"],
                        status = "success"
                    });
                }
            }
            else
            {
                return BadRequest(new {
                    status = "error",
                    message = "Missing Arguments"
                });
            }
        }
    }
}
