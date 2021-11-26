using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Domain.Models
{
    public class Employee
    {
        public String Name { get; set; }
        public List<Schedule> Schedules { get; set; }

        public Employee()
        {
            Schedules = new List<Schedule>();
        }
    }
}
