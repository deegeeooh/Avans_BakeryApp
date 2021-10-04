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
        // public variables definition
        
        public static ConsoleKeyInfo inputKey = new ConsoleKeyInfo();
        

        static void Main(string[] args)
        {
            //initialize variables
            // Prevent example from ending if CTL+C is pressed.
            // Console.TreatControlCAsInput = true;                                         // doesn't seem to work correctly


            // init console window properties
            Console.Title = "Employee Data Maintenance Console Program v0.1_alpha";
            Console.SetWindowSize(80, 35);                                                  
            Color(5);
            Console.Clear();

            do
            {
                DisplayMenu("Main Menu", "(L)ogin\n(A)dd Employee\n(V)iew Employees\n(D)elete Employee\n");
                inputKey = Console.ReadKey(true);               // 'true' | dont'display the input on the console
                CheckMenuInput();


            } while (inputKey.Key != ConsoleKey.Escape);

            Console.WriteLine("\n\nYou have been logged out.. Goodbye!");
            Color(0);
            Thread.Sleep(500);
        }

        private static void AddRecord()
        {

            Employee newEmployee = new Employee();      // create temp variable of class Employee
            string dateHelpstring;                      // temp string which is filled from console input to
            DateTime parsedDateHelpstring;              // be parsed into valid DateTime string;

            newEmployee.SurName = GetInput("Surname:", 30, 45, true, true).ToString();
            newEmployee.FirstName = GetInput("First Name:", 30, 30, true, true).ToString();
            dateHelpstring = GetInput("Date of Birth (dd/mm/yy):", 30, 10, true, true);
            newEmployee.Address = GetInput("Address:", 30, 45, true, true);
            newEmployee.Zipcode = GetInput("Zipcode: (####ZZ)", 30, 6, true, true);
            newEmployee.City = GetInput("City:", 30, 45, true, true);
            newEmployee.Telephone = GetInput("Telephone:", 30, 14, true, true);
            newEmployee.email = GetInput("Email:", 30, 45, true, true);

            //Console.Write($"{"Enter Date of birth (dd/mm/yyyy)",-40}:"); dateHelpstring = Console.ReadLine();
            // string pattern = "mm/dd/yyyy";

            parsedDateHelpstring = ParseToDateTime(newEmployee, dateHelpstring);

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
                        DisplayMenu("Add Record", "(L)ogin\n(A)dd Employee\n(V)iew Employees\n(D)elete Employee\n");
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

        static DateTime ParseToDateTime(Employee newEmployee, string dateHelpstring)
        {
            DateTime parsedDateHelpstring;
            if (DateTime.TryParse(dateHelpstring, out parsedDateHelpstring))   // Tryparse method passing back two values: bool and out var
            {
                Console.WriteLine("\nParsed date string succesfully to {0:dd MM yyyy}", parsedDateHelpstring);

                int age = CalculateAge(parsedDateHelpstring);

                Console.WriteLine("Employee born on: {0:dddd dd MMMM}'{0:yy}, age {1}", parsedDateHelpstring, age);
                newEmployee.DateOfBirth = parsedDateHelpstring;
            }
            else
            {
                Console.WriteLine("Could not parse date string {0} ", dateHelpstring);
            }

            return parsedDateHelpstring;
        }

        static int CalculateAge(DateTime dateOfBirth)
        {
            // calculate age
            var age = DateTime.Today.Year - dateOfBirth.Year;                   // not taking date into account eg. 2021-1997
            int nowMonthandDay = int.Parse(DateTime.Now.ToString("MMdd"));      // convert month and day to int
            int thenMonthandDay = int.Parse(dateOfBirth.ToString("MMdd"));
            // Console.WriteLine("Now {0} and then {1}", nowMonthandDay, thenMonthandDay);
            if (nowMonthandDay < thenMonthandDay)
            {
                age--;                                                          // if current date (in MMdd) < date of birth subtract 1 year
            }
            //
            return age;
        }


        static string GetInput(string inputstring, int lengthQuestionField, int lengthInputField, bool lineFeed, bool showInput)                                
        {

            //Console.WriteLine("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
            //Console.WriteLine("         1         2         3         4         5         6         7         8         9         1");
           
            // declare local variables

            int cursorLeft = Console.CursorLeft;                                    // store current cursorposition, left and top  
            int cursorTop = Console.CursorTop;                                      // TODO: Why doesn't Console.GetCursorPosition exist/Work? (see MS docs https://docs.microsoft.com/en-us/dotnet/api/system.console.getcursorposition?view=net-5.0 )
            StringBuilder inputStringbuilder = new StringBuilder();                 // stringbuilder to append the single input characters to
            StringBuilder inputStringbuilderNoShow = new StringBuilder();           // helper string for passwords
            ConsoleKeyInfo inp = new ConsoleKeyInfo();
            int indexInStringbuilder = 1;

            // display field boundaries with padded spaces

            Color(4);
            Console.Write(inputstring.PadRight(lengthQuestionField, ' '));
            Console.Write("|".PadRight(lengthInputField + 1, ' ') + "|");           // print input field boundaries
            Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);                   // reset cursorposition to beginning of the input field
            Color(1);
            
            do
            {

                inp = Console.ReadKey(true);                            // read 1 key, don't display the readkey input (true)

                if (inp.Key != ConsoleKey.Backspace & indexInStringbuilder <= lengthInputField & inp.Key != ConsoleKey.Escape & inp.Key != ConsoleKey.Enter & inp.Key != ConsoleKey.Tab)
                {
                    indexInStringbuilder++;
                    Checkfieldlength(lengthInputField, indexInStringbuilder -1);
                    inputStringbuilder.Append(inp.KeyChar);
                    inputStringbuilderNoShow.Append("*");
                    Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);      // position cursor at start inputfield

                    if (showInput)
                    {
                        Console.Write(inputStringbuilder);
                    }
                    else
                    {
                        Console.Write(inputStringbuilderNoShow);                        // show the dummy inputstring
                    }

                }
                else if (inp.Key == ConsoleKey.Backspace & indexInStringbuilder > 1)
                {
                    indexInStringbuilder--;
                    Checkfieldlength(lengthInputField, indexInStringbuilder -1 );
                    inputStringbuilder.Remove(indexInStringbuilder - 1, 1);
                    inputStringbuilderNoShow.Remove(indexInStringbuilder - 1, 1);
                    Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);
                    

                    if (showInput)
                    {
                        Console.Write(inputStringbuilder + " ");                           // overwrite the rightmost char of string with extra space
                    }
                    else
                    {
                        Console.Write(inputStringbuilderNoShow + " ");
                    }
                    
                    int helpCursorLeft = Console.CursorLeft;
                    Console.SetCursorPosition(helpCursorLeft - 1, cursorTop);             // and place the curser back one position
                }
                else if (inp.Key == ConsoleKey.Escape)
                {
                    PrintOnConsole("Do you want to stop editing this record?",1, 34);
                    Console.ReadKey();
                }

            } while (inp.Key != ConsoleKey.Enter);

            string returnString = inputStringbuilder.ToString();
            
            if (lineFeed) { Console.WriteLine();}

            Checkfieldlength(lengthInputField, 1); // reset warning incase cursor was at last position before 'Enter'

            Color(5);
            return returnString;

            
        }

        private static void Checkfieldlength(int lengthInputField, int indexInStringbuilder)
        {
            if (indexInStringbuilder == lengthInputField )
            {
                Color(3);
                PrintOnConsole("Max field length", 1, 1);
                Color(1);
            }
            else
            {
                PrintOnConsole("                             ", 1, 1);
            }
        }

        private static void PrintOnConsole(string consoleString, int left, int top)
        {
            int currentCursorPosTop = Console.CursorTop;                // store current cursor pos
            int currentCursorPosLeft = Console.CursorLeft;
            Console.SetCursorPosition(left, top);
            Console.Write(consoleString);
            Console.SetCursorPosition(currentCursorPosLeft, currentCursorPosTop);   // reset cursorpos
        }

        private static void DeleteRecord()
        {

        }

        private static void ViewRecords()
        {

        }

        private static void DisplayMenu(string title, string menuString)
        {
            Console.Clear();
            Color(5);
            Console.WriteLine("\n\n=============================================================================");
            Color(4);
            Console.Write("Employee data maintenance"); Console.Write("{0:dd/MM/yyyy}".PadLeft(30, ' '), DateTime.Now);
            Color(5);
            if (Password.validPassword)
                {
                    Color(1); Console.Write("* Logged in *".PadLeft(25, ' ')); Color(4);
                }
            else
                {
                    Console.Write("* Not Logged in *".PadLeft(25, ' '));
                }
            Console.Write("\n");
            Console.WriteLine("=============================================================================");
            Color(2); Console.WriteLine(title + "\n"); Color(4);
            Console.WriteLine(menuString);
            //Console.Write("(L)ogin\n(A)dd Employee\n(V)iew Employees\n(D)elete Employee\n");
            Console.WriteLine("Enter your choice, press <Escape> to exit\n");
            Console.WriteLine("=============================================================================\n");
            PrintOnConsole("___________________________________________________________________________", 1, 33);

        }

        private static void CheckMenuInput()
        {
            //inputKey = Console.ReadKey(true);               // 'true' | dont'display the input on the console
            //if ((inputKey.Modifiers & ConsoleModifiers.Control) != 0) { Console.Write("Control+"); }
            
            if (inputKey.Key == ConsoleKey.L || !Password.validPassword & inputKey.Key != ConsoleKey.Escape)
            {
                //Console.WriteLine("  You Pressed L");
                string passWordInput = GetInput("Enter password: ", 18, 56, true, false);

                //Console.Write("Enter Password: ");
                //string passWordInput = Console.ReadLine();
                if (passWordInput == Password.passWord)
                {
                    Password.validPassword = true;
                    PrintOnConsole("You have been logged in succesfully", 1, 34);
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
                DisplayMenu("Add Record", "(C)hange\nEnter Validate Field\n");
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

                Console.WriteLine("Login with a valid password");
                Thread.Sleep(500);
            }

        }

        private static void CreateTestRecords()
        {
            throw new NotImplementedException();
        }

        private static void Color(int color)                    // sets or resets console font color
        {                                                       // TODO: call with colour name string instead of number
            Console.BackgroundColor = ConsoleColor.Black;
            switch (color)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
        }
        
    }

}