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
       
        public Person(string name)
        {
            Name = name;
        }

        public static void ActionMenu()
        {
            using (ContextWithDB db = new ContextWithDB())
            {
                Console.Write("Введите Ваше имя: ");
                string name = Console.ReadLine();
                //goto отбрасывает сюда для повторного ввода имени, начало программы
                StartProgram:
                Settings.Roles empRole = new Settings.Roles();
                if (db.AllEmployeesHoursWorkedList.Any(n => n.Name.ToLower() == name.ToLower()))
                {
                    empRole = db.AllEmployeesHoursWorkedList.FirstOrDefault(n => n.Name.ToLower() == name.ToLower()).RoleEmployee;
                }
                else
                {
                    empRole = 0;
                }
                byte selectedAction;
                Settings.Roles selectedRole = new Settings.Roles();
                bool exit = false;
                string role = "";
                //проверяем по имени к какой роли относится сотрудник и присваиваем соответствующее значение

                switch (empRole)
                {
                    case Settings.Roles.Manager:
                        { role = "Руководитель"; }
                        break;
                    case Settings.Roles.Employee:
                        { role = "Сотрудник на зарплате"; }
                        break;
                    case Settings.Roles.Freelancer:
                        { role = "Внештатный сотрудник"; }
                        break;
                    default:
                        {                            
                            Console.WriteLine($"Сотрудник с именем {name} не найден, нажмите 1 для выхода или введите имя");
                            name = Console.ReadLine();
                            if (name == "1")
                            {
                                return;
                            }
                            //отбрасывает для повторного ввода имени
                            goto StartProgram;
                        }
                }

                Console.WriteLine($"Здравствуйте, {name}!");
                Console.WriteLine($"Ваша роль: {role}");

                //goto отбрасывает сюда, для зацикленности меню пользователя
                StartMenu:
                Console.WriteLine("\nВыберите желаемое действие:\n");

                switch (empRole)
                {
                    case Settings.Roles.Manager:
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
                                        while ((int)selectedRole < 1 || (int)selectedRole > 3)
                                        {
                                            try
                                            {
                                                Console.Write("Нужно ввести цифру из предложенных вариантов: ");
                                                //Приводим к ролям из перечисления Settings.Roles
                                                selectedRole = (Settings.Roles)byte.Parse(Console.ReadLine());
                                                Console.WriteLine("\n");
                                            }
                                            catch { continue; }
                                        }

                                        Console.Write("Укажите имя для добавления сотрудника: ");
                                        string nameEmployee = Console.ReadLine();
                                        ActionsOfEmployees.AddEmployee(nameEmployee, selectedRole: selectedRole);
                                    }
                                    break;
                                case 2:
                                    { Statistics.DisplayAllStats(); }
                                    break;
                                case 3:
                                    { Statistics.DisplayStats(name); }
                                    break;
                                case 4:
                                    { ActionsOfEmployees.AddHours(name); }
                                    break;
                                case 5:
                                    { exit = true; }
                                    break;
                            }
                        }
                        break;
                    case Settings.Roles.Employee:
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
                                    { ActionsOfEmployees.AddHours(name); }
                                    break;
                                case 2:
                                    { Statistics.DisplayStats(name); }
                                    break;
                                case 3:
                                    { exit = true; }
                                    break;
                            }
                        }
                        break;
                    case Settings.Roles.Freelancer:
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
                                    { ActionsOfEmployees.AddHours(name); }
                                    break;
                                case 2:
                                    { Statistics.DisplayStats(name); }
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
}
