using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BakeryConsole
{
    internal class Customer : Address
    {
        //private static int lengthQuestionField      = 30;
        //private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-@|' .,_";
        //private static string telephoneString       = "0123456789+-";
        //private static string zipCodeString         = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";


        //private static int[,] fieldProperties = { { 0,   8,  1 },               //NICE : Use attributes instead
        //                                          { 1,  45,  1 },
        //                                          { 2,  45,  0 },
        //                                          { 3,   6,  1 },
        //                                          { 4,  45,  0 },
        //                                          { 5,  45,  0 },
        //                                          { 6,  14,  0 },
        //                                          { 7,  45,  0 } };

        //// user interface fields

        //private static String[] fieldNames = { "Record ID:"                  ,   // 0
        //                                       "Name:"                       ,   // 1
        //                                       "Street Address: "            ,   // 2
        //                                       "Zipcode: (####ZZ)"           ,   // 3
        //                                       "City: "                      ,   // 4
        //                                       "Country: "                   ,   // 5
        //                                       "Telephone: "                 ,   // 6
        //                                       "Email: "                     };  //7

        //public string ID                    { get; set; }
        //public string Name                  { get; set; }
        //public string Street                { get; set; }
        //public string Zipcode               { get; set; }
        //public string City                  { get; set; }
        //public string Country               { get; set; }
        //public string Telephone             { get; set; }
        //public string Email                 { get; set; }


        //public Customer () : base ()
        //{

        //    Name        = IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
        //    Street      = IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
        //    Zipcode     = IO.GetInput(fieldNames[3], "", zipCodeString        , lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
        //    City        = IO.GetInput(fieldNames[4], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2]);
        //    Country     = IO.GetInput(fieldNames[5], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2]);
        //    Telephone   = IO.GetInput(fieldNames[6], "", telephoneString      , lengthQuestionField, fieldProperties[6, 1], false, true, true, true, true, fieldProperties[6, 2]);
        //    Email       = IO.GetInput(fieldNames[7], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]);
        //    ID          =  ConstructID(this);
        //    CheckMutations(this, " ", "[Created:]", "", 0);

        //}

        //public Customer(bool clearForm) : base(clearForm)  // Every class needs this routine to display its fields
        //{                                                                              
        //    var cursor = Console.CursorTop;
        //    if (clearForm)
        //    {
        //        for (int i = 0; i < fieldProperties.GetLength(0); i++)
        //        {
        //            IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
        //        }
        //    }
        //}

        //public Customer(Customer aCustomer, bool displayOnly) : base(aCustomer, displayOnly)    // Constructor for edit and display existing record
        //{
        //    if (!displayOnly)  //EDIT
        //    {
        //        RecordCounter = aCustomer.RecordCounter;
        //        Name          = IO.GetInput(fieldNames[1], aCustomer.Name, checkinputStringAlpha,    lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
        //        Street        = IO.GetInput(fieldNames[2], aCustomer.Street, checkinputStringAlpha,  lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
        //        Zipcode       = IO.GetInput(fieldNames[3], aCustomer.Zipcode, zipCodeString,         lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
        //        City          = IO.GetInput(fieldNames[4], aCustomer.City, checkinputStringAlpha,    lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2]);
        //        Country       = IO.GetInput(fieldNames[5], aCustomer.Country, checkinputStringAlpha, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2]);
        //        Telephone     = IO.GetInput(fieldNames[6], aCustomer.Telephone, telephoneString,     lengthQuestionField, fieldProperties[6, 1], false, true, true, true, true, fieldProperties[6, 2]);
        //        Email         = IO.GetInput(fieldNames[7], aCustomer.Email, checkinputStringAlpha,   lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]);
        //        ID            = ConstructID(this);
        //        Active = true;

        //        this.Mutations = aCustomer.Mutations;

        //        CheckMutations(aCustomer, aCustomer.ID,             this.ID,          fieldNames[0], aCustomer.Mutations.Count);
        //        CheckMutations(aCustomer, aCustomer.Name,           this.Name,        fieldNames[1], aCustomer.Mutations.Count);
        //        CheckMutations(aCustomer, aCustomer.Street,         this.Street,      fieldNames[2], aCustomer.Mutations.Count);
        //        CheckMutations(aCustomer, aCustomer.Zipcode,        this.Zipcode,     fieldNames[3], aCustomer.Mutations.Count);
        //        CheckMutations(aCustomer, aCustomer.City,           this.City,        fieldNames[4], aCustomer.Mutations.Count);
        //        CheckMutations(aCustomer, aCustomer.Country,        this.Country,     fieldNames[5], aCustomer.Mutations.Count);
        //        CheckMutations(aCustomer, aCustomer.Telephone,      this.Telephone,   fieldNames[6], aCustomer.Mutations.Count);
        //        CheckMutations(aCustomer, aCustomer.Email,          this.Email,       fieldNames[7], aCustomer.Mutations.Count);
        //    }
        //    else              // display only
        //    {
        //        var cursorColumn = Console.CursorTop;
        //        //IfActive(aCustomer, lengthQuestionField + fieldProperties[0, 1] + 5, cursorColumn);

        //        IO.PrintBoundaries(fieldNames[0], aCustomer.ID,         lengthQuestionField, fieldProperties[0, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
        //        IO.PrintBoundaries(fieldNames[1], aCustomer.Name,       lengthQuestionField, fieldProperties[1, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
        //        IO.PrintBoundaries(fieldNames[2], aCustomer.Street,     lengthQuestionField, fieldProperties[2, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
        //        IO.PrintBoundaries(fieldNames[3], aCustomer.Zipcode,    lengthQuestionField, fieldProperties[3, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
        //        IO.PrintBoundaries(fieldNames[4], aCustomer.City,       lengthQuestionField, fieldProperties[4, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
        //        IO.PrintBoundaries(fieldNames[5], aCustomer.Country,    lengthQuestionField, fieldProperties[5, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
        //        IO.PrintBoundaries(fieldNames[6], aCustomer.Telephone,  lengthQuestionField, fieldProperties[6, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
        //        IO.PrintBoundaries(fieldNames[7], aCustomer.Email,      lengthQuestionField, fieldProperties[7, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
        //    }
        //}


        //[JsonConstructor]                                                 // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        //public Customer(string JUST4JSON_DontCall)
        //{
        //    //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        //}
        // private string ConstructID(Customer aCustomer)
        //{
        //    string a = aCustomer.RecordCounter.ToString("D5");            // make a string consisting of 5 decimals
        //    string b;
        //    if (aCustomer.Name.Length >= 3)
        //    {
        //        b = aCustomer.Name.Substring(0, 3).ToUpper();             // take first 3 chars in uppercase
        //    }                                                             // TODO: remove whitespace if exists ("de Groot")
        //    else
        //    {
        //        b = aCustomer.Name.Substring(0, aCustomer.Name.Length)    // or build to 3 chars with added "A" chars
        //            .ToUpper()
        //            .PadRight(3, 'A');
        //    }
        //    return b + a;
        //}



        private static int lengthQuestionField      = 30;
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-@|' .,_";
        private static string telephoneString       = "0123456789+-";
        private static string zipCodeString         = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";



        private static int[,] fieldProperties = { { 0, 45, 0 },                //NICE : Use attributes instead
                                                  { 1, 45, 0 },
                                                  { 2,  1, 0 } };

        // user interface fields

        private static String[] fieldNames = { "Representative:",                // 0
                                               "Job Title:"     ,                // 1
                                               "Customer Type:" };               // 2


        public string MainContact { get; set; }           // placeholder, this will be replaced by Person.ID in a list 
        public string Jobtitle { get; set; }
        public string CustomerType { get; set; }
        //public List<Person> Representatives { get; set; }


        public Customer() : base("Company name")
        {

            var cursorRow = Console.CursorTop;


            MainContact     = IO.GetInput(fieldNames[0], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2]);
            Jobtitle        = IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
            CustomerType    = IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);

            CheckMutations(this, " ", "[Created:]", "", 0);  // create a single mutation to indicate creation datestamp

        }

        public Customer(bool clearForm) : base(clearForm)  // TODO: move to Recordmanager ?
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

        public Customer(Customer aCustomer, bool displayOnly) : base(aCustomer, displayOnly)    // Constructor for edit and display existing record
        {
            if (!displayOnly)  //EDIT
            {
                RecordCounter   = aCustomer.RecordCounter;
                MainContact     = IO.GetInput(fieldNames[0], aCustomer.MainContact, checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2]);
                Jobtitle        = IO.GetInput(fieldNames[1], aCustomer.Jobtitle, checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
                CustomerType    = IO.GetInput(fieldNames[2], aCustomer.CustomerType, checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);

                //ID = ConstructID(this);
                //Active = true;

                this.Mutations = aCustomer.Mutations;

                CheckMutations(aCustomer, aCustomer.MainContact, this.MainContact, fieldNames[1], aCustomer.Mutations.Count);
                CheckMutations(aCustomer, aCustomer.Jobtitle, this.Jobtitle, fieldNames[1], aCustomer.Mutations.Count);
                CheckMutations(aCustomer, aCustomer.CustomerType, this.CustomerType, fieldNames[1], aCustomer.Mutations.Count);
            }
            else              // display only
            {
                var cursorColumn = Console.CursorTop;
                /*!*/         //IfActive(aCustomer, lengthQuestionField + fieldProperties[0, 1] + 5, cursorColumn);

                IO.PrintBoundaries(fieldNames[0], aCustomer.MainContact, lengthQuestionField, fieldProperties[0, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[1], aCustomer.Jobtitle, lengthQuestionField, fieldProperties[1, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[2], aCustomer.CustomerType, lengthQuestionField, fieldProperties[1, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;

            }

        }


        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Customer(Int64 JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        //private string ConstructID(Customer aCustomer)
        //{
        //    string a = aCustomer.RecordCounter.ToString("D5");            // make a string consisting of 5 decimals
        //    string b;
        //    if (aCustomer.Name.Length >= 3)
        //    {
        //        b = aCustomer.Name.Substring(0, 3).ToUpper();             // take first 3 chars in uppercase
        //    }                                                            // TODO: remove whitespace if exists ("de Groot")
        //    else
        //    {
        //        b = aCustomer.Name.Substring(0, aCustomer.Name.Length)     // or build to 3 chars with added "A" chars
        //            .ToUpper()
        //            .PadRight(3, 'A');
        //    }
        //    return b + a;
        //}

    }
}