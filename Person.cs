using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BakeryConsole
{

    class Person : RecordManager
    {
        public static int lengthQuestionField = 30;

        // input validation strings
        public static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";
        public static string checkinputStringDate =  "0123456789/-";
        public static string checkinputStringNum =   "0123456789";

        
        // private static int totalRecords = 0;             
        
        //
        // 2 dimensional array with 3 columns per row: fieldNames index (for readability, not necessary), field max length, field min required length
        //

        private static int[,] fieldProperties = { { 0,   8,  1 },               //NICE : Use attributes instead
                                                  { 1,  45,  1 },
                                                  { 2,  35,  0 },
                                                  { 3,  30,  1 },
                                                  { 4,   1,  1 },
                                                  { 5,   1,  1 },
                                                  { 6,  10, 10 },
                                                  { 7,  45,  0 },
                                                  { 8,   6,  0 },
                                                  { 9,  45,  0 },
                                                  { 10, 45,  0 },
                                                  { 11, 14,  0 },
                                                  { 12, 45,  1 } };

        // user interface fields

        private static String[] fieldNames = { "PersonID: "                 ,   // 0
                                               "Last name: "                ,   // 1
                                               "Prefix:"                    ,   // 2
                                               "First Name:"                ,   // 3
                                               "Gender: (M/F/X)"            ,   // 4
                                               "Relation type: (Aa-Zz)"     ,   // 5
                                               "Date of Birth: (dd/mm/yyyy)",   // 6
                                               "Address: "                  ,   // 7
                                               "Zipcode: (####ZZ)"          ,   // 8
                                               "City: "                     ,   // 9
                                               "Country: "                  ,   //10
                                               "Telephone: "                ,   //11
                                               "Email: "                     }; //12
        
        /*public int            RecordCounter { get; set; }   */           // generated
        public string         ID            { get; set; }              // idem
        //public bool           Active        { get; set; }              // flag for deletion
        //public List<Mutation> Mutations     { get; set; }              // just as PoC; every record stores all mutations which is probably not preferable
        public string         Gender        { get; set; }
        public string         RelationType  { get; set; }              
        public string         FirstName     { get; set; }
        public string         LastName      { get; set; }
        public string         Prefix        { get; set; }
        public DateTime       DateOfBirth   { get; set; }
        public string         Address       { get; set; }
        public string         Zipcode       { get; set; }
        public string         City          { get; set; }
        public string         Country       { get; set; }
        public string         Telephone     { get; set; }
        public string         Email         { get; set; }

        public Person() : base ()                           // Main Constructor method;
        {
            //totalRecords++;                               }
            //Active        = true;                         }  these are inherited from the superclass RecordManager via
            //RecordCounter = totalRecords;                 }  the general Constructor

            var cursor    = Console.CursorTop;

/*Addr1st*/ LastName      = IO.GetInput(fieldNames[1],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
            
            Prefix        = IO.GetInput(fieldNames[2],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
            FirstName     = IO.GetInput(fieldNames[3],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
            Gender        = IO.GetInput(fieldNames[4],  "", "MmFfXx", lengthQuestionField, fieldProperties[4, 1], true, true, true, true, true, fieldProperties[4, 2]);
            RelationType  = IO.GetInput(fieldNames[5],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2]);
            DateOfBirth   = IO.ParseToDateTime(IO.GetInput(fieldNames[6], "", checkinputStringDate, lengthQuestionField, fieldProperties[6, 1], false, true, true, true, true, fieldProperties[6, 2]), true);
            CheckAge(DateOfBirth, cursor + 5);
            
/*Addr2nd*/ Address       = IO.GetInput(fieldNames[7],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]);
            Zipcode       = IO.GetInput(fieldNames[8],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[8, 1], false, true, true, true, true, fieldProperties[8, 2]);
            City          = IO.GetInput(fieldNames[9],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[9, 1], false, true, true, true, true, fieldProperties[9, 2]);
            Country       = IO.GetInput(fieldNames[10], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[10, 1], false, true, true, true, true, fieldProperties[10, 2]);
            Telephone     = IO.GetInput(fieldNames[11], "", "0123456789+-", lengthQuestionField, fieldProperties[11, 1], false, true, true, true, true, fieldProperties[11, 2]);
            Email         = IO.GetInput(fieldNames[12], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[12, 1], false, true, true, true, true, fieldProperties[12, 2]);
            ID            = ConstructID(this);
            //CheckMutations(this, " ", "[Created:]", "", 0);  // create a single mutation to indicate creation datestamp
        }

        public Person(bool clearForm) : base (clearForm)                                // point to this dummy, otherwise () will be called
        {                                                                               // and TotalRecords etc will be increased which we only want from ()
            var cursor = Console.CursorTop;
            if (clearForm)
            {
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
                }
            }
        }

        public Person(Person aPerson, bool displayOnly) : base (aPerson, displayOnly)   // point to dummy, idem as above
        {
            if (!displayOnly)               //EDIT record
            {
                // call GetInput() with the passed values of aPerson
                RecordCounter = aPerson.RecordCounter;
                LastName = IO.GetInput(fieldNames[1], aPerson.LastName,
                                                      checkinputStringAlpha,
                                                      lengthQuestionField,
                                                      fieldProperties[1, 1],
                                                      false,
                                                      true,
                                                      true,
                                                      true,
                                                      true,
                                                      fieldProperties[1, 2]);

                Prefix          = IO.GetInput(fieldNames[2], aPerson.Prefix, checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
                FirstName       = IO.GetInput(fieldNames[3], aPerson.FirstName, checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
                Gender          = IO.GetInput(fieldNames[4], aPerson.Gender, "mMfFxX", lengthQuestionField, fieldProperties[4, 1], true, true, true, true, true, fieldProperties[4, 2]);
                RelationType    = IO.GetInput(fieldNames[5], aPerson.RelationType, checkinputStringAlpha, lengthQuestionField, fieldProperties[5, 1], true, true, true, true, true, fieldProperties[5, 2]);
                DateOfBirth     = IO.ParseToDateTime(IO.GetInput(fieldNames[6], aPerson.DateOfBirth.ToString("dd/MM/yyyy"), checkinputStringDate, lengthQuestionField, fieldProperties[6, 1], false, true, true, false, true, fieldProperties[6, 2]), true);
                     CheckAge (this.DateOfBirth, Console.CursorTop - 1);
                Address         = IO.GetInput(fieldNames[7], aPerson.Address, checkinputStringAlpha, lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]);
                Zipcode         = IO.GetInput(fieldNames[8], aPerson.Zipcode, checkinputStringAlpha, lengthQuestionField, fieldProperties[8, 1], false, true, true, true, true, fieldProperties[8, 2]);
                City            = IO.GetInput(fieldNames[9], aPerson.City, checkinputStringAlpha, lengthQuestionField, fieldProperties[9, 1], false, true, true, true, true, fieldProperties[9, 2]);
                Country         = IO.GetInput(fieldNames[10], aPerson.Country, checkinputStringAlpha, lengthQuestionField, fieldProperties[10, 1], false, true, true, true, true, fieldProperties[10, 2]);
                Telephone       = IO.GetInput(fieldNames[11], aPerson.Telephone, "0123456789+-", lengthQuestionField, fieldProperties[11, 1], false, true, true, true, true, fieldProperties[11, 2]);
                Email           = IO.GetInput(fieldNames[12], aPerson.Email, checkinputStringAlpha, lengthQuestionField, fieldProperties[12, 1], false, true, true, true, true, fieldProperties[12, 2]);
                ID              = ConstructID(aPerson);
                Active          = true;

                // check which values changed and store them in the Person.Mutations list
                
                //if (aPerson.Mutations == null)                          // TODO: obsolete since there is always a mutation created when new record
                //{
                //    aPerson.Mutations = new List<Mutation>();
                //}
                this.Mutations = aPerson.Mutations;                     // copy existing mutations to this new instance
                
                CheckMutations(aPerson, aPerson.LastName,               this.LastName,  fieldNames[1], aPerson.Mutations.Count);                // we are using this with the new instanced value:
                CheckMutations(aPerson, aPerson.Prefix,                 this.Prefix,    fieldNames[2], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.FirstName,              this.FirstName, fieldNames[3], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Gender,                 this.Gender,    fieldNames[4], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.RelationType,           this.RelationType, fieldNames[5], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.DateOfBirth.ToString(), this.DateOfBirth.ToString(), fieldNames[6], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Address,                this.Address,   fieldNames[7], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Zipcode,                this.Zipcode,   fieldNames[8], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.City,                   this.City,      fieldNames[9], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Country,                this.Country,   fieldNames[10], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Telephone,              this.Telephone, fieldNames[11], aPerson.Mutations.Count);
                CheckMutations(aPerson, aPerson.Email,                  this.Email,     fieldNames[12], aPerson.Mutations.Count);
            }
            else        // Display Record
            {
                var cursor = Console.CursorTop;
               
                IO.PrintBoundaries(fieldNames[0], aPerson.ID, lengthQuestionField, fieldProperties[0, 1], cursor, aPerson.Active);
                IfActive(aPerson, lengthQuestionField + fieldProperties[0, 1] + 5, cursor); Console.WriteLine(); cursor++;

                IO.PrintBoundaries(fieldNames[1],  aPerson.LastName, lengthQuestionField, fieldProperties[1, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[2],  aPerson.Prefix, lengthQuestionField, fieldProperties[2, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[3],  aPerson.FirstName, lengthQuestionField, fieldProperties[3, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[4],  aPerson.Gender, lengthQuestionField, fieldProperties[4, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[5],  aPerson.RelationType, lengthQuestionField, fieldProperties[5, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[6],  aPerson.DateOfBirth.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[6, 1], cursor, aPerson.Active);
                    CheckAge(aPerson.DateOfBirth, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[7],  aPerson.Address, lengthQuestionField, fieldProperties[7, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[8],  aPerson.Zipcode, lengthQuestionField, fieldProperties[8, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[9],  aPerson.City, lengthQuestionField, fieldProperties[9, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[10], aPerson.Country, lengthQuestionField, fieldProperties[10, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[11], aPerson.Telephone, lengthQuestionField, fieldProperties[11, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[12], aPerson.Email, lengthQuestionField, fieldProperties[12, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
            }
        }

        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Person(Int64 JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        public static string ConstructID(Person aPerson)
            {
                string a = aPerson.RecordCounter.ToString("D5");              // make a string consisting of 5 decimals
            string b;
            if (aPerson.LastName.Length >= 3)
            {
                b = aPerson.LastName.Substring(0, 3).ToUpper();               // take first 3 chars in uppercase
            }                                                                 // TODO: remove whitespace if exists ("de Groot")
            else
            {
                b = aPerson.LastName.Substring(0, aPerson.LastName.Length)    // or build to 3 chars with added "A" chars
                    .ToUpper()
                    .PadRight(3, 'A');
            }

            return b + a;
        }

        private void CheckAge(DateTime aDatetime, int aCursor)
        {
            if (aDatetime.ToString("ddMMyyyy") != "01010001")
            {
                IO.PrintOnConsole("(Age: " + (IO.CalculateAge(aDatetime).ToString()) + ")   "
                    , lengthQuestionField + fieldProperties[6, 1] + 5, aCursor, Color.TextColors.Defaults);
            }
            else
            {
                IO.PrintOnConsole("            "
                    , lengthQuestionField + fieldProperties[6, 1] + 5, aCursor, Color.TextColors.Defaults);
            }
        }

        private static Person SelectPersonFromList(int aCursor)
        {
            var peopleList = IO.PopulateList<Person>(Program.filePeople); 
            if (peopleList.Count > 0)
            {

            }





            return peopleList[0];
        }


    }
}