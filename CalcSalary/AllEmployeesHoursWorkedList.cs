using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcSalary
{
    class AllEmployeesHoursWorkedList
    {
        public int Id { get; set; } //Автоматически  будет исользоваться как primary key
        public Settings.Roles RoleEmployee { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public byte WorkedHours { get; set; }
        public string Message { get; set; }

        public static void AddEmployee(Settings.Roles role, string name, DateTime date, byte hours = 0, string message = "First day work")
        {
            using (ContextWithDB db = new ContextWithDB())
            {
                // Создаем экземпляр AllEmployeesHoursWorkedList
                AllEmployeesHoursWorkedList employee = new AllEmployeesHoursWorkedList
                {
                    RoleEmployee = role,
                    Name = name,
                    Date = date,
                    WorkedHours = hours,
                    Message = message
                };

                // Добавляем экземпляр в БД
                db.AllEmployeesHoursWorkedList.Add(employee);

                // Сохраняем изменения
                db.SaveChanges();
                Console.WriteLine("Данные успешно сохранены");
            }
        }
        public static void RemoveEmployee(string name)
        {
            using (ContextWithDB db = new ContextWithDB())
            {
                // получаем объекты
                var employee = db.AllEmployeesHoursWorkedList.Where(n => n.Name.ToLower() == name.ToLower()).ToList();

                //проходим по всем объектам и удаляем совпадения
                if (employee is not null)
                {
                    foreach (var e in employee)
                    {
                        db.AllEmployeesHoursWorkedList.Remove(e);

                        // Сохраняем изменения
                        db.SaveChanges();
                    }
                    Console.WriteLine("Данные успешно удалены");
                }
                else
                {
                    Console.WriteLine($"Сотрудник {name} не найден");
                }                
            }
        }
    }
}

    
