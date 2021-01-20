using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Linq;

namespace CalcSalary
{
   public class Person
    {
        public string Name { get; private set; }
        public decimal TotalPay { get; set; }
        

        public Person(string name)
        {
            Name = name;
        }
        public Person()
        {}
        
        public static void ActionMenu()
        {
            bool exit = false;
            
            Console.Write("Введите Ваше имя: ");
            string name = Console.ReadLine();
            ActionsOfEmployees actionOfEmp = new ActionsOfEmployees();
            Settings.Role? role = actionOfEmp.RoleIdentification(name);
            byte selectedAction;
            Statistics stat = new Statistics();

            //проверяем по имени к какой роли относится сотрудник и присваиваем соответствующее значение

            while (role == 0)
            {
                Console.Write($"Сотрудник с именем {name} не найден, нажмите 1 для выхода или введите имя: ");
                name = Console.ReadLine();
                Console.WriteLine("\n");
                if (name == "1")
                {
                    exit = true;
                    break;
                }
                else 
                {
                    role = actionOfEmp.RoleIdentification(name);
                }
            }

            Console.WriteLine($"Здравствуйте, {name}!");
            Console.WriteLine($"Ваша роль: {role}");
            while (!exit)
            {

                Console.WriteLine("\nВыберите желаемое действие:\n");

                switch (role)
                {
                    case Settings.Role.Manager:
                        {
                            Console.WriteLine("(1) Добавить сотрудника: ");
                            Console.WriteLine("(2) Просмотреть отчет по всем сотрудникам: ");
                            Console.WriteLine("(3) Просмотреть отчет по конкретному сотруднику: ");
                            Console.WriteLine("(4) Добавить часы работы: ");
                            Console.WriteLine("(5) Выход из программы: ");
                            selectedAction = 0;
                            while (selectedAction < 1 || selectedAction > 5)
                            {
                                try
                                {
                                    Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                    selectedAction = byte.Parse(Console.ReadLine());
                                    Console.WriteLine("\n");
                                }
                                catch { continue; }
                            }
                            switch (selectedAction)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Укажите должность для добавления сотрудника: ");
                                        Console.WriteLine("Руководитель - нажмите 1: ");
                                        Console.WriteLine("Сотрудник на зарплате - нажмите 2: ");
                                        Console.WriteLine("Внештатный сотрудник - нажмите 3: ");
                                        Settings.Role selectedEmployee = 0;
                                        while (selectedEmployee < (Settings.Role)1 || selectedEmployee > (Settings.Role)3)
                                        {
                                            try
                                            {
                                                Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                                selectedEmployee = (Settings.Role)int.Parse(Console.ReadLine());
                                                Console.WriteLine("\n");
                                            }
                                            catch { continue; }
                                        }

                                        Console.Write("Укажите имя для добавления сотрудника: ");
                                        string nameEmployee = Console.ReadLine();
                                        actionOfEmp.AddEmployee(nameEmployee, selected: selectedEmployee);
                                    }
                                    break;
                                case 2:
                                    {
                                        stat.DisplayAllStats();
                                    }
                                    break;
                                case 3:
                                    {
                                        stat.DisplayStats(name, role.Value);
                                    }
                                    break;
                                case 4:
                                    {
                                        actionOfEmp.AddHours(name, role.Value);
                                    }
                                    break;
                                case 5:
                                    { exit = true; }
                                    break;
                            }
                        }
                        break;
                    case Settings.Role.Employee:
                        {
                            Console.WriteLine("(1) Добавить часы работы: ");
                            Console.WriteLine("(2) Просмотреть отчет по отработанным часам и зарплате за период: ");
                            Console.WriteLine("(3) Выход из программы: ");
                            selectedAction = 0;
                            while (selectedAction < 1 || selectedAction > 3)
                            {
                                try
                                {
                                    Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                    selectedAction = byte.Parse(Console.ReadLine());
                                    Console.WriteLine("\n");
                                }
                                catch { continue; }
                            }
                            switch (selectedAction)
                            {
                                case 1:
                                    {
                                        actionOfEmp.AddHours(name, role.Value);
                                    }
                                    break;
                                case 2:
                                    { stat.DisplayStats(name, role.Value); }
                                    break;
                                case 3:
                                    { exit = true; }
                                    break;
                            }
                        }
                        break;
                    case Settings.Role.Freelancer:
                        {
                            Console.WriteLine("(1) Добавить часы работы: ");
                            Console.WriteLine("(2) Просмотреть отчет по отработанным часам и зарплате за период: ");
                            Console.WriteLine("(3) Выход из программы:");
                            selectedAction = 0;
                            while (selectedAction < 1 || selectedAction > 3)
                            {
                                try
                                {
                                    Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                    selectedAction = byte.Parse(Console.ReadLine());
                                    Console.WriteLine("\n");
                                }
                                catch { continue; }
                            }

                            switch (selectedAction)
                            {
                                case 1:
                                    {
                                        actionOfEmp.AddHours(name, role.Value);
                                    }
                                    break;
                                case 2:
                                    { stat.DisplayStats(name, role.Value); }
                                    break;
                                case 3:
                                    { exit = true; }
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
