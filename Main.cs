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

    enum ClassSelect
    {
        Person,
        Employee,
        Customers,
        Products
    }

    internal class Program
    {
        // declare variables
        private static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();

        public static string  filePeople        = "people.json";
        private static string fileCustomers     = "customers.json";
        private static string fileEmployeeRoles = "employeeRoles.json";
        private static string fileEmployees     = "employees.json";
        private static string fileProducts      = "products.json";

        public static int windowHeight          =  35;
        public static int windowWidth           =  80;
        public static int warningLenghtDefault  = 750;              // displaytime in ms for system messages 

        public static ClassSelect classSelect = new ClassSelect();

        private static void Main(string[] args)
        {
            
            // Prevent using CTL+C for termination
            Console.TreatControlCAsInput = true;
            // init console window properties
            // Console.SetWindowPosition(11, 9);                     //TODO: figure out SetWindowsPosition

            // initialize variables and window
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(windowWidth, windowHeight);
            IO.SetWarningLength  (warningLenghtDefault);
            Console.Title        = "Bakery for Console";
            Console.CursorSize   = 60;
            Color.InitializeColors();                               // read settings.json and set color scheme
            Console.Clear();

            // Main Menu Loop
            do
            {
                IO.DisplayMenu("Main Menu", "(L)ogin\n(P)eople\n(E)mployees\n(C)ustomers\nPro(D)ucts\n(M)asterdata\n\n(F3-F10) change colors, (F11) reset (F12) save\n\nEnter your choice, Escape to Exit program\n\n", Color.TextColors.MenuSelect);
                inputKey = Console.ReadKey(true);                    // 'true' | dont'display the input on the console
                CheckMenuInput();
            } while (inputKey.Key != ConsoleKey.Escape);

            Console.WriteLine("\n\nYou have been logged out.. Goodbye!");       // De MZZL!
            Thread.Sleep(500);
            Console.ResetColor();
        }

        private static void CheckMenuInput()
        {
            //if ((inputKey.Modifiers & ConsoleModifiers.Control) != 0) { Console.Write("Control+"); }

            if (inputKey.Key == ConsoleKey.L || !Login.validPassword & inputKey.Key != ConsoleKey.Escape)
            {
                _ = new Login();                               // _ discard unnecessary var declaration
            }

            else if (inputKey.Key == ConsoleKey.P)                                                 // Person
            {
                IO.DisplayMenu("Browse/edit People", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                BrowseRecords(filePeople, classSelect);
                //People();
            }

            else if (inputKey.Key == ConsoleKey.E & Login.validPassword)                           // Employees
            {
                IO.DisplayMenu("Browse/edit Employees", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                BrowseRecords(fileEmployees, classSelect);
                //EditEmployees();
            }

            else if (inputKey.Key == ConsoleKey.C & Login.validPassword)                           // Customers
            {
                IO.DisplayMenu("Browse/edit customer records", "(A)dd\nArrows to browse\n(Del)ete\n", Color.TextColors.MenuSelect);
                BrowseRecords(fileCustomers, classSelect);
            }

            else if (inputKey.Key == ConsoleKey.D & Login.validPassword)                           // Products
            {
                IO.DisplayMenu("Browse/edit product records", "(A)dd\nArrows to browse\n(Del)ete\n", Color.TextColors.MenuSelect);
                BrowseRecords(fileProducts, classSelect);
            }

            else if (inputKey.Key == ConsoleKey.M & Login.validPassword)                           // Master Data
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
            else if (inputKey.Key == ConsoleKey.F11) { Color.SetStandardColor();            }      // Set/Reset to standard color scheme
            else if (inputKey.Key == ConsoleKey.F12) { Color.SaveColors();                  }      // 
            
            else if (inputKey.Key == ConsoleKey.A)                                                 // Assembly info via recursion (test routine)
            
            {
                //IO.DisplayMenu("Browse/edit product records", "(A)dd\nArrows to browse\n(Del)ete\n", 2);
                ShowAssemblyInfo();
            }
            else if (inputKey.Key == ConsoleKey.Escape)                               // exit program
            {
                return;
            }
        }

        private static void ShowAssemblyInfo()                                          // test recursion routine 
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

        ////private static void BrowseRecords<P, E, Pr, C>(string aFilename, ClassSelect anEnum)  
        //                    where P  : Person 
        //                    where E  : Employee 
        //                    where Pr : Product
        //                    where C  : Customer
        
        private static void BrowseRecords(string aFilename, Enum classSelector)
        {

            var peopleList   = new List<Person>();
            var employeeList = new List<Employee> ();
            var customerList = new List<Customer> ();
            var productList  = new List<Product>();

            int cursorLeft   = Console.CursorLeft;                           // store current cursorposition, left and top
            int cursorTop    = Console.CursorTop;
            int recordIndex  = 1;
            int recordsInList= 0;

            string inputString = "abcdefghijklmnopqrstuvwxyz" + ConsoleKey.Backspace.ToString();

            switch (classSelector)

                {
                case ClassSelect.Person:
                    peopleList = IO.PopulateList<Person>(aFilename);
                    if (peopleList.Count > 0)
                    {
                        recordsInList = peopleList.Count;
                        Person.SetTotalRecords(recordsInList);
                        _ = new Person(peopleList[recordIndex - 1], true, false);
                        //Person.DisplayRecord(peopleList, recordIndex, false);
                    }
                    else
                    {
                        _ = new Person(peopleList[recordIndex - 1], true, true);
                        //Person.DisplayRecord(peopleList, recordIndex, false);
                    }
                    break;
                case ClassSelect.Employee:
                    employeeList = IO.PopulateList<Employee> (aFilename);
                    if (employeeList.Count > 0)
                    {
                        recordsInList = employeeList.Count;
                        Employee.SetTotalRecords(recordsInList);
                        Employee.DisplayRecord(employeeList, recordIndex, false);
                    } else
                    {
                        Employee.DisplayRecord(employeeList, recordIndex, false);
                    }
                    break;
                case ClassSelect.Customers:
                    customerList = IO.PopulateList<Customer> (aFilename);
                    if (customerList.Count > 0)
                    {
                        recordsInList = customerList.Count;
                        //Customer.SetTotalRecords(maxRecords);
                        //Customer.DisplayRecord(peopleList, recordIndex, false);
                    } else
                    {
                        //Customer.DisplayRecord(peopleList, recordIndex, false);
                    }
                    break;
                case ClassSelect.Products:
                    productList  = IO.PopulateList<Product>(aFilename);
                    if (productList.Count > 0)
                    {
                        recordsInList = productList.Count;
                        //Products.SetTotalRecords(maxRecords);
                        //Products.DisplayRecord(peopleList, recordIndex, false);
                    }
                    else
                    {
                        //Products.DisplayRecord(peopleList, recordIndex, false);
                    }
                    break;
                }

            UpdateTotalRecordsOnScreen(recordsInList);
            //string inputString = "abcdefghijklmnopqrstuvwxyz" + ConsoleKey.Backspace.ToString();
            StringBuilder zoekstring = new StringBuilder();

            do                                                                              // NICE: figure out generic calling of methods via a generic
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.Enter:                                                  // edit current record being displayed

                        if (recordsInList > 0 & Person.CheckIfActive(peopleList, recordIndex)) // some record is being displayed
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop + 1);           // set cursor on first inputfield after ID

                            switch (classSelector)
                            {
                                case ClassSelect.Person:
                                    peopleList.Insert(recordIndex - 1,                      // insert record at current position list
                                         new Person(peopleList[recordIndex - 1],false, false));  // (recordindex starts @ 1, list index @ 0
                                    peopleList.RemoveAt(recordIndex);                       // remove next entry (this was the old record)
                                    IO.WriteToFile(aFilename, peopleList);                  // write to file , #records unchanged
                                    Console.SetCursorPosition(cursorLeft, cursorTop);       // cursor back to top
                                    _ = new Person(peopleList[recordIndex - 1], true, false);
                                    //Person.DisplayRecord(peopleList, recordIndex, false);   // refresh record for updated employeeID and age
                                    break;
                                case ClassSelect.Employee:
                                    break;
                                case ClassSelect.Customers:
                                    break;
                                case ClassSelect.Products:
                                    break;
                            }
                            IO.SystemMessage("Record has been updated in file", false);
                        }
                        break;

                    case ConsoleKey.Insert:                                                 // add new record
                        
                        recordsInList++;                                                    // increase number of records
                        Console.SetCursorPosition(cursorLeft, cursorTop);

                        switch (classSelector)
                        {
                            case ClassSelect.Person:

                                _ = new Person(peopleList[recordIndex - 1], true, true);
                                //Person.DisplayRecord(peopleList, recordIndex, true);        // clear inputform
                                Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                                peopleList.Add(new Person());                               // call standard constructor
                                Person.SetTotalRecords(recordsInList);                      // update records in class static
                                Console.SetCursorPosition(cursorLeft, cursorTop);
                                _ = new Person(peopleList[recordIndex - 1], true, false);
                                //Person.DisplayRecord(peopleList, recordIndex, false);       // refresh record on for updated employeeID and age
                                IO.WriteToFile(aFilename, peopleList);                      // write to JSON file
                                break;

                            case ClassSelect.Employee:
                                break;
                            case ClassSelect.Customers:
                                break;
                            case ClassSelect.Products:
                                break;
                        }

                        recordIndex = recordsInList;                                           // set recordindex to last
                        UpdateTotalRecordsOnScreen(recordsInList);
                        IO.SystemMessage("Record has been written to file", false);

                        break;

                    case ConsoleKey.Delete:                                                 // mark record for deletion
                        if (recordsInList > 0)
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                           
                            switch (classSelector)
                            {
                                case ClassSelect.Person:
                                    Person.ToggleDeletionFlag(peopleList, recordIndex);     // toggle .Active property
                                    _ = new Person(peopleList[recordIndex - 1], true, false);
                                    //Person.DisplayRecord(peopleList, recordIndex, false);    // refresh record
                                    break;
                                case ClassSelect.Employee:
                                    break;
                                case ClassSelect.Customers:
                                    break;
                                case ClassSelect.Products:
                                    break;
                            }

                            if (Person.CheckIfActive(peopleList, recordIndex))
                            {
                                IO.SystemMessage("Record has been set to Active", false);
                            }
                            else
                            {
                                IO.SystemMessage("Record has been marked for Deletion", false);
                            }
                        }
                        break;

                    case ConsoleKey.LeftArrow:                                              // browse to previous
                    case ConsoleKey.UpArrow:

                        if (recordIndex > 1)
                        {
                            recordIndex--;
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            switch (classSelector)
                            {
                                case ClassSelect.Person:
                                    _ = new Person(peopleList[recordIndex - 1], true, false);
                                    //Person.DisplayRecord(peopleList, recordIndex, false);           
                                    break;
                                case ClassSelect.Employee:
                                    break;
                                case ClassSelect.Customers:
                                    break;
                                case ClassSelect.Products:
                                    break;
                            }
                            
                        }
                        break;

                    case ConsoleKey.RightArrow:                                             //browse
                    case ConsoleKey.DownArrow:

                        if (recordIndex < recordsInList)                                       // while not EoF
                        {
                            recordIndex++;
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            
                            switch (classSelector) 
                            {
                                case ClassSelect.Person:
                                    _ = new Person(peopleList[recordIndex - 1], true, false);
                                    //Person.DisplayRecord(peopleList, recordIndex, false);
                                    break;
                                case ClassSelect.Employee:
                                    
                                    break;
                                case ClassSelect.Customers:
                                    
                                    break;
                                case ClassSelect.Products:
                                    
                                    break;
                            }
                        }

                        break;

                    default:                                                                // search

                        switch (classSelector)
                        {
                            case ClassSelect.Person:
                                recordIndex = SearchStringInList(peopleList, cursorLeft, cursorTop, recordIndex, zoekstring);
                                break;
                            case ClassSelect.Employee:
                                
                                break;
                            case ClassSelect.Customers:
                                
                                break;
                            case ClassSelect.Products:
                                
                                break;
                        }

                        break;
                }
            } while (inputKey.Key != ConsoleKey.Home);
        }


        //void selectDisplayRecord(ClassSelect ds)
        //    {

        //    }

           
        //}

        //private static void People()
        //{
        //    var peopleList = IO.PopulateList<Person>(filePeople);          // remark: reading entire file into list, probably want an indexfile IRL
        //    int cursorLeft = Console.CursorLeft;                           // store current cursorposition, left and top
        //    int cursorTop = Console.CursorTop;
        //    int recordIndex;
        //    int maxRecords;

        //    if (peopleList.Count > 0)
        //    {
        //        maxRecords = peopleList.Count;                              // not necessary, just use static class attribute totalRecords directly
        //        recordIndex = 1;

        //        Person.SetTotalRecords(maxRecords);
        //        Person.DisplayRecord(peopleList, recordIndex, false);
        //    }
        //    else
        //    {
        //        maxRecords = 0;
        //        recordIndex = 1;
        //        Person.DisplayRecord(peopleList, recordIndex, true);        // display a clear form
        //    }

        //    UpdateTotalRecordsOnScreen(maxRecords);
        //    string inputString = "abcdefghijklmnopqrstuvwxyz" + ConsoleKey.Backspace.ToString();
        //    StringBuilder zoekstring = new StringBuilder();

        //    do                                             // TODO: cleanup and generalize
        //    {
        //        inputKey = Console.ReadKey(true);

        //        switch (inputKey.Key)
        //        {
        //            case ConsoleKey.Enter:                                                  // edit current record being displayed

        //                if (maxRecords > 0 & Person.CheckIfActive(peopleList, recordIndex)) // some record is being displayed
        //                {
        //                    Console.SetCursorPosition(cursorLeft, cursorTop + 1);           // set cursor on first inputfield after ID
        //                    peopleList.Insert(recordIndex - 1, new Person(peopleList[recordIndex - 1]));
        //                    peopleList.RemoveAt(recordIndex);
        //                    IO.WriteToFile(filePeople, peopleList);                         // write to file

        //                    Console.SetCursorPosition(cursorLeft, cursorTop);               // cursor back to top

        //                    Person.DisplayRecord(peopleList, recordIndex, false);           // refresh record for updated employeeID and age

        //                    IO.SystemMessage("Record has been updated in file", false);
        //                }
        //                break;

        //            case ConsoleKey.Insert:                                                 // add new record

        //                Console.SetCursorPosition(cursorLeft, cursorTop);
        //                Person.DisplayRecord(peopleList, recordIndex, true);                // clear inputform

        //                Console.SetCursorPosition(cursorLeft, cursorTop + 1);

        //                peopleList.Add(new Person());                                       // call standard constructor
        //                maxRecords++;                                                       // increase number of records
        //                recordIndex = maxRecords;                                           // set recordindex to last record;
        //                Person.SetTotalRecords(maxRecords);                                 // update records in class

        //                UpdateTotalRecordsOnScreen(maxRecords);

        //                Console.SetCursorPosition(cursorLeft, cursorTop);
        //                Person.DisplayRecord(peopleList, recordIndex, false);               // refresh record on for updated employeeID and age
        //                IO.WriteToFile(filePeople, peopleList);                             // write to JSON file
        //                                                                                    //

        //                IO.SystemMessage("Record has been written to file", false);

        //                break;

        //            case ConsoleKey.Delete:                                                 // mark record for deletion
        //                if (maxRecords > 0)
        //                {
        //                    Person.ToggleDeletionFlag(peopleList, recordIndex);             // toggle .Active property
        //                    Console.SetCursorPosition(cursorLeft, cursorTop);
        //                    Person.DisplayRecord(peopleList, recordIndex, false);           // refresh record
        //                    if (Person.CheckIfActive(peopleList, recordIndex))
        //                    {
        //                        IO.SystemMessage("Record has been set to Active", false);
        //                    }
        //                    else
        //                    {
        //                        IO.SystemMessage("Record has been marked for Deletion", false);
        //                    }
        //                }
        //                break;

        //            case ConsoleKey.LeftArrow:                                              // browse to previous
        //            case ConsoleKey.UpArrow:

        //                if (recordIndex > 1)
        //                {
        //                    recordIndex--;
        //                    Console.SetCursorPosition(cursorLeft, cursorTop);
        //                    Person.DisplayRecord(peopleList, recordIndex, false);           // display
        //                }
        //                break;

        //            case ConsoleKey.RightArrow:                                             //browse
        //            case ConsoleKey.DownArrow:

        //                if (recordIndex < maxRecords)                                       // while not EoF
        //                {
        //                    recordIndex++;
        //                    Console.SetCursorPosition(cursorLeft, cursorTop);
        //                    Person.DisplayRecord(peopleList, recordIndex, false);
        //                }

        //                break;

        //            default:                                                                // search

        //                recordIndex = SearchStringInList(peopleList, cursorLeft, cursorTop, recordIndex, zoekstring);

        //                break;
        //        }
        //    } while (inputKey.Key != ConsoleKey.Home);
        //}

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
                    _ = new Person(peopleList[recordIndex - 1], true, false);
                    //Person.DisplayRecord(peopleList, personSearchResult.RecordCounter, false);
                    recordIndex = personSearchResult.RecordCounter;
                }
                else
                {
                    zoekstring.Clear();
                }
            }

            return recordIndex;
        }

        private static void UpdateTotalRecordsOnScreen(int numberOfRecords)                  // NICE: display inactive records as well
        {
            IO.PrintOnConsole("[" + numberOfRecords.ToString() + "] active records\t", 30, 5, Color.TextColors.MenuSelect);
        }

        private static void Customers()
        {
            Customer newCustomer = new Customer();
        }
    }
}