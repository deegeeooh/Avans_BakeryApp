using System;
using System.Text;
using System.Threading;

namespace Vlaaieboer
{
    internal class Program
    {
        // declare variables
        private static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();

        private static string filePeople = "employees.json";
        private static string fileCustomers = "customers.json";
        private static string fileEmployeeRoles = "employeeRoles.json";

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
                IO.DisplayMenu("Main Menu", "(L)ogin\n(P)eople\n(C)ustomers\nPro(D)ucts\n(M)asterdata\n\nEnter your choice, Escape to Exit program\n\n", 2);
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
            else if (inputKey.Key == ConsoleKey.P & Login.validPassword)             // People
            {
                IO.DisplayMenu("Browse/edit People", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", 2);
                Employees();
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
            else if (inputKey.Key == ConsoleKey.Escape)                                 // exit program
            {
                return;
            }
        }

        private static void Employees()
        {
            var peopleList = Person.PopulateList(filePeople);
            int cursorLeft = Console.CursorLeft;                           // store current cursorposition, left and top
            int cursorTop = Console.CursorTop;
            int recordIndex = 0;
            int maxRecords = 0;

            if (peopleList.Count > 0)
            {
                maxRecords = peopleList.Count;
                recordIndex = 1;
                Person.DisplayRecord(peopleList, recordIndex, false);
            }
            else
            {
                maxRecords = 0;
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
                            Person.WriteToFile(filePeople, peopleList);          // write to file
                            Console.SetCursorPosition(cursorLeft, cursorTop);           // cursor back to top
                            Person.DisplayRecord(peopleList, recordIndex, false);   // display record for updated employeeID and age
                        }

                        break;

                    case ConsoleKey.Insert:                 // add new record

                        //IO.DisplayMenu("Edit Customer master data", "Enter to validate field\nDel/Insert character\nArrow keys, Home, End to navigate\n");
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Person.DisplayRecord(peopleList, recordIndex, true);
                        Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                        peopleList.Add(new Person(true));
                        maxRecords = Person.totalRecords;
                        recordIndex++;
                        UpdateTotalRecordsOnScreen(maxRecords);
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Person.DisplayRecord(peopleList, recordIndex, false);
                        Person.WriteToFile(filePeople, peopleList);

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

                        zoekstring.Append(inputKey.KeyChar.ToString());
                        IO.PrintOnConsole("Searching: [ " + zoekstring.ToString() + " ]".PadRight(20, ' '), 1, cursorTop - 1);

                        if (zoekstring.ToString().Contains(inputKey.KeyChar.ToString()))

                        {
                            Person employeeSearchResult = peopleList.Find
                            (
                                delegate (Person emp)
                                {
                                    return emp.SurName.StartsWith(zoekstring.ToString());
                                }
                            );
                            if (employeeSearchResult != null)
                            {
                                //var ix = empResult.RecordCounter;
                                Console.SetCursorPosition(cursorLeft, cursorTop);
                                Person.DisplayRecord(peopleList, employeeSearchResult.RecordCounter, false);
                                recordIndex = employeeSearchResult.RecordCounter;
                            }
                            else
                            {
                                zoekstring.Clear();
                            }
                        }

                        break;
                }
            } while (inputKey.Key != ConsoleKey.Home);
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