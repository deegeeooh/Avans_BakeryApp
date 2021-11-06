using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Vlaaieboer
{
    internal class Program
    {
        // declare variables
        private static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();

        private static string filePeople = "people.json";
        private static string fileCustomers = "customers.json";
        private static string fileEmployeeRoles = "employeeRoles.json";
        private static string fileEmployees = "employees.json";

        private static void Main(string[] args)
        {
            //initialize variables
            // Prevent example from ending if CTL+C is pressed.
            // Console.TreatControlCAsInput = true;                                         // doesn't seem to work correctly ?

            // init console window properties

            Console.Title = "Avans C# Console Application prototype";
            Console.SetWindowSize(80, 35);
            //Console.SetWindowPosition(11, 9);                     //TODO: figure out SetWindowsPosition
            IO.Color(5);
            Console.Clear();

            do
            {
                IO.DisplayMenu("Main Menu", "(L)ogin\n(P)eople\n(E)mployees\n(C)ustomers\nPro(D)ucts\n(M)asterdata\n\nEnter your choice, Escape to Exit program\n\n", 2);
                inputKey = Console.ReadKey(true);               // 'true' | dont'display the input on the console
                CheckMenuInput();
            } while (inputKey.Key != ConsoleKey.Escape);

            Console.WriteLine("\n\nYou have been logged out.. Goodbye!");
            IO.Color(0);
            Thread.Sleep(500);
        }

        private static void CheckMenuInput()
        {
            //inputKey = Console.ReadKey(true);               // 'true' | dont'display the input on the console
            //if ((inputKey.Modifiers & ConsoleModifiers.Control) != 0) { Console.Write("Control+"); }

            if (inputKey.Key == ConsoleKey.L || !Login.validPassword & inputKey.Key != ConsoleKey.Escape)
            {
                _ = new Login();            // _ discard unnecessary var declaration
            }
            else if (inputKey.Key == ConsoleKey.P)             // People
            {
                IO.DisplayMenu("Browse/edit People", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", 2);
                People();
            }
            else if (inputKey.Key == ConsoleKey.E & Login.validPassword)             // Employees
            {
                IO.DisplayMenu("Browse/edit Employees", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", 2);
                EditEmployees();
            }
            else if (inputKey.Key == ConsoleKey.C & Login.validPassword)             // Customers
            {
                IO.DisplayMenu("Browse/edit customer records", "(A)dd\nArrows to browse\n(Del)ete\n", 2);
                Customers();
            }
            else if (inputKey.Key == ConsoleKey.D & Login.validPassword)             // Products
            {
                IO.DisplayMenu("Browse/edit product records", "(A)dd\nArrows to browse\n(Del)ete\n", 2);
                //Products();
            }
            else if (inputKey.Key == ConsoleKey.M & Login.validPassword)             // Master Data
            {
                IO.DisplayMenu("Edit Master Data", "(A)dd\nArrows to browse\n(Del)ete\n", 2);
                Console.WriteLine("  You Pressed D");
            }
            else if (inputKey.Key == ConsoleKey.A)                                  // Assembly info
            {
                //IO.DisplayMenu("Browse/edit product records", "(A)dd\nArrows to browse\n(Del)ete\n", 2);
                ShowAssemblyInfo();
            }
            else if (inputKey.Key == ConsoleKey.Escape)                               // exit program
            {
                return;
            }
        }

        private static void ShowAssemblyInfo()
        {
            Console.Clear();

            var assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine(assembly.FullName);

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                Console.WriteLine("Type: " + type.Name + " Base Type: " + type.BaseType);

                var props = type.GetProperties();
                foreach (var prop in props)
                {
                    Console.WriteLine("\tProperty name: " + prop.Name.PadRight(20, ' ') + "\t Property Type: " + prop.PropertyType);
                }

                var fields = type.GetFields();
                foreach (var field in fields)
                {
                    Console.WriteLine("\tField: " + field.Name);
                }

                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    Console.WriteLine("\t\tMethod name: " + method.Name.PadRight(20, ' '));
                }
                Console.ReadKey();
            }
            Console.ReadKey();
        }

        private static void EditEmployees()
        {
            var empl = new Employee();
        }

        private static void People()
        {
            var peopleList  = IO.PopulateList<Person>(filePeople);          // remark: reading entire file into list, probably want an indexfile IRL
            int cursorLeft  = Console.CursorLeft;                           // store current cursorposition, left and top
            int cursorTop   = Console.CursorTop;
            int recordIndex = 0;
            int maxRecords  = 0;

            if (peopleList.Count > 0)
            {
                maxRecords = peopleList.Count;                              // not necessary, just use static class attribute totalRecords directly
                Person.SetTotalRecords(maxRecords);             
                recordIndex = 1;
                Person.DisplayRecord(peopleList, recordIndex, false);
            }
            else
            {
                maxRecords  = 0;
                recordIndex = 1;
                Person.DisplayRecord(peopleList, recordIndex, true);
            }
            UpdateTotalRecordsOnScreen(maxRecords);
            string inputString = "abcdefghijklmnopqrstuvwxyz" + ConsoleKey.Backspace.ToString();
            StringBuilder zoekstring = new StringBuilder();

            do
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.Enter:                  // edit current existing record

                        if (maxRecords > 0)                 // some record is being displayed
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop + 1);       // set cursor on first inputfield
                            Person.EditRecord(peopleList, recordIndex);             // edit current record
                            IO.WriteToFile(filePeople, peopleList);          // write to file
                            Console.SetCursorPosition(cursorLeft, cursorTop);           // cursor back to top
                            Person.DisplayRecord(peopleList, recordIndex, false);   // display record for updated employeeID and age
                        }
                        break;

                    case ConsoleKey.Insert:                 // add new record

                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Person.DisplayRecord(peopleList, recordIndex, true);
                        Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                        peopleList.Add(new Person());
                        maxRecords++;
                        recordIndex++;
                        Person.SetTotalRecords(maxRecords);
                        UpdateTotalRecordsOnScreen(maxRecords);
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Person.DisplayRecord(peopleList, recordIndex, false);
                        IO.WriteToFile(filePeople, peopleList);

                        break;

                    case ConsoleKey.Delete:             // delete current record

                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.UpArrow:

                        if (recordIndex > 1)
                        {
                            recordIndex--;
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            Person.DisplayRecord(peopleList, recordIndex, false);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.DownArrow:

                        if (recordIndex < maxRecords)
                        {
                            recordIndex++;
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            Person.DisplayRecord(peopleList, recordIndex, false);
                        }

                        break;

                    default:

                        recordIndex = SearchStringInList(peopleList, cursorLeft, cursorTop, recordIndex, zoekstring);

                        break;
                }
            } while (inputKey.Key != ConsoleKey.Home);
        }

        private static int SearchStringInList(List<Person> peopleList, int cursorLeft, int cursorTop, int recordIndex, StringBuilder zoekstring)
        {
            zoekstring.Append(inputKey.KeyChar.ToString());
            IO.PrintOnConsole("Searching: [ " + zoekstring.ToString() + " ]".PadRight(20, ' '), 1, cursorTop - 1);

            if (zoekstring.ToString().Contains(inputKey.KeyChar.ToString()))

            {
                Person personSearchResult = peopleList.Find
                (
                    delegate (Person emp)
                    {
                        return emp.LastName.StartsWith(zoekstring.ToString());
                    }
                );
                if (personSearchResult != null)
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    Person.DisplayRecord(peopleList, personSearchResult.RecordCounter, false);
                    recordIndex = personSearchResult.RecordCounter;
                }
                else
                {
                    zoekstring.Clear();
                }
            }

            return recordIndex;
        }

        private static void UpdateTotalRecordsOnScreen(int maxRecords)                  // TODO: check on .Relationtype = "Y"
        {
            IO.Color(2);
            IO.PrintOnConsole("[" + maxRecords.ToString() + "] employee records", 30, 5);
            IO.Color(5);
        }

        private static void Customers()
        {
            Customer newCustomer = new Customer();
        }
    }
}