using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionTest
{
    internal class Test
    {
        private static void Main(string[] args)
        {
            var boss = new List<People>()
            {
                new People() {ID="BAK01",       Name = "Bakker" },
                new People() {ID="VRE02",       Name = "Vries" },
            };

            var employees = new List<Employees>()
            {
                new Employees() {EmployeeNumber=1,
                                 Salary=2000,
                                 JobTitle = "Head Nerds",
                                 ID="DEG01",
                                 Name="De Groot"},

                new Employees() {EmployeeNumber=2,
                                 Salary=3500,
                                 JobTitle = "Regular Nerd",
                                 ID="LOS01",
                                 Name="Lossie"},
            };

            PrintList<People>(boss);
            Console.ReadKey();
            PrintList<Employees>(employees);
        }

        private static void PrintList<T>(List<T> aList) where T : class
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = aList[0].GetType();

            Console.WriteLine("Type: " + type.Name + " Base Type: " + type.BaseType);

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                Console.WriteLine("\tProperty name: " + prop.Name.PadRight(20, ' ') + "\t Property Type: " + prop.PropertyType);

                for (int i = 0; i < aList.Count; i++)
                {
                    try
                    {
                        Console.WriteLine("\t" + aList[i].GetType().GetProperty(prop.Name).GetValue(aList[i], null));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Exception while getting properties {e}");
                    }
                }
            }
        }

        public static T GetPropertyValue<T>(object obj, string propName)
        {
            return (T)obj.GetType().GetProperty(propName).GetValue(obj, null);
        }
    }

    internal class People
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    internal class Employees : People
    {
        private static int recordnumber = 0;

        public int EmployeeNumber { get; set; }
        public int Salary { get; set; }
        public string JobTitle { get; set; }
    }
}