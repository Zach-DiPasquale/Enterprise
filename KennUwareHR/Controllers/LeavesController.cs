using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Utilities;

namespace KennUwareHR.Controllers
{
    [Produces("application/json")]
    [Route("api/Leaves")]
    public class LeavesController : Controller
    {
        private readonly HRContext _context;

        public LeavesController(HRContext context)
        {
            _context = context;
        }

        // GET: api/Leaves
        [HttpGet]
        public IEnumerable<Leave> GetAllLeaves()
        {
            return _context.Leaves;
        }

        // GET: api/Leaves/5
        [HttpGet("approved/{id}")]
        public IEnumerable<LeaveDAO> GetApprovedLeaves([FromRoute] int id)
        {

           var leaveUtil = new LeaveUtil(_context);
           return leaveUtil.GetLeaves(id).Where(e => e.AppStatus == true);
           
        }

        [HttpGet("unapproved/{id}")]
        public IEnumerable<LeaveDAO> GetUnapprovedLeaves([FromRoute] int id)
        {
            var leaveUtil = new LeaveUtil(_context);
            return leaveUtil.GetLeaves(id).Where(e => e.AppStatus == false);
        }
        
        private bool LeaveExists(int id)
        {
            return _context.Leaves.Any(e => e.Id == id);
        }
    }
}