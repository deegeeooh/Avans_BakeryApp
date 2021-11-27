using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;


namespace BakeryConsole
{
    internal enum ClassSelect       // OBSOLETE
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
        private static string validation            = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 " + "\b";
        private static string compareString         = "";
        public  static string filePeople            = "people.json";
        private static string fileCustomers         = "customers.json";
        private static string fileEmployeeRoles     = "employee_roles.json";
        private static string fileProductTypes      = "product_types.json";
        private static string fileCustomerTypes     = "customer_types.json";
        private static string fileRelationTypes     = "relation_types.json";
        private static string fileEmployees         = "employees.json";
        private static string fileProducts          = "products.json";
        private static string fileUsers             = "users.json";
        public  static string licenseString         = "Royal Vlaaienboer Inc.";
        public  static string buildVersion          = "0.20";                   //  NICE: use assemblyversion attribute
        public  static int    windowHeight          = 35;
        public  static int    windowWidth           = 80;
        public  static int    warningLenghtDefault  = 1000;                     // displaytime in ms for system messages

        private static string[] fieldNames;
        private static int[,] fieldProperties;

        private static void Main()
        {
            // Control - C interrupt handling

            Console.TreatControlCAsInput = true;                     // ConsoleCancel Eventhandler toggle;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(HandleCTRLC);  //set custom eventhandler

            // initialize variables and window

            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(windowWidth, windowHeight);
            IO.SetWarningLength(warningLenghtDefault);
            Console.Title = "Bakery for Console v" + buildVersion;
            Console.CursorSize = 60;

            // read settings.json and set color scheme

            Color.InitializeColors();
            Console.Clear();

            // Main Menu Loop
            do
/*MAIN*/    {
                IO.DisplayMenu("Main Menu", "(L)ogin\n(P)eople\n(E)mployees\n(C)ustomers\nPro(D)ucts\n(M)asterdata\n\n(F3-F10) change colors, (F11) reset (F12) save\n\nEnter your choice, Escape to Exit program\n\n", Color.TextColors.MenuSelect);
                //DebugMessage("Debug Mode is ON");
                inputKey = Console.ReadKey(true);                               // 'true' : dont'display the input on the console
                CheckMenuInput();
                RecordManager.SetTotalRecords(0);                             // Reset counter to 0 to when switching classes

            } while (inputKey.Key != ConsoleKey.Escape);

            Console.WriteLine("\n\nYou have been logged out.. Goodbye!");       // De MZZL!
            Thread.Sleep(500);
            Console.ResetColor();
        }

        private static void HandleCTRLC( object sender, ConsoleCancelEventArgs args )     // custom Control-C eventhandler
        {
            IO.SystemMessage("Control-C is pressed, returning to main menu", false);
            args.Cancel = true;
            //return;
        }

        [Conditional("DEBUG")]                                                  // Only executed with DEBUG configuration Build && _debugEnabled
        private static void DebugMessage( string aString )                        // using System.Diagnostics;
        {
            //bool a = new Program()._debugEnabled;
            if (_debugEnabled) IO.SystemMessage(aString, false);
            Login.validPassword = true;
        }

