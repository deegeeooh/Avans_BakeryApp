using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Text.Json;


namespace Vlaaieboer
{
    class Program
    {
        // declare variables 
        static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();
        //static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .,_";
        //static string checkinputStringDate = "0123456789/-";
        static string fileEmployees = "employee.json";
        static string fileCustomers = "customers.json";
        static string fileEmployeeRoles = "employeeRoles.json";

        static void Main(string[] args)
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
                IO.DisplayMenu("Main Menu", "(L)ogin\n(E)mployees\n(C)ustomers\n");
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

            Employee newEmployee = new Employee();        // instantiate object of class Employee by calling constructor method 

            IO.PrintOnConsole("Age: " + newEmployee.CalculateAge().ToString(), 34, 1);

            Console.WriteLine("\nPress 'Enter' to store entry, (C)hange or (E)xit");
            do
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.C:
                        IO.DisplayMenu("Add Record", "(L)ogin\n(A)dd Employee\n(V)iew Employees\n(D)elete Employee\n");
                        Employees();
                        break;

                    case ConsoleKey.Enter:

                        //newEmployee.WriteToFile(fileEmployees);
                       
                        return; //back to main menu
                        

                    default:

                        break;
                }


            } while (inputKey.Key != ConsoleKey.E);

            
        }

        private static void Customers()
        {
            Customer newCustomer = new Customer();

        }
        
    }

}