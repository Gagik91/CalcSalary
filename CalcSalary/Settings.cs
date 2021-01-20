using System;
using System.Collections.Generic;
using System.Text;

namespace CalcSalary
{
    
    public static class Settings
    {
        public enum Role
        {
            Manager = 1,
            Employee,
            Freelancer
        }

        public class Manager
        {
            public const decimal MonthBonus = 20000;
            public const decimal MonthSalary = 200000;
        }
        public class Employee
        {
            public const decimal MonthBonus = 20000;
            public const decimal MonthSalary = 120000;
        }
        public class Freelancer
        {
            public const decimal PayPerHour = 1000;
        }

        /// <summary>
        /// Количество рабочих часов в месяце
        /// </summary>
        public const byte WorkHoursInMonth = 160;

        /// <summary>
        /// рабочие часы в день
        /// </summary>
        public const byte WorkHourInDay = 8;
    }
}
