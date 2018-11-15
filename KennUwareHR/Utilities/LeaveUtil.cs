using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KennUwareHR.Contexts;
using KennUwareHR.Models;

namespace KennUwareHR.Utilities
{
    public class LeaveUtil
    {
        private readonly HRContext _context;

        public LeaveUtil(HRContext context)
        {
            _context = context;
        }

        
        public List<LeaveDAO> GetLeaves(int empId)
        {
            IQueryable<Leave> Leaves = _context.Leaves;
            Leaves = Leaves.Where(e => e.EmployeeId == empId);
            List<LeaveDAO> lvs = Leaves.Select(e => new LeaveDAO
            {
                Id = e.Id,
                start = e.StartTime,
                end = e.EndTime.AddDays(1), // Adding one to the day count so that Full calendar will extend the event display to the last end date as expected
                AppStatus = e.Approved,
                title = (e.Approved) ? "(Approved!) Leave Request # " + e.Id.ToString() : "(Unapproved) Leave Request # " + e.Id.ToString(),
                url = "/Leave/Details?id=" + e.Id
            }).ToList();


            return lvs;
        }
    }
}
