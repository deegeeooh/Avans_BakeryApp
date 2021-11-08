using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Vlaaieboer
{
    internal class IO
    {
        public enum TextColors
        {
            Input,
            MenuSelect,
            Error,
            Text,
            DefaultForeground,
            DefaultBackground,
            Inactive,
            Title,
            Inverted
        }

        //
        // User changeable colors
        //

        private static List<UserColor> userColor = new List<UserColor>();

        public static void SetStandardColor()
        {
            userColor.Add(new UserColor());
        }

        public static void CycleColors(int aChoice, bool aRndBackground)
        {
            //userColor.Add(new UserColor());

            switch (aChoice)
            {
                case 0:     //Text High
                    var e = (int)userColor[0].TextHigh;
                    e++; if (e == 16) { e = 0; }
                    userColor[0].TextHigh = (ConsoleColor)e;
                    break;

                case 1:     //foreground
                    var a = (int)userColor[0].ForeGroundDefault;
                    a++; if (a == 16) { a = 0; }
                    userColor[0].ForeGroundDefault = (ConsoleColor)a;
                    break;

                case 2:     //background

                    var b = (int)userColor[0].BackGroundDefault;
                    b++; if (b == 16) { b = 0; }
                    userColor[0].BackGroundDefault = (ConsoleColor)b;
                    Console.BackgroundColor = userColor[0].BackGroundDefault;    // set backgroundcolor here before Console.Clear() in main loop
                    break;

                case 3:     //title

                    var c = (int)userColor[0].MenuSelectDefault;
                    c++; if (c == 16) { c = 0; }
                    userColor[0].MenuSelectDefault = (ConsoleColor)c;
                    break;

                case 4:     //menucolor

                    var d = (int)userColor[0].Title;
                    d++; if (d == 16) { d = 0; }
                    userColor[0].Title = (ConsoleColor)d;
                    break;

                case 5:     //menucolor
                    var rand = new Random();
                    if (aRndBackground) { userColor[0].BackGroundDefault = (ConsoleColor)rand.Next(16); }

                    do { userColor[0].TextHigh = (ConsoleColor)rand.Next(16); } while (userColor[0].TextHigh == userColor[0].BackGroundDefault);
                    do { userColor[0].ForeGroundDefault = (ConsoleColor)rand.Next(16); } while (userColor[0].ForeGroundDefault == userColor[0].BackGroundDefault);
                    do { userColor[0].MenuSelectDefault = (ConsoleColor)rand.Next(16); } while (userColor[0].MenuSelectDefault == userColor[0].BackGroundDefault |
                                                                                 userColor[0].MenuSelectDefault == userColor[0].ForeGroundDefault);

                    do { userColor[0].Title = (ConsoleColor)rand.Next(16); } while (userColor[0].Title == userColor[0].BackGroundDefault);
                    do { userColor[0].InputText = (ConsoleColor)rand.Next(16); } while (userColor[0].InputText == userColor[0].BackGroundDefault |
                                                                                 userColor[0].InputText == userColor[0].TextHigh |
                                                                                 userColor[0].InputText == userColor[0].ForeGroundDefault |
                                                                                 userColor[0].InputText == userColor[0].MenuSelectDefault);

                    Console.BackgroundColor = userColor[0].BackGroundDefault;
                    break;

                case 6:     // input text color
                    var f = (int)userColor[0].InputText;
                    f++; if (f == 16) { f = 0; }
                    userColor[0].InputText = (ConsoleColor)f;
                    break;

                default:
                    break;
            }

            //foreach (ConsoleColor textColors in Enum.GetValues(typeof(ConsoleColor)))
            //{
            //}
        }

        public static void Color(TextColors textColor)                     // sets or resets console font color
        {
            // TODO: call with colour name string instead of number
            Console.BackgroundColor = userColor[0].BackGroundDefault;
            //Console.ForegroundColor = foreGroundText;
            switch (textColor)
            {
                case TextColors.Input:

                    Console.ForegroundColor = userColor[0].InputText;
                    break;

                case TextColors.MenuSelect:

                    Console.ForegroundColor = userColor[0].MenuSelectDefault;
                    break;

                case TextColors.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case TextColors.Text:

                    Console.ForegroundColor = userColor[0].TextHigh;
                    break;

                case TextColors.DefaultForeground:                                        // Standard foreground color

                    Console.ForegroundColor = userColor[0].ForeGroundDefault;
                    break;

                case TextColors.DefaultBackground:
                    Console.BackgroundColor = userColor[0].BackGroundDefault;
                    break;

                case TextColors.Inactive:
                    if (Console.BackgroundColor != ConsoleColor.DarkGray)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    break;

                case TextColors.Title:

                    Console.ForegroundColor = userColor[0].Title;
                    break;

                case TextColors.Inverted:
                    Console.ForegroundColor = userColor[0].BackGroundDefault;
                    Console.BackgroundColor = userColor[0].ForeGroundDefault;
                    break;

                default:
                    break;
            }
        }

        public static void DisplayMenu(string title, string menuString, TextColors aColorMenuOption)
        {
            Console.Clear();
            IO.Color(TextColors.Text);
            Console.WriteLine("\n\n=============================================================================");
            IO.Color(TextColors.Title);
            Console.Write("Bakker Vlaaieboer & Zn."); IO.Color(TextColors.Text); Console.Write("{0:dd/MM/yyyy HH:mm}".PadLeft(30, ' '), DateTime.Now);
            IO.Color(TextColors.DefaultForeground);

            if (Login.validPassword)
            {
                IO.Color(TextColors.Input); Console.Write("* Logged in *".PadLeft(25, ' ')); IO.Color(TextColors.Text);
            }
            else
            {
                Console.Write("* Not Logged in *".PadLeft(25, ' '));
            }
            IO.Color(TextColors.Text);
            Console.Write("\n");
            Console.WriteLine("=============================================================================");
            IO.Color(TextColors.MenuSelect); Console.WriteLine(title + "\n");    // menu name

            PrintMenuString(menuString, TextColors.MenuSelect);
            IO.Color(TextColors.Text);
            Console.WriteLine("=============================================================================\n");
            IO.PrintOnConsole("___________________________________________________________________________", 1, 33);
        }

        private static void PrintMenuString(string menuString, TextColors aColorMenuOption)                 // TODO
        {
            IO.Color(TextColors.DefaultForeground);
            var menustringCharArray = menuString.ToCharArray();
            for (int i = 0; i < menuString.Length; i++)
            {
                if (menustringCharArray[i].ToString() == "(")
                {
                    Console.Write(menustringCharArray[i]);      // print the "("
                    i++;
                    do                                          // until ")" is found print every char in color
                    {
                        IO.Color(aColorMenuOption);
                        Console.Write(menustringCharArray[i]);
                        i++;
                    } while (menustringCharArray[i].ToString() != ")");

                    IO.Color(TextColors.DefaultForeground);
                }
                Console.Write(menustringCharArray[i]);
            }
        }

        public static void PrintOnConsole(string aString, int left, int top)  //TODO: capture screenbuffer to display 'windows'
        {
            int currentCursorPosTop = Console.CursorTop;                            // store current cursor pos
            int currentCursorPosLeft = Console.CursorLeft;
            Console.SetCursorPosition(left, top);
            Console.Write(aString);
            Console.SetCursorPosition(currentCursorPosLeft, currentCursorPosTop);   // reset cursorpos
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
            int cursorTop = Console.CursorTop;                             // TODO: Why doesn't Console.GetCursorPosition exist/Work? (see MS docs https://docs.microsoft.com/en-us/dotnet/api/system.console.getcursorposition?view=net-5.0 )
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
                IO.Color(TextColors.Input);
                PrintInputString(showInput, false, inputStringbuilder);
                IO.Color(TextColors.DefaultForeground);
            }
            do
            {
                do
                {
                    inp = Console.ReadKey(true);                            // read 1 key, don't display the readkey input (true)
                    string tempString;
                    if (checkinputString.Contains(inp.KeyChar.ToString()) & indexInStringbuilder <= lengthInputField)         // only accept valid characters other than functions

                    {
                        indexInStringbuilder++;
                        Checkfieldlength(lengthInputField, indexInStringbuilder - 1);

                        if (toUpper)
                        {
                            tempString = inp.KeyChar.ToString().ToUpper();
                        }
                        else
                        {
                            tempString = inp.KeyChar.ToString();
                        }

                        if (inputStringbuilder.Length + 1 < indexInStringbuilder)
                        {
                            inputStringbuilder.Append(tempString);
                        }
                        else
                        {
                            inputStringbuilder.Insert(indexInStringbuilder - 2, tempString);
                        }

                        Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);      // position cursor at start inputfield
                        IO.Color(TextColors.Input);
                        PrintInputString(showInput, false, inputStringbuilder);
                        Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                    }
                    else if (inp.Key == ConsoleKey.Backspace & indexInStringbuilder > 1)
                    {
                        indexInStringbuilder--;
                        Checkfieldlength(lengthInputField, indexInStringbuilder - 1);
                        inputStringbuilder.Remove(indexInStringbuilder - 1, 1);
                        Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);
                        PrintInputString(showInput, true, inputStringbuilder);
                        Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                    }
                    else if (inp.Key == ConsoleKey.Delete)
                    {
                        if (inputStringbuilder.Length > 0 & indexInStringbuilder <= inputStringbuilder.Length)
                        {
                            inputStringbuilder.Remove(indexInStringbuilder - 1, 1);
                            Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);
                            PrintInputString(showInput, true, inputStringbuilder);
                            IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + "       ", 1, 1);
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }
                    }
                    else if (inp.Key == ConsoleKey.LeftArrow ||                         // move cursor
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
                        else if (inp.Key == ConsoleKey.Home)
                        {
                            indexInStringbuilder = 1;
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.End & inputStringbuilder.Length > 0)
                        {
                            indexInStringbuilder = inputStringbuilder.Length + 1;
                            Console.SetCursorPosition(lengthQuestionField + indexInStringbuilder, cursorTop);
                        }

                        IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + "       ", 1, 1);
                    }
                } while (inp.Key != ConsoleKey.Enter & inp.Key != ConsoleKey.Escape);

                if (inputStringbuilder.Length >= minInputLength)
                {
                    checkedValidLength = true;
                    IO.PrintOnConsole("                             ", 1, 1);
                }
                else
                {
                    IO.Color(IO.TextColors.Error);
                    IO.PrintOnConsole("Field cannot be empty", 1, 1);
                    IO.Color(TextColors.DefaultForeground);
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
                IO.Color(TextColors.Text);
                Console.Write(returnString.PadRight(lengthInputField, ' ')); // pad with spaces to length field
            }

            if (lineFeed) { Console.WriteLine(); }

            Checkfieldlength(lengthInputField, 1);                          // reset warning incase cursor was at last position before 'Enter'

            IO.Color(TextColors.DefaultForeground);
            return returnString;
        }

        public static void PrintBoundaries(string displayString,            // textstring of name of field
                                           string fieldValue,               // current value of this field (for edit purposes)
                                           int lengthQuestionField,         // Length of the name of field to be padded
                                           int lengthInputField,            // idem input field
                                           int cursorTop,                   // reset cursor to row
                                           bool active)
        {
            var inactiveColor = (active) ? TextColors.Text : TextColors.Inactive;

            IO.Color(TextColors.DefaultForeground);
            Console.Write(displayString.PadRight(lengthQuestionField, ' '));
            if (fieldValue == "")
            {
                Console.Write("|".PadRight(lengthInputField + 1, ' ') + "|");            // print input field boundaries
            }
            else                                                                         // if an existing field value is passed on, print that
            {
                Console.Write("|");
                IO.Color(inactiveColor); Console.Write(fieldValue.PadRight(lengthInputField, ' '));
                IO.Color(TextColors.DefaultForeground); Console.Write("|");
            }
            Console.SetCursorPosition(lengthQuestionField + 1, cursorTop);                // reset cursorposition to beginning of the input field
            IO.Color(TextColors.DefaultForeground);
        }

        public static DateTime ParseToDateTime(string aDateString)

        {
            DateTime parsedDateHelpstring;
            if (DateTime.TryParse(aDateString, out parsedDateHelpstring))   // Tryparse method passing back two values: bool and out var
            {
                if (CalculateAge(parsedDateHelpstring) > 100 || CalculateAge(parsedDateHelpstring) < 18)      //TODO: move age check to set; of DoB?
                {
                    IO.PrintOnConsole($"Invalid Age: {CalculateAge(parsedDateHelpstring)} ".PadRight(30, ' '), 1, 34);
                    parsedDateHelpstring = DateTime.Parse("01/01/0001");
                }
                else
                {
                    IO.PrintOnConsole($"Parsed date string succesfully to {parsedDateHelpstring:dd-MM-yyyy}", 1, 34);
                }
            }
            else
            {
                // if invalid date, DateTime remains at initialised 01/01/01 value
                IO.PrintOnConsole($"Could not parse date string, set to {parsedDateHelpstring:dd-MM-yy}", 1, 34);
            }

            return parsedDateHelpstring;
        }

        public static int CalculateAge(DateTime aDateTime)
        {
            var age = DateTime.Today.Year - aDateTime.Year;                   // not taking date into account eg. 2021-1997
            int nowMonthandDay = int.Parse(DateTime.Now.ToString("MMdd"));      // convert month and day to int
            int thenMonthandDay = int.Parse(aDateTime.ToString("MMdd"));
            if (nowMonthandDay < thenMonthandDay)
            {
                age--;                                                          // if current date (in MMdd) < date of birth subtract 1 year
            }
            return age;
        }

        public static void Checkfieldlength(int lengthInputField, int indexInStringbuilder)
        {
            if (indexInStringbuilder == lengthInputField)
            {
                IO.Color(IO.TextColors.Error);
                IO.PrintOnConsole("Max field length", 1, 1);
                IO.Color(TextColors.DefaultForeground);
            }
            else
            {
                IO.PrintOnConsole("                             ", 1, 1);
            }
        }

        private static void PrintInputString(bool showInput, bool deltrailspace, StringBuilder inputStringbuilder)
        {
            if (showInput)
            {
                Console.Write(inputStringbuilder);
            }
            else
            {
                Console.Write("".PadRight(inputStringbuilder.Length, '*'));
            }
            if (deltrailspace)
            {
                Console.Write(" ");
            }
        }

        public static List<T> PopulateList<T>(string aFilename) where T : class
        {
            var getaListFromJSON = DeserializeJSONfile<T>(aFilename);
            if (getaListFromJSON != null)                            // file Exists
            {
                var a = new List<T>();
            }
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
                    IO.Color(IO.TextColors.Error);
                    IO.PrintOnConsole($"Error parsing json file{aFilename} {e}", 1, 1);
                    Thread.Sleep(500);
                    IO.Color(TextColors.DefaultForeground);
                }
            }
            else
            {
                IO.PrintOnConsole($"File {aFilename} doesn't exist, creating new file ", 1, 34);
                Thread.Sleep(750);
                IO.PrintOnConsole($"".PadRight(80, ' '), 1, 34);
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
                IO.Color(IO.TextColors.Error);
                IO.PrintOnConsole($"Error writing to file {aFilename} {e}", 1, 34);
                IO.Color(TextColors.DefaultForeground);
                Thread.Sleep(500);
            }
        }
    }
}