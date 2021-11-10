using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryConsole
{
    class Products      //TODO: finish products
                        //NICE: create product orders class
    {
        private static int lenghtQuestionField = 30;
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";
        private static string checkinputStringNum = "0123456789";

        private static int[,] fieldProperties = { { 0, 8, 1 },
                                              { 1, 45, 1 },
                                              { 2, 3, 0 },
                                              { 3, 5, 1 },
                                              { 4, 5, 1 } };

        private static String[] fieldNames = { "ID:",         //0
                                               "Name:",       //1
                                               "Type:",       //2
                                               "Price:",      //3
                                               "Cost:" };     //4
        public int RecordCounter { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime ProducionDate { get; set; }
        public int SalesPrice { get; set; }
        public int CostPrice { get; set; }
        public int Stock { get; set; }

        public Products()
        {

        }
    }
}
