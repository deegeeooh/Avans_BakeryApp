using System.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace BakeryConsole
{
    internal class IO
    {
        private static readonly object ConsoleLock = new object();

        private static int warningLength;             // length in ms of system message events

        private static int currentWindowWidth; //= Prefs.GetWindowWidth();
        private static int currentWindowHeight;// = Prefs.GetWindowHeight();
        
        public static void SetWarningLength(int aValueInMs)
        {
            warningLength = aValueInMs;
        }

        public static void SetConsoleDimensions(int aWidth, int aHeight)
        {
            currentWindowWidth = aWidth;
            currentWindowHeight = aHeight;
        }

        public static void DisplayTable(Table aTable, int cursorRow, int cursorColumn, bool resizeWindow)
        {
            int totalWidth = 0;
            foreach (Box box in aTable.Columns)
            {
                totalWidth = totalWidth + box.Width;   
            }

            if (totalWidth <= Console.LargestWindowWidth)
            {
                
                if (totalWidth > currentWindowWidth & resizeWindow )
                {
                    Prefs.ChangeWindowSize(totalWidth - currentWindowWidth, 0);
                    Prefs.ResizeConsoleWindow();
                }
                else if (totalWidth > currentWindowWidth)
                {
                    throw new Exception("Table width is greater than current Window Width");
                }
                               
                
                int nextCursorPosition = 0;
                for (int i = 0; i < aTable.Columns.Count; i++)
                {
                    Box.DrawWindow(aTable.Columns[i], cursorRow, cursorColumn + nextCursorPosition, Prefs.Color.Text, Prefs.Color.Text);
                    nextCursorPosition += aTable.Columns[i].Width - 1;
                }
            }
        }

        public static void DisplayMenu(string title, string menuString, Prefs.Color aColorMenuOption)                  //Cleanup
        {
            currentWindowWidth = Prefs.GetWindowWidth();

            var numberOfMenuItems = menuString.Split('\n').Length;

            lock (ConsoleLock)
            {
                Prefs.SetColor(Prefs.Color.Defaults);
                Prefs.ResizeConsoleWindow();

                Console.Clear();
                PrintOnConsole(Prefs.GetWindowHeight().ToString()+" "+Prefs.GetWindowWidth().ToString(), 0, 0, Prefs.Color.Defaults);
                
                Box topWindow = new Box("", 2, 2, false, "", 1, Prefs.GetWindowWidth(), 2);
                Box.DrawWindow(topWindow, 1, 0,Prefs.Color.Text,Prefs.Color.Text);
                
                Prefs.SetColor(Prefs.Color.Text);
                
                PrintOnConsole(Program.licenseString, 2, 2, Prefs.Color.Title);
                PrintOnConsole (DateTime.Now.ToString(format:"f"), Prefs.GetWindowWidth() - 33, 2, Prefs.Color.Text);
                Console.WriteLine();

                if (Login.validPassword)
                {
                    PrintOnConsole("* Logged in *", Prefs.GetWindowWidth() - 16, 3, Prefs.Color.Input);
                }
                else
                {
                    PrintOnConsole("* Logged out *", Prefs.GetWindowWidth() - 15, 3, Prefs.Color.DefaultForeGround);
                }

                Box menuWindow = new Box( "", 2, 2, true, "", 1, Prefs.GetWindowWidth(), numberOfMenuItems + 1 );
                Box.DrawWindow( menuWindow, 5, 0, Prefs.Color.Text, Prefs.Color.Text );
                
                PrintOnConsole(title, 1, 6, Prefs.Color.MenuSelect);
                
                SetCursorPosition(1, 8);
                PrintMenuString(menuString, Prefs.Color.MenuSelect);
           
                string line = "─".PadRight(Prefs.GetWindowWidth() -1, '─')+"─";
                PrintOnConsole (line, 0, Prefs.GetWindowHeight() - 2,Prefs.Color.Text);
           
                SetCursorPosition(1, numberOfMenuItems + 9);
            }
        }

        private static void PrintMenuString(string menuString, Prefs.Color aColorMenuOption)                 
        {
            var menustringCharArray = menuString.ToCharArray();
            
            Prefs.SetColor(Prefs.Color.DefaultForeGround);
            lock (ConsoleLock)
            {
                for (int i = 0; i < menuString.Length; i++)
                {
                    if (menustringCharArray[i].ToString() == "(")   // NICE make this method compacter
                    {
                        Console.Write(menustringCharArray[i]);      // print the "("
                        i++;
                        do                                          // until ")" is found print every char in color
                        {
                            Prefs.SetColor(aColorMenuOption);
                            Console.Write(menustringCharArray[i]);
                            i++;
                        } while (menustringCharArray[i].ToString() != ")");
                    }
                    
                    if(menustringCharArray[i].ToString() == "\n")
                    {
                        Prefs.SetColor(Prefs.Color.DefaultForeGround);
                        Console.Write(menustringCharArray[i]);
                        SetCursorPosition (Console.CursorLeft + 1, Console.CursorTop);  
                        PrintOnConsole(menustringCharArray[i].ToString(), Console.CursorLeft + 1, Console.CursorTop, Prefs.Color.DefaultForeGround);
                    }else
                    {
                        Prefs.SetColor(Prefs.Color.DefaultForeGround);
                        Console.Write(menustringCharArray[i]);
                    }
                }
            }
        }

        public static void PrintOnConsole(string aString, int column, int row, Prefs.Color aColor)  
        {
            lock (ConsoleLock)
            {
                Prefs.SetColor(aColor);
                int currentCursorPosTop = Console.CursorTop;                                // store current cursor pos
                int currentCursorPosLeft = Console.CursorLeft;
                {
                    if (Console.CursorTop <= Prefs.GetWindowHeight() - 1)
                    {
                        Console.SetCursorPosition(column, row);
                        Console.Write(aString);
                    }
                    Console.SetCursorPosition(currentCursorPosLeft, currentCursorPosTop);   // restore cursorpos
                } 
            }
        }
        
        public static void SystemMessage(string aString, bool aWarning)     
        {
            Prefs.SetWarningColor(aWarning);
            System.Threading.Timer aTimer = new System.Threading.Timer(EventPrint, aString, 100, Timeout.Infinite);
        }

        public static void EventPrint(Object state)
        {
            var aThread = new Thread(EventTask) { IsBackground = true };
            aThread.Start();
            void EventTask()
            {
                    string a = "(System): " + state;
                    lock (ConsoleLock)
                    {
                        PrintOnConsole(a.PadRight(Prefs.GetWindowWidth() - 1, ' '), 0, Prefs.GetWindowHeight() - 1, Prefs.Color.SystemMessage);
                    }
                    Thread.Sleep(warningLength);
                    lock (ConsoleLock)
                    {
                        PrintOnConsole("".PadRight(Prefs.GetWindowWidth() - 1, ' '), 0, Prefs.GetWindowHeight() - 1, Prefs.Color.Defaults);
                    }
            }
        }

        public static void SetCursorPosition(int aCursorColumn, int aCursorRow)
        {
            lock (ConsoleLock)
            {
                try
                {
                    Console.CursorTop = aCursorRow;
                    Console.CursorLeft = aCursorColumn;
                }
                catch (Exception e)         // for when cursor is positioned outside console buffer
                {
                    SystemMessage($"Exception while setting cursor {e}".PadRight(Prefs.GetWindowWidth() - 1,' '), true);
                }
            }
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
                                      int minInputLength,            // minimal inputlength required
                                      int cursorOffset)              // offset from left cursorpos
        {
            //Console.WriteLine("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
            //Console.WriteLine("         1         2         3         4         5         6         7         8         9         1");

            // declare local variables

            int cursorLeft = Console.CursorLeft;                           // store current cursorposition, left and top
            int cursorTop  = Console.CursorTop;                             
            
            StringBuilder inputStringbuilder = new StringBuilder();        // stringbuilder to append the single input characters to
            ConsoleKeyInfo inp               = new ConsoleKeyInfo();
            int indexInStringbuilder         = 1;                          // where is the cursor in het stringbuilder
            string returnString;
            bool checkedValidLength          = false;

            // display field boundaries with padded spaces

            Console.SetCursorPosition(cursorOffset, cursorTop);
            //Console.SetCursorPosition(cursorColumn, cursorTop);

            if (showBoundaries)
            {
                PrintBoundaries(fieldName, fieldValue, "", lengthQuestionField, lengthInputField, cursorTop, cursorOffset, true);
            }

            if (fieldValue != "")                                          // if a edit value is given, assign to inputStringbuilder and print it
            {
                inputStringbuilder.Append(fieldValue);
                indexInStringbuilder = inputStringbuilder.Length + 1;      // cursor 1 position after string
                //Checkfieldlength(lengthInputField, indexInStringbuilder - 1);
                PrintInputString(showInput, false, inputStringbuilder, Prefs.Color.Input, cursorOffset);
            }
            do
            {
                do
                {
                    IO.PrintOnConsole((indexInStringbuilder.ToString() + " " + inputStringbuilder.ToString()).PadRight(79, ' '), 0 + cursorOffset, 0, Prefs.Color.Defaults);
                    inp = Console.ReadKey(true);                            // read 1 key, don't display the readkey input (true)
                    string tempString;
                    
                    if (checkinputString.Contains(inp.KeyChar.ToString()) & indexInStringbuilder <= lengthInputField)         // only accept valid characters other than functions

                    {
                        if (indexInStringbuilder <= lengthInputField)
                        {
                            if (toUpper)
                            {
                                tempString = inp.KeyChar.ToString().ToUpper();
                            }
                            else
                            {
                                tempString = inp.KeyChar.ToString();
                            }

                            if (inputStringbuilder.Length < indexInStringbuilder)                                   // append when cursor is the end
                            {
                                inputStringbuilder.Append(tempString);
                                indexInStringbuilder++;                                                             // index embedded in if{} because 
                            }                                                                                       // input might not be valid with insert =>
                            else
                            {
                                if (inputStringbuilder.Length < lengthInputField)                                   // Insert only as long as not max inputlenght
                                {
                                    inputStringbuilder.Insert(indexInStringbuilder - 1, tempString);                //INSERT CHARACTER
                                    indexInStringbuilder++;                                                         // and move cursor one to the right
                                }
                                else
                                {
                                    IO.SystemMessage("Maximum input length", false);
                                }
                            } 
                        }else
                        {
                            IO.SystemMessage("Maximum input length", false);
                        }
                        IO.SetCursorPosition(lengthQuestionField + 1 + cursorOffset, cursorTop);                               // position cursor at start inputfield
                        PrintInputString(showInput, false, inputStringbuilder,Prefs.Color.Input, cursorOffset);
                        IO.SetCursorPosition(lengthQuestionField + indexInStringbuilder + cursorOffset, cursorTop);
                    }
                    else if (inp.Key == ConsoleKey.Backspace & indexInStringbuilder > 1)                        //BACKSPACE
                    {
                        indexInStringbuilder--;
                        Checkfieldlength(lengthInputField, indexInStringbuilder - 1);
                        inputStringbuilder.Remove(indexInStringbuilder - 1, 1);
                        
                        IO.SetCursorPosition(lengthQuestionField + 1 + cursorOffset, cursorTop);
                        PrintInputString(showInput, true, inputStringbuilder,Prefs.Color.Input, cursorOffset);
                        IO.SetCursorPosition(lengthQuestionField + indexInStringbuilder + cursorOffset, cursorTop);
                    }
                    else if (inp.Key == ConsoleKey.Delete)                                                      //DELETE
                    {
                        if (inputStringbuilder.Length > 0 & indexInStringbuilder <= inputStringbuilder.Length)
                        {
                            inputStringbuilder.Remove(indexInStringbuilder - 1, 1);
                            IO.SetCursorPosition(lengthQuestionField + 1 + cursorOffset, cursorTop);
                            PrintInputString(showInput, true, inputStringbuilder,Prefs.Color.Input, cursorOffset);
                            IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + "       ", 0, 0,Prefs.Color.Defaults);
                            IO.SetCursorPosition(lengthQuestionField + indexInStringbuilder + cursorOffset, cursorTop);
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
                            IO.SetCursorPosition(lengthQuestionField + indexInStringbuilder + cursorOffset, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.RightArrow & indexInStringbuilder <= inputStringbuilder.Length)
                        {
                            indexInStringbuilder++;
                            IO.SetCursorPosition(lengthQuestionField + indexInStringbuilder + cursorOffset, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.Home)                                                    // HOME
                        {
                            indexInStringbuilder = 1;
                            IO.SetCursorPosition(lengthQuestionField + indexInStringbuilder + cursorOffset, cursorTop);
                        }
                        else if (inp.Key == ConsoleKey.End & inputStringbuilder.Length > 0)                     // END
                        {
                            indexInStringbuilder = inputStringbuilder.Length + 1;
                            IO.SetCursorPosition(lengthQuestionField + indexInStringbuilder + cursorOffset, cursorTop);
                        }

                       // IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + "       ", 0, 0,Color.TextColors.Defaults);
                    }
                    else if (checkinputString.Contains(inp.KeyChar.ToString()))              // valid input but end of field 
                    {                                                                        // since we already tested on valid input
                        IO.SystemMessage("Maximum input length", false);                     // and field length in first if statement
                    }
                    //IO.PrintOnConsole(indexInStringbuilder.ToString() + " " + inputStringbuilder + "  "+  lengthInputField.ToString() + "      ", 0, 0, Color.TextColors.Defaults);
                
                } while (inp.Key != ConsoleKey.Enter & inp.Key != ConsoleKey.Escape);

                if (inputStringbuilder.Length >= minInputLength)//TODO obsolete
                {
                    checkedValidLength = true;
                    IO.PrintOnConsole("                             ", 0, 0,Prefs.Color.Defaults);
                }
                else
                {
                    SystemMessage($"Field has to be {minInputLength} characters long", true);
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
            IO.SetCursorPosition(lengthQuestionField + 1 + cursorOffset, cursorTop);
            if (showInput)                                                  // not with password
            {
                Prefs.SetColor(Prefs.Color.Text);
                Console.Write(returnString.PadRight(lengthInputField, ' ')); // pad with spaces to length field
            }

            if (lineFeed) { Console.WriteLine(); }

            //Checkfieldlength(lengthInputField, 1);                          // reset warning incase cursor was at last position before 'Enter'

            Prefs.SetColor(Prefs.Color.Text);
            return returnString;
        }
                
        public static void PrintBoundaries(string displayString,            // textstring of name of field
                                           string fieldValue,               // current value of this field (for edit purposes)
                                           string aHighlight,               // part of string to be highlighted (when searching)
                                           int lengthQuestionField,         // Length of the name of field to be padded
                                           int lengthInputField,            // idem input field
                                           int cursorRow,                   // reset cursor to row
                                           int cursorCol,
                                           bool active)
        {
            var inactiveColor = (active) ? Prefs.Color.Text : Prefs.Color.Inactive;
            Prefs.SetColor(Prefs.Color.DefaultForeGround);
            lock (ConsoleLock)
            {
                Console.SetCursorPosition (cursorCol,cursorRow);
                Console.Write(displayString.PadRight(lengthQuestionField, ' '));
                if (fieldValue == "")
                {
                    Console.Write("[".PadRight(lengthInputField + 1, ' ') + "]");            // print input field boundaries

                    if (Debugger.IsAttached)
                    {
                        Console.Write("".PadRight(Prefs.GetWindowWidth() - lengthQuestionField - lengthInputField - 4, 'X')); // fillout with " " to edge
                    }
                    else
                    {
                        Console.Write("".PadRight(Prefs.GetWindowWidth() - lengthQuestionField - lengthInputField - 4, ' '));
                    }

                }
                else                                                                         // if an existing field value is passed on, print that
                {
                    //Console.Write("[");
                    
                    Console.Write("[".PadRight(lengthInputField + 1, ' ') + "]");
                    Console.SetCursorPosition (cursorCol + lengthQuestionField + 1,cursorRow);
                    
                    var charsInfieldValue = fieldValue.ToCharArray();
                    if (fieldValue != "01/01/0001" & fieldValue != "01-01-0001")              //TODO: this is really bad, find another solution
                    {
                        
                        //fieldValue.
                        
                        if (aHighlight != "" & fieldValue.Contains(aHighlight))                                   // check if it is in there anyway
                        {
                            for (int i = 0; i < fieldValue.Length; i++)
                            {

                                if (fieldValue.IndexOf(aHighlight, i) != -1)
                                {
                                    if (i == fieldValue.IndexOf(aHighlight, i))
                                    {
                                                                                
                                        Prefs.SetColor(Prefs.Color.Inverted);
                                        Console.Write(aHighlight);
                                        i += aHighlight.Length - 1;
                                    }else
                                    {
                                        Prefs.SetColor(inactiveColor);
                                        Console.Write(charsInfieldValue[i].ToString());
                                    }

                                    
                                }else
                                {
                                    Prefs.SetColor(inactiveColor);
                                    Console.Write(charsInfieldValue[i].ToString());
                                }
                               
                            }
                        }else 
                        {
                            Prefs.SetColor(inactiveColor); Console.Write(fieldValue.PadRight(lengthInputField, ' ')); 
                        }
                    }else
                    {
                        Prefs.SetColor(inactiveColor); Console.Write("".PadRight(lengthInputField, ' ')); 
                    }
                }
            }
            IO.SetCursorPosition(lengthQuestionField + 1 + cursorCol, cursorRow);                // reset cursorposition to beginning of the input field
            Prefs.SetColor(Prefs.Color.DefaultForeGround);
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

        private static void PrintInputString(bool showInput, bool deltrailspace, StringBuilder inputStringbuilder, Prefs.Color aColor, int cursorOffset)         //TODO a ccept cursorposition & refactor
        {
            lock (ConsoleLock)
            {
                //Console.SetCursorPosition(Console.CursorLeft + cursorOffset, Console.CursorTop);
                Prefs.SetColor(aColor);
                if (showInput) { Console.Write(inputStringbuilder); }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Insert(0,"*",inputStringbuilder.Length);
                    Console.Write(stringBuilder);
                }
                
                
                //else { Console.Write("".PadRight(inputStringbuilder.Length, '*')); }

                if (deltrailspace) { Console.Write(" "); }
            }
        }
    }
}