using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BakeryConsole
{
    class Product : RecordManager     
                        //NICE: create product orders class
    {
        // class variables
        private static int    lengthQuestionField    = 30;
        private static string checkinputStringAlpha  = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";
        private static string checkinputStringNum    = "0123456789" + NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;             //depending on OS region settings
        private static string _DescriptionFieldName  = "Product Name"; 
        

        private static int[,] fieldProperties = { { 0,   1,  1 },
                                                  { 1,  10,  1 },
                                                  { 2,  10,  0 },
                                                  { 3,  10,  0 },
                                                  { 4,  10,  0 },
                                                  { 5,  10,  0 } };

        private static String[] fieldNames =      {"Product type:",         //0   NICE: make prod. type class
                                                   "Production Date:",      //1
                                                   "Expiration Date:",      //2
                                                   "Sales Price:",          //3
                                                   "Cost Price:",           //4
                                                   "Stock" };               //5

        //public int RecordCounter        { get; set; }             =>
        //public bool Active              { get; set; }             =>  inherited from base class
        //public List<Mutation> Mutations { get; set; }             =>

        //public string ID { get; set; }
        //public string Name              { get; set; }
        
        public string ProductType       { get; set; }
        public DateTime ProductionDate  { get; set; }
        public DateTime ExpirationDate  { get; set; }
        public float SalesPrice         { get; set; }
        public float CostPrice          { get; set; }
        public int Stock                { get; set; }


        public Product() : base (_DescriptionFieldName)                                  // Main Constructor, add new Record
        {
            
            ProductType    =                    IO.GetInput(fieldNames[0], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2], 1);
            ProductionDate = IO.ParseToDateTime(IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2], 1),false);
            ExpirationDate = IO.ParseToDateTime(IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2], 1), false);
            
            while (ExpirationDate != null & ExpirationDate.CompareTo(ProductionDate) <= 0)
                
            {
                IO.SetCursorPosition(0, Console.CursorTop - 1);
                IO.SystemMessage("Expiration date should be after Production date", false);
                ExpirationDate = IO.ParseToDateTime(IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2], 1), false);
            }
            string getSalesPrice = IO.GetInput(fieldNames[3], "", checkinputStringNum, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2], 1);
            if (getSalesPrice != "")
            {
                SalesPrice = float.Parse(getSalesPrice);
            }
            else
            {
                SalesPrice = 0;
            }
            string getCostPrice = IO.GetInput(fieldNames[4], "", checkinputStringNum, lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2], 1);
            if (getCostPrice != "")
            {
                CostPrice = float.Parse(getCostPrice);
            }
            else
            {
                CostPrice = 0;
            }

            Stock          = Int32.Parse(IO.GetInput(fieldNames[5], "", checkinputStringNum, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2], 1));
           
        }

        public Product(bool clearForm, bool _Activatordummy) : base (clearForm, _DescriptionFieldName, true)                                   // Constructor for displaying clear input form
        {
            var cursor = Console.CursorTop;
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, 1, false); Console.WriteLine(); cursor++;
                }
        }

        public Product(Product aProduct, bool displayOnly) : base (aProduct, displayOnly, _DescriptionFieldName, true)    // Constructor for edit and display existing record
        {
            if (!displayOnly)  //EDIT
            {
                RecordCounter = aProduct.RecordCounter;
               
                ProductType    = IO.GetInput(fieldNames[0], aProduct.ProductType, checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2], 1);
                ProductionDate = IO.ParseToDateTime(IO.GetInput(fieldNames[1], aProduct.ProductionDate.ToString("dd/MM/yyyy"), checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2], 1), false);
                ExpirationDate = IO.ParseToDateTime(IO.GetInput(fieldNames[2], aProduct.ExpirationDate.ToString("dd/MM/yyyy"), checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2], 1), false);

                while (ExpirationDate != null & ExpirationDate.CompareTo(ProductionDate) <= 0)
                {
                    IO.SetCursorPosition(0, Console.CursorTop - 1);
                    IO.SystemMessage("Expiration date should be after Production date", false);
                    ExpirationDate = IO.ParseToDateTime(IO.GetInput(fieldNames[2], aProduct.ExpirationDate.ToString("dd/MM/yyyy"), checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2], 1), false);
                }
                string getSalesPrice = IO.GetInput(fieldNames[3], aProduct.SalesPrice.ToString(format: "F2"), checkinputStringNum, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2], 1);
                          SalesPrice = (getSalesPrice!="") ? float.Parse(getSalesPrice) : 0;

                string getCostPrice  = IO.GetInput(fieldNames[4], aProduct.CostPrice.ToString(format: "F2"), checkinputStringNum, lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2], 1);
                           CostPrice = (getCostPrice != "") ? float.Parse(getCostPrice) : 0;
                
                Stock =               Int16.Parse(IO.GetInput(fieldNames[5], aProduct.Stock.ToString(), checkinputStringNum, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2], 1));
                
                this.Mutations = aProduct.Mutations;
                
                CheckMutations(aProduct, aProduct.ProductType,               this.ProductType,               fieldNames[0], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.ProductionDate.ToString(), this.ProductionDate.ToString(), fieldNames[1], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.ExpirationDate.ToString(), this.ExpirationDate.ToString(), fieldNames[2], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.SalesPrice.ToString(),     this.SalesPrice.ToString(),     fieldNames[3], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.CostPrice.ToString(),      this.CostPrice.ToString(),      fieldNames[4], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.Stock.ToString(),          this.Stock.ToString(),          fieldNames[5], aProduct.Mutations.Count);
            }
            else              // display only
            {
                var cursorColumn = Console.CursorTop;

                IO.PrintBoundaries(fieldNames[0], aProduct.ProductType, lengthQuestionField, fieldProperties[0, 1], cursorColumn, 1, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[1], aProduct.ProductionDate.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[1, 1], cursorColumn, 1, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[2], aProduct.ExpirationDate.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[2, 1], cursorColumn, 1, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[3], aProduct.SalesPrice.ToString("F2").PadLeft(fieldProperties[3 ,1],' ') , lengthQuestionField, fieldProperties[3, 1], cursorColumn, 1, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[4], aProduct.CostPrice.ToString("F2").PadLeft(fieldProperties[4, 1], ' '), lengthQuestionField, fieldProperties[4, 1], cursorColumn, 1, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[5], aProduct.Stock.ToString().PadLeft(fieldProperties[5, 1], ' '), lengthQuestionField, fieldProperties[5, 1], cursorColumn, 1, aProduct.Active); Console.WriteLine(); cursorColumn++;
            
            }
            
        }

        [JsonConstructor]                                              
        public Product(Int64 JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

    }
}
