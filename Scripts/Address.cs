using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;
using ConsoleLibrary;

namespace BakeryConsole
{
    class Address : RecordManager
    {
        private static int lengthQuestionField      = 30;
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-@|' .,_";
        private static string telephoneString       = "0123456789+-";
        private static string zipCodeString         = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";


        private static int[,] fieldProperties = { { 0,  45,  0 },
                                                  { 1,   6,  1 },
                                                  { 2,  45,  0 },
                                                  { 3,  45,  0 },
                                                  { 4,  14,  0 },
                                                  { 5,  45,  0 } };

        // user interface fields

        public static new String[] fieldNames = { "Street address: "            ,   // 0
                                                  "Zipcode: (####ZZ)"           ,   // 1
                                                  "City: "                      ,   // 2
                                                  "Country: "                   ,   // 3
                                                  "Telephone: "                 ,   // 4
                                                  "Email: "                     } ;  // 5
        public string Street                { get; set; }
        public string Zipcode               { get; set; }
        public string City                  { get; set; }
        public string Country               { get; set; }
        public string Telephone             { get; set; }
        public string Email                 { get; set; }


 /*1st*/public Address(string aStringFor_Name ) : base(aStringFor_Name)    //first part of properties to be set when instantiating new parent
        {

        }

 /*2nd*/public Address() : base (true)                      // fill second part of Properties called from parent and call dummy in base
        {                                           
            Street    = IO.GetInput(fieldNames[0], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2], 1);
            Zipcode   = IO.GetInput(fieldNames[1], "", zipCodeString, lengthQuestionField,         fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2], 1);
            City      = IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2], 1);
            Country   = IO.GetInput(fieldNames[3], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2], 1);
            Telephone = IO.GetInput(fieldNames[4], "", telephoneString, lengthQuestionField,       fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2], 1);
            Email     = IO.GetInput(fieldNames[5], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2], 1);
        }

        public Address(bool clearForm, string aStringFor_Name, bool _ExecuteParentConstructorOnly) : base(clearForm, aStringFor_Name, _ExecuteParentConstructorOnly)  // Every class needs this routine to display its fields
        {
            int start;
            int lenght;
/*1st*/     if (_ExecuteParentConstructorOnly)                            // only handle first two fields, ID and Name
            {
                //start = 0;
                //lenght = 2;
                //DisplayClearInputFields();
            }
/*2nd*/     else                                   // handle from fieldNames[2] to end
            {
                //start = 2;
                start = 0;
                lenght = fieldProperties.GetLength(0);
                DisplayClearInputFields();
            }

            void DisplayClearInputFields( )
            {
                var cursor = Console.CursorTop;
                    for (int i = start; i < lenght; i++)
                    {
                        IO.PrintBoundaries(fieldNames[i], "", "", lengthQuestionField, fieldProperties[i, 1], cursor, 1, false); Console.WriteLine(); cursor++;
                    }
            }
        }

        public Address(Address anAddress, string aHighLight, bool displayOnly, string aStringFor_Name, bool _ExecuteParentConstructorOnly) : base(anAddress, aHighLight, displayOnly, aStringFor_Name, _ExecuteParentConstructorOnly)    // Constructor for edit and display existing record
        {
            if (!displayOnly)  //EDIT
            {
/*1st*/         if (_ExecuteParentConstructorOnly)                         // just handle ID and Name in RecordManager
                {
                    //RecordCounter  = anAddress.RecordCounter;
                    //Name           = IO.GetInput(fieldNames[1], anAddress.Name, checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
                    //ID             = ConstructID(this);
                    //this.Mutations = anAddress.Mutations;
                    //Active         = true;

                    //CheckMutations(anAddress, anAddress.ID, this.ID, fieldNames[0], anAddress.Mutations.Count);
                    //CheckMutations(anAddress, anAddress.Name, this.Name, fieldNames[1], anAddress.Mutations.Count); 

/*2nd*/         }else          
                {
                    Street    = IO.GetInput(fieldNames[0], anAddress.Street, checkinputStringAlpha,  lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2], 1);
                    Zipcode   = IO.GetInput(fieldNames[1], anAddress.Zipcode, zipCodeString,         lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2], 1);
                    City      = IO.GetInput(fieldNames[2], anAddress.City, checkinputStringAlpha,    lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2], 1);
                    Country   = IO.GetInput(fieldNames[3], anAddress.Country, checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2], 1);
                    Telephone = IO.GetInput(fieldNames[4], anAddress.Telephone, telephoneString,     lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2], 1);
                    Email     = IO.GetInput(fieldNames[5], anAddress.Email, checkinputStringAlpha,   lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2], 1);
                
                   // we check mutations in parent class here, because we are in a new instance here, not the parents'
                }
            }
            else              // DISPLAY ONLY
            {
/*1st*/         if (_ExecuteParentConstructorOnly)
                {
                    //int cursorColumn = Console.CursorTop;
                    //IfActive(anAddress, lengthQuestionField + fieldProperties[0, 1] + 5, cursorColumn);
                    //IO.PrintBoundaries(fieldNames[0], anAddress.ID, lengthQuestionField, fieldProperties[0, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;
                    //IO.PrintBoundaries(fieldNames[1], anAddress.Name, lengthQuestionField, fieldProperties[1, 1], cursorColumn, anAddress.Active); Console.WriteLine(); cursorColumn++;

/*2nd*/         }else
                {
                    int cursorColumn = Console.CursorTop;
                IO.PrintBoundaries(fieldNames[0], anAddress.Street,     aHighLight, lengthQuestionField, fieldProperties[0, 1], cursorColumn, 1, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[1], anAddress.Zipcode,    aHighLight, lengthQuestionField, fieldProperties[1, 1], cursorColumn, 1, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[2], anAddress.City,       aHighLight, lengthQuestionField, fieldProperties[2, 1], cursorColumn, 1, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[3], anAddress.Country,    aHighLight, lengthQuestionField, fieldProperties[3, 1], cursorColumn, 1, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[4], anAddress.Telephone,  aHighLight, lengthQuestionField, fieldProperties[4, 1], cursorColumn, 1, anAddress.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[5], anAddress.Email,      aHighLight, lengthQuestionField, fieldProperties[5, 1], cursorColumn, 1, anAddress.Active); Console.WriteLine(); cursorColumn++;
                }
            }
        }


        [JsonConstructor]                                                 // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Address(Int64 JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }
        public override string ConstructSearchString()
        {
        string searchString =   base.ConstructSearchString() +"\r" +
                                this.Street +"\r" +
                                this.Zipcode +"\r" +
                                this.City +"\r" +
                                this.Country +"\r" +
                                this.Telephone +"\r" +
                                this.Email ;
        return searchString;
        }




    }

}
