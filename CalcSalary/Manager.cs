using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{    
    public class Manager: Staff
    {
        public new decimal TotalPay { get; set; }
        public Manager(string name) : base(name, Settings.Manager.MonthSalary)
        { }
    }
}
