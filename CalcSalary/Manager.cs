using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{    
    public class Manager: Staff
    {
        public static List<Manager> manager = new List<Manager>();
        public static decimal TotalPay { get; set; }
        public Manager(string name, List<TimeRecord> timeRecords) : base(name, Settings.Manager.MonthSalary, timeRecords)
        { }
    }
}
