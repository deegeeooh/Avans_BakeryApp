using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryConsole
{
    internal class Window
    {
        public string Title { get; set; }
        public int TitlePosition { get; set; }
        public int Type { get; set; }
        public bool HeaderRow { get; set; }
        public string HeaderTitle { get; set; }
        public int Width { get; set; }
        public int Height  { get; set; }

        public Window(string title, int titlePosition, int type, bool headerRow,string headerTitle, int width, int height)
        {
            Title = title;
            TitlePosition = titlePosition;  
            Type = type;
            HeaderRow = headerRow;
            HeaderTitle = headerTitle;
            Width = width;
            Height = height;
        }


    }

    

}
