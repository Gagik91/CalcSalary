using System;
using System.Collections.Generic;
using System.IO;

namespace CalcSalary
{
    public class Files
    {
        public static string manFile = @"Files\managerHoursWorkedList.csv";     //список всех менеджеров с отработанными часами
        public static string empFile = @"Files\employeeHoursWorkedList.csv";    //список всех сотрудников на зарплате с отработанными часами
        public static string freeFile = @"Files\freelancerHoursWorkedList.csv"; //список всех фрилансеров с отработанными часами
        public static string employeeListFile = @"Files\employeeListFile.csv";  //список всех сотрудников с указанием ролей

        public static void FilesActionManager()
        {
            //получаем массив string в каждом элементе следующая информация:
            //дата, имя, отработанных количество часов, сообщение
            var manData = ManagerReader().Split('\n', int.MaxValue);

            List<string> manName = new List<string>();
            List<string> manDate = new List<string>();
            List<byte> manHours = new List<byte>();
            List<string> manMessage = new List<string>();

            foreach (var item in manData)
            {
                int startName = 0;
                int endName = 0;
                int startHours = 0;
                int endHours = 0;
                int startMessage = 0;

                if (item.Length > 1)
                {
                    manDate.Add(item[0..10]);
                    startName = item.IndexOf(", ") + 2;
                    string tempName = item.Substring(startName);
                    endName = tempName.IndexOf(", ");
                    manName.Add(tempName.Substring(0, endName));

                    startHours = endName + 1;
                    string tempHours = tempName.Substring(startHours);
                    endHours = tempHours.IndexOf(", ");
                    manHours.Add(byte.Parse(tempHours.Substring(0, endHours)));

                    startMessage = endHours + 2;
                    manMessage.Add(tempHours.Substring(startMessage));
                }
            }

            for (int i = 0; i < manName.Count; i++)
            {
                ActionsOfEmployees.AddMan(manName[i], manHours[i], DateTime.Parse(manDate[i]), manMessage[i]);
            }
        }

        public static void FilesActionEmployee()
        {
            //получаем массив string в каждом элементе следующая информация:
            //дата, имя, отработанных количество часов, сообщение
            var empData = EmployeeReader().Split('\n', int.MaxValue);

            List<string> empName = new List<string>();
            List<string> empDate = new List<string>();
            List<byte> empHours = new List<byte>();
            List<string> empMessage = new List<string>();

            foreach (var item in empData)
            {
                int startName = 0;
                int endName = 0;
                int startHours = 0;
                int endHours = 0;
                int startMessage = 0;

                if (item.Length > 1)
                {
                    empDate.Add(item[0..10]);
                    startName = item.IndexOf(", ") + 2;
                    string tempName = item.Substring(startName);
                    endName = tempName.IndexOf(", ");
                    empName.Add(tempName.Substring(0, endName));

                    startHours = endName + 1;
                    string tempHours = tempName.Substring(startHours);
                    endHours = tempHours.IndexOf(", ");
                    empHours.Add(byte.Parse(tempHours.Substring(0, endHours)));

                    startMessage = endHours + 2;
                    empMessage.Add(tempHours.Substring(startMessage));
                }
            }

            for (int i = 0; i < empName.Count; i++)
            {
                ActionsOfEmployees.AddEmp(empName[i], empHours[i], DateTime.Parse(empDate[i]), empMessage[i]);
            }
        }

        public static void FilesActionFreelancer()
        {
            //получаем массив string в каждом элементе следующая информация:
            //дата, имя, отработанных количество часов, сообщение
            var freeData = FreelancerReader().Split('\n', int.MaxValue);

            List<string> freeName = new List<string>();
            List<string> freeDate = new List<string>();
            List<byte> freeHours = new List<byte>();
            List<string> freeMessage = new List<string>();

            foreach (var item in freeData)
            {
                int startName = 0;
                int endName = 0;
                int startHours = 0;
                int endHours = 0;
                int startMessage = 0;

                if (item.Length > 1)
                {
                    freeDate.Add(item[0..10]);
                    startName = item.IndexOf(", ") + 2;
                    string tempName = item.Substring(startName);
                    endName = tempName.IndexOf(", ");
                    freeName.Add(tempName.Substring(0, endName));

                    startHours = endName + 1;
                    string tempHours = tempName.Substring(startHours);
                    endHours = tempHours.IndexOf(", ");
                    freeHours.Add(byte.Parse(tempHours.Substring(0, endHours)));

                    startMessage = endHours + 2;
                    freeMessage.Add(tempHours.Substring(startMessage));
                }
            }

            for (int i = 0; i < freeName.Count; i++)
            {
                ActionsOfEmployees.AddFree(freeName[i], freeHours[i], DateTime.Parse(freeDate[i]), freeMessage[i]);
            }
        }

