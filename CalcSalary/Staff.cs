using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    public class Staff:Person
    {
        public decimal MonthSalary { get; set; }
        public Staff(string name, decimal monthSalary, List<TimeRecord> timeRecords) : base(name, timeRecords)
        {
            MonthSalary = monthSalary;
        }
    }
}
