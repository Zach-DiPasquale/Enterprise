using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KennUwareHR.Models
{
    public class LeaveDAO
    {
        public int Id { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public bool AppStatus { get; set; }
        public string title { get; set; }
        public string url { get; set; }
    }
}
