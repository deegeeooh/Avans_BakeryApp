using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BakeryConsole
{

    class Person : Address
    {
        public static int lengthQuestionField = 30;

        // input validation strings  TODO: move to a class
        public static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";
        public static string checkinputStringDate =  "0123456789/-";
        public static string checkinputStringNum =   "0123456789";


        // private static int totalRecords = 0;             

        //
        // 2 dimensional array with 3 columns per row: fieldNames index (for readability, not necessary), field max length, field min required length
        //

        private static int[,] fieldProperties = { { 0,  35,  0 },
                                                  { 1,  30,  1 },
                                                  { 2,   1,  1 },
                                                  { 3,   1,  1 },
                                                  { 4,  10, 10 } };
                                                 

        // user interface fields

        private static new String[] fieldNames ={ "Prefix:"                     ,   // 0
                                                  "First Name:"                 ,   // 1
                                                  "Gender: (M/F/X)"             ,   // 2
                                                  "Relation type: (Aa-Zz)"      ,   // 3
                                                  "Date of Birth: (dd/mm/yyyy)" };  // 4
       
        private static string _DescriptionFieldName = "Last Name";               // to set fieldname of Address class' generic Name Property
        
        public string         Prefix        { get; set; }
        public string         FirstName     { get; set; }
        public string         Gender        { get; set; }
        public string         RelationType  { get; set; }
        public DateTime       DateOfBirth   { get; set; }

/*1st*/ public Person() : base (_DescriptionFieldName)                                            // Main Constructor method;
        {
            var cursorRow    = Console.CursorTop;

            Prefix        = IO.GetInput(fieldNames[0],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2], 1);
            FirstName     = IO.GetInput(fieldNames[1],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2], 1);
            Gender        = IO.GetInput(fieldNames[2],  "", "MmFfXx",              lengthQuestionField, fieldProperties[2, 1], true, true, true, true, true,  fieldProperties[2, 2], 1);
            RelationType  = IO.GetInput(fieldNames[3],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2], 1);
            DateOfBirth   = IO.ParseToDateTime(IO.GetInput(fieldNames[4], 
                                                        "", checkinputStringDate,  lengthQuestionField, fieldProperties[4, 1], false, true, true, true, true, fieldProperties[4, 2], 1), true);
            DisplayAge(this.DateOfBirth, cursorRow + 4);

/*2nd*/     GetAddressFields(new Address());                                            // get Address' fields and copy them from that instance to this one.
        }

/*1st*/ public Person(bool clearForm, bool _Activatordummy) : base (clearForm, _DescriptionFieldName, true)             // clear fields after base cleared 1st part fields
        {                                                                                         // and TotalRecords etc will be increased which we only want from ()
            var cursor = Console.CursorTop;
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, 1, false); Console.WriteLine(); cursor++;
                }
            _ = new Address(clearForm, _DescriptionFieldName, false);                             // call Address to clear it's specific fields, 2nd part
/*2nd*/ }

/*1st*/ public Person(Person aPerson, bool displayOnly) : base (aPerson, displayOnly, _DescriptionFieldName, true)   
        {
            if (!displayOnly)               //EDIT 
            {
                // call GetInput() with the passed values of aPerson
                RecordCounter   = aPerson.RecordCounter;

                Prefix          = IO.GetInput(fieldNames[0], aPerson.Prefix, checkinputStringAlpha,       lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true,  fieldProperties[0, 2], 1);
                FirstName       = IO.GetInput(fieldNames[1], aPerson.FirstName, checkinputStringAlpha,    lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true,  fieldProperties[1, 2], 1);
                Gender          = IO.GetInput(fieldNames[2], aPerson.Gender, "mMfFxX",                    lengthQuestionField, fieldProperties[2, 1], true, true, true, true, true,   fieldProperties[2, 2], 1);
                RelationType    = IO.GetInput(fieldNames[3], aPerson.RelationType, checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], true, true, true, true, true,   fieldProperties[3, 2], 1);
                DateOfBirth     = IO.ParseToDateTime(IO.GetInput(fieldNames[4], aPerson.DateOfBirth.ToString("dd/MM/yyyy"), 
                                                                                   checkinputStringDate,  lengthQuestionField, fieldProperties[4, 1], false, true, true, false, true, fieldProperties[4, 2], 1), true);
                DisplayAge (this.DateOfBirth, Console.CursorTop - 1);

                this.Mutations  = aPerson.Mutations;                     // copy existing mutations to this new instance
                
                CheckMutations(aPerson, aPerson.Prefix,                 this.Prefix,                 fieldNames[0], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.FirstName,              this.FirstName,              fieldNames[1], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Gender,                 this.Gender,                 fieldNames[2], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.RelationType,           this.RelationType,           fieldNames[3], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.DateOfBirth.ToString(), this.DateOfBirth.ToString(), fieldNames[4], aPerson.Mutations.Count);

/*2nd*/         GetAddressFields(new Address(aPerson, displayOnly, _DescriptionFieldName, false));
                
                CheckMutations(aPerson, aPerson.Street,                 this.Street,    Address.fieldNames[0],  aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Zipcode,                this.Zipcode,   Address.fieldNames[1],  aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.City,                   this.City,      Address.fieldNames[2],  aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Country,                this.Country,   Address.fieldNames[3],  aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Telephone,              this.Telephone, Address.fieldNames[4],  aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Email,                  this.Email,     Address.fieldNames[5],  aPerson.Mutations.Count);
            }
/*1st*/     else        // DISPLAY ONLY
            {
                var cursor = Console.CursorTop;
               
                IO.PrintBoundaries(fieldNames[0],  aPerson.Prefix,       lengthQuestionField, fieldProperties[0, 1], cursor, 1, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[1],  aPerson.FirstName,    lengthQuestionField, fieldProperties[1, 1], cursor, 1, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[2],  aPerson.Gender,       lengthQuestionField, fieldProperties[2, 1], cursor, 1, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[3],  aPerson.RelationType, lengthQuestionField, fieldProperties[3, 1], cursor, 1, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[4],  aPerson.DateOfBirth.ToString("dd/MM/yyyy"), 
                                                                         lengthQuestionField, fieldProperties[4, 1], cursor, 1, aPerson.Active);     
                DisplayAge(aPerson.DateOfBirth, cursor); Console.WriteLine(); cursor++;

/*2nd*/          _ = new Address(aPerson, displayOnly, _DescriptionFieldName, false);
            }
        }

        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Person(Int64 JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        private void DisplayAge(DateTime aDatetime, int aCursor)
        {
            if (aDatetime.ToString("ddMMyyyy") != "01010001")
            {
                IO.PrintOnConsole("(Age: " + (IO.CalculateAge(aDatetime).ToString()) + ")   "
                    , lengthQuestionField + fieldProperties[4, 1] + 5, aCursor, Prefs.Color.Defaults);
            }
            else
            {
                IO.PrintOnConsole("            "
                    , lengthQuestionField + fieldProperties[4, 1] + 5, aCursor, Prefs.Color.Defaults);
            }
        }

        public static Person SelectPersonFromList(int aCursor)
        {

            IO.SystemMessage("In SelectPersonFromList", false);


            var peopleList = JSON.PopulateList<Person>(Program.filePeople);
            if (peopleList.Count > 0)
            {

            }
            return peopleList[0];
        }
        private void GetAddressFields(Address _newInstance)
        {
            this.Street    = _newInstance.Street;
            this.Zipcode   = _newInstance.Zipcode;
            this.City      = _newInstance.City;
            this.Country   = _newInstance.Country;
            this.Telephone = _newInstance.Telephone;
            this.Email     = _newInstance.Email;
        }


    }
}