using System;
namespace KennUwareHR.Models
{
    public class Authentication
    {
        
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }

    }
}
