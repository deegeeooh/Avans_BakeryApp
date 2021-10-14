using System;
using System.Threading;

namespace Vlaaieboer
{
    internal class Program
    {
        // declare variables
        private static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();

        private static string fileEmployees = "employees.json";
        private static string fileCustomers = "customers.json";
        private static string fileEmployeeRoles = "employeeRoles.json";

        private static void Main(string[] args)
        {
            //initialize variables
            // Prevent example from ending if CTL+C is pressed.
            // Console.TreatControlCAsInput = true;                                         // doesn't seem to work correctly ?

            // init console window properties
            Console.Title = "Avans C# Console Application exercise";
            Console.SetWindowSize(80, 35);
            //Console.SetWindowPosition(11, 9);                     //TODO: figure out SetWindowsPosition
            IO.Color(5);
            Console.Clear();

            do
            {
                IO.DisplayMenu("Main Menu", "(L)ogin\n(E)mployees\n(C)ustomers\n(P)roducts\n(M)asterdata");
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
                _ = new Login();
            }
            else if (inputKey.Key == ConsoleKey.E & Login.validPassword)             // Employees
            {
                IO.DisplayMenu("Edit Employee master data", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n");
                Employees();
            }
            else if (inputKey.Key == ConsoleKey.C & Login.validPassword)             // Customers
            {
                IO.DisplayMenu("Edit Customer master data", "(A)dd\nArrows to browse\n(Del)ete\n");
                Customers();
            }
            else if (inputKey.Key == ConsoleKey.D & Login.validPassword)             // delete record
            {
                Console.WriteLine("  You Pressed D");
            }
            else if (inputKey.Key == ConsoleKey.Escape)                                 // exit program
            {
                return;
            }
        }

        private static void Employees()                 // TODO: read & count records into array, display first record, browse and delete
        {
            // var employeeList = new List<Employee>();
            var employeeList = Employee.PopulateList(fileEmployees);

            int cursorLeft = Console.CursorLeft;                           // store current cursorposition, left and top
            int cursorTop = Console.CursorTop;
            int recordIndex = 0;
            int maxRecords = 0;

            if (employeeList.Count > 0)
            {
                maxRecords = employeeList.Count;
                recordIndex = 1;
                Employee.DisplayRecord(employeeList, recordIndex, false);
                IO.Color(2);
                IO.PrintOnConsole("[" + maxRecords.ToString() + "] employee records", 30, 5);
                IO.Color(5);
            }
            else
            {
                Employee.DisplayRecord(employeeList, recordIndex, true);
            }

            // IO.PrintOnConsole("Age: " + newEmployee.CalculateAge().ToString(), 34, 1);

            Console.WriteLine("\nPress 'Enter' to store entry, (C)hange or (E)xit");
            do
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.C:
                        //IO.DisplayMenu("Add Record", "(L)ogin\n(A)dd Employee\n(V)iew Employees\n(D)elete Employee\n");
                        //Employees();
                        break;

                    case ConsoleKey.Enter:                  // save record to file

                        return; //back to main menu

                    case ConsoleKey.Insert:                 // add record

                        //IO.DisplayMenu("Edit Customer master data", "Enter to validate field\nDel/Insert character\nArrow keys, Home, End to navigate\n");
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Employee.DisplayRecord(employeeList, recordIndex, true);
                        Console.SetCursorPosition(cursorLeft, cursorTop + 1);
                        employeeList.Add(new Employee(true));
                        Employee.WriteToFile(fileEmployees, employeeList);

                        break;

                    case ConsoleKey.LeftArrow:

                        if (recordIndex > 1)
                        {
                            recordIndex--;
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            Employee.DisplayRecord(employeeList, recordIndex, false);
                        }
                        break;

                    case ConsoleKey.RightArrow:

                        if (recordIndex < maxRecords)
                        {
                            recordIndex++;
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                            Employee.DisplayRecord(employeeList, recordIndex, false);
                        }

                        break;

                    default:

                        break;
                }
            } while (inputKey.Key != ConsoleKey.E);
        }

        //static List<Employee> ExampleOneSimpleClassObject()
        //{
        //    string filename = fileEmployees;
        //    var customers = new List<Employee>();

        //    if (File.Exists(filename))
        //    {
        //        customers = JsonConvert.DeserializeObject<List<Employee>>
        //            (File.ReadAllText(filename));

        //        return customers;
        //    }

        //    return customers;
        //}

        private static void Customers()
        {
            Customer newCustomer = new Customer();
        }
    }
}