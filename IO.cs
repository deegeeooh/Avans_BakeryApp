using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace BakeryConsole
{
    internal class IO
    {
        public static int WarningLength { get; set; }                   // length in ms of system message events
        public static void SetWarningLength (int aValueInMs)            
        {
            WarningLength = aValueInMs;
        }

        public static void DisplayMenu(string title, string menuString, Color.TextColors aColorMenuOption)
        {
            Console.SetWindowSize(Program.windowWidth, Program.windowHeight);
            Console.Clear();
            Color.SetColor(Color.TextColors.Text);
            Console.Write("\n"+ "=".PadRight(80, '=')); 
            Color.SetColor(Color.TextColors.Title);
            //Console.Write("Bakker Vlaaieboer & Zn.\n"); Color.SetColor(Color.TextColors.Text); Console.Write("{0:dd/MM/yyyy HH:mm}".PadRight(30, ' '), DateTime.Now);
            Console.Write("Bakker Vlaaieboer & Zn."); 
            Color.SetColor(Color.TextColors.Text); Console.Write("{0:f}".PadLeft(28, ' '), DateTime.Now); Console.Write("\n");
            //Color.SetColor(Color.TextColors.DefaultForeGround);

            if (Login.validPassword)
            {
                PrintOnConsole("* Logged in *", 65, Console.CursorTop, Color.TextColors.Input);
            }
            else
            {
                PrintOnConsole("* Logged out *", 65, Console.CursorTop,Color.TextColors.DefaultForeGround);
            }
            Color.SetColor(Color.TextColors.Text);
            Console.Write("\n");
            Console.Write("=".PadRight(80, '=') + "\n");
            Color.SetColor(Color.TextColors.MenuSelect); Console.WriteLine(title + "\n");    // menu name

            PrintMenuString(menuString, Color.TextColors.MenuSelect);
            Color.SetColor(Color.TextColors.Text);
            
            if (Debugger.IsAttached)
            {
                Console.Write("1234567890_234567890_234567890_234567890_234567890_234567890_234567890_234567890\n");
            }else
            {
                Console.Write    ("=".PadRight(80,'=') + "\n");
            }
            PrintOnConsole   ("_".PadRight(80,'_'), 0, 33,Color.TextColors.Text);
        }

        private static void PrintMenuString(string menuString, Color.TextColors aColorMenuOption)                 
        {
            Color.SetColor(Color.TextColors.DefaultForeGround);
            var menustringCharArray = menuString.ToCharArray();
            for (int i = 0; i < menuString.Length; i++)
            {
                if (menustringCharArray[i].ToString() == "(")   // NICE make this method compacter
                {
                    Console.Write(menustringCharArray[i]);      // print the "("
                    i++;
                    do                                          // until ")" is found print every char in color
                    {
                        Color.SetColor(aColorMenuOption);
                        Console.Write(menustringCharArray[i]);
                        i++;
                    } while (menustringCharArray[i].ToString() != ")");

                    Color.SetColor(Color.TextColors.DefaultForeGround);
                }
                Color.SetColor(Color.TextColors.DefaultForeGround);
                Console.Write(menustringCharArray[i]);
            }
        }

        public static void PrintOnConsole(string aString, int left, int top, Color.TextColors aColor)  
        {
            Color.SetColor(aColor);
            int currentCursorPosTop = Console.CursorTop;                            // store current cursor pos
            int currentCursorPosLeft = Console.CursorLeft;
            Console.SetCursorPosition(left, top);
            Console.Write(aString);
            Console.SetCursorPosition(currentCursorPosLeft, currentCursorPosTop);
        }

        public static void SystemMessage(string aString, bool aWarning)     
        {
            Color.SetWarningColor(aWarning);
            System.Threading.Timer aTimer = new System.Threading.Timer(EventPrint, aString, 100, Timeout.Infinite);
        }

        public static void EventPrint(Object state)
        {
            string a = "(System): " + state;
            PrintOnConsole(a.PadRight(79, ' '), 0, 34, Color.TextColors.SystemMessage);
            Thread.Sleep(WarningLength);
            PrintOnConsole("".PadRight(79, ' '), 0, 34, Color.TextColors.Defaults);
        }

        public static void StoreCursorPos(out int currentCursorPosTop, out int currentCursorPosLeft)
        {
            currentCursorPosTop  = Console.CursorTop;
            currentCursorPosLeft = Console.CursorLeft;
        }

        public static string GetInput(string fieldName,              // string to display               
                                      string fieldValue,             // string value for editing
                                      string checkinputString,       // accepted input
                                      int lengthQuestionField,       // field width displays string
                                      int lengthInputField,          // field width input field
                                      bool toUpper,                  // convert to uppercase?
                                      bool lineFeed,                 // linefeed after input?
                                      bool showInput,                // display the typed characters?
                                      bool trim,                     // return trimmed string ?
                                      bool showBoundaries,           // print field boundaries?
                                      int minInputLength)            // minimal inputlength required
        {
            //Console.WriteLine("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
            //Console.WriteLine("         1         2         3         4         5         6         7         8         9         1");

            // declare local variables

            int cursorLeft = Console.CursorLeft;                           // store current cursorposition, left and top
            int cursorTop = Console.CursorTop;                             
            
            StringBuilder inputStringbuilder = new StringBuilder();        // stringbuilder to append the single input characters to
            ConsoleKeyInfo inp = new ConsoleKeyInfo();
            int indexInStringbuilder = 1;
            string returnString;
            bool checkedValidLength = false;

            // display field boundaries with padded spaces

            if (showBoundaries)
            {
                PrintBoundaries(fieldName, fieldValue, lengthQuestionField, lengthInputField, cursorTop, true);
            }

            if (fieldValue != "")                                          // if a edit value is given, assign to inputStringbuilder and print it
            {
                inputStringbuilder.Append(fieldValue);
                indexInStringbuilder = inputStringbuilder.Length + 1;      // cursor 1 position after string
                Checkfieldlength(lengthInputField, indexInStringbuilder - 1);
                PrintInputString(showInput, false, inputStringbuilder, Color.TextColors.Input);
            }
            do
            {
                do
                {
                    inp = Console.ReadKey(true);                            // read 1 key, don't display the readkey input (true)
                    string tempString;
                    IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + lengthInputField.ToString()+ "      ", 0, 0, Color.TextColors.Defaults);
                    if (checkinputString.Contains(inp.KeyChar.ToString()) & indexInStringbuilder <= lengthInputField)         // only accept valid characters other than functions

                    {
                        
                        //Checkfieldlength(lengthInputField, indexInStringbuilder - 1);

                        if (toUpper)
                        {
                            tempString = inp.KeyChar.ToString().ToUpper();
                        }
                        else
                        {
                            tempString = inp.KeyChar.ToString();
                        }

                      if (inputStringbuilder.Length < indexInStringbuilder)                               // append when cursor is the end
                        {
                            inputStringbuilder.Append(tempString);
                            indexInStringbuilder++;                                                       // index embedded in if{} because 
                        }                                                                                 // input might not be valid with insert =>
                        else
                        {
                            if (inputStringbuilder.Length < lengthInputField)                            // Insert only as long as not max inputlenght
                            {
                                inputStringbuilder.Insert(indexInStringbuilder - 2, tempString);
                                indexInStringbuilder++;
                            }
                            else
                            {
                                IO.SystemMessage("Maximum input length", false);
                            }
                        }

                        Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);      // position cursor at start inputfield
                        PrintInputString(showInput, false, inputStringbuilder,Color.TextColors.Input);
                        Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                    }
                    else if (inp.Key == ConsoleKey.Backspace & indexInStringbuilder > 1)                        //BACKSPACE
                    {
                        indexInStringbuilder--;
                        Checkfieldlength(lengthInputField, indexInStringbuilder - 1);
                        inputStringbuilder.Remove(indexInStringbuilder - 1, 1);
                        Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);
                        PrintInputString(showInput, true, inputStringbuilder,Color.TextColors.Input);
                        Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                    }
                    else if (inp.Key == ConsoleKey.Delete)                                                      //DELETE
                    {
                        if (inputStringbuilder.Length > 0 & indexInStringbuilder <= inputStringbuilder.Length)
                        {
                            inputStringbuilder.Remove(indexInStringbuilder - 1, 1);
                            Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);
                            PrintInputString(showInput, true, inputStringbuilder,Color.TextColors.Input);
                            IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + "       ", 0, 0,Color.TextColors.Defaults);
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }
                    }
                    else if (inp.Key == ConsoleKey.LeftArrow ||                                                 // move cursor
                             inp.Key == ConsoleKey.RightArrow ||
                             inp.Key == ConsoleKey.Home ||
                             inp.Key == ConsoleKey.End)
                    {
                        if (inp.Key == ConsoleKey.LeftArrow & indexInStringbuilder > 1)
                        {
                            indexInStringbuilder--;         // one to the left
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.RightArrow & indexInStringbuilder <= inputStringbuilder.Length)
                        {
                            indexInStringbuilder++;
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.Home)                                                    // HOME
                        {
                            indexInStringbuilder = 1;
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.End & inputStringbuilder.Length > 0)                     // END
                        {
                            indexInStringbuilder = inputStringbuilder.Length + 1;
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }

                        IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + "       ", 0, 0,Color.TextColors.Defaults);
                    }
                    else if (checkinputString.Contains(inp.KeyChar.ToString()))              // valid input but end of field 
                    {                                                                        // since we already tested on valid input
                        IO.SystemMessage("Maximum input length", false);                     // and field length in first if statement
                    }
                    IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + lengthInputField.ToString() + "      ", 0, 0, Color.TextColors.Defaults);
                
                } while (inp.Key != ConsoleKey.Enter & inp.Key != ConsoleKey.Escape);

                if (inputStringbuilder.Length >= minInputLength)
                {
                    checkedValidLength = true;
                    IO.PrintOnConsole("                             ", 0, 0,Color.TextColors.Defaults);
                }
                else
                {
                    SystemMessage("Field cannot be empty", true);
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
                Color.SetColor(Color.TextColors.Text);
                Console.Write(returnString.PadRight(lengthInputField, ' ')); // pad with spaces to length field
            }

            if (lineFeed) { Console.WriteLine(); }

            Checkfieldlength(lengthInputField, 1);                          // reset warning incase cursor was at last position before 'Enter'

            Color.SetColor(Color.TextColors.Text);
            return returnString;
        }

        public static void PrintBoundaries(string displayString,            // textstring of name of field
                                           string fieldValue,               // current value of this field (for edit purposes)
                                           int lengthQuestionField,         // Length of the name of field to be padded
                                           int lengthInputField,            // idem input field
                                           int cursorTop,                   // reset cursor to row
                                           bool active)
        {
            var inactiveColor = (active) ? Color.TextColors.Text : Color.TextColors.Inactive;

            Color.SetColor(Color.TextColors.DefaultForeGround);
            Console.Write(displayString.PadRight(lengthQuestionField, ' '));
            if (fieldValue == "")
            {
                //Console.Write("|".PadRight(lengthInputField + 1, ' ') +            // print input field boundaries
                //"|".PadRight(Program.windowWidth - lengthQuestionField - lengthInputField + 2, ' ')); // fillout with " " to edge

                Console.Write("|".PadRight(lengthInputField + 1, ' ') + "|");            // print input field boundaries
                Console.Write("".PadRight(Program.windowWidth - lengthQuestionField - lengthInputField - 2, 'X')); // fillout with " " to edge
            }
            else                                                                         // if an existing field value is passed on, print that
            {
                Console.Write("|");
                Color.SetColor(inactiveColor); Console.Write(fieldValue.PadRight(lengthInputField, ' '));
                Color.SetColor(Color.TextColors.DefaultForeGround); Console.Write("|");
            }
            Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);                // reset cursorposition to beginning of the input field
            Color.SetColor(Color.TextColors.DefaultForeGround);
        }

        public static DateTime ParseToDateTime(string aDateString, bool checkAge)

        {
            DateTime parsedDateHelpstring;
            if (DateTime.TryParse(aDateString, out parsedDateHelpstring))   // Tryparse method passing back two values: bool and out var
            {

            if (checkAge)
                {
                    if (CalculateAge(parsedDateHelpstring) > 100 || CalculateAge(parsedDateHelpstring) < 1)
                    {
                        IO.SystemMessage($"Impossible age: {CalculateAge(parsedDateHelpstring)}", true);
                        parsedDateHelpstring = DateTime.Parse("01/01/0001");
                    }
                    else
                    {
                        IO.SystemMessage($"Parsed date string succesfully to {parsedDateHelpstring:dd-MM-yyyy}", false);
                    }
                }
            
            }
            else
            {
                // if invalid date, DateTime remains at initialised 01/01/01 value
                IO.SystemMessage($"Warning: Could not parse date string, set to {parsedDateHelpstring:dd-MM-yy}", false);
            }

            return parsedDateHelpstring;
        }

        public static int CalculateAge(DateTime aDateTime)
        {
            var age = DateTime.Today.Year - aDateTime.Year;                     // not taking date into account eg. 2021-1997
            int nowMonthandDay = int.Parse(DateTime.Now.ToString("MMdd"));      // convert month and day to int
            int thenMonthandDay = int.Parse(aDateTime.ToString("MMdd"));
            if (nowMonthandDay < thenMonthandDay)
            {
                age--;                                                          // if current date (in MMdd) < date of birth subtract 1 year
            }
            return age;
        }

        public static void Checkfieldlength(int lengthInputField, int indexInStringbuilder)  // OBSOLETE
        {
            if (indexInStringbuilder == lengthInputField & lengthInputField > 1)
            {
                IO.SystemMessage("Maximum field length", false);
            }
        }

        private static void PrintInputString(bool showInput, bool deltrailspace, StringBuilder inputStringbuilder, Color.TextColors aColor)
        {
            Color.SetColor(aColor);

            if (showInput)     { Console.Write(inputStringbuilder); } 
            else               { Console.Write("".PadRight(inputStringbuilder.Length, '*')); }
            
            if (deltrailspace) { Console.Write(" "); }
        }

        // Generic JSON routines

        public static List<T> PopulateList<T>(string aFilename) where T : class
        {
            var getaListFromJSON = DeserializeJSONfile<T>(aFilename);
            return getaListFromJSON;
        }

        private static List<T> DeserializeJSONfile<T>(string aFilename) where T : class
        {
            var getaListFromJSON = new List<T>();                             // define here so method doesn't return NULL
            if (File.Exists(aFilename))                                       // and causes object not defined error
            {                                                                 // when calling employeeList.add from main()
                try
                {
                    getaListFromJSON = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(aFilename));          // JsonConvert will call the default() constructor here
                    return getaListFromJSON;                                                                         // circumvent with  [JsonConstructor] attribute or by using arguments
                }                                                                                                    // on the constructor
                catch (Exception e)
                {
                    IO.SystemMessage($"Error parsing json file{aFilename} {e}", true);
                }
            }
            else
            {
                IO.SystemMessage($"File {aFilename} doesn't exist, creating new file ", false);
            }
            return getaListFromJSON;
        }

        public static void WriteToFile<T>(string aFilename, List<T> aListOfObjects) where T : class
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(aListOfObjects, Formatting.Indented);
                File.WriteAllText(aFilename, jsonString);
            }
            catch (Exception e)
            {
                IO.SystemMessage($"Error writing to file {aFilename} {e}", true);
            }
        }
    }
}