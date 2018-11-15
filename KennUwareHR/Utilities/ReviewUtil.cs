using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Controllers;

namespace KennUwareHR.Utilities
{
    public class ReviewUtil
    {
        private readonly HRContext _context;
        public ReviewUtil(HRContext context)
        {
            _context = context;
        }

        public async Task<List<CSReviewsDAO>> GetCSReviews(int empId)
        {
            List<CSReviewsDAO> Reviews = await new APIWrapper().GetCSReviews(empId);

            foreach(CSReviewsDAO emp in Reviews){
                int forEmpId = emp.AgentId;
                emp.Agent = _context.Employees.Where(e => e.Id == forEmpId).First();
            }

            return Reviews;
        }

        public List<Review> GetHRReviews()
        {
            return null;
        }
    }
}
