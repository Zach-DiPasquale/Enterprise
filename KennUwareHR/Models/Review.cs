using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace KennUwareHR.Models
{
    public class Review
    {

        public int Id { get; set; }

        [Display(Name = "Reviewed")]
        public Employee ReviewedEmployee { get; set; }
        public int ReviewedEmployeeId { get; set; }

        [Display(Name = "Reviewer")]
        public Employee ReviewerEmployee { get; set; }
        public int ReviewerEmployeeId { get; set; }

        public string Comment { get; set; }

    }
}
