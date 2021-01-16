using NUnit.Framework;
using System;
using CalcSalary;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class PersonsTests
    {
        [SetUp]
        public void Setup()
        {
        }
        

        //тест оплаты менеджера 
        [Test]
        public void ManagerTotalPay()
        {
            AllEmployeesHoursWorkedList.RemoveEmployee("tm");
            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Manager,"Tm",DateTime.Now.AddDays(-1),5);

            Statistics.CalcStats("tm", 2);

            AllEmployeesHoursWorkedList.RemoveEmployee("tm");
            Assert.IsTrue(Statistics.TotalPay == 6250);
        }

        //тест оплаты штатного сотрудника 
        [Test]
        public void EmployeeTotalPay()
        {
            AllEmployeesHoursWorkedList.RemoveEmployee("te");
            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Employee, "Te", DateTime.Now.AddDays(-1), 5);

            Statistics.CalcStats("te", 2);

            AllEmployeesHoursWorkedList.RemoveEmployee("te");
            Assert.IsTrue(Statistics.TotalPay == 3750);
        }

        //тест оплаты фрилансера с выбором периода
        [Test]
        public void FreelancerTotalPay()
        {
            AllEmployeesHoursWorkedList.RemoveEmployee("tf");
            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Freelancer, "Tf", DateTime.Now.AddDays(-1), 5);

            Statistics.CalcStats("tf", 2);

            AllEmployeesHoursWorkedList.RemoveEmployee("tf");
            Assert.IsTrue(Statistics.TotalPay == 5000);
        }

        //тест оплаты всех сотрудников 
        [Test]
        public void TotalPayOfAll()
        {
            AllEmployeesHoursWorkedList.RemoveEmployee("tm");
            AllEmployeesHoursWorkedList.RemoveEmployee("te");
            AllEmployeesHoursWorkedList.RemoveEmployee("tf");

            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Manager, "Tm", DateTime.Now.AddDays(-1), 5);     //6250  -   час 1250
            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Employee, "Te", DateTime.Now.AddDays(-1), 5);    //3750  -   час 750
            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Freelancer, "Tf", DateTime.Now.AddDays(-1), 5);  //5000  -   час 1000
            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Freelancer, "Tf", DateTime.Now.AddDays(-2), 1);  //1000  -   час 1000

            Statistics.DisplayAllStats(3);  //1    -   за вчерашний день,     2    -   за неделю(7) дней,     3    -   за месяц

            //к тестовым прибавляются не тестовые 12750р.
            //всего 28750
             
            AllEmployeesHoursWorkedList.RemoveEmployee("tm");
            AllEmployeesHoursWorkedList.RemoveEmployee("te");
            AllEmployeesHoursWorkedList.RemoveEmployee("tf");
            Assert.IsTrue(Statistics.TotalPay == 28750);
        }

        //тест добавления всех типов сотрудников
        [Test]
        public void AddEmployee()
        {
            AllEmployeesHoursWorkedList.RemoveEmployee("tm");
            AllEmployeesHoursWorkedList.RemoveEmployee("te");
            AllEmployeesHoursWorkedList.RemoveEmployee("tf");

            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Manager, "Tm", DateTime.Now.AddDays(-1));     
            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Employee, "Te", DateTime.Now.AddDays(-1));    
            AllEmployeesHoursWorkedList.AddEmployee(Settings.Roles.Freelancer, "Tf", DateTime.Now.AddDays(-1));
            using (ContextWithDB db = new ContextWithDB())
            {
                Assert.IsTrue(db.AllEmployeesHoursWorkedList.Any(n => n.Name == "Tm"));
                Assert.IsTrue(db.AllEmployeesHoursWorkedList.Any(n => n.Name == "Te"));
                Assert.IsTrue(db.AllEmployeesHoursWorkedList.Any(n => n.Name == "Tf"));
            }
            AllEmployeesHoursWorkedList.RemoveEmployee("tm");
            AllEmployeesHoursWorkedList.RemoveEmployee("te");
            AllEmployeesHoursWorkedList.RemoveEmployee("tf");
        }

    }
}