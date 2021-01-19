using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    public class Freelancer : Person
    {
        public new decimal TotalPay { get; set; }
        public static List<Freelancer> freelancer = new List<Freelancer>();
        public Freelancer(string name, List<TimeRecord> timeRecords) : base(name, timeRecords)
        { }
    }
}

