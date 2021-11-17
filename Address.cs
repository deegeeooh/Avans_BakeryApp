using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;

namespace BakeryConsole
{
    class Address : RecordManager
    {
        private static int lengthQuestionField      = 30;
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-@|' .,_";
        private static string telephoneString       = "0123456789+-";
        private static string zipCodeString         = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";


        private static int[,] fieldProperties = { { 0,   8,  1 },               //NICE : Use attributes instead
                                                  { 1,  45,  1 },
                                                  { 2,  45,  0 },
                                                  { 3,   6,  1 },
                                                  { 4,  45,  0 },
                                                  { 5,  45,  0 },
                                                  { 6,  14,  0 },
                                                  { 7,  45,  0 } };

        // user interface fields

        public static String[] fieldNames = { "Record ID:"                   ,   // 0
                                               "Name:"                       ,   // 1
                                               "Street address: "            ,   // 2
                                               "Zipcode: (####ZZ)"           ,   // 3
                                               "City: "                      ,   // 4
                                               "Country: "                   ,   // 5
                                               "Telephone: "                 ,   // 6
                                               "Email: "                     };  //7

        public string ID                    { get; set; }
        public string Name                  { get; set; }
        public string Street                { get; set; }
        public string Zipcode               { get; set; }
        public string City                  { get; set; }
        public string Country               { get; set; }
        public string Telephone             { get; set; }
        public string Email                 { get; set; }


 /*1st*/public Address(string aStringFor_Name ) : base()    //first part of properties to be set when instantiating new parent
        {
            fieldNames[1] = aStringFor_Name;                // set derived classes specific string for Name 

            Name          = IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
            ID            = ConstructID(this);
        }

 /*2nd*/public Address() : base (true)                      // fill second part of Properties called from parent 
        {                                           
            Street    = IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
            Zipcode   = IO.GetInput(fieldNames[3], "", zipCodeString, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
            City      = IO.GetInput(fieldNames[4], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2]);
            Country   = IO.GetInput(fieldNames[5], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2]);
            Telephone = IO.GetInput(fieldNames[6], "", telephoneString, lengthQuestionField, fieldProperties[6, 1], false, true, true, true, true, fieldProperties[6, 2]);
            Email     = IO.GetInput(fieldNames[7], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]);
        }

        public Address(bool clearForm, string aStringFor_Name, bool _First) : base(clearForm)  // Every class needs this routine to display its fields
        {
            fieldNames[1] = aStringFor_Name;       // set derived classes specific .Name 

            int start;
            int lenght;
/*1st*/     if (_First)                            // only handle first two fields, ID and Name
            {
                start = 0;
                lenght = 2;
                DisplayClearInputFields();
            }
/*2nd*/    else                                   // handle from fieldNames[2] to end
            {
                start = 2;
                lenght = fieldProperties.GetLength(0);
                DisplayClearInputFields();
            }

            void DisplayClearInputFields( )
            {
                var cursor = Console.CursorTop;
                if (clearForm)
                {
                    for (int i = start; i < lenght; i++)
                    {
                        IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
                    }
                }
            }
        }

        public Address(Address anAddress, bool displayOnly, string aStringFor_Name, bool _First) : base(anAddress, displayOnly)    // Constructor for edit and display existing record
        {
            fieldNames[1] = aStringFor_Name;        // set derived classes specific .Name 

            if (!displayOnly)  //EDIT
            {
/*1st*/         if (_First)
                {
                    RecordCounter  = anAddress.RecordCounter;
                    Name           = IO.GetInput(fieldNames[1], anAddress.Name, checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
                    ID             = ConstructID(this);
                    this.Mutations = anAddress.Mutations;
                    Active         = true;

                    CheckMutations(anAddress, anAddress.ID, this.ID, fieldNames[0], anAddress.Mutations.Count);
                    CheckMutations(anAddress, anAddress.Name, this.Name, fieldNames[1], anAddress.Mutations.Count); 

/*2nd*/         }else          // DISPLAY ONLY
                {
                    Street    = IO.GetInput(fieldNames[2], anAddress.Street, checkinputStringAlpha,  lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
                    Zipcode   = IO.GetInput(fieldNames[3], anAddress.Zipcode, zipCodeString,         lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
                    City      = IO.GetInput(fieldNames[4], anAddress.City, checkinputStringAlpha,    lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2]);
                    Country   = IO.GetInput(fieldNames[5], anAddress.Country, checkinputStringAlpha, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2]);
                    Telephone = IO.GetInput(fieldNames[6], anAddress.Telephone, telephoneString,     lengthQuestionField, fieldProperties[6, 1], false, true, true, true, true, fieldProperties[6, 2]);
                    Email     = IO.GetInput(fieldNames[7], anAddress.Email, checkinputStringAlpha,   lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]);
                
                   // we check mutations in parent class here, because we are in a new instance here, not the parents'
                }
            }
            else              // display only
            {
/*1st*/         if (_First)
                {
                    int cursorColumn = Console.CursorTop;
                    IfActive(anAddress, lengthQuestionField + fieldProperties[0, 1] + 5, cursorColumn);
                    IO.PrintBoundaries(fieldNames[0], anAddress.ID, lengthQuestionField, fieldProperties[0, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;
                    IO.PrintBoundaries(fieldNames[1], anAddress.Name, lengthQuestionField, fieldProperties[1, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;

/*2nd*/         }else
                {
                    int cursorColumn = Console.CursorTop;
                IO.PrintBoundaries(fieldNames[2], anAddress.Street,     lengthQuestionField, fieldProperties[2, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[3], anAddress.Zipcode,    lengthQuestionField, fieldProperties[3, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[4], anAddress.City,       lengthQuestionField, fieldProperties[4, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[5], anAddress.Country,    lengthQuestionField, fieldProperties[5, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[6], anAddress.Telephone,  lengthQuestionField, fieldProperties[6, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[7], anAddress.Email,      lengthQuestionField, fieldProperties[7, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;
                }
            }
        }

        [JsonConstructor]                                                 // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Address(Int64 JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }
         private string ConstructID(Address anAddress)
        {
            string a = anAddress.RecordCounter.ToString("D5");            // make a string consisting of 5 decimals
            string b;
            if (anAddress.Name.Length >= 3)
            {
                b = anAddress.Name.Substring(0, 3).ToUpper();             // take first 3 chars in uppercase
            }                                                             // TODO: remove whitespace if exists ("de Groot")
            else
            {
                b = anAddress.Name.Substring(0, anAddress.Name.Length)    // or build to 3 chars with added "A" chars
                    .ToUpper()
                    .PadRight(3, 'A');
            }
            return b + a;
        }
    }



}
