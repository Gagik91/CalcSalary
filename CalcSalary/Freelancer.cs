﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    public class Freelancer : Person
    {
        public static List<Freelancer> freelancer = new List<Freelancer>();
        public Freelancer(string name) : base(name)
        { }
    }
}

