using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    public class Employee : Staff
    {
        public new decimal TotalPay { get; set; }
        public static List<Employee> employee = new List<Employee>();        
        public Employee(string name, List<TimeRecord> timeRecords) : base(name, Settings.Employee.MonthSalary, timeRecords)
        { }
    }
}

