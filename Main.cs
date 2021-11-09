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

        public static string  filePeople        = "people.json";
        private static string fileCustomers     = "customers.json";
        private static string fileEmployeeRoles = "employeeRoles.json";
        private static string fileEmployees     = "employees.json";

        public static int windowHeight  = 35;
        public static int windowWidth   = 80;

        private static void Main(string[] args)
        {
            //initialize variables
            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;                                         // doesn't seem to work correctly ?
            // init console window properties

            Console.Title = "Avans C# Console Application prototype";
            Console.SetWindowSize(windowWidth, windowHeight);
            //Console.SetWindowPosition(11, 9);                     //TODO: figure out SetWindowsPosition
            // IO.Color(IO.TextColors.DefaultForeground);
            //IO.CycleColors(5, false);
            IO.InitializeColors();
            Console.Clear();

            do
            {
                IO.DisplayMenu("Main Menu", "(L)ogin\n(P)eople\n(E)mployees\n(C)ustomers\nPro(D)ucts\n(M)asterdata\n\n(F3-F10) change colors, (F11) reset (F12) save\n\nEnter your choice, Escape to Exit program\n\n", IO.TextColors.MenuSelect);
                
                inputKey = Console.ReadKey(true);               // 'true' | dont'display the input on the console
                CheckMenuInput();
                //
            } while (inputKey.Key != ConsoleKey.Escape);

            Console.WriteLine("\n\nYou have been logged out.. Goodbye!");
            Thread.Sleep(500);
            Console.ResetColor();
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
                IO.DisplayMenu("Browse/edit People", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", IO.TextColors.MenuSelect);
                People();
            }
            else if (inputKey.Key == ConsoleKey.E & Login.validPassword)             // Employees
            {
                IO.DisplayMenu("Browse/edit Employees", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", IO.TextColors.MenuSelect);
                EditEmployees();
            }
            else if (inputKey.Key == ConsoleKey.C & Login.validPassword)             // Customers
            {
                IO.DisplayMenu("Browse/edit customer records", "(A)dd\nArrows to browse\n(Del)ete\n", IO.TextColors.MenuSelect);
                Customers();
            }
            else if (inputKey.Key == ConsoleKey.D & Login.validPassword)             // Products
            {
                IO.DisplayMenu("Browse/edit product records", "(A)dd\nArrows to browse\n(Del)ete\n", IO.TextColors.MenuSelect);
                //Products();
            }
            else if (inputKey.Key == ConsoleKey.M & Login.validPassword)             // Master Data
            {
                IO.DisplayMenu("Edit Master Data", "(A)dd\nArrows to browse\n(Del)ete\n", IO.TextColors.MenuSelect);
                Console.WriteLine("  You Pressed D");
            }
            else if (inputKey.Key == ConsoleKey.F3)  { IO.CycleColors(6, false); return; }      // input text color
            else if (inputKey.Key == ConsoleKey.F4)  { IO.CycleColors(0, false); return; }      // highlighted text color
            else if (inputKey.Key == ConsoleKey.F5)  { IO.CycleColors(1, false); return; }      // normal text 
            else if (inputKey.Key == ConsoleKey.F6)  { IO.CycleColors(2, false); return; }      // background
            else if (inputKey.Key == ConsoleKey.F7)  { IO.CycleColors(3, false); return; }      // Menu select color
            else if (inputKey.Key == ConsoleKey.F8)  { IO.CycleColors(4, false); return; }      // Software license nameholder Color
            else if (inputKey.Key == ConsoleKey.F9)  { IO.CycleColors(5, true ); return; }      // Random colors including background
            else if (inputKey.Key == ConsoleKey.F10) { IO.CycleColors(5, false); return; }      // Random colors excluding background
            else if (inputKey.Key == ConsoleKey.F11)
            {
                IO.SetStandardColor();
                IO.SaveColors();
            }
            else if (inputKey.Key == ConsoleKey.F12) 
            {
                IO.SaveColors();
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
                recordIndex = 1;
                Person.SetTotalRecords(maxRecords);             
                Person.DisplayRecord(peopleList, recordIndex, false);
            }
            else
            {
                maxRecords  = 0;
                recordIndex = 1;
                Person.DisplayRecord(peopleList, recordIndex, true);        // display a clear form 
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

                        if (maxRecords > 0 & Person.CheckIfActive(peopleList[recordIndex - 1]))                 // some record is being displayed
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
                        Person.DisplayRecord(peopleList, recordIndex, true);        // clear inputform
                        Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                        peopleList.Add(new Person());
                        maxRecords++;
                        recordIndex = maxRecords;
                        Person.SetTotalRecords(maxRecords);
                        UpdateTotalRecordsOnScreen(maxRecords);
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Person.DisplayRecord(peopleList, recordIndex, false);
                        IO.WriteToFile(filePeople, peopleList);
                        
                        
                        break;

                    case ConsoleKey.Delete:                   // mark record for deletion
                        if (maxRecords > 0)                 
                        {
                            Person.ToggleDeletionFlag(peopleList, recordIndex);
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            Person.DisplayRecord(peopleList, recordIndex, false);       
                        }
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

        private static void UpdateTotalRecordsOnScreen(int maxRecords)                  // NICE: display inactive records as well
        {
            IO.Color(IO.TextColors.MenuSelect);
            IO.PrintOnConsole("[" + maxRecords.ToString() + "] active records\t", 30, 5);
            IO.Color(IO.TextColors.DefaultForeground);
        }

        private static void Customers()
        {
            Customer newCustomer = new Customer();
        }
    }
}