using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.IO;
using System.Threading;

namespace CalcSalary
{
    public class TimeRecord
    {
        public DateTime Date{ get; set; }
        public string Name{ get; set; }
        public byte Hours { get; set; }
        public string Message { get; set; }
        public decimal TotalPay { get; set; }

        public TimeRecord(DateTime date, string name, byte hours, string message, decimal tPay, string role, bool newAdd = false)
        {
            Date = date;
            Name = name;
            Hours = hours;
            Message = message;
            TotalPay = tPay;

            if (newAdd)
            {
                if (role == "manager")
                {
                    Files.ManagerWriter(Date, Name, Hours, Message);
                    Files.ManagerReader();
                }
                else if (role == "employee")
                {
                    Files.EmployeeWriter(Date, Name, Hours, Message);
                    Files.EmployeeReader();
                }
                else if (role == "freelancer")
                {
                    Files.FreelancerWriter(Date, Name, Hours, Message);
                    Files.FreelancerReader();
                }
                Files.EmployeeListWriter(Name, role);
            }
        }
    }
}
