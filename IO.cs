using System;
using System.Text;

namespace Vlaaieboer
{
    internal class IO
    {
        public static void DisplayMenu(string title, string menuString)
        {
            Console.Clear();
            IO.Color(5);
            Console.WriteLine("\n\n=============================================================================");
            IO.Color(4);
            Console.Write("Bakker Vlaaieboer & Zn."); Console.Write("{0:dd/MM/yyyy hh:mm}".PadLeft(30, ' '), DateTime.Now);
            IO.Color(5);
            if (Login.validPassword)
            {
                IO.Color(1); Console.Write("* Logged in *".PadLeft(25, ' ')); IO.Color(4);
            }
            else
            {
                Console.Write("* Not Logged in *".PadLeft(25, ' '));
            }
            Console.Write("\n");
            Console.WriteLine("=============================================================================");
            IO.Color(2); Console.WriteLine(title + "\n"); IO.Color(4);
            Console.WriteLine(menuString);
            //Console.Write("(L)ogin\n(A)dd Employee\n(V)iew Employees\n(D)elete Employee\n");
            Console.WriteLine("Enter your choice, press <Escape> to exit\n");
            Console.WriteLine("=============================================================================\n");
            IO.PrintOnConsole("___________________________________________________________________________", 1, 33);
        }

        public static void PrintOnConsole(string consoleString, int left, int top)              //TODO: capture screenbuffer to display 'windows'
        {
            int currentCursorPosTop = Console.CursorTop;                // store current cursor pos
            int currentCursorPosLeft = Console.CursorLeft;
            Console.SetCursorPosition(left, top);
            Console.Write(consoleString);
            Console.SetCursorPosition(currentCursorPosLeft, currentCursorPosTop);   // reset cursorpos
        }

        public static void Color(int color)                    // sets or resets console font color
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

                case 6:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                default:
                    Console.ResetColor();
                    break;
            }
        }

        public static string GetInput(string fieldName,              // string to display
                                      string fieldValue,             // string value for editing
                                      string checkinputString,       // accepted input
                                      int lengthQuestionField,       // field width displays string
                                      int lengthInputField,          // field width input field
                                      bool lineFeed,                 // linefeed after input?
                                      bool showInput,                // display the typed characters?
                                      bool trim,                     // return with trimmed spaces ?
                                      bool showBoundaries,           // print field boundaries?
                                      int minInputLength)            // minimal inputlength required
        {
            //Console.WriteLine("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
            //Console.WriteLine("         1         2         3         4         5         6         7         8         9         1");

            // declare local variables

            int cursorLeft = Console.CursorLeft;                           // store current cursorposition, left and top
            int cursorTop = Console.CursorTop;                             // TODO: Why doesn't Console.GetCursorPosition exist/Work? (see MS docs https://docs.microsoft.com/en-us/dotnet/api/system.console.getcursorposition?view=net-5.0 )
            StringBuilder inputStringbuilder = new StringBuilder();        // stringbuilder to append the single input characters to
            StringBuilder inputStringbuilderNoShow = new StringBuilder();  // helper string for no show input (passwords)
            ConsoleKeyInfo inp = new ConsoleKeyInfo();
            int indexInStringbuilder = 1;
            string returnString;
            bool checkedValidLength = false;

            if (fieldValue != "")                                          // if a edit value is given, assign to inputStringbuilder and print it
            {
                inputStringbuilder.Append(fieldValue);
                PrintInputString(showInput, false, inputStringbuilder, inputStringbuilderNoShow);
            }
            // display field boundaries with padded spaces

            if (showBoundaries)
            {
                PrintBoundaries(fieldName, fieldValue, lengthQuestionField, lengthInputField, cursorTop);
            }

            do
            {
                do
                {
                    inp = Console.ReadKey(true);                            // read 1 key, don't display the readkey input (true)

                    if (checkinputString.Contains(inp.KeyChar.ToString()) & indexInStringbuilder <= lengthInputField)         // only accept valid characters other than functions

                    {
                        indexInStringbuilder++;
                        Checkfieldlength(lengthInputField, indexInStringbuilder - 1);
                        inputStringbuilder.Append(inp.KeyChar);
                        inputStringbuilderNoShow.Append("*");
                        Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);      // position cursor at start inputfield
                        IO.Color(1);
                        PrintInputString(showInput, false, inputStringbuilder, inputStringbuilderNoShow);
                    }
                    else if (inp.Key == ConsoleKey.Backspace & indexInStringbuilder > 1)
                    {
                        indexInStringbuilder--;
                        Checkfieldlength(lengthInputField, indexInStringbuilder - 1);
                        inputStringbuilder.Remove(indexInStringbuilder - 1, 1);
                        inputStringbuilderNoShow.Remove(indexInStringbuilder - 1, 1);
                        Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);

                        PrintInputString(showInput, true, inputStringbuilder, inputStringbuilderNoShow);

                        int helpCursorLeft = Console.CursorLeft;
                        Console.SetCursorPosition(helpCursorLeft - 1, cursorTop);             // and place the curser back one position
                    }
                    else if (inp.Key == ConsoleKey.Delete)
                    {
                        if (inputStringbuilder.Length > 0 & indexInStringbuilder <= inputStringbuilder.Length)
                        {
                            inputStringbuilder.Remove(indexInStringbuilder - 1, 1);
                            inputStringbuilderNoShow.Remove(indexInStringbuilder - 1, 1);
                            Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);
                            PrintInputString(showInput, true, inputStringbuilder, inputStringbuilderNoShow);
                            IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + "       ", 1, 1);
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }
                    }
                    else if (inp.Key == ConsoleKey.LeftArrow ||
                             inp.Key == ConsoleKey.RightArrow ||
                             inp.Key == ConsoleKey.Home ||
                             inp.Key == ConsoleKey.End)
                    {
                        if (inp.Key == ConsoleKey.LeftArrow & indexInStringbuilder > 1)
                        {
                            indexInStringbuilder--;         // one to the left
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.RightArrow & indexInStringbuilder < inputStringbuilder.Length)
                        {
                            indexInStringbuilder++;
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.Home)
                        {
                            indexInStringbuilder = 1;
                            Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.End & inputStringbuilder.Length > 0)
                        {
                            indexInStringbuilder = inputStringbuilder.Length;
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }

                        IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + "       ", 1, 1);
                    }
                    else if (inp.Key == ConsoleKey.Escape)
                    {
                        break;
                        //IO.PrintOnConsole("Do you want to stop editing this record?", 1, 34);
                        //var respons = IO.GetInput("", "", "YyNn", 0, 1, false, true, true, false, 1);
                        //Console.ReadKey();
                    }
                } while (inp.Key != ConsoleKey.Enter);

                if (inputStringbuilder.Length >= minInputLength)
                {
                    checkedValidLength = true;
                    IO.PrintOnConsole("                             ", 1, 1);
                }
                else
                {
                    IO.Color(3);
                    IO.PrintOnConsole("Field cannot be empty", 1, 1);
                    IO.Color(5);
                }
            } while (!checkedValidLength);

            // set returnstring trimmed
            if (!trim)
            {
                returnString = inputStringbuilder.ToString();
            }
            else
            {
                returnString = (inputStringbuilder.ToString()).Trim();
            }

            // print inputString to field in case anything was trimmed
            Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);
            if (showInput)                                                  // not with password
            {
                Console.Write(returnString.PadRight(lengthInputField, ' ')); // pad with spaces to length field
            }

            if (lineFeed) { Console.WriteLine(); }

            Checkfieldlength(lengthInputField, 1);                          // reset warning incase cursor was at last position before 'Enter'

            IO.Color(5);
            return returnString;
        }

        public static void PrintBoundaries(string displayString, string fieldValue, int lengthQuestionField, int lengthInputField, int cursorTop)
        {
            IO.Color(4);
            Console.Write(displayString.PadRight(lengthQuestionField, ' '));
            if (fieldValue == "")
            {
                Console.Write("|".PadRight(lengthInputField + 1, ' ') + "|");            // print input field boundaries
            }
            else                                                                         // if an existing field value is passed on, print that
            {
                Console.Write("|" + fieldValue.PadRight(lengthInputField, ' ') + "|");
            }
            Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);                // reset cursorposition to beginning of the input field
            IO.Color(5);
        }

        public static void Checkfieldlength(int lengthInputField, int indexInStringbuilder)
        {
            if (indexInStringbuilder == lengthInputField)
            {
                IO.Color(3);
                IO.PrintOnConsole("Max field length", 1, 1);
                IO.Color(5);
            }
            else
            {
                IO.PrintOnConsole("                             ", 1, 1);
            }
        }

        private static void PrintInputString(bool showInput, bool deltrailspace, StringBuilder inputStringbuilder, StringBuilder inputStringbuilderNoShow)
        {
            if (showInput)
            {
                Console.Write(inputStringbuilder);
            }
            else
            {
                Console.Write(inputStringbuilderNoShow);                        // show the dummy inputstring
            }
            if (deltrailspace)
            {
                Console.Write(" ");
            }
        }
    }
}