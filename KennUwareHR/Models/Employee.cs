using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KennUwareHR.Models
{
    public class Employee
    {

        public int Id { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int Salary { get; set; }

        [Display(Name = " Full Name")]
        public String FullName { get { return FirstName + " " + LastName; } }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EmploymentStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EmploymentEndDate { get; set; }


        public int RegionId { get; set; }

        public Region Region { get; set; }


        public int RoleId { get; set; }

        public Role Role { get; set; }
        public int Commission { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }


        public int PositionId { get; set; }

        public Position Position { get; set; }

        public int MyManagerId { get; set; }
        
        [ForeignKey("Employee")]
        public Employee MyManager { get; set; }

        public List<Employee> Managing { get; set; }

        public int UserId { get; set; }

        public string Name()
        {
            return FirstName + " " + LastName;
        }
    }
}
