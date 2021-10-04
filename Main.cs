using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EmployeeMaint
{
    class Program
    {
        // declare variables 
        
        public static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();
        static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .";
        static string checkinputStringDate = "0123456789/";

        static void Main(string[] args)
        {
            //initialize variables
            // Prevent example from ending if CTL+C is pressed.
            // Console.TreatControlCAsInput = true;                                         // doesn't seem to work correctly

            // init console window properties
            
            Console.Title = "Employee Data Maintenance Console Program v0.1_alpha";
            Console.SetWindowSize(80, 35);
            //Console.SetWindowPosition(11, 9);
            IO.Color(5);
            Console.Clear();
            

            do
            {
                IO.DisplayMenu("Main Menu", "(L)ogin\n(A)dd Employee\n(V)iew Employees\n(D)elete Employee\n");
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
                string passWordInput = IO.GetInput("Enter password: ", checkinputStringAlpha + "!#$%^&*", 18, 56, true, false, false, true);

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
            else if (inputKey.Key == ConsoleKey.A & Password.validPassword)             // Add records
            {
                IO.DisplayMenu("Add Record", "(C)hange\nEnter Validate Field\n");
                AddRecord();
            }
            else if (inputKey.Key == ConsoleKey.V & Password.validPassword)             // view records
            {
                Console.WriteLine("  You Pressed V");
                ViewRecords();
            }
            else if (inputKey.Key == ConsoleKey.D & Password.validPassword)             // delete record
            {
                Console.WriteLine("  You Pressed D");
                DeleteRecord();
            }
            else if (inputKey.Key == ConsoleKey.Escape)                                 // exit program
            {
                return;

            }

        }

        private static void AddRecord()
        {
            
            Employee newEmployee = new Employee();      // create temp variable of class Employee
            string dateHelpstring;                      // temp string which is filled from console input to
            DateTime parsedDateHelpstring;              // be parsed into valid DateTime string;

            newEmployee.SurName = IO.GetInput("Surname:",checkinputStringAlpha, 30, 45, true, true, true, true);
            newEmployee.FirstName = IO.GetInput("First Name:",checkinputStringAlpha, 30, 30, true, true, true, true);
            dateHelpstring = IO.GetInput("Date of Birth (dd/mm/yy):",checkinputStringDate, 30, 10, true, true, false, true);
            newEmployee.Address = IO.GetInput("Address:",checkinputStringAlpha, 30, 45, true, true, true, true);
            newEmployee.Zipcode = IO.GetInput("Zipcode: (####ZZ)",checkinputStringAlpha, 30, 6, true, true, true, true);
            newEmployee.City = IO.GetInput("City:",checkinputStringAlpha, 30, 45, true, true, true, true);
            newEmployee.Telephone = IO.GetInput("Telephone:","0123456789+-", 30, 14, true, true, true, true);
            newEmployee.email = IO.GetInput("Email:",checkinputStringAlpha, 30, 45, true, true, true, true);

            //Console.Write($"{"Enter Date of birth (dd/mm/yyyy)",-40}:"); dateHelpstring = Console.ReadLine();
            // string pattern = "mm/dd/yyyy";

            parsedDateHelpstring = IO.ParseToDateTime(newEmployee, dateHelpstring);

            //int a = newEmployee.SurName.Length;
            //int b = newEmployee.FirstName.Length;
            //int c = newEmployee.DateOfBirth.ToString().Length;
            //Console.WriteLine("Voornaam: {0} Lengte voornaam: {1}", newEmployee.FirstName, b);
            //Console.WriteLine("Achternaam: {0} Lengte achternaam {1}", newEmployee.SurName, a);
            //Console.WriteLine("Date of Birth: {0} Lenght DoB: {1}", newEmployee.DateOfBirth, c);


            Console.WriteLine("\nPress 'Enter' to store entry, (C)hange or (E)xit");
            do
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.C:
                        IO.DisplayMenu("Add Record", "(L)ogin\n(A)dd Employee\n(V)iew Employees\n(D)elete Employee\n");
                        AddRecord();
                        break;

                    default:

                        break;
                }


            } while (inputKey.Key != ConsoleKey.E);

            //
            // TODO: opslaan record in array en wegschrijven in textfile
            //
            //

        }

        private static void DeleteRecord()
        {

        }

        private static void ViewRecords()
        {

        }

        private static void CreateTestRecords()
        {
            throw new NotImplementedException();
        }
        
    }

}