using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace BakeryConsole
{

    enum ClassSelect
    {
        Person,
        Employee,
        Customers,
        Product
    }

    internal class Program
    {
        // declare variables
        private static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();
        private static string validation        = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ " + ConsoleKey.Backspace.ToString();
        private static string compareString     = "";
        public  static string filePeople        = "people.json";
        private static string fileCustomers     = "customers.json";
        private static string fileEmployeeRoles = "employeeRoles.json";
        private static string fileEmployees     = "employees.json";
        private static string fileProducts      = "products.json";

        public static int windowHeight          =  36;
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
                //var emp = new Employee();
                //Console.ReadKey();

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
                BrowseRecords(filePeople, ClassSelect.Person);
                //People();
            }

            else if (inputKey.Key == ConsoleKey.E & Login.validPassword)                           // Employees
            {
                IO.DisplayMenu("Browse/edit Employees", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                BrowseRecords(fileEmployees, ClassSelect.Employee);
                //EditEmployees();
            }

            else if (inputKey.Key == ConsoleKey.C & Login.validPassword)                           // Customers
            {
                IO.DisplayMenu("Browse/edit customer records", "(A)dd\nArrows to browse\n(Del)ete\n", Color.TextColors.MenuSelect);
                BrowseRecords(fileCustomers, ClassSelect.Customers);
            }

            else if (inputKey.Key == ConsoleKey.D & Login.validPassword)                           // Products
            {
                IO.DisplayMenu("Browse/edit product records", "(A)dd\nArrows to browse\n(Del)ete\n", Color.TextColors.MenuSelect);
                BrowseRecords(fileProducts, ClassSelect.Product);
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

            var peopleList    = new List<Person> ();
            var employeeList  = new List<Employee> ();
            var customerList  = new List<Customer> ();
            var productList   = new List<Product>();

            int cursorLeft    = Console.CursorLeft;                           // store current cursorposition, left and top
            int cursorTop     = Console.CursorTop;
            int recordIndex   = 1;
            int recordsInList = 0;

            

            switch (classSelector)

                {
                case ClassSelect.Person:
                    peopleList = IO.PopulateList<Person>(aFilename);
                    if (peopleList.Count > 0)
                    {
                        recordsInList = peopleList.Count;
                        Person.SetTotalRecords(recordsInList);
                        _ = new Person(peopleList[recordIndex - 1], true, false);
                    }
                    else
                    {
                        _ = new Person(true);                   // display empty input form
                    }
                    break;

                case ClassSelect.Employee:
                    employeeList = IO.PopulateList<Employee> (aFilename);
                    if (employeeList.Count > 0)
                    {
                        recordsInList = employeeList.Count;
                        Employee.SetTotalRecords(recordsInList);
                        _ = new Employee(employeeList[recordIndex - 1], true, false);
                    } else
                    {
                        _ = new Employee(true);
                    }
                    break;

                case ClassSelect.Customers:
                    customerList = IO.PopulateList<Customer> (aFilename);
                    if (customerList.Count > 0)
                    {
                       
                    } else
                    {
                        //Customer.DisplayRecord(peopleList, recordIndex, false);
                    }
                    break;

                case ClassSelect.Product:
                    productList  = IO.PopulateList<Product>(aFilename);
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
            
            do                                                                              // NICE: figure out generic calling of methods via a generic
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.Enter:          // *** EDIT Record *** //

                        if (recordsInList > 0) // & Person.CheckIfActive(peopleList[recordIndex - 1], recordIndex)) // some record is being displayed
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop + 1);           // set cursor on first inputfield after ID
                                
                            switch (classSelector)      //TODO: fix check on active record
                            {
                                case ClassSelect.Person:
                                    if (peopleList[recordIndex - 1].Active) 
                                    {
                                        peopleList.Insert(recordIndex - 1,                      // insert record at current position list
                                             new Person(peopleList[recordIndex - 1], false, false));  // (recordindex starts @ 1, list index @ 0
                                        peopleList.RemoveAt(recordIndex);                       // remove next entry (this was the old record)
                                        IO.WriteToFile(aFilename, peopleList);                  // write to file , #records unchanged
                                        Console.SetCursorPosition(cursorLeft, cursorTop);       // cursor back to top
                                        _ = new Person(peopleList[recordIndex - 1], true, false);
                                    }
                                    break;

                                case ClassSelect.Employee:
                                    if (employeeList[recordIndex - 1].Active)
                                    {
                                        employeeList.Insert(recordIndex - 1,
                                            new Employee(employeeList[recordIndex - 1], false, false));
                                        employeeList.RemoveAt(recordIndex);                       
                                        IO.WriteToFile(aFilename, employeeList);                  
                                        Console.SetCursorPosition(cursorLeft, cursorTop);       
                                        _ = new Employee(employeeList[recordIndex - 1], true, false);
                                    }
                                    break;

                                case ClassSelect.Customers:
                                    break;

                                case ClassSelect.Product:

                                    if (productList[recordIndex - 1].Active)
                                    {
                                        productList.Insert(recordIndex - 1,
                                            new Product(productList[recordIndex - 1], false));
                                        productList.RemoveAt(recordIndex);
                                        IO.WriteToFile(aFilename, productList);
                                        Console.SetCursorPosition(cursorLeft, cursorTop);
                                        _ = new Product(productList[recordIndex - 1], true);
                                    }
                                    break;
                            }

                            IO.SystemMessage("Record has been updated in file", false);
                        }
                        break;

                    case ConsoleKey.Insert:         // *** Add New Record *** //                                       

                                                              // increase number of records
                        Console.SetCursorPosition(cursorLeft, cursorTop);

                        switch (classSelector)
                        
                        {
                            case ClassSelect.Person:

                                _ = new Person(true);                                       // clear inputform
                                Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                                peopleList.Add(new Person());                               // call standard constructor
                                recordsInList++;
                                Person.SetTotalRecords(recordsInList);                      // update records in class static
                                Console.SetCursorPosition(cursorLeft, cursorTop);
                                _ = new Person(peopleList[recordIndex - 1], true, false);       // display next record (we update recordindex
                                IO.WriteToFile(aFilename, peopleList);                      // write to JSON file
                                break;

                            case ClassSelect.Employee:
                               
                                _ = new Employee(true);
                                Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                                employeeList.Add(new Employee());                               // call standard constructor
                                recordsInList++;
                                Employee.SetTotalRecords(recordsInList);                      // update records in class static
                                Console.SetCursorPosition(cursorLeft, cursorTop);
                                _ = new Employee(employeeList[recordIndex - 1], true, false);
                                IO.WriteToFile(aFilename, employeeList);                      // write to JSON file

                                break;

                            case ClassSelect.Customers:
                                break;

                            case ClassSelect.Product:
                                
                                _ = new Product(true);
                                Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                                productList.Add(new Product());                               // call standard constructor
                                recordsInList++;
                                Product.SetTotalRecords(recordsInList);                      // update records in class static
                                Console.SetCursorPosition(cursorLeft, cursorTop);
                                _ = new Product(productList[recordIndex - 1], true);
                                IO.WriteToFile(aFilename, productList);                      // write to JSON file
                                break;

                                
                        }
                        recordIndex = recordsInList;                                                               // set recordindex to last
                        UpdateTotalRecordsOnScreen(recordsInList);
                        IO.SystemMessage("Record has been written to file", false);

                        break;

                    case ConsoleKey.Delete:         // *** DELETE Record *** //                      
                        if (recordsInList > 0)
                        {
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                           
                            switch (classSelector)
                            {
                                case ClassSelect.Person:

                                    Person.ToggleDeletionFlag(peopleList[recordIndex - 1], recordIndex);     // toggle .Active property
                                    _ = new Person(peopleList[recordIndex - 1], true, false);
                                    IO.WriteToFile(aFilename, peopleList);
                                    break;
                                case ClassSelect.Employee:

                                    Employee.ToggleDeletionFlag(employeeList[recordIndex - 1], recordIndex);    
                                    _ = new Employee(employeeList[recordIndex - 1], true, false);
                                    IO.WriteToFile(aFilename, employeeList);
                                    break;

                                case ClassSelect.Customers:
                                    break;

                                case ClassSelect.Product:
                                    Product.ToggleDeletionFlag(productList[recordIndex - 1], recordIndex);
                                    _ = new Product(productList[recordIndex - 1], true);
                                    IO.WriteToFile(aFilename, productList);

                                    break;
                            }

                            //if (Person.CheckIfActive(peopleList, recordIndex))
                            //{
                            //    IO.SystemMessage("Record has been set to Active", false);
                            //}
                            //else
                            //{
                            //    IO.SystemMessage("Record has been marked for Deletion", false);
                            //}
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
                                    _ = new Employee(employeeList[recordIndex - 1], true, false);
                                    break;
                                case ClassSelect.Customers:
                                    break;
                                case ClassSelect.Product:
                                    _ = new Product(productList[recordIndex - 1], true);
                                    break;
                            }
                            
                        }
                        break;

                    case ConsoleKey.RightArrow:                                             //browse
                    case ConsoleKey.DownArrow:

                        if (recordIndex < recordsInList)                                    // while not EoF
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
                                    _ = new Employee(employeeList[recordIndex - 1], true, false);
                                    break;
                                case ClassSelect.Customers:
                                    
                                    break;
                                case ClassSelect.Product:
                                    _ = new Product(productList[recordIndex - 1], true);
                                    break;
                            }
                        }

                        break;

                    default:                                                                // search  
                                                                                            // NICE: sort option
                        switch (classSelector)                                              // NICE: search through entire record as 1 string 
                        {
                            case ClassSelect.Person:
                                //recordIndex = SearchStringInList(peopleList, cursorLeft, cursorTop, recordIndex, zoekstring);

                                compareString = BuildZoekString(zoekString, cursorTop);

                                Person foundPerson = peopleList.Find(person =>
                                                     person.LastName.StartsWith(compareString));
                                
                                if (foundPerson != null)
                                {
                                    recordIndex = foundPerson.RecordCounter;
                                    Console.SetCursorPosition(cursorLeft, cursorTop);
                                    _ = new Person(foundPerson, true, false);
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
                                    Console.SetCursorPosition(cursorLeft, cursorTop);
                                    _ = new Employee(foundEmployee, true, false);
                                }
                                else
                                {
                                    zoekString.Clear();
                                }
                                //recordIndex = SearchStringInList(productList, cursorLeft, cursorTop, recordIndex, zoekstring);
                                break;
                            case ClassSelect.Customers:
                                
                                break;
                            case ClassSelect.Product:
                                compareString = BuildZoekString(zoekString, cursorTop);

                                Product foundProduct = productList.Find(product =>
                                                     product.Name.StartsWith(compareString));

                                if (foundProduct != null)
                                {
                                    recordIndex = foundProduct.RecordCounter;
                                    Console.SetCursorPosition(cursorLeft, cursorTop);
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
            } while (inputKey.Key != ConsoleKey.Home);
        }

        private static string  BuildZoekString(StringBuilder zoekstring, int cursorTop) 
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
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    _ = new Person(peopleList[recordIndex - 1], true, false);
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

        private static void Customers()
        {
            Customer newCustomer = new Customer();
        }
    }
}