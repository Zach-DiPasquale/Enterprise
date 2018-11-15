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
    public class EmployeeController : Controller
    {
        private readonly HRContext _context;

        public EmployeeController(HRContext context)
        {
            _context = context;
        }

        //Get api/employee/ returns test value "hello"
        [HttpGet]
        public IActionResult GetEmployees([FromQuery] string region, 
                                   [FromQuery] string position, 
                                   [FromQuery] string department,
                                   [FromQuery] string id,
                                   [FromQuery] string userId,
                                   [FromQuery] string limit,
                                   [FromQuery] string pageNum)
        {
            var EmployeeUtilities = new EmployeeUtil(_context);

            if (!String.IsNullOrEmpty(id))
            {
                EmployeeAPIDAO e = EmployeeUtilities.GetEmployee(Int32.Parse(id));

                if (e == null) {
                    return BadRequest(new
                    {
                        error = "Invalid EmployeeId Provided"
                    });
                }

                return Ok(e);
            }

            if (!String.IsNullOrEmpty(userId))
            {
                EmployeeAPIDAO e = EmployeeUtilities.GetEmployeeByUserId(Int32.Parse(userId));

                if (e == null) {
                    return BadRequest(new
                    {
                        error = "Invalid userId Provided"
                    });
                }

                return Ok(e);
            }

            int itemLimit;

            if (String.IsNullOrEmpty(limit)){
                itemLimit = 25;

            }else {
                itemLimit = Int32.Parse(limit);
            }

            int page;

            if (String.IsNullOrEmpty(pageNum))
            {
                page = 1;

            }
            else
            {
                page = Int32.Parse(pageNum);
            }


            List<EmployeeAPIDAO> emps = EmployeeUtilities
                .GetEmployees(region, position, department, ((page-1)*itemLimit), itemLimit);

            if (emps.Count() < itemLimit)
            {
                return Ok(new
                {
                    data = emps
                });
            }

            var nextString = "";

            if (!String.IsNullOrEmpty(region))
            {
                nextString += "&region=" + region;
            }
            if (!String.IsNullOrEmpty(department))
            {
                nextString += "&department=" + department;
            }
            if (!String.IsNullOrEmpty(position))
            {
                nextString += "&position=" + position;
            }
                

            return Ok(new
            {
                data = emps,
                page = new {
                    nextPage = "?pageNum="+(page+1)+"&limit="+itemLimit + nextString
                }
            });
        }


    }
}
