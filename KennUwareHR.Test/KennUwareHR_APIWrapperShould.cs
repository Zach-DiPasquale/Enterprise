using System;
using Xunit;
using System.Linq;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Controllers;
using System.Collections.Generic;

namespace KennUwareHR.Test.Utilities
{
    public class KennUwareHR_APIWrapperShould : IClassFixture<DatabaseFixture>
    {
        private readonly APIWrapper _apiWrapper;
        private readonly HRContext _context;

        public KennUwareHR_APIWrapperShould(DatabaseFixture fixture)
        {
            _apiWrapper = new APIWrapper();
            _context = fixture.Context;
        }

        [Fact]
        public void ReturnListOfCSReviews()
        {
            List<CSReviewsDAO> reviews = _apiWrapper.GetCSReviews(2).Result;

            foreach (var review in reviews)
            {
                Assert.True(!String.IsNullOrEmpty(review.Content));
                Assert.True(review.AgentId >= 0);
            }

        }

        [Fact]
        public void ReturnNumForQuarterlyRevenue()
        {
            Assert.True(_apiWrapper.GetQuarterlyRevenue() >= 0, "The Quarterly Revenue should be above zero");
        }

        [Fact]
        public void ReturnNumForMonthlyBonusForRegion()
        {
            Region region = _context.Regions.Where(reg => reg.Id == 1).First();

            Assert.True(_apiWrapper.GetMonthlyRevenue(region) >= 0, "The Quarterly Revenue should be above zero");
        }

        [Fact]
        public void ReturnNumForAnEmployeeSales()
        {
            Employee emp = _context.Employees.Where(e => e.Id == 1).First();
            Assert.True(_apiWrapper.GetEmployeeSales(emp) >= 0, "The Quarterly Revenue should be above zero");
        }


    }
}
