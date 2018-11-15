using System;
using KennUwareHR.Models;
using System.ComponentModel.DataAnnotations;


namespace KennUwareHR.Models
{
    public class CSReviewsDAO
    {

        public int AgentId { get; set; }
        public Employee Agent { get; set; }

        public string Content { get; set; }


    }
}
