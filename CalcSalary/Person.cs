using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Linq;

namespace CalcSalary
{
   public class Person
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public byte Hours { get; set; }
        public decimal Pay { get; set; }
        public string Message { get; set; }
        public Settings.Role Role { get; set; }
                        
        public Person(string name)
        {
            Name = name;
        }
        public Person()
        {}
        
        
    }
}
