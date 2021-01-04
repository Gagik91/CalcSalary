using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Linq;

namespace CalcSalary
{
   public class Person
    {
        public string Name { get; set; }
        public static decimal TotalPay { get; set; }
        public List<TimeRecord> TimeRecords { get; set; }

        public Person(string name, List<TimeRecord> timeRecords)
        {
            Name = name;
            TimeRecords = timeRecords;
        }
        
        public static void ActionMenu()
        {
            Console.Write("Введите Ваше имя: ");
            string name = Console.ReadLine();
            string role = null;
            byte selectedAction;
            bool exit = false;
            //проверяем по имени к какой роли относится сотрудник и присваиваем соответствующее значение
            if (Manager.manager.Any(n => n.Name.ToLower() == name.ToLower()))
            { role = "Руководитель"; }
            else if (Employee.employee.Any(n => n.Name.ToLower() == name.ToLower()))
            { role = "Сотрудник на зарплате"; }
            else if (Freelancer.freelancer.Any(n => n.Name.ToLower() == name.ToLower()))
            { role = "Внештатный сотрудник"; }
            else
            { 
                Console.WriteLine($"Сотрудник с именем {name} не найден, нажмите любую кнопку для выхода");
                return;
            }
            
            Console.WriteLine($"Здравствуйте, {name}!");
            Console.WriteLine($"Ваша роль: {role}");
            
            //goto отбрасывает сюда, для зацикленности меню пользователя
            StartMenu: 
            Console.WriteLine("\nВыберите желаемое действие:\n");

            switch (role)
            {
                case "Руководитель":
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
                                    byte selectedEmployee = 0;
                                    while (selectedEmployee < 1 || selectedEmployee > 3)
                                    {
                                        try
                                        {
                                            Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                            selectedEmployee = byte.Parse(Console.ReadLine());
                                            Console.WriteLine("\n");
                                        }
                                        catch { continue; }
                                    }

                                    Console.Write("Укажите имя для добавления сотрудника: ");
                                    string nameEmployee = Console.ReadLine();
                                    ActionsOfEmployees.AddEmployee(nameEmployee, selected: selectedEmployee, addNew: true);
                                }
                                break;
                            case 2:
                                { 
                                    Statistics.DisplayAllStats();
                                }
                                break;
                            case 3:
                                { 
                                    Statistics.DisplayStats(manager: true);
                                }
                                break;
                            case 4:
                                { 
                                    ActionsOfEmployees.AddHours(name, true);
                                }
                                break;
                            case 5:
                                { exit = true; }
                                break;
                        }
                    }
                    break;
                case "Сотрудник на зарплате":
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
                                { ActionsOfEmployees.AddHours(name, false); }
                                break;
                            case 2:
                                { Statistics.DisplayStats(name, false); }
                                break;
                            case 3:
                                { exit = true; }
                                break;
                        }
                    }
                    break;
                case "Внештатный сотрудник":
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
                                { ActionsOfEmployees.AddHours(name, false); }
                                break;
                            case 2:
                                { Statistics.DisplayStats(name, false); }
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
            //если role не null, то отбросить к точке начала меню - StartMenu:
            if (role is not null && exit is false)
            {
                //goto отбрасывает на начало меню, для зацикленности
                goto StartMenu;
            }
        }
    }
}
