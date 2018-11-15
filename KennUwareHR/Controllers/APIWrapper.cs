using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using KennUwareHR.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KennUwareHR.Controllers
{

    public class APIWrapper
    {

        public double GetQuarterlyRevenue()
        {
            return 250000000.0;
        }

        public double GetMonthlyRevenue(Region region)
        {
            return 25000000.0;
        }

        public double GetEmployeeSales(Employee emp)
        {
            return 12000000.0;
        }

        public async Task<List<CSReviewsDAO>> GetCSReviews(int employeeId)
        {
            List<CSReviewsDAO> CSReviews = new List<CSReviewsDAO>();

            HttpClient client = new HttpClient();

            var url = "https://api-customerservice.azurewebsites.net/api/reviews/?agentId=" + employeeId;

            var response = await client.GetAsync(url);

            var responseString = await response.Content.ReadAsStringAsync();

            try {
                CSReviews = JsonConvert.DeserializeObject<List<CSReviewsDAO>>(responseString);
            }
            catch(JsonReaderException) {
                CSReviews = new List<CSReviewsDAO>();
            }
     
            return CSReviews;
        }

     
    }
}
