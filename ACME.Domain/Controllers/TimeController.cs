using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ACME.Domain.Models;

namespace ACME.Domain.Controllers
{
    public class TimeController
    {
        public List<Employee> Employees { get; set; }
        private LogController log;
        public TimeController(string logPath = "")
        {
            Employees = new List<Employee>();
            log = new LogController(logPath);
        }
        public void AddEmployee(string line, int lineNumber = 0)
        {
            Employee employee = new Employee();

            if (line.Split('=').Length < 2)
            {
                log.WriteLog("Format error in line #" + lineNumber);
                return;
            }

            string name = line.Split('=')[0];
            
            if (name.Trim().Length == 0)
            {
                log.WriteLog("Name not found in line #" + lineNumber);
                return;
            }

            foreach (Employee iEmployee in Employees)
            {
                if (iEmployee.Name.Equals(name))
                {
                    log.WriteLog("Name " + name + " already registered");
                    return;
                }
            }

            employee.Name = name;
            
            string days = line.Split('=')[1];


            if (days.Trim().Length == 0)
            {
                log.WriteLog("Schedules not found in line #" + lineNumber);
                return;
            }

            int dayQty = days.Split(',').Length;
            for (int dayPosition = 0; dayPosition < dayQty; dayPosition++)
            {
                string day = days.Split(',')[dayPosition];
                string dayName = day.Substring(0, 2);
                string hours = day.Substring(2).Trim();
                if (hours.Split('-').Length < 2)
                {
                    log.WriteLog("Schedule format error in line #" + lineNumber + ", schedule position #" + (dayPosition + 1));
                    continue;
                }
                if (!ValidateDay(dayName))
                {
                    log.WriteLog("Invalid day in line #" + lineNumber + ", schedule position #" + (dayPosition + 1));
                    continue;
                }

                bool dayRepeated = false;
                foreach (Schedule iSchedule in employee.Schedules)
                {
                    if (iSchedule.Day.Equals(dayName))
                    {
                        log.WriteLog("Day " + dayName + " already registered in line #" + lineNumber);
                        dayRepeated = true;
                        break;
                    }
                }
                
                if (!dayRepeated)
                {
                    string inTime = hours.Split('-')[0];
                    string outTime = hours.Split('-')[1];

                    if (inTime.Split(':').Length < 2 || outTime.Split(':').Length < 2)
                    {
                        log.WriteLog("Time format error in line #" + lineNumber + ", schedule position #" + (dayPosition + 1));
                        continue;
                    }
                    int inHour = int.Parse(inTime.Split(':')[0]);
                    int inMinute = int.Parse(inTime.Split(':')[1]);
                    int outHour = int.Parse(outTime.Split(':')[0]);
                    int outMinute = int.Parse(outTime.Split(':')[1]);

                    if (inHour < 0 || inHour > 23 || inMinute < 0 || inMinute > 59 || outHour < 0 || outHour > 23 ||
                        outMinute < 0 || outMinute > 59)
                    {
                        log.WriteLog("Time format error in line #" + lineNumber + ", schedule position #" + (dayPosition + 1));
                        continue;
                    }

                    DateTime timeIn = new DateTime(2000, 1, 1, inHour, inMinute, 0);
                    DateTime timeOut = new DateTime(2000, 1, 1, outHour, outMinute, 0);
                    if (DateTime.Compare(timeIn,timeOut) > 0)
                    {
                        log.WriteLog("Timeline error in line #" + lineNumber + ", schedule position #" + (dayPosition + 1));
                        continue;
                    }
                    Schedule schedule = new Schedule()
                    {
                        Day = dayName,
                        TimeIn = timeIn,
                        TimeOut = timeOut

                    };

                    employee.Schedules.Add(schedule);
                }
            }
            Employees.Add(employee);
        }

        public bool ValidateDay(string dayName)
        {
            bool result;
            switch (dayName.ToUpper())
            {
                case "SU":
                case "MO":
                case "TU":
                case "WE":
                case "TH":
                case "FR":
                case "SA":
                    result = true;
                    break;
                default:
                    result = false;
                    break;

            }

            return result;

        }

        public string GenerateTable()
        {
            string table = "";
            List<Employee> employees = Employees.OrderBy(e => e.Name).ToList();

            for (int i = 0; i < employees.Count - 1; i++)
            {
                for (int j = i + 1; j < employees.Count; j++)
                {
                    Employee employee1 = employees[i];
                    Employee employee2 = employees[j];

                    int coincidences = 0;

                    foreach (Schedule schedule1 in employee1.Schedules)
                    {
                        foreach (Schedule schedule2 in employee2.Schedules)
                        {
                            if (schedule1.Day.Equals(schedule2.Day) && 
                                ((schedule1.TimeIn >= schedule2.TimeIn && schedule1.TimeIn <= schedule2.TimeOut) || 
                                 (schedule1.TimeOut >= schedule2.TimeIn && schedule1.TimeOut <= schedule2.TimeOut) ||
                                 (schedule2.TimeIn >= schedule1.TimeIn && schedule2.TimeIn <= schedule1.TimeOut) || 
                                 (schedule2.TimeOut >= schedule1.TimeIn && schedule2.TimeOut <= schedule1.TimeOut))) coincidences++;
                        }
                    }

                    if (coincidences > 0)
                    {
                        table += employee1.Name + "-" + employee2.Name + ": " + coincidences + "<br>";
                    }

                }
            }

            return table;
        }
    }
}
