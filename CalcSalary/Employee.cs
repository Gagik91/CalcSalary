using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    public class Employee : Staff
    {
        public Employee(string name) : base(name, Settings.Employee.MonthSalary)
        { }
    }
}

