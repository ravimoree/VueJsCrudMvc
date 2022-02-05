using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VueJsCrudMvc.Models
{
    public class EmployeeContext: DbContext
    {
        public EmployeeContext() : base("name=DBCS")
        {
        }
    }
}