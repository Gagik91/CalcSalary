using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{    
    public class Manager: Staff
    {
        public static new decimal TotalPay { get; set; }
        public static List<Manager> manager = new List<Manager>();
        public Manager(string name) : base(name, Settings.Manager.MonthSalary)
        { }
    }
}
