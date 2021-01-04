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

        //тест оплаты менеджера с выбором периода
        [Test]
        public void ManagerTotalPay()
        {
            ActionsOfEmployees.AddMan("test manager", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            ActionsOfEmployees.AddMan("test manager", 1, dt: DateTime.Now.AddDays(-5), message: "test message");
           
            Statistics.CalcStats("test manager", 2);

            Assert.IsTrue(Manager.TotalPay == 2500);
        }

        //тест оплаты штатного сотрудника с выбором периода
        [Test]
        public void EmployeeTotalPay()
        {
            ActionsOfEmployees.AddEmp("test employee", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            ActionsOfEmployees.AddEmp("test employee", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            
            Statistics.CalcStats("test employee", 2);
           
            Assert.IsTrue(Employee.TotalPay == 1500);
        }

        //тест оплаты фрилансера с выбором периода
        [Test]
        public void FreelancerTotalPay()
        {

            ActionsOfEmployees.AddFree("test freelancer", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            ActionsOfEmployees.AddFree("test freelancer", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            
            Statistics.CalcStats("test freelancer", 2);
            
            Assert.IsTrue(Freelancer.TotalPay == 2000);
        }

        //тест оплаты всех сотрудников с выбором периода
        [Test]
        public void TotalPayOfAll()
        {
            Manager.manager.Clear();
            Employee.employee.Clear();
            Freelancer.freelancer.Clear();

            ActionsOfEmployees.AddMan("test manager", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            ActionsOfEmployees.AddMan("test manager", 1, dt: DateTime.Now.AddDays(-2), message: "test message");

            ActionsOfEmployees.AddEmp("test employee", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            ActionsOfEmployees.AddEmp("test employee", 1, dt: DateTime.Now.AddDays(-2), message: "test message");

            ActionsOfEmployees.AddFree("test freelancer", 1, dt: DateTime.Now.AddDays(-1), message: "test message");
            ActionsOfEmployees.AddFree("test freelancer", 1, dt: DateTime.Now.AddDays(-2), message: "test message");

            Statistics.CalcStatsOfAll(2);

            Assert.IsTrue(Person.TotalPay == 6000);
        }

        //тест добавления всех типов сотрудников
        [Test]
        public void AddEmployee()
        {
            ActionsOfEmployees.AddEmployee("test manager", selected: 1);
            ActionsOfEmployees.AddEmployee("test employee", selected: 2);
            ActionsOfEmployees.AddEmployee("test freelancer", selected: 3);

            Assert.IsTrue(Manager.manager.Any(n=>n.Name == "test manager"));
            Assert.IsTrue(Employee.employee.Any(n => n.Name == "test employee"));
            Assert.IsTrue(Freelancer.freelancer.Any(n => n.Name == "test freelancer"));
        }

    }
}