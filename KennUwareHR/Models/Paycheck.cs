using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace KennUwareHR.Models
{
    public class Paycheck
    {
        
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "Issued Date")]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Pay Period Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PayPeriodStart { get; set; }

        [Display(Name = "Pay Period End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PayPeriodEnd { get; set; }

        [Display(Name = "Base Pay")]
        public Double BasePay { get; set; }

        [Display(Name = "Bonus Pay")]
        public Double BonusPay { get; set; }
        public Double Commission { get; set; }

    }
}