        private static void CheckMenuInput()
        {
            //if ((inputKey.Modifiers & ConsoleModifiers.Control) != 0) { Console.Write("Control+"); }

            if (inputKey.KeyChar == 126)                         // DEBUG mode; tilde key ascii 126, not the same as ConsoleKey.F15 (?)
            {
                DebugMessage("Debug Mode is ON");
                _debugEnabled = _debugEnabled ? false : true;   // toggle static property
            }

            if (inputKey.Key == ConsoleKey.L || !Login.validPassword & inputKey.Key != ConsoleKey.Escape)
            {
                _ = new Login();                               // _ discard unnecessary var declaration
            }
            else if (inputKey.Key == ConsoleKey.P)                                                  // Person
            {
                IO.DisplayMenu("Browse/edit People", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse, type to search\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);

                var peepsList = IO.PopulateList<Person>(filePeople);
                HandleRecords<Person>(peepsList, filePeople);
                
            }
            else if (inputKey.Key == ConsoleKey.E & Login.validPassword)                            // Employees
            {
                IO.DisplayMenu("Browse/edit Employees", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse, type to search\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                
                var empList = IO.PopulateList<Employee>(fileEmployees);
                HandleRecords<Employee>(empList, fileEmployees);
                
            }
            else if (inputKey.Key == ConsoleKey.C & Login.validPassword)                            // Customers
            {
                IO.DisplayMenu("Browse / edit Customers", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse, type to search\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
                
                var customerList = IO.PopulateList<Customer>(fileCustomers);
                HandleRecords<Customer>(customerList, fileCustomers);
                
            }
            else if (inputKey.Key == ConsoleKey.D & Login.validPassword)                            // Products
            {
                IO.DisplayMenu("Browse / edit Products", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse, type to search\n(Home) Main menu\n\n", Color.TextColors.MenuSelect);
             
                var prodList = IO.PopulateList<Product>(fileProducts);
                HandleRecords<Product>(prodList, fileProducts);
            }
            else if (inputKey.Key == ConsoleKey.M & Login.validPassword)                            // Master Data 
            {
                do
                {
                    IO.DisplayMenu("Edit Master Data", "(E)mployee Roles\n(C)ustomer types\n(P)roduct types\n(R)elation Types\n(U)ser accounts\n(Home) Main menu\n", Color.TextColors.MenuSelect);
                    inputKey = Console.ReadKey(true);                               // 'true' : dont'display the input on the console
                    CheckMenuInputMasterData();
                    RecordManager.SetTotalRecords(0);

                } while (inputKey.Key != ConsoleKey.Home);

                CheckMenuInputMasterData();
            }

            else if (inputKey.Key == ConsoleKey.F3)  { Color.CycleColors(6, false); return; }       // input text color
            else if (inputKey.Key == ConsoleKey.F4)  { Color.CycleColors(0, false); return; }       // highlighted text color
            else if (inputKey.Key == ConsoleKey.F5)  { Color.CycleColors(1, false); return; }       // normal text
            else if (inputKey.Key == ConsoleKey.F6)  { Color.CycleColors(2, false); return; }       // background
            else if (inputKey.Key == ConsoleKey.F7)  { Color.CycleColors(3, false); return; }       // Menu select color
            else if (inputKey.Key == ConsoleKey.F8)  { Color.CycleColors(4, false); return; }       // Software license nameholder Color
            else if (inputKey.Key == ConsoleKey.F9)  { Color.CycleColors(5, true);  return; }       // random colors including background
            else if (inputKey.Key == ConsoleKey.F10) { Color.CycleColors(5, false); return; }       // random colors excluding background
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
        private static void CheckMenuInputMasterData ()
        {
            
            List<GenericDataClass> _browselist = new List<GenericDataClass>();
            IO.DisplayMenu("Browse / edit Employee Roles", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n(Home) Main Menu\n(End) Previous Menu\n\n", Color.TextColors.MenuSelect);

            switch (inputKey.Key)
            {
               
                case ConsoleKey.A:
                    break;
                case ConsoleKey.B:
                    break;
                case ConsoleKey.C:          // Customer Types

                    GenericDataClass.SetFieldNamesArray(fieldNames = new string[] { "Search code"," Branche code" }) ;
                    GenericDataClass.SetFieldPropertiesArray(fieldProperties = new int[,] { { 0, 6, 1 }, { 1, 3, 0 } });
                    GenericDataClass.SetNameFieldName("Customer type description");
                    _browselist = IO.PopulateList<GenericDataClass>(fileCustomerTypes);
                    HandleRecords<GenericDataClass>(_browselist, fileCustomerTypes);

                    break;
                case ConsoleKey.D:
                    break;
                case ConsoleKey.E:          // Employee roles
                    
                    GenericDataClass.SetFieldNamesArray(fieldNames = new string[] { "Search code","Salary Scale" });
                    GenericDataClass.SetFieldPropertiesArray(fieldProperties = new int[,] { { 0, 3, 1 }, { 1, 1, 0 } });
                    GenericDataClass.SetNameFieldName("Employee job description");
                    _browselist = IO.PopulateList<GenericDataClass>(fileEmployeeRoles);
                    HandleRecords<GenericDataClass>(_browselist, fileEmployeeRoles);

                    break;

                case ConsoleKey.R:         // Relation type types
                    
                    GenericDataClass.SetFieldNamesArray(fieldNames = new string[] { "Search code" });
                    GenericDataClass.SetFieldPropertiesArray(fieldProperties = new int[,] { { 0, 3, 1 } });
                    GenericDataClass.SetNameFieldName("Relation Type");
                    _browselist = IO.PopulateList<GenericDataClass>(fileRelationTypes);
                    HandleRecords<GenericDataClass>(_browselist, fileRelationTypes);

                    break;
               
                case ConsoleKey.P:         // Product types
                    
                    GenericDataClass.SetFieldNamesArray(fieldNames = new string[] { "Search code" });
                    GenericDataClass.SetFieldPropertiesArray(fieldProperties = new int[,] { { 0, 3, 1 } });
                    GenericDataClass.SetNameFieldName("Product Type");
                    _browselist = IO.PopulateList<GenericDataClass>(fileProductTypes);
                    HandleRecords<GenericDataClass>(_browselist, fileProductTypes);
                    
                    break;

                case ConsoleKey.U:         // Product types
                    
                    GenericDataClass.SetFieldNamesArray(fieldNames = new string[] { "Password", "Access People", "Access Employees", "Access Customers", "Access Products", "Access Orders", "Access Master data", "Reset Password next login" });
                    GenericDataClass.SetFieldPropertiesArray(fieldProperties = new int[,] { { 0, 40, 8 },{ 0, 1, 1 },{ 0, 1, 1 },{ 0, 1, 1 },{ 0, 1, 1 },{ 0, 1, 1 },{ 0, 1, 1 },{ 0, 1, 1 } });
                    GenericDataClass.SetNameFieldName("User Name");
                    _browselist = IO.PopulateList<GenericDataClass>(fileUsers);
                    HandleRecords<GenericDataClass>(_browselist, fileUsers);

                    break;

                default:
                    break;
            }

        }
       
        private static Dictionary<int, T> PopulateDictionary<T>(string aFilename) where T : Address
        {

            var browseList = IO.PopulateList<T>(aFilename);             // reads json file and populates list of class T
            List<int>[] searchResult;
            
            var returnDictionary = browseList.ToDictionary(item => item.RecordCounter, item => item);

            return returnDictionary;   //TODO: optimaliseren 
        }

        public Dictionary<string, object> GetProperties<T>(T anObject) 
        {
            return typeof(T).GetProperties().ToDictionary(p => p.Name, p => p.GetValue(anObject));
        }

         public static void DisplayMethodInfo(MethodInfo[] myArrayMethodInfo)
                {
                    // Display information for all methods.
                    for(int i=0;i<myArrayMethodInfo.Length;i++)
                    {
                        MethodInfo myMethodInfo = (MethodInfo)myArrayMethodInfo[i];
                        Console.WriteLine("\nThe name of the method is {0}.", myMethodInfo.Name);
                    }
                }

        private static void HandleRecords<T> (List<T> aList, string aFilename) where T : RecordManager
        {
            int cursorLeft    = Console.CursorLeft;                             // store current cursorposition, left and top
            int cursorTop     = Console.CursorTop;
            int recordIndex   = 1;                                              // actual recordnumber in file being handled
            int recordsInList = aList.Count;                                    // total number of records in file

            // init help variables for search selection    
            var selectedRecordsList = aList;
            var recordsInSelection  = recordsInList;
            bool change = false;
                            
            if (aList.Count > 0)                                                //display first record
            {
                RecordManager.SetTotalRecords(recordsInList);                   // store total records in Recordmanager
                _ = (T)Activator.CreateInstance(typeof(T), aList[0], true);     // display first record on screen
            }
            else
            {
                _ = (T)Activator.CreateInstance(typeof(T), true, true);         // display empty inputfield
            }
                
            UpdateTotalRecordsOnScreen(recordsInList);
            StringBuilder zoekString = new StringBuilder();

            do                                                                  // NICE: figure out generic calling of methods via a generic
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                   
 /*** EDIT **/      case ConsoleKey.Enter:

                        if (recordsInList > 0)                                  // some record is being displayed
                        {
                            if (selectedRecordsList[recordIndex -1].Active)
                            {
                                IO.SetCursorPosition(cursorLeft, cursorTop + 1);       
                                selectedRecordsList[recordIndex -1] = (T)Activator.CreateInstance(typeof(T), selectedRecordsList[recordIndex - 1], false);     // call editor constructor
                                aList[selectedRecordsList[recordIndex - 1].RecordCounter - 1] = selectedRecordsList[recordIndex - 1];
                                IO.WriteToFile(aFilename, aList, true);                                             // write to file
                                IO.SetCursorPosition(cursorLeft, cursorTop);
                                _ = (T)Activator.CreateInstance(typeof(T), selectedRecordsList[recordIndex - 1], true);           // update current record on screen
                            }
                        }
                        break;
                    //case ConsoleKey.I:
/*** ADD ***/       case ConsoleKey.Insert:

                        selectedRecordsList = aList;                                                            // reset searchlist
                        IO.SetCursorPosition(cursorLeft, cursorTop);
                        if (recordsInList != 0) _ = (T)Activator.CreateInstance(typeof(T), true, true);         // clear form if record on screen
                        IO.SetCursorPosition(cursorLeft, cursorTop + 1);                    
                        aList.Add((T)Activator.CreateInstance(typeof(T)));                                      // add new record 
                        recordsInList++; recordsInSelection++;                                                                        // increase record counter
                        recordIndex = recordsInList;                                                            // set index to new created record
                        RecordManager.SetTotalRecords(recordsInList);                                           // store number of records
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        _ = (T)Activator.CreateInstance(typeof(T), aList[recordIndex - 1], true);               // refresh record on screen
                        IO.WriteToFile(aFilename, aList, true);                                                 // write to file 
                        UpdateTotalRecordsOnScreen(recordsInList);                                              // update total records on screen

                        break;
                   
 /*** DELETE ***/   case ConsoleKey.Delete:
                        if (recordsInList > 0)
                        {
                            IO.SetCursorPosition(cursorLeft, cursorTop);                        
                            RecordManager.ToggleDeletionFlag<T>(selectedRecordsList[recordIndex - 1], recordIndex - 1);           // toggle flag
                            _ = (T)Activator.CreateInstance(typeof(T), selectedRecordsList[recordIndex - 1], true);               // refresh record on screen
                            selectedRecordsList[selectedRecordsList[recordIndex - 1].RecordCounter - 1].Active = selectedRecordsList[recordIndex - 1].Active;
                            IO.WriteToFile(aFilename, aList, false);                                                // write
                        }
                        break;
                    
/* BROWSE */        case ConsoleKey.LeftArrow:
                    case ConsoleKey.UpArrow:
                        if (recordIndex > 1)
                        {
                            recordIndex--;
                            IO.SetCursorPosition(cursorLeft, cursorTop);
                            _ = (T)Activator.CreateInstance(typeof(T), selectedRecordsList[recordIndex - 1], true);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.DownArrow:
                        if (recordIndex < recordsInSelection)
                        {
                            recordIndex++;
                            IO.SetCursorPosition(cursorLeft, cursorTop);
                            _ = (T)Activator.CreateInstance(typeof(T), selectedRecordsList[recordIndex - 1], true);
                        }
                        break;
                    
/* SEARCH */        default:
                        
                        compareString       = BuildZoekString(zoekString, cursorTop);
                        int foundListIndex  = aList.FindIndex(item => (item.Description + item.ID).Contains(compareString));
                        int numberFound     = aList.Count(item => (item.Description + item.ID).Contains(compareString));
                        
                        if (numberFound > 0)
                        {
                            change = true;
                                                        
                            selectedRecordsList = aList.FindAll(item => (item.Description + item.ID).Contains(compareString));
                            
                            recordsInSelection = selectedRecordsList.Count;
                            
                        }else
                        {
                            if (change)
                            {
                                selectedRecordsList = aList;
                                change = false;
                            }
                        }
                        IO.PrintOnConsole("Searching: [ " + compareString.ToString() + " ] matches: "+ numberFound.ToString().PadRight(20, ' '), 1, cursorTop - 1, Color.TextColors.Defaults);
                        
                        if (foundListIndex != -1)         // found something
                        {
                            recordIndex = 1;
                            IO.SetCursorPosition(cursorLeft, cursorTop);
                             _ = (T)Activator.CreateInstance(typeof(T), selectedRecordsList[0], true);         // display first found record
                        }else
                        {
                            zoekString.Clear();
                        }
                        break;
                }

                if (Debugger.IsAttached)
                {
                    IO.PrintOnConsole("Recordindex: " + recordIndex, 0, 0, Color.TextColors.Defaults);
                }
            } while (inputKey.Key != ConsoleKey.Home & inputKey.Key != ConsoleKey.Q & inputKey.Key != ConsoleKey.End);
        }


        private static string BuildZoekString(StringBuilder zoekstring, int cursorTop)
        {
            string returnString;
           
            //zoekstring.Append(inputKey.KeyChar.ToString());

            if (validation.ToString().Contains(inputKey.KeyChar.ToString() )) 
            {
                if (inputKey.KeyChar == 8 & zoekstring.Length > 1)
                {
                    zoekstring.Length--;
                }else
                {
                    zoekstring.Append(inputKey.KeyChar.ToString());
                }
            }else
            {
                zoekstring.Clear();
            }

            //IO.PrintOnConsole("Searching: [ " + zoekstring.ToString() + " ]".PadRight(20, ' '), 1, cursorTop - 1, Color.TextColors.Defaults);
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
                        return emp.Description.StartsWith(zoekstring.ToString());
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

        private static void UpdateTotalRecordsOnScreen(int numberOfRecords)     // NICE: display inactive records as well
        {
            IO.PrintOnConsole("[" + numberOfRecords.ToString() + "] records in file", 30, 5, Color.TextColors.MenuSelect);
        }
        private static void ShowAssemblyInfo()                                  // test recursion routine
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

     }
}