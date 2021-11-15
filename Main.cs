using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;


namespace BakeryConsole
{
    internal enum ClassSelect
    {
        Person,
        Employee,
        Customers,
        Product
    }

    internal class Program
    {
        public static bool _debugEnabled { get; set; }

        // init object references
        private static ConsoleKeyInfo inputKey      = new ConsoleKeyInfo();
        public static ClassSelect classSelect       = new ClassSelect();

        // declare variables
        private static string validation            = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ " + ConsoleKey.Backspace.ToString();
        private static string compareString         = "";
        public static string  filePeople            = "people.json";
        private static string fileCustomers         = "customers.json";
        private static string fileEmployeeRoles     = "employeeRoles.json";
        private static string fileEmployees         = "employees.json";
        private static string fileProducts          = "products.json";
        public static string  licenseString         = "Royal Vlaaienboer Inc.";
        public static string  buildVersion          = "0.18";          //  NICE: use assemblyversion attribute
        public static int     windowHeight          = 35;
        public static int     windowWidth           = 80;
        public static int     warningLenghtDefault  = 1000;              // displaytime in ms for system messages

        private static void Main()
        {
            // Control - C interrupt handling
            Console.TreatControlCAsInput = true;                     // ConsoleCancel Eventhandler toggle;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(HandleCTRLC);  //set custom eventhandler

            // init console window properties
            // Console.SetWindowPosition(11, 9);                     //TODO: figure out SetWindowsPosition

            // initialize variables and window
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(windowWidth, windowHeight);
            IO.SetWarningLength(warningLenghtDefault);
            Console.Title = "Bakery for Console v" + buildVersion;
            Console.CursorSize = 60;
            Color.InitializeColors();                               // read settings.json and set color scheme
            Console.Clear();

            // Main Menu Loop
            do
            {
                IO.DisplayMenu("Main Menu", "(L)ogin\n(P)eople\n(E)mployees\n(C)ustomers\nPro(D)ucts\n(M)asterdata\n\n(F3-F10) change colors, (F11) reset (F12) save\n\nEnter your choice, Escape to Exit program\n\n", Color.TextColors.MenuSelect);
                DebugMessage("Debug Mode is ON");
                inputKey = Console.ReadKey(true);                               // 'true' | dont'display the input on the console
                CheckMenuInput();
                RecordManager.ResetRecordCounter();                             // Reset counter to 0 to when switching classes
            } while (inputKey.Key != ConsoleKey.Escape);

            Console.WriteLine("\n\nYou have been logged out.. Goodbye!");       // De MZZL!
            Thread.Sleep(500);
            Console.ResetColor();
        }

        private static void HandleCTRLC(object sender, ConsoleCancelEventArgs args)     // custom Control-C eventhandler
        {
            IO.SystemMessage("Control-C is pressed, returning to main menu", false);
            args.Cancel = true;
            return;
        }

        [Conditional("DEBUG")]                                                  // Only executed with DEBUG configuration Build && _debugEnabled
        private static void DebugMessage(string aString)                        // using System.Diagnostics;
        {
            //bool a = new Program()._debugEnabled;
            if (_debugEnabled) IO.SystemMessage(aString, false);
        }

        private static void CheckMenuInput()
        {
            //if ((inputKey.Modifiers & ConsoleModifiers.Control) != 0) { Console.Write("Control+"); }

            if (inputKey.KeyChar == 126)                         // DEBUG mode; tilde key ascii 126, not the same as ConsoleKey.F15 (?)
            {
                _debugEnabled = _debugEnabled ? false : true;   // toggle static property
            }

            if (inputKey.Key == ConsoleKey.L || !Login.validPassword & inputKey.Key != ConsoleKey.Escape)
            {
                _ = new Login();                               // _ discard unnecessary var declaration
            }
            else if (inputKey.Key == ConsoleKey.P)                                                  // Person
            {
                IO.DisplayMenu("Browse/edit People", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                BrowseRecords(filePeople, ClassSelect.Person);                                      //People
            }
            else if (inputKey.Key == ConsoleKey.E & Login.validPassword)                            // Employees
            {
                IO.DisplayMenu("Browse/edit Employees", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                BrowseRecords(fileEmployees, ClassSelect.Employee);
                //EditEmployees();
            }
            else if (inputKey.Key == ConsoleKey.C & Login.validPassword)                            // Customers
            {
                IO.DisplayMenu("Browse / edit Customers", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                BrowseRecords(fileCustomers, ClassSelect.Customers);
            }
            else if (inputKey.Key == ConsoleKey.D & Login.validPassword)                            // Products
            {
                IO.DisplayMenu("Browse / edit Products", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                BrowseRecords(fileProducts, ClassSelect.Product);
            }
            else if (inputKey.Key == ConsoleKey.M & Login.validPassword)                            // Master Data
            {
                IO.DisplayMenu("Edit Master Data", "(A)dd\nArrows to browse\n(Del)ete\n", Color.TextColors.MenuSelect);
                Console.WriteLine("  You Pressed D");
            }
            else if (inputKey.Key == ConsoleKey.F3)  { Color.CycleColors(6, false); return; }       // input text color
            else if (inputKey.Key == ConsoleKey.F4)  { Color.CycleColors(0, false); return; }       // highlighted text color
            else if (inputKey.Key == ConsoleKey.F5)  { Color.CycleColors(1, false); return; }       // normal text
            else if (inputKey.Key == ConsoleKey.F6)  { Color.CycleColors(2, false); return; }       // background
            else if (inputKey.Key == ConsoleKey.F7)  { Color.CycleColors(3, false); return; }       // Menu select color
            else if (inputKey.Key == ConsoleKey.F8)  { Color.CycleColors(4, false); return; }       // Software license nameholder Color
            else if (inputKey.Key == ConsoleKey.F9)  { Color.CycleColors(5, true); return; }        // Random colors including background
            else if (inputKey.Key == ConsoleKey.F10) { Color.CycleColors(5, false); return; }       // Random colors excluding background
            else if (inputKey.Key == ConsoleKey.F11) { Color.SetStandardColor(); }                  // Set/Reset to standard color scheme
            
            else if (inputKey.Key == ConsoleKey.F12) { Color.SaveColors(); }                        //
            
            else if (inputKey.Key == ConsoleKey.A)                                                  // Assembly info via recursion (test routine)
            {
                ShowAssemblyInfo();
            }
            else if (inputKey.Key == ConsoleKey.Escape)                                             // exit program
            {
                return;
            }
        }

        private static void ShowAssemblyInfo()                                                      // test recursion routine
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

        private static void BrowseRecords(string aFilename, Enum classSelector)                     // Main record handling routine /TODO: a lot of refactoring
        {
            var peopleList = new List<Person>();
            var employeeList = new List<Employee>();
            var customerList = new List<Customer>();
            var productList = new List<Product>();

            int cursorLeft = Console.CursorLeft;                                                    // store current cursorposition, left and top
            int cursorTop = Console.CursorTop;
            int recordIndex = 1;
            int recordsInList = 0;

            switch (classSelector)

            {
                case ClassSelect.Person:
                    peopleList = IO.PopulateList<Person>(aFilename);                                // try to read JSON file to list
                    if (peopleList.Count > 0)                                                       // succeeded, there are records
                    {
                        recordsInList = peopleList.Count;
                        Person.SetTotalRecords(recordsInList);
                        _ = new Person(peopleList[recordIndex - 1], true);                          //display first record
                    }
                    else
                    {
                        _ = new Person(true);                                                       // display empty input form
                    }
                    break;

                case ClassSelect.Employee:
                    employeeList = IO.PopulateList<Employee>(aFilename);
                    if (employeeList.Count > 0)
                    {
                        recordsInList = employeeList.Count;
                        Employee.SetTotalRecords(recordsInList);
                        _ = new Employee(employeeList[recordIndex - 1], true);
                    }
                    else
                    {
                        _ = new Employee(true);
                    }
                    break;

                case ClassSelect.Customers:
                    customerList = IO.PopulateList<Customer>(aFilename);
                    if (customerList.Count > 0)
                    {
                        recordsInList = customerList.Count;
                        Customer.SetTotalRecords(recordsInList);
                        _ = new Customer(customerList[recordIndex - 1], true);
                    }
                    else
                    {
                        _ = new Customer(true);
                    }
                    break;

                case ClassSelect.Product:
                    productList = IO.PopulateList<Product>(aFilename);
                    if (productList.Count > 0)
                    {
                        recordsInList = productList.Count;
                        Product.SetTotalRecords(recordsInList);
                        _ = new Product(productList[recordIndex - 1], true);
                    }
                    else
                    {
                        _ = new Product(true);
                    }
                    break;
            }

            UpdateTotalRecordsOnScreen(recordsInList);
            //string inputString = "abcdefghijklmnopqrstuvwxyz" + ConsoleKey.Backspace.ToString();
            StringBuilder zoekString = new StringBuilder();

            do                                                                                      // NICE: figure out generic calling of methods via a generic
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
/*** EDIT **/           case ConsoleKey.Enter:          

                        if (recordsInList > 0) // & Person.CheckIfActive(peopleList[recordIndex - 1], recordIndex)) // some record is being displayed
                        {
                            // set cursor on first inputfield after ID

                            switch (classSelector)      //TODO: fix check on active record
                            {
                                case ClassSelect.Person:
                                    if (peopleList[recordIndex - 1].Active)
                                    {
                                        IO.SetCursorPosition(cursorLeft, cursorTop + 1);
                                        peopleList.Insert(recordIndex - 1,                     // insert record at current position list
                                             new Person(peopleList[recordIndex - 1], false));  // (recordindex starts @ 1, list index @ 0
                                        peopleList.RemoveAt(recordIndex);                      // remove next entry (this was the old record)
                                        IO.WriteToFile(aFilename, peopleList, "");             // write to file , #records unchanged
                                        IO.SetCursorPosition(cursorLeft, cursorTop);
                                        _ = new Person(peopleList[recordIndex - 1], true);     // refresh record on screen
                                    }
                                    break;

                                case ClassSelect.Employee:
                                    if (employeeList[recordIndex - 1].Active)
                                    {
                                        IO.SetCursorPosition(cursorLeft, cursorTop + 1);
                                        employeeList.Insert(recordIndex - 1,
                                            new Employee(employeeList[recordIndex - 1], false));
                                        employeeList.RemoveAt(recordIndex);
                                        IO.WriteToFile(aFilename, employeeList, "");
                                        IO.SetCursorPosition(cursorLeft, cursorTop);
                                        _ = new Employee(employeeList[recordIndex - 1], true);
                                    }
                                    break;

                                case ClassSelect.Customers:
                                    if (customerList[recordIndex - 1].Active)
                                    {
                                        IO.SetCursorPosition(cursorLeft, cursorTop + 1);
                                        customerList.Insert(recordIndex - 1,
                                            new Customer(customerList[recordIndex - 1], false));
                                        customerList.RemoveAt(recordIndex);
                                        IO.WriteToFile(aFilename, customerList, "");
                                        IO.SetCursorPosition(cursorLeft, cursorTop);
                                        _ = new Customer(customerList[recordIndex - 1], true);
                                    }
                                    break;

                                case ClassSelect.Product:

                                    if (productList[recordIndex - 1].Active)
                                    {
                                        IO.SetCursorPosition(cursorLeft, cursorTop + 1);
                                        productList.Insert(recordIndex - 1,
                                            new Product(productList[recordIndex - 1], false));
                                        productList.RemoveAt(recordIndex);
                                        IO.WriteToFile(aFilename, productList, "");
                                        IO.SetCursorPosition(cursorLeft, cursorTop);
                                        _ = new Product(productList[recordIndex - 1], true);
                                    }
                                    break;
                            }
                        }
                        break;

                    case ConsoleKey.I:
                    case ConsoleKey.Insert:
                        
                        IO.SetCursorPosition(cursorLeft, cursorTop);

/*** ADD ***/           switch (classSelector)                  

                        {
                            case ClassSelect.Person:

                                _ = new Person(true);                                       // clear inputform  TODO: unnecessary when recordsInList = 0
                                IO.SetCursorPosition(cursorLeft, cursorTop + 1);
                                peopleList.Add(new Person());                               // call standard constructor
                                recordsInList++; recordIndex = recordsInList;
                                Console.SetCursorPosition(cursorLeft, cursorTop);
                                _ = new Person(peopleList[recordIndex - 1], true);          // display next record
                                IO.WriteToFile(aFilename, peopleList, "");                  // write to JSON file
                                break;

                            case ClassSelect.Employee:

                                _ = new Employee(true);
                                IO.SetCursorPosition(cursorLeft, cursorTop + 1);
                                employeeList.Add(new Employee());                             // call standard constructor
                                recordsInList++; recordIndex = recordsInList;
                                Console.SetCursorPosition(cursorLeft, cursorTop);
                                _ = new Employee(employeeList[recordIndex - 1], true);
                                IO.WriteToFile(aFilename, employeeList, "");                  // write to JSON file

                                break;

                            case ClassSelect.Customers:

                                _ = new Customer(true);
                                IO.SetCursorPosition(cursorLeft, cursorTop + 1);
                                customerList.Add(new Customer());                             // call standard constructor
                                recordsInList++; recordIndex = recordsInList;
                                Console.SetCursorPosition(cursorLeft, cursorTop);
                                _ = new Customer(customerList[recordIndex - 1], true);        // refresh record on screen
                                IO.WriteToFile(aFilename, customerList, "");                  // write to JSON file

                                break;

                            case ClassSelect.Product:

                                _ = new Product(true);
                                IO.SetCursorPosition(cursorLeft, cursorTop + 1);
                                productList.Add(new Product());                               // call standard constructor
                                recordsInList++; recordIndex = recordsInList;
                                Console.SetCursorPosition(cursorLeft, cursorTop);
                                _ = new Product(productList[recordIndex - 1], true);
                                IO.WriteToFile(aFilename, productList, "");                   // write to JSON file
                                break;
                        }

                        UpdateTotalRecordsOnScreen(recordsInList);
                        IO.SystemMessage("Record has been written to file", false);

                        break;

/*** DELETE ***/        case ConsoleKey.Delete:                     
                        if (recordsInList > 0)
                        {
                            IO.SetCursorPosition(cursorLeft, cursorTop);

                            switch (classSelector)
                            {
                                case ClassSelect.Person:

                                    Person.ToggleDeletionFlag(peopleList[recordIndex - 1], recordIndex);     // toggle .Active property
                                    _ = new Person(peopleList[recordIndex - 1], true);
                                    IO.WriteToFile(aFilename, peopleList, "");
                                    break;

                                case ClassSelect.Employee:

                                    Employee.ToggleDeletionFlag(employeeList[recordIndex - 1], recordIndex);
                                    _ = new Employee(employeeList[recordIndex - 1], true);
                                    IO.WriteToFile(aFilename, employeeList, "");
                                    break;

                                case ClassSelect.Customers:
                                    Customer.ToggleDeletionFlag(customerList[recordIndex - 1], recordIndex);
                                    _ = new Customer(customerList[recordIndex - 1], true);
                                    IO.WriteToFile(aFilename, customerList, "");

                                    break;

                                case ClassSelect.Product:
                                    Product.ToggleDeletionFlag(productList[recordIndex - 1], recordIndex);
                                    _ = new Product(productList[recordIndex - 1], true);
                                    IO.WriteToFile(aFilename, productList, "");

                                    break;
                            }
                        }
                        break;

/* BROWSE */        case ConsoleKey.LeftArrow:                                             
                    case ConsoleKey.UpArrow:

                        if (recordIndex > 1)
                        {
                            recordIndex--;
                            IO.SetCursorPosition(cursorLeft, cursorTop);
                            switch (classSelector)
                            {
                                case ClassSelect.Person:
                                    _ = new Person(peopleList[recordIndex - 1], true);
                                    break;

                                case ClassSelect.Employee:
                                    _ = new Employee(employeeList[recordIndex - 1], true);
                                    break;

                                case ClassSelect.Customers:
                                    _ = new Customer(customerList[recordIndex - 1], true);
                                    break;

                                case ClassSelect.Product:
                                    _ = new Product(productList[recordIndex - 1], true);
                                    break;
                            }
                        }
                        break;

/* BROWSE */        case ConsoleKey.RightArrow:                                             
                    case ConsoleKey.DownArrow:

                        if (recordIndex < recordsInList)                                    // while not EoF
                        {
                            recordIndex++;
                            IO.SetCursorPosition(cursorLeft, cursorTop);

                            switch (classSelector)
                            {
                                case ClassSelect.Person:
                                    _ = new Person(peopleList[recordIndex - 1], true);
                                    break;

                                case ClassSelect.Employee:
                                    _ = new Employee(employeeList[recordIndex - 1], true);
                                    break;

                                case ClassSelect.Customers:
                                    _ = new Customer(customerList[recordIndex - 1], true);
                                    break;

                                case ClassSelect.Product:
                                    _ = new Product(productList[recordIndex - 1], true);
                                    break;
                            }
                        }

                        break;

/* SEARCH */            default:                                                              
                                                                                            // NICE: sort option
                        switch (classSelector)                                              // NICE: search through entire record as 1 string
                        {
                            case ClassSelect.Person:

                                compareString = BuildZoekString(zoekString, cursorTop);

                                Person foundPerson = peopleList.Find(person =>
                                                     person.LastName.StartsWith(compareString));

                                if (foundPerson != null)
                                {
                                    recordIndex = foundPerson.RecordCounter;
                                    IO.SetCursorPosition(cursorLeft, cursorTop);
                                    _ = new Person(foundPerson, true);
                                }
                                else
                                {
                                    zoekString.Clear();
                                }

                                break;

                            case ClassSelect.Employee:
                                compareString = BuildZoekString(zoekString, cursorTop);

                                Employee foundEmployee = employeeList.Find(employee =>
                                                     employee.LastName.StartsWith(compareString));

                                if (foundEmployee != null)
                                {
                                    recordIndex = foundEmployee.RecordCounter;
                                    IO.SetCursorPosition(cursorLeft, cursorTop);
                                    _ = new Employee(foundEmployee, true);
                                }
                                else
                                {
                                    zoekString.Clear();
                                }
                                break;

                            case ClassSelect.Customers:
                                compareString = BuildZoekString(zoekString, cursorTop);

                                Customer foundCustomer = customerList.Find(customer =>
          /*!*/                                          customer.Name.StartsWith(compareString));

                                if (foundCustomer != null)
                                {
                                    recordIndex = foundCustomer.RecordCounter;
                                    IO.SetCursorPosition(cursorLeft, cursorTop);
                                    _ = new Customer(foundCustomer, true);
                                }
                                else
                                {
                                    zoekString.Clear();
                                }

                                break;

                            case ClassSelect.Product:
                                compareString = BuildZoekString(zoekString, cursorTop);

                                Product foundProduct = productList.Find(product =>
                                                     product.Name.StartsWith(compareString));

                                if (foundProduct != null)
                                {
                                    recordIndex = foundProduct.RecordCounter;
                                    IO.SetCursorPosition(cursorLeft, cursorTop);
                                    _ = new Product(foundProduct, true);
                                }
                                else
                                {
                                    zoekString.Clear();
                                }

                                break;
                        }

                        break;
                }

                if (Debugger.IsAttached)
                {
                    IO.PrintOnConsole("Recordindex: " + recordIndex, 0, 0, Color.TextColors.Defaults);
                }
            } while (inputKey.Key != ConsoleKey.Home & inputKey.Key != ConsoleKey.Q);
        }

        private static string BuildZoekString(StringBuilder zoekstring, int cursorTop)
        {
            string returnString;

            if (validation.ToString().Contains(inputKey.KeyChar.ToString()))
            {
                zoekstring.Append(inputKey.KeyChar.ToString());
            }

            IO.PrintOnConsole("Searching: [ " + zoekstring.ToString() + " ]".PadRight(20, ' '), 1, cursorTop - 1, Color.TextColors.Defaults);
            returnString = zoekstring.ToString();
            return returnString;
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
                );                                                                          // I hate how this syntax looks
                if (personSearchResult != null)
                {
                    IO.SetCursorPosition(cursorLeft, cursorTop);
                    _ = new Person(peopleList[recordIndex - 1], true);
                    recordIndex = personSearchResult.RecordCounter;
                }
                else
                {
                    zoekstring.Clear();
                }
            }

            return recordIndex;
        }

        private static void UpdateTotalRecordsOnScreen(int numberOfRecords)                 // NICE: display inactive records as well
        {
            IO.PrintOnConsole("[" + numberOfRecords.ToString() + "] records in file", 30, 5, Color.TextColors.MenuSelect);
        }
     }
}