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
        public DateTime Date{ get; private set; }
        public string Name{ get; private set; }
        public byte Hours { get; private set; }
        public string Message { get; private set; }
        public decimal TotalPay { get; private set; }

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
                    Files.Writer(Files.manFile, Date, Name, Hours, Message);
                    //Files.ManagerWriter(Date, Name, Hours, Message);
                    Files.Reader(Files.manFile);
                }
                else if (role == "employee")
                {
                    Files.Writer(Files.empFile, Date, Name, Hours, Message);
                    Files.Reader(Files.empFile);
                }
                else if (role == "freelancer")
                {
                    Files.Writer(Files.freeFile, Date, Name, Hours, Message);
                    Files.Reader(Files.freeFile);
                }
                Files.EmployeeListWriter(Name, role);
            }
        }
    }
}
