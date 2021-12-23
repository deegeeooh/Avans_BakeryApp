using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryConsole
{
    internal class Box
    {
        private int     _titlePosition;
        private string  _title;
        private int     _width;
        private int     _height;
        private bool    _headerRow;
        private string  _headerTitle;
        private int     _headerPos;
        private int     _type;

        private static string topLeft;
        private static string topRight;
        private static string bottomLeft;
        private static string bottomRight;
        private static string horizontalTop;
        private static string horizontalBot;
        private static string vertical;
        private static string headerLeft;
        private static string headerRight;
        private static string headerHor;
        private static readonly object ConsoleLock = new object();

        public string Title
        {
            get { return _title; }

            private set
            {
                if (value != null)
                {
                    if (value.Length <= _width - 2 )
                        {
                            _title = value;
                        }
                        else
                        {
                            _title = value.Substring(0, _width - 2);    
                        }
                }else
                {
                    _title = "";
                }
                
            }
        }

        public int TitlePos
        {
            get { return _titlePosition; }

            private set
            {
                if (value >= 0 && value < 3)
                {
                    _titlePosition = value;
                } else
                {
                    throw new ArgumentOutOfRangeException("Title position should be 0,1 or 2");
                }
            }
        }
                 
        public int Type             
        { 
            get { return _type; }

            private set
            {
                if (value >= 0 && value < 6)
                {
                    _type = value;
                }else
                {
                    throw new ArgumentOutOfRangeException("Type is valid 0 - 5");
                }
            }
        }
        public bool HeaderRow      
        {
            get { return _headerRow;  }
            
            private set
            {
                _headerRow = value;
            }
        }

        public string HeaderTitle
        {
            get { return _headerTitle;  }

            private set
            {
                if (value != null)
                {
                    if (value.Length <= _width - 2)
                    {
                        _headerTitle = value;
                    } else
                    {
                        _headerTitle = value.Substring(0, _width - 2);
                    } 
                }else
                {
                    _headerTitle = "";
                }

            }
        }

        public int Width            
        { 
            get { return _width; }

            private set
            {
                if (value < Console.LargestWindowWidth)
                {
                    _width = value;
                }else
                {
                    throw new ArgumentOutOfRangeException("Width cannot be larger than LargestWindowWidth");
                }
            }
        }
        public int Height           
         { 
            get { return _height; }

            private set
            {
                if (value < Console.LargestWindowHeight)
                {
                    _height = value;
                }else
                {
                    throw new ArgumentOutOfRangeException("Height cannot be larger than LargestWindowHeight");
                }
            }
        }

        public int HeaderPos
        {
            get { return _headerPos; }
        
            private set 
            {
                if (value >= 0 && value < 3)
                {
                    _headerPos = value;
                }else
                {
                    throw new Exception("Header position should be 0-2 ");
                }
            }
        }


        /// <summary>
        /// Sets the window object to be displayed with DrawWindow() 
        /// </summary>
        /// <param name="title">Window title string</param>
        /// <param name="titlePosition">0 for left, 1 for centered, 2 for right alignment</param>
        /// <param name="type">0: double lines, 1 single lines, 2, double/single, 3-5: left, middle, right panes for multiple columns</param>
        /// <param name="headerRow">true: separate single header row</param>
        /// <param name="headerTitle">header cell title string</param>
        /// <param name="headerPosition">header title alignment</param>
        /// <param name="width">set the width of the Box</param>
        /// <param name="height">set the Height of the Box</param>
        /// 

        public Box(string title, int titlePosition, int type, bool headerRow, string headerTitle, int headerPosition, int width, int height)
        {
            Width           = width;
            Height          = height;
            Title           = title;
            TitlePos        = titlePosition;  
            Type            = type;
            HeaderRow       = headerRow;
            HeaderTitle     = headerTitle;
            HeaderPos       = headerPosition;
        }
        
        public static void DrawWindow ( Box aWindow,
                                        int cursorRow,
                                        int cursorCol,
                                        Prefs.Color titleColor,
                                        Prefs.Color windowColor )
        
        {
            var OrgCursorCol = Console.CursorLeft;
            var OrgCursorRow = Console.CursorTop;
            
            topLeft = "╔";
            topRight = "╗";
            bottomLeft = "╚";
            bottomRight = "╝";
            horizontalTop = "═";
            horizontalBot = "═";
            vertical = "║";
            headerLeft = "╠";
            headerRight = "╣";
            headerHor = "═";
            
            switch (aWindow.Type)
            {
             //         0                     1                   2                 3                   4                   5
             //╔═════<title >═════╗ ┌──────────────────┐╔<123456>══════════╗╔═════<123456>═════╦═════<123456>═════╦═════<123456>═════╗
             //║     headerTitle  ║ │                  ││                  ││                  │                  │                  │
             //╠══════════════════╣ ├──────────────────┤├──────────────────┤├──────────────────┼──────────────────┼──────────────────┤
             //║                  ║ │                  ││                  ││                  │                  │                  │
             //║                  ║ │                  ││                  ││                  │                  │                  │
             //╚══════════════════╝ └──────────────────┘└──────────────────┘└──────────────────┴──────────────────┴──────────────────┘
             
                case 0:
                    //topLeft = "╔";
                    //topRight = "╗";
                    //bottomLeft = "╚";
                    //bottomRight = "╝";
                    //horizontalTop = "═";
                    //horizontalBot = "═";
                    //vertical = "║";
                    //headerLeft = "╠";
                    //headerRight = "╣";
                    //headerHor = "═";
                    break;

                case 1:
                    topLeft = "┌";
                    topRight = "┐";
                    bottomLeft = "└";
                    bottomRight = "┘";
                    horizontalTop = "─";
                    horizontalBot = "─";
                    vertical = "│";
                    headerLeft = "├";
                    headerRight = "┤";
                    headerHor = "─";
                    break;

                case 2:
                    //topLeft = "╔";
                    //topRight = "╗";
                    bottomLeft = "└";
                    bottomRight = "┘";
                    //horizontalTop = "═";
                    horizontalBot = "─";
                    vertical = "│";
                    headerLeft = "├";
                    headerRight = "┤";
                    headerHor = "─";

                    break;
                case 3:
                    //topLeft = "╔";
                    topRight = "╦";
                    bottomLeft = "└";
                    bottomRight = "┴";
                    //horizontalTop = "═";
                    horizontalBot = "─";
                    vertical = "│";
                    headerLeft = "├";
                    headerRight = "┼";
                    headerHor = "─";

                    break;
                case 4:
                    topLeft = "╦";
                    topRight = "╦";
                    bottomLeft = "┴";
                    bottomRight = "┴";
                    //horizontalTop = "═";
                    horizontalBot = "─";
                    vertical = "│";
                    headerLeft = "┼";
                    headerRight = "┼";
                    headerHor = "─";

                    break;

                case 5:
                    topLeft = "╦";
                    //topRight = "╗";
                    bottomLeft = "┴";
                    bottomRight = "┘";
                    //horizontalTop = "═";
                    horizontalBot = "─";
                    vertical = "│";
                    headerLeft = "┼";
                    headerRight = "┤";
                    headerHor = "─";

                    break;

                default:
                    break;
            }       // determine box shape
            lock (ConsoleLock)

            {
                IO.SetCursorPosition(cursorCol, cursorRow);
                Prefs.SetColor(windowColor);

                Console.Write(topLeft + (new StringBuilder().Insert(0, horizontalTop, aWindow.Width - 2).ToString()) + topRight);

                IO.PrintOnConsole(aWindow.Title, cursorCol + CalcStringPosition(aWindow.TitlePos, aWindow.Title), cursorRow, titleColor);
                
                Prefs.SetColor(windowColor);

                cursorRow++;
        
                for (int i = 0; i < aWindow.Height; i++)
                {
                    IO.SetCursorPosition(cursorCol, cursorRow + i);
                    if (i == 1 && aWindow.HeaderRow)
                    {
                        Console.Write(headerLeft + (new StringBuilder().Insert(0, headerHor, aWindow.Width - 2).ToString()) + headerRight);
                        if (aWindow.HeaderTitle != "" )
                        {
                            IO.SetCursorPosition(cursorCol + CalcStringPosition(aWindow.HeaderPos, aWindow.HeaderTitle), Console.CursorTop - 1);
                            Console.Write(aWindow.HeaderTitle);
                        }
                    } else
                        {
                            Console.Write(vertical + (new StringBuilder().Insert(0, " ", aWindow.Width - 2).ToString()) + vertical);
                        }
                }
                IO.SetCursorPosition(cursorCol, Console.CursorTop);
                Console.Write(bottomLeft + (new StringBuilder().Insert(0, horizontalBot, aWindow.Width - 2).ToString()) + bottomRight);
                
                IO.SetCursorPosition (OrgCursorCol, OrgCursorRow);
                Prefs.SetColor(Prefs.Color.Defaults);
            }

            int CalcStringPosition(int anInt, string aString)
            {
                int stringPos = 0;
                switch (anInt)
                {
                    case 0:     //left
                        stringPos = 1;
                        break;
                    case 1:     //middle
                        stringPos = (aWindow.Width / 2) - (aString.Length / 2);
                        break;
                    case 2:     //right
                        stringPos = aWindow.Width - aString.Length - 1;
                        break;
                    default:
                        break;
                }
                return stringPos;
            }
        }
    }
}
