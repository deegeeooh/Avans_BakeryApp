using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryConsole
{
    internal class Table

    {
        public List<Box> Columns { get; private set; }

        public Table(List<Box> boxes)
        {
            Columns = boxes;
        }

        public static Table ConstrucStdTable(string title, bool headerRow, string header, int numberOfColumns, int width, int height)
        {
            List<Box> boxList = new List<Box>();

            if (width > Console.LargestWindowWidth)
            {
                throw new Exception("Table can not be wider than screen");
            }
                for (int i = 0; i < numberOfColumns; i++)
                {
                    if (i == 0)
                    {
                        if (numberOfColumns == 1)
                        {
                            boxList.Add(new Box(title, 1, 1, headerRow, header, 1, width, height));
                        }else
                        {
                            boxList.Add(new Box(title, 1, 3, headerRow, header, 1, width, height));
                        }
                    } else if (i == numberOfColumns - 1)
                    {
                        boxList.Add(new Box("", 1, 5, headerRow, header, 1, width, height));
                    } else
                    {
                        boxList.Add(new Box("", 1, 4, headerRow, header, 1, width, height));
                    }
                }
            return new Table(boxList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="titlePos"></param>
        /// <param name="headerRow"></param>
        /// <param name="header"></param>
        /// <param name="headerPos"></param>
        /// <param name="numberOfColumns"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        
        public static Table ConstructVarTable(string[] title, int[] titlePos, bool headerRow, string[] header, int[] headerPos, int[] width, int height)
        {
            List<Box> boxList   = new List<Box>();
            var numberOfColumns = title.Length;
           
            if (width.Sum() > Console.LargestWindowWidth)
            {
                throw new Exception("Table can not be wider than screen");
            }
                for (int i = 0; i < numberOfColumns; i++)
                {
                    if (i == 0)
                    {
                        boxList.Add(new Box(title[i], titlePos[i], 3, headerRow, header[i], headerPos[i], width[i], height));
                    } else if (i == numberOfColumns - 1)
                    {
                        boxList.Add(new Box(title[i], titlePos[i], 5, headerRow,  header[i], headerPos[i], width[i], height));
                    } else
                    {
                        boxList.Add(new Box(title[i], titlePos[i], 4, headerRow,  header[i], headerPos[i], width[i], height));
                    }
                }
            return new Table(boxList);
        }




    }
}
