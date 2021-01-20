using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    public class Freelancer : Person
    {
        public new decimal TotalPay { get; set; }
        public Freelancer(string name) : base(name)
        { }
    }
}

