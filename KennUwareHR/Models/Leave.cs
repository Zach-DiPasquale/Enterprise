using System;
using System.ComponentModel.DataAnnotations;

namespace KennUwareHR.Models
{
    public class Leave
    {

        public int Id { get; set; }

        
        [Display(Name = "Approved Status")]
        public bool Approved  { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [StartDateValidation(ErrorMessage = "The start date of your leave must be after the current date.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndTime { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
