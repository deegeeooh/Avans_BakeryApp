using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Text.Json;


namespace EmployeeMaint
{
    class Program
    {
        // declare variables 
        
        public static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();
        static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .,_";
        static string checkinputStringDate = "0123456789/-";
        static string fileEmployees = "employee.json";
        static string fileCustomers = "customers.json";
        static string fileEmployeeRoles = "employeeRoles.json";


        static void Main(string[] args)
        {
            //initialize variables
            // Prevent example from ending if CTL+C is pressed.
            // Console.TreatControlCAsInput = true;                                         // doesn't seem to work correctly

            // init console window properties
            
            Console.Title = "Console Collections v0.1_alpha";
            Console.SetWindowSize(80, 35);
            //Console.SetWindowPosition(11, 9);
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

            if (inputKey.Key == ConsoleKey.L || !Password.validPassword & inputKey.Key != ConsoleKey.Escape)
            {
                string passWordInput = IO.GetInput("Enter password: ", checkinputStringAlpha + "!#$%^&*", 18, 56, true, false, false, true, 0);

                if (passWordInput == Password.passWord)
                {
                    Password.validPassword = true;
                    IO.PrintOnConsole("You have been logged in succesfully", 1, 34);
                    Thread.Sleep(500);
                    return;                             // exit if statement
                }
                else
                {
                    Console.WriteLine("Invalid password");
                    Password.validPassword = false;
                    Thread.Sleep(500);
                }
            }
            else if (inputKey.Key == ConsoleKey.E & Password.validPassword)             // Employees
            {
                IO.DisplayMenu("Edit Employee master data", "(Ins)ert to Add\n(Enter)to Edit\n(Del)ete to remove Record\nUse arrow keys to browse\n");
                Employees();
            }
            else if (inputKey.Key == ConsoleKey.C & Password.validPassword)             // Customers
            {
                IO.DisplayMenu("Edit Customer master data", "(A)dd\nArrows to browse\n(Del)ete\n");
                
            }
            else if (inputKey.Key == ConsoleKey.D & Password.validPassword)             // delete record
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
            
            Employee newEmployee = new Employee();      // instantiate temp object of class Employee
            string dateHelpstring;                      // temp string which is filled from console input to
            DateTime parsedDateHelpstring;              // be parsed into valid DateTime string;

            newEmployee.RecordCounter = 1;              
            newEmployee.SurName = IO.GetInput("Surname:",checkinputStringAlpha, 30, 45, true, true, true, true, 1);
            newEmployee.Prefix = IO.GetInput("Prefix", checkinputStringAlpha, 30, 35, true, true, true, true, 0);
            newEmployee.FirstName = IO.GetInput("First Name:",checkinputStringAlpha, 30, 30, true, true, true, true, 1);
            dateHelpstring = IO.GetInput("Date of Birth (dd/mm/yyyy):",checkinputStringDate, 30, 10, true, true, false, true, 10);
            
            // validate date string input
            parsedDateHelpstring = IO.ParseToDateTime(newEmployee, dateHelpstring);

            newEmployee.Address = IO.GetInput("Address:",checkinputStringAlpha, 30, 45, true, true, true, true, 0);
            newEmployee.Zipcode = IO.GetInput("Zipcode: (####ZZ)",checkinputStringAlpha, 30, 6, true, true, true, true, 0);
            newEmployee.City = IO.GetInput("City:",checkinputStringAlpha, 30, 45, true, true, true, true, 0);
            newEmployee.Telephone = IO.GetInput("Telephone:","0123456789+-", 30, 14, true, true, true, true, 0);
            newEmployee.Email = IO.GetInput("Email:",checkinputStringAlpha, 30, 45, true, true, true, true, 1);

            // construct unique employee ID
            string a = newEmployee.RecordCounter.ToString("D5");
            string b;
            if (newEmployee.SurName.Length >= 3)
            {
                b = newEmployee.SurName.Substring(0, 3).ToUpper();
            }
            else
            {
                b = newEmployee.SurName.Substring(0, newEmployee.SurName.Length)
                    .ToUpper()
                    .PadRight(3-newEmployee.SurName.Length,'A');
            }
            newEmployee.EmployeeID = b + a;


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
                        
                       
                        var options = new JsonSerializerOptions { WriteIndented = true };                   //TODO: refactor to method
                        string jsonString = JsonSerializer.Serialize(newEmployee, options);
                        
                        using (StreamWriter sw = File.AppendText(fileEmployees))
                        {
                            sw.WriteLine(jsonString);
                        }
                        return; //back to main menu
                        

                    default:

                        break;
                }


            } while (inputKey.Key != ConsoleKey.E);

            //
            // TODO: opslaan record in array en wegschrijven in textfile
            //
            //

        }

        private static void Customers()
        {

        }
        
    }

}