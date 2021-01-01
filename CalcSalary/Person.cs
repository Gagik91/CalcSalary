using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace CalcSalary
{
   public class Person
    {
        public string Name { get; set; }

        public List<TimeRecord> TimeRecords { get; set; }

        public Person(string name, List<TimeRecord> timeRecords)
        {
            Name = name;
            TimeRecords = timeRecords;
        }
    }
}
