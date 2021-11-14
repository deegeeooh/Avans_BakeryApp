using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BakeryConsole
{
    class Product : RecordManager     //TODO: finish products
                        //NICE: create product orders class
    {
        // class variables
        private static int    lengthQuestionField    = 30;
        private static string checkinputStringAlpha  = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";

        //string nfi = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
       
        private static string checkinputStringNum    = "0123456789" + NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;             //depending on OS region settings
        //private static int    totalRecords = 0;
        

        private static int[,] fieldProperties = { { 0,   8,  1 },
                                                  { 1,  45,  1 },
                                                  { 2,   1,  1 },
                                                  { 3,  10,  1 },
                                                  { 4,  10,  1 },
                                                  { 5,  10,  0 },
                                                  { 6,  10,  0 },
                                                  { 7,  10,  0 } };

        private static String[] fieldNames =      { "ID:",                  //0
                                                   "Name:",                 //1
                                                   "Product type:",         //2   NICE: make prod. type class
                                                   "Production Date:",      //3
                                                   "Expiration Date:",      //4
                                                   "Sales Price:",          //5
                                                   "Cost Price:",           //6
                                                   "Stock" };               //7

        //public int RecordCounter        { get; set; }             =>
        //public bool Active              { get; set; }             =>  inherited from base class
        //public List<Mutation> Mutations { get; set; }             =>

        public string ID { get; set; }
        public string Name              { get; set; }
        public string ProductType       { get; set; }
        public DateTime ProductionDate  { get; set; }
        public DateTime ExpirationDate  { get; set; }
        public float SalesPrice         { get; set; }
        public float CostPrice          { get; set; }
        public int Stock                { get; set; }


        public Product() : base ()                                  // Main Constructor, add new Record
        {
            //totalRecords++;
            //RecordCounter  = totalRecords;
            
            Name           = IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
            ProductType    = IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
            ProductionDate = IO.ParseToDateTime(IO.GetInput(fieldNames[3], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]),false);
            ExpirationDate = IO.ParseToDateTime(IO.GetInput(fieldNames[4], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2]), false);
            
            while (ExpirationDate != null & ExpirationDate.CompareTo(ProductionDate) <= 0)
                
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                IO.SystemMessage("Expiration date should be after Production date", false);
                ExpirationDate = IO.ParseToDateTime(IO.GetInput(fieldNames[4], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2]), false);
            }
            string getSalesPrice = IO.GetInput(fieldNames[5], "", checkinputStringNum, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2]);
            if (getSalesPrice != "")
            {
                SalesPrice = float.Parse(getSalesPrice);
            }
            else
            {
                SalesPrice = 0;
            }
            string getCostPrice = IO.GetInput(fieldNames[6], "", checkinputStringNum, lengthQuestionField, fieldProperties[6, 1], false, true, true, true, true, fieldProperties[6, 2]);
            if (getCostPrice != "")
            {
                CostPrice = float.Parse(getCostPrice);
            }
            else
            {
                CostPrice = 0;
            }

            Stock          = Int32.Parse(IO.GetInput(fieldNames[7], "", checkinputStringNum, lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]));
            ID             = ConstructID(this);
            Active         = true;

            CheckMutations (this, " ", "[Created:]", "", 0);
        }

        public Product(bool clearForm) : base (clearForm)                                   // Constructor for displaying clear input form
        {
            var cursor = Console.CursorTop;
            if (clearForm)
            {
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
                }
            }
        }

        public Product(Product aProduct, bool displayOnly) : base (aProduct, displayOnly)    // Constructor for edit and display existing record
        {
            if (!displayOnly)  //EDIT
            {
                RecordCounter = aProduct.RecordCounter;
                
                
                Name =                              IO.GetInput(fieldNames[1], aProduct.Name, checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);      //TODO iterate through fields
                ProductType =                       IO.GetInput(fieldNames[2], aProduct.ProductType, checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
                ProductionDate = IO.ParseToDateTime(IO.GetInput(fieldNames[3], aProduct.ProductionDate.ToString("dd/MM/yyyy"), checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]), false);
                ExpirationDate = IO.ParseToDateTime(IO.GetInput(fieldNames[4], aProduct.ExpirationDate.ToString("dd/MM/yyyy"), checkinputStringAlpha, lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2]), false);

                while (ExpirationDate != null & ExpirationDate.CompareTo(ProductionDate) <= 0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    IO.SystemMessage("Expiration date should be after Production date", false);
                    ExpirationDate = IO.ParseToDateTime(IO.GetInput(fieldNames[4], aProduct.ExpirationDate.ToString("dd/MM/yyyy"), checkinputStringAlpha, lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2]), false);
                }
                string getSalesPrice = IO.GetInput(fieldNames[5], aProduct.SalesPrice.ToString(format: "F2"), checkinputStringNum, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2]);
                SalesPrice = (getSalesPrice!="") ? float.Parse(getSalesPrice) : 0;

                //if (getSalesPrice != "") 
                //{
                //    SalesPrice = float.Parse(getSalesPrice);
                //}else
                //{
                //    SalesPrice = 0;
                //}

                string getCostPrice = IO.GetInput(fieldNames[6], aProduct.CostPrice.ToString(format: "F2"), checkinputStringNum, lengthQuestionField, fieldProperties[6, 1], false, true, true, true, true, fieldProperties[6, 2]);
                CostPrice = (getCostPrice != "") ? float.Parse(getCostPrice) : 0;


                //if (getCostPrice != "")
                //{
                //    CostPrice = float.Parse(getCostPrice);
                //}else
                //{
                //    CostPrice = 0;
                //}
                
                Stock =               Int16.Parse(IO.GetInput(fieldNames[7], aProduct.Stock.ToString(), checkinputStringNum, lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]));
                ID = ConstructID(this);
                Active = true;
                
                this.Mutations = aProduct.Mutations;

                CheckMutations(aProduct, aProduct.Name,                      this.Name,                      fieldNames[1], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.ProductType,               this.ProductType,               fieldNames[2], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.ProductionDate.ToString(), this.ProductionDate.ToString(), fieldNames[3], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.ExpirationDate.ToString(), this.ExpirationDate.ToString(), fieldNames[4], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.SalesPrice.ToString(),     this.SalesPrice.ToString(),     fieldNames[5], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.CostPrice.ToString(),      this.CostPrice.ToString(),      fieldNames[6], aProduct.Mutations.Count);
                CheckMutations(aProduct, aProduct.Stock.ToString(),          this.Stock.ToString(),          fieldNames[7], aProduct.Mutations.Count);
            }
            else              // display only
            {
                var cursorColumn = Console.CursorTop;
                IfActive(aProduct, lengthQuestionField + fieldProperties[0, 1] + 5, cursorColumn);
                IO.PrintBoundaries(fieldNames[0], aProduct.ID, lengthQuestionField, fieldProperties[0, 1], cursorColumn, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[1], aProduct.Name, lengthQuestionField, fieldProperties[1, 1], cursorColumn, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[2], aProduct.ProductType, lengthQuestionField, fieldProperties[2, 1], cursorColumn, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[3], aProduct.ProductionDate.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[3, 1], cursorColumn, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[4], aProduct.ExpirationDate.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[4, 1], cursorColumn, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[5], aProduct.SalesPrice.ToString("F2").PadLeft(fieldProperties[5,1],' ') , lengthQuestionField, fieldProperties[5, 1], cursorColumn, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[6], aProduct.CostPrice.ToString("F2").PadLeft(fieldProperties[5, 1], ' '), lengthQuestionField, fieldProperties[6, 1], cursorColumn, aProduct.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[7], aProduct.Stock.ToString().PadLeft(fieldProperties[5, 1], ' '), lengthQuestionField, fieldProperties[7, 1], cursorColumn, aProduct.Active); Console.WriteLine(); cursorColumn++;
            
            }
            
        }

        [JsonConstructor]                                              
        public Product(string JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        //private static void IfActive(Product aProduct, int aCursor)
        //{
        //    if (!aProduct.Active)
        //    {
        //        IO.PrintOnConsole(" *Inactive* ", lengthQuestionField + fieldProperties[0, 1] + 5, aCursor, Color.TextColors.Inverted);
        //    }
        //    else
        //    {
        //        IO.PrintOnConsole("".PadRight(12, ' '), lengthQuestionField + fieldProperties[0, 1] + 5, aCursor, Color.TextColors.Defaults);
        //    }
        //}
        private static string ConstructID(Product aProduct)
        {
            string a = aProduct.RecordCounter.ToString("D5");            // make a string consisting of 5 decimals
            string b;
            if (aProduct.Name.Length >= 3)
            {
                b = aProduct.Name.Substring(0, 3).ToUpper();             // take first 3 chars in uppercase
            }                                                            // TODO: remove whitespace if exists ("de Groot")
            else
            {
                b = aProduct.Name.Substring(0, aProduct.Name.Length)     // or build to 3 chars with added "A" chars
                    .ToUpper()
                    .PadRight(3, 'A');
            }
            return b + a;
        }
        //public static void CheckMutations(Product aProduct, string old, string newVal, string fieldName, int existingNumberOfMutations)
        //{
        //    if (old != newVal)
        //    {
        //        if (aProduct.Mutations == null)
        //        {
        //            aProduct.Mutations = new List<Mutation>();
        //        }

        //        Mutation newMutation = new Mutation(existingNumberOfMutations + 1,
        //                                   DateTime.Now,
        //                                   fieldName,
        //                                   old,
        //                                   "",                            // placeholder because:
        //                                   //newVal.Replace(old, ""),     // TODO: old cannot be empty, throws exception
        //                                   newVal);
        //        aProduct.Mutations.Add(newMutation);                      // needs object reference when = null;
        //    }
        //}
        //public static void SetTotalRecords(int aRecord)
        //{
        //    totalRecords = aRecord;
        //}
        //public static void ToggleDeletionFlag(Product aProduct, int aRecordnumber)
        //{
        //    bool flagToggle = aProduct.Active ? false : true;
        //    aProduct.Active = flagToggle;
            
        //    if (aProduct.Active)
        //    {
        //        IO.SystemMessage("Record has been set to Active", false);
        //    }
        //    else
        //    {
        //        IO.SystemMessage("Record has been marked for Deletion", false);
        //    }
        //}

    }
}
