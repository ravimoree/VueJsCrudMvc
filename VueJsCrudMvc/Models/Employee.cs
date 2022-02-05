using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VueJsCrudMvc.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int? PID { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string Gender { get; set; } 
        public string Email { get; set; }
        public bool IsActive { get; set; } 
    }
}