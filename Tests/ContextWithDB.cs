using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CalcSalary
{
    //Логика подключения к БД, настройка бд, какая таблица будет использоваться при подключении к БД
    class ContextWithDB : DbContext
    {
        public DbSet<AllEmployeesHoursWorkedList> AllEmployeesHoursWorkedList { get; set; } //Таблица со всеми свойствами AllEmployeesHoursWorkedList

        public ContextWithDB()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CalcSalary;Trusted_Connection=True;");
        }
    }
}