        //чтение из файла managerHoursWorkedList.csv с проверкой на существование папки(если нет создать) и существование файла (если нет создать)    
        //список всех менеджеров с отработанными часами
        public static string ManagerReader()
        {
            string str = "";
            if (Directory.Exists(@"Files"))
            {
                if (File.Exists(manFile))
                {
                    using (StreamReader managerListStreamReader = new StreamReader(manFile, true))
                    {
                        str = managerListStreamReader.ReadToEnd();
                    }
                }
                else
                {
                    ManagerWriter(DateTime.MinValue," ",0," ");
                    ManagerReader();
                }
            }
            else
            {
                Directory.CreateDirectory(manFile);
                ManagerReader();
            }
            return str;
        }

        //чтение из файла employeeHoursWorkedList.csv с проверкой на существование папки(если нет создать) и существование файла (если нет создать)
        //список всех штатных сотрудников с отработанными часами
        public static string EmployeeReader()
        {            
            string str = "";
            if (Directory.Exists(@"Files"))
            {
                if (File.Exists(empFile))
                {
                    using (StreamReader employeeListStreamReader = new StreamReader(empFile, true))
                    {
                        str = employeeListStreamReader.ReadToEnd();
                    }
                }
                else
                {
                    EmployeeWriter(DateTime.MinValue, " ", 0, " ");
                    EmployeeReader();
                }
            }
            else
            {
                Directory.CreateDirectory(empFile);
                EmployeeReader();
            }
            return str;
        }

        //чтение из файла freelancerHoursWorkedList.csv с проверкой на существование папки(если нет создать) и существование файла (если нет создать)
        //список всех внештатных сотрудников с отработанными часами
        public static string FreelancerReader()
        {            
            string str = "";
            if (Directory.Exists(@"Files"))
            {
                if (File.Exists(freeFile))
                {
                    using (StreamReader freelancerListStreamReader = new StreamReader(freeFile, true))
                    {
                        str = freelancerListStreamReader.ReadToEnd();
                    }
                }
                else
                {
                    FreelancerWriter(DateTime.MinValue, " ", 0, " ");
                    FreelancerReader();
                }
            }
            else
            {
                Directory.CreateDirectory(freeFile);
                FreelancerReader();
            }
            return str;
        }

        //запись в файл managerHoursWorkedList.csv    -   список всех менеджеров с отработанными часами
        public static void ManagerWriter(DateTime date, string name, byte hours, string message)
        {
            using (StreamWriter managerListStreamWriter = new StreamWriter(manFile, true))
            {
                managerListStreamWriter.WriteLine($"{date.ToShortDateString()}, {name}, {hours}, {message}");
            }
        }

        //запись в файл employeeHoursWorkedList.csv    -   список всех штатных сотрудников с отработанными часами
        public static void EmployeeWriter(DateTime date, string name, byte hours, string message)
        {
            using (StreamWriter employeeListStreamWriter = new StreamWriter(empFile, true))
            {
                employeeListStreamWriter.WriteLine($"{date.ToShortDateString()}, {name}, {hours}, {message}");
            }
        }

        //запись в файл freelancerHoursWorkedList.csv    -   список всех внештатных сотрудников с отработанными часами
        public static void FreelancerWriter(DateTime date, string name, byte hours, string message)
        {
            using (StreamWriter freelancerListStreamWriter = new StreamWriter(freeFile, true))
            {
                freelancerListStreamWriter.WriteLine($"{date.ToShortDateString()}, {name}, {hours}, {message}");
            }
        }

        //запись в файл employeeListFile.csv    -   список всех сотрудников с указанием ролей
        public static void EmployeeListWriter(string name, string role)
        {
            using (StreamWriter employeeListStreamWriter = new StreamWriter(employeeListFile, true))
            {
                employeeListStreamWriter.WriteLine($"{name}, {role}");
            }
        }
    }  
}
