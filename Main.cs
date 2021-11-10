using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace BakeryConsole
{
    //
    // 
    //

    internal class Program
    {
        // declare variables
        private static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();

        public static string  filePeople        = "people.json";
        private static string fileCustomers     = "customers.json";
        private static string fileEmployeeRoles = "employeeRoles.json";
        private static string fileEmployees     = "employees.json";

        public static int windowHeight          =  35;
        public static int windowWidth           =  80;
        public static int warningLenghtDefault  = 750;

        private static void Main(string[] args)
        {
            //initialize variables
            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;                                         // doesn't seem to work correctly ?
            // init console window properties

            Console.Title = "Bakery for Console";
            Console.SetWindowSize(windowWidth, windowHeight);
            //Console.SetWindowPosition(11, 9);                     //TODO: figure out SetWindowsPosition
            
            Console.SetBufferSize(windowWidth, windowHeight);
            Console.CursorSize = 60;
            IO.SetWarningLength(warningLenghtDefault);
            Color.InitializeColors();
            Console.Clear();
            do
            {
                IO.DisplayMenu("Main Menu", "(L)ogin\n(P)eople\n(E)mployees\n(C)ustomers\nPro(D)ucts\n(M)asterdata\n\n(F3-F10) change colors, (F11) reset (F12) save\n\nEnter your choice, Escape to Exit program\n\n", Color.TextColors.MenuSelect);
                inputKey = Console.ReadKey(true);               // 'true' | dont'display the input on the console
                CheckMenuInput();

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
                IO.DisplayMenu("Browse/edit People", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                People();
            }
            else if (inputKey.Key == ConsoleKey.E & Login.validPassword)             // Employees
            {
                IO.DisplayMenu("Browse/edit Employees", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                EditEmployees();
            }
            else if (inputKey.Key == ConsoleKey.C & Login.validPassword)             // Customers
            {
                IO.DisplayMenu("Browse/edit customer records", "(A)dd\nArrows to browse\n(Del)ete\n", Color.TextColors.MenuSelect);
                Customers();
            }
            else if (inputKey.Key == ConsoleKey.D & Login.validPassword)             // Products
            {
                IO.DisplayMenu("Browse/edit product records", "(A)dd\nArrows to browse\n(Del)ete\n", Color.TextColors.MenuSelect);
                //Products();
            }
            else if (inputKey.Key == ConsoleKey.M & Login.validPassword)             // Master Data
            {
                IO.DisplayMenu("Edit Master Data", "(A)dd\nArrows to browse\n(Del)ete\n", Color.TextColors.MenuSelect);
                Console.WriteLine("  You Pressed D");
            }
            else if (inputKey.Key == ConsoleKey.F3)  { Color.CycleColors(6, false); return; }      // input text color
            else if (inputKey.Key == ConsoleKey.F4)  { Color.CycleColors(0, false); return; }      // highlighted text color
            else if (inputKey.Key == ConsoleKey.F5)  { Color.CycleColors(1, false); return; }      // normal text 
            else if (inputKey.Key == ConsoleKey.F6)  { Color.CycleColors(2, false); return; }      // background
            else if (inputKey.Key == ConsoleKey.F7)  { Color.CycleColors(3, false); return; }      // Menu select color
            else if (inputKey.Key == ConsoleKey.F8)  { Color.CycleColors(4, false); return; }      // Software license nameholder Color
            else if (inputKey.Key == ConsoleKey.F9)  { Color.CycleColors(5, true ); return; }      // Random colors including background
            else if (inputKey.Key == ConsoleKey.F10) { Color.CycleColors(5, false); return; }      // Random colors excluding background
            else if (inputKey.Key == ConsoleKey.F11)
            {
                Color.SetStandardColor();
            }
            else if (inputKey.Key == ConsoleKey.F12) 
            {
                Color.SaveColors();
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







        //private static void RecordBrowser<T>(string aFilename, object anObject) where T : class
        //{




        //    var browseList = new List<T>();
        //    browseList = IO.PopulateList<T>(aFilename);          // remark: reading entire file into list, probably want an indexfile IRL
        //    int cursorLeft = Console.CursorLeft;                     // store current cursorposition, left and top
        //    int cursorTop = Console.CursorTop;
        //    int recordIndex;
        //    int maxRecords;

        //    if (browseList.Count > 0)
        //    {
        //        maxRecords = browseList.Count;                              // not necessary, just use static class attribute totalRecords directly
        //        recordIndex = 1;
        //        //class.SetTotalRecords(recordIndex);
        //        Person.DisplayRecord(browseList, recordIndex, false);
        //    }
        //    else

        //    {
        //        maxRecords = 0;
        //        recordIndex = 1;
        //        Person.DisplayRecord(browseList, recordIndex, true);        // display a clear form 
        //    }

        //    UpdateTotalRecordsOnScreen(maxRecords);
        //    string inputString = "abcdefghijklmnopqrstuvwxyz" + ConsoleKey.Backspace.ToString();
        //    StringBuilder zoekstring = new StringBuilder();

        //    do                                             // TODO: cleanup and generalize
        //    {
        //        inputKey = Console.ReadKey(true);

        //        switch (inputKey.Key)
        //        {
        //            case ConsoleKey.Enter:                  // edit current existing record

        //                if (maxRecords > 0 & Person.CheckIfActive(browseList, recordIndex))                 // some record is being displayed
        //                {
        //                    Console.SetCursorPosition(cursorLeft, cursorTop + 1);       // set cursor on first inputfield after ID

        //                    Person.EditRecord(browseList, recordIndex);                 // edit current record
        //                    IO.WriteToFile(filePeople, browseList);                     // write to file

        //                    Console.SetCursorPosition(cursorLeft, cursorTop);           // cursor back to top
        //                    Person.DisplayRecord(browseList, recordIndex, false);       // refresh record for updated employeeID and age

        //                    Color.SetWarningColor(false);
        //                    IO.SystemMessage("Record has been updated in file");
        //                }
        //                break;

        //            case ConsoleKey.Insert:                 // add new record

        //                Console.SetCursorPosition(cursorLeft, cursorTop);
        //                Person.DisplayRecord(browseList, recordIndex, true);        // clear inputform

        //                Console.SetCursorPosition(cursorLeft, cursorTop + 1);
        //                browseList.Add(new Person());
        //                maxRecords++;
        //                recordIndex = maxRecords;
        //                Person.SetTotalRecords(maxRecords);
        //                UpdateTotalRecordsOnScreen(maxRecords);
        //                Console.SetCursorPosition(cursorLeft, cursorTop);
        //                Person.DisplayRecord(browseList, recordIndex, false);
        //                IO.WriteToFile(filePeople, browseList);
        //                //
        //                Color.SetWarningColor(false);
        //                IO.SystemMessage("Record has been written to file");

        //                break;

        //            case ConsoleKey.Delete:                   // mark record for deletion
        //                if (maxRecords > 0)
        //                {
        //                    Person.ToggleDeletionFlag(browseList, recordIndex);
        //                    Console.SetCursorPosition(cursorLeft, cursorTop);
        //                    Person.DisplayRecord(browseList, recordIndex, false);
        //                    Color.SetWarningColor(false);
        //                    if (Person.CheckIfActive(browseList, recordIndex))
        //                    {
        //                        IO.SystemMessage("Record has been set to Active");
        //                    }
        //                    else
        //                    {
        //                        IO.SystemMessage("Record has been marked for Deletion");
        //                    }

        //                }
        //                break;
        //            case ConsoleKey.LeftArrow:
        //            case ConsoleKey.UpArrow:

        //                if (recordIndex > 1)
        //                {
        //                    recordIndex--;
        //                    Console.SetCursorPosition(cursorLeft, cursorTop);
        //                    Person.DisplayRecord(browseList, recordIndex, false);
        //                }
        //                break;

        //            case ConsoleKey.RightArrow:
        //            case ConsoleKey.DownArrow:

        //                if (recordIndex < maxRecords)
        //                {
        //                    recordIndex++;
        //                    Console.SetCursorPosition(cursorLeft, cursorTop);
        //                    Person.DisplayRecord(browseList, recordIndex, false);
        //                }

        //                break;

        //            default:

        //                recordIndex = SearchStringInList(browseList, cursorLeft, cursorTop, recordIndex, zoekstring);

        //                break;
        //        }
        //    } while (inputKey.Key != ConsoleKey.Home);
        //}


        private static void People()
        {

            var peopleList = IO.PopulateList<Person>(filePeople);          // remark: reading entire file into list, probably want an indexfile IRL
            int cursorLeft = Console.CursorLeft;                           // store current cursorposition, left and top
            int cursorTop = Console.CursorTop;
            int recordIndex;
            int maxRecords;

            if (peopleList.Count > 0)
            {
                maxRecords = peopleList.Count;                              // not necessary, just use static class attribute totalRecords directly
                recordIndex = 1;
                Person.SetTotalRecords(maxRecords);
                Person.DisplayRecord(peopleList, recordIndex, false);
            }
            else
            {
                maxRecords = 0;
                recordIndex = 1;
                Person.DisplayRecord(peopleList, recordIndex, true);        // display a clear form 
            }

            UpdateTotalRecordsOnScreen(maxRecords);
            string inputString = "abcdefghijklmnopqrstuvwxyz" + ConsoleKey.Backspace.ToString();
            StringBuilder zoekstring = new StringBuilder();

            do                                             // TODO: cleanup and generalize
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.Enter:                                                  // edit current record being displayed

                        if (maxRecords > 0 & Person.CheckIfActive(peopleList, recordIndex)) // some record is being displayed
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop + 1);           // set cursor on first inputfield after ID
                            //Person newP = new Person(peopleList, recordIndex);
                            peopleList.Insert(recordIndex -  1, new Person(peopleList[recordIndex - 1]));
                            
                            peopleList.RemoveAt(recordIndex);
                            // Person.EditRecord(peopleList, recordIndex);                     // edit current record
                            IO.WriteToFile(filePeople, peopleList);                         // write to file

                            Console.SetCursorPosition(cursorLeft, cursorTop);               // cursor back to top
                            Person.DisplayRecord(peopleList, recordIndex, false);           // refresh record for updated employeeID and age

                            Color.SetWarningColor(false);
                            IO.SystemMessage("Record has been updated in file");
                        }
                        break;

                    case ConsoleKey.Insert:                                                 // add new record

                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Person.DisplayRecord(peopleList, recordIndex, true);                // clear inputform

                        Console.SetCursorPosition(cursorLeft, cursorTop + 1);


                        peopleList.Add(new Person());                                       // call standard constructor
                        maxRecords++;                                                       // increase number of records 
                        recordIndex = maxRecords;                                           // set recordindex to last record;
                        Person.SetTotalRecords(maxRecords);                                 // update records in class

                        UpdateTotalRecordsOnScreen(maxRecords);

                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Person.DisplayRecord(peopleList, recordIndex, false);               // refresh record on for updated employeeID and age
                        IO.WriteToFile(filePeople, peopleList);                             // write to JSON file
                        //
                        Color.SetWarningColor(false);
                        IO.SystemMessage("Record has been written to file");

                        break;

                    case ConsoleKey.Delete:                                                 // mark record for deletion
                        if (maxRecords > 0)
                        {
                            Person.ToggleDeletionFlag(peopleList, recordIndex);             // toggle .Active property
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            Person.DisplayRecord(peopleList, recordIndex, false);           // refresh record

                            Color.SetWarningColor(false);
                            if (Person.CheckIfActive(peopleList, recordIndex))
                            {
                                IO.SystemMessage("Record has been set to Active");
                            }
                            else
                            {
                                IO.SystemMessage("Record has been marked for Deletion");
                            }

                        }
                        break;
                    case ConsoleKey.LeftArrow:                                              // browse to previous
                    case ConsoleKey.UpArrow:

                        if (recordIndex > 1)
                        {
                            recordIndex--;
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            Person.DisplayRecord(peopleList, recordIndex, false);           // display
                        }
                        break;

                    case ConsoleKey.RightArrow:                                             //browse
                    case ConsoleKey.DownArrow:

                        if (recordIndex < maxRecords)                                       // while not EoF
                        {
                            recordIndex++;
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            Person.DisplayRecord(peopleList, recordIndex, false);
                        }

                        break;

                    default:                                                                // search

                        recordIndex = SearchStringInList(peopleList, cursorLeft, cursorTop, recordIndex, zoekstring);

                        break;
                }
            } while (inputKey.Key != ConsoleKey.Home);
        }

        private static int SearchStringInList(List<Person> peopleList, int cursorLeft, int cursorTop, int recordIndex, StringBuilder zoekstring)
        {
            zoekstring.Append(inputKey.KeyChar.ToString());
            IO.PrintOnConsole("Searching: [ " + zoekstring.ToString() + " ]".PadRight(20, ' '), 1, cursorTop - 1, Color.TextColors.Defaults);

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
            IO.PrintOnConsole("[" + maxRecords.ToString() + "] active records\t", 30, 5,Color.TextColors.MenuSelect);
        }

        private static void Customers()
        {
            Customer newCustomer = new Customer();
        }
    }
}