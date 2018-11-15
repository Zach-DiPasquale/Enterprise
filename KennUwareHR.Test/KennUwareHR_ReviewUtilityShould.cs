using System;
using Xunit;
using KennUwareHR.Contexts;
using KennUwareHR.Models;
using KennUwareHR.Utilities;
using System.Collections.Generic;

namespace KennUwareHR.Test.Utilities
{
    public class KennUwareHR_ReviewUtilityShould : IClassFixture<DatabaseFixture>
    {
        private readonly ReviewUtil _reviewUtil;

        public KennUwareHR_ReviewUtilityShould(DatabaseFixture fixture)
        {
            _reviewUtil = new ReviewUtil(fixture.Context);
        }

        [Fact]
        public void ReturnListOfCSReviews()
        {
            List<CSReviewsDAO> reviews = _reviewUtil.GetCSReviews(2).Result;

            foreach (var review in reviews)
            {
                Assert.True(!String.IsNullOrEmpty(review.Agent.FirstName));
                Assert.True(!String.IsNullOrEmpty(review.Agent.LastName));
                Assert.True(!String.IsNullOrEmpty(review.Content));
            }
        }


    }
}
