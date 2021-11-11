using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BakeryConsole
{
    public class Field : Attribute
    {
    }

    internal class Person
    {
        private static int lengthQuestionField = 30;

        // input validation strings
        public static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";
        public static string checkinputStringDate =  "0123456789/-";
        public static string checkinputStringNum =   "0123456789";

        // public accessible total record counter
        private static int totalRecords = 0;             // static class attribute, belongs to the class not the object instances
        private static int totalInActiveRecords = 0;
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
                                               "Surname: "                  ,   // 1
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
        /*

        {get; set;} is shorthand for:

        private string name;                // this is the variable
        public String Name                  // this is a class property (which is why it has a Capital, it's not a variable
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

       */
        public int            RecordCounter { get; set; }              // generated
        public string         PersonID      { get; set; }              // idem
        public bool           Active        { get; set; }              // flag for deletion
        public List<Mutation> Mutations     { get; set; }              // just as PoC; every record stores all mutations which is probably not preferable
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

        public Person()                                 // Main Constructor method;
        {
            totalRecords++;

            //Console.WriteLine(Person.fieldNames[1]);

            RecordCounter = totalRecords;
            LastName =      IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
            Prefix =        IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
            FirstName =     IO.GetInput(fieldNames[3], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
            Gender =        IO.GetInput(fieldNames[4], "", "MmFfXx", lengthQuestionField, fieldProperties[4, 1], true, true, true, true, true, fieldProperties[4, 2]);
            RelationType =  IO.GetInput(fieldNames[5], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[5, 1], false, true, true, true, true, fieldProperties[5, 2]);
            DateOfBirth =   IO.ParseToDateTime(IO.GetInput(fieldNames[6], "", checkinputStringDate, lengthQuestionField, fieldProperties[6, 1], false, true, true, true, true, fieldProperties[6, 2]));
            Address =       IO.GetInput(fieldNames[7], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]);
            Zipcode =       IO.GetInput(fieldNames[8], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[8, 1], false, true, true, true, true, fieldProperties[8, 2]);
            City =          IO.GetInput(fieldNames[9], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[9, 1], false, true, true, true, true, fieldProperties[9, 2]);
            Country =       IO.GetInput(fieldNames[10], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[10, 1], false, true, true, true, true, fieldProperties[10, 2]);
            Telephone =     IO.GetInput(fieldNames[11], "", "0123456789+-", lengthQuestionField, fieldProperties[11, 1], false, true, true, true, true, fieldProperties[11, 2]);
            Email =         IO.GetInput(fieldNames[12], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[12, 1], false, true, true, true, true, fieldProperties[12, 2]);

            CheckMutations(this, " ", "[Created:]", 1);  // create a single mutation to indicate creation datestamp

            // construct unique employee ID
            PersonID = ConstructID(this);
            // set deleteflag
            Active = true;
        }

        public Person(Person aPerson, bool displayOnly, bool clearForm) 
        {
            if (!displayOnly)
            {
                LastName = IO.GetInput(fieldNames[1],
                                             aPerson.LastName,
                                             checkinputStringAlpha,
                                             lengthQuestionField,
                                             fieldProperties[1, 1],
                                             false,
                                             true,
                                             true,
                                             true,
                                             true,
                                             fieldProperties[1, 2]);

                // call GetInput() with the passed values of alist  //TODO: this can also be done as a separate constructor
                RecordCounter = aPerson.RecordCounter;
                Prefix = IO.GetInput(fieldNames[2], aPerson.Prefix, checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
                FirstName = IO.GetInput(fieldNames[3], aPerson.FirstName, checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
                Gender = IO.GetInput(fieldNames[4], aPerson.Gender, "mMfFxX", lengthQuestionField, fieldProperties[4, 1], true, true, true, true, true, fieldProperties[4, 2]);
                RelationType = IO.GetInput(fieldNames[5], aPerson.RelationType, checkinputStringAlpha, lengthQuestionField, fieldProperties[5, 1], true, true, true, true, true, fieldProperties[5, 2]);         //TODO: select from array from EmployeeRoles
                DateOfBirth = IO.ParseToDateTime(IO.GetInput(fieldNames[6], aPerson.DateOfBirth.ToString("dd/MM/yyyy"), checkinputStringDate, lengthQuestionField, fieldProperties[6, 1], false, true, true, false, true, fieldProperties[6, 2]));
                Address = IO.GetInput(fieldNames[7], aPerson.Address, checkinputStringAlpha, lengthQuestionField, fieldProperties[7, 1], false, true, true, true, true, fieldProperties[7, 2]);
                Zipcode = IO.GetInput(fieldNames[8], aPerson.Zipcode, checkinputStringAlpha, lengthQuestionField, fieldProperties[8, 1], false, true, true, true, true, fieldProperties[8, 2]);
                City = IO.GetInput(fieldNames[9], aPerson.City, checkinputStringAlpha, lengthQuestionField, fieldProperties[9, 1], false, true, true, true, true, fieldProperties[9, 2]);
                Country = IO.GetInput(fieldNames[10], aPerson.Country, checkinputStringAlpha, lengthQuestionField, fieldProperties[10, 1], false, true, true, true, true, fieldProperties[10, 2]);
                Telephone = IO.GetInput(fieldNames[11], aPerson.Telephone, "0123456789+-", lengthQuestionField, fieldProperties[11, 1], false, true, true, true, true, fieldProperties[11, 2]);
                Email = IO.GetInput(fieldNames[12], aPerson.Email, checkinputStringAlpha, lengthQuestionField, fieldProperties[12, 1], false, true, true, true, true, fieldProperties[12, 2]);
                PersonID = ConstructID(aPerson);
                Active = true;

                // check which values changed and store them in the Person.Mutations list

                CheckMutations(this, aPerson.LastName, this.LastName, 1);                // we are using this with the new instanced value:
                CheckMutations(this, aPerson.Prefix, this.Prefix, 2);
                CheckMutations(this, aPerson.FirstName, this.FirstName, 3);
                CheckMutations(this, aPerson.Gender, this.Gender, 4);
                CheckMutations(this, aPerson.RelationType, this.RelationType, 5);
                CheckMutations(this, aPerson.DateOfBirth.ToString(), this.DateOfBirth.ToString(), 6);
                CheckMutations(this, aPerson.Address, this.Address, 7);
                CheckMutations(this, aPerson.Zipcode, this.Zipcode, 8);
                CheckMutations(this, aPerson.City, this.City, 9);
                CheckMutations(this, aPerson.Country, this.Country, 10);
                CheckMutations(this, aPerson.Telephone, this.Telephone, 11);
                CheckMutations(this, aPerson.Email, this.Email, 12);
            }
            else
            {
                var cursor = Console.CursorTop;
                if (clearForm)
                {
                    for (int i = 0; i < fieldProperties.GetLength(0); i++)
                    {
                        IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
                    }
                }
                else
                {
                    IO.PrintBoundaries(fieldNames[0], aPerson.PersonID, lengthQuestionField, fieldProperties[0, 1], cursor, aPerson.Active);
                    IfActive();
                    Console.WriteLine(); cursor++;

                    IO.PrintBoundaries(fieldNames[1], aPerson.LastName, lengthQuestionField, fieldProperties[1, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[2], aPerson.Prefix, lengthQuestionField, fieldProperties[2, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[3], aPerson.FirstName, lengthQuestionField, fieldProperties[3, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[4], aPerson.Gender, lengthQuestionField, fieldProperties[4, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[5], aPerson.RelationType, lengthQuestionField, fieldProperties[5, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[6], aPerson.DateOfBirth.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[6, 1], cursor, aPerson.Active);
                    CheckAge(); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[7], aPerson.Address, lengthQuestionField, fieldProperties[7, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[8], aPerson.Zipcode, lengthQuestionField, fieldProperties[8, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[9], aPerson.City, lengthQuestionField, fieldProperties[9, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[10], aPerson.Country, lengthQuestionField, fieldProperties[10, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[11], aPerson.Telephone, lengthQuestionField, fieldProperties[11, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(fieldNames[12], aPerson.Email, lengthQuestionField, fieldProperties[12, 1], cursor, aPerson.Active); Console.WriteLine(); cursor++;
                }

                void IfActive()
                {
                    if (!aPerson.Active)
                    {
                        IO.PrintOnConsole(" *Inactive* ", lengthQuestionField + fieldProperties[0, 1] + 5, cursor, Color.TextColors.Inverted);
                    }
                    else
                    {
                        IO.PrintOnConsole("".PadRight(12, ' '), lengthQuestionField + fieldProperties[0, 1] + 5, cursor, Color.TextColors.Defaults);
                    }
                }

                void CheckAge()
                {
                    if (aPerson.DateOfBirth.ToString("ddMMyyyy") != "01010001")
                    {
                        IO.PrintOnConsole("(Age: " + (IO.CalculateAge(aPerson.DateOfBirth).ToString()) + ")   "
                            , lengthQuestionField + fieldProperties[6, 1] + 5, cursor, Color.TextColors.Defaults);
                    }
                    else
                    {
                        IO.PrintOnConsole("            "
                            , lengthQuestionField + fieldProperties[6, 1] + 5, cursor, Color.TextColors.Defaults);
                    }
                }

            }
        }


        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Person(string JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        public static string ConstructID(Person anEmployee)
        {
            string a = anEmployee.RecordCounter.ToString("D5");         // make a string consisting of 5 decimals
            string b;
            if (anEmployee.LastName.Length >= 3)
            {
                b = anEmployee.LastName.Substring(0, 3).ToUpper();      // take first 3 chars in uppercase
            }                                                           // TODO: remove whitespace if exists ("de Groot")
            else
            {
                b = anEmployee.LastName.Substring(0, anEmployee.LastName.Length)    // or build to 3 chars with added "A" chars
                    .ToUpper()
                    .PadRight(3, 'A');
            }

            return b + a;
        }

        public static void CheckMutations<T>(T aPerson, string old, string newVal, int aFieldnumber) where T : Person                   // NICE: make method generic and store mutations in separate file
        {
            if (old != newVal)
            {
                int len;
                if (aPerson.Mutations != null)
                {
                    len = aPerson.Mutations.Count;
                }
                else
                {
                    aPerson.Mutations = new List<Mutation>();            // set object reference so we can use Mutations.Add
                    len = 0;
                }

                Mutation a = new Mutation(len + 1,
                                          DateTime.Now,
                                          fieldNames[aFieldnumber],
                                          old,
                                          "",                            // placeholder because:
                                          //newVal.Replace(old, ""),     // TODO: old cannot be empty, throws exception
                                          newVal);
                aPerson.Mutations.Add(a);                                // needs object reference when = null;
            }
        }
            
        //public static void DisplayRecord(List<Person> aList, int aRecord, bool clearform)               //NICE: Somehow make this compacter (iterate Properties with reflection?)
        //{
        //    var cursor = Console.CursorTop;
        //    if (clearform)
        //    {
        //        for (int i = 0; i < fieldProperties.GetLength(0); i++)
        //        {
        //            IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
        //        }
        //    }
        //    else
        //    {
        //        IO.PrintBoundaries(fieldNames[0], aList[aRecord - 1].PersonID, lengthQuestionField, fieldProperties[0, 1], cursor, aList[aRecord - 1].Active);
        //        IfActive();
        //        Console.WriteLine(); cursor++;

        //        IO.PrintBoundaries(fieldNames[1],  aList[aRecord - 1].LastName, lengthQuestionField, fieldProperties[1, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[2],  aList[aRecord - 1].Prefix, lengthQuestionField, fieldProperties[2, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[3],  aList[aRecord - 1].FirstName, lengthQuestionField, fieldProperties[3, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[4],  aList[aRecord - 1].Gender, lengthQuestionField, fieldProperties[4, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[5],  aList[aRecord - 1].RelationType, lengthQuestionField, fieldProperties[5, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[6],  aList[aRecord - 1].DateOfBirth.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[6, 1], cursor, aList[aRecord - 1].Active); 
        //        CheckAge(); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[7],  aList[aRecord - 1].Address, lengthQuestionField, fieldProperties[7, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[8],  aList[aRecord - 1].Zipcode, lengthQuestionField, fieldProperties[8, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[9],  aList[aRecord - 1].City, lengthQuestionField, fieldProperties[9, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[10], aList[aRecord - 1].Country, lengthQuestionField, fieldProperties[10, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[11], aList[aRecord - 1].Telephone, lengthQuestionField, fieldProperties[11, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //        IO.PrintBoundaries(fieldNames[12], aList[aRecord - 1].Email, lengthQuestionField, fieldProperties[12, 1], cursor, aList[aRecord - 1].Active); Console.WriteLine(); cursor++;
        //    }

        //    void IfActive()
        //    {
        //        if (!aList[aRecord - 1].Active)
        //        {
        //            IO.PrintOnConsole(" *Inactive* ", lengthQuestionField + fieldProperties[0, 1] + 5, cursor, Color.TextColors.Inverted);
        //        }
        //        else
        //        {
        //            IO.PrintOnConsole("".PadRight(12,' '), lengthQuestionField + fieldProperties[0, 1] + 5, cursor, Color.TextColors.Defaults);
        //        }
        //    }

        //    void CheckAge()
        //    {
        //        if (aList[aRecord - 1].DateOfBirth.ToString("ddMMyyyy") != "01010001")
        //        {
        //            IO.PrintOnConsole("(Age: " + (IO.CalculateAge(aList[aRecord - 1].DateOfBirth).ToString()) + ")   "
        //                ,lengthQuestionField + fieldProperties[6, 1] + 5, cursor, Color.TextColors.Defaults);
        //        }
        //        else
        //        {
        //            IO.PrintOnConsole("            " 
        //                , lengthQuestionField + fieldProperties[6, 1] + 5, cursor, Color.TextColors.Defaults);
        //        }
        //    }
        //}

        public static void SetTotalRecords(int aRecord)
        {
            totalRecords = aRecord;
        }

        public static void ToggleDeletionFlag(List<Person> aList, int aRecordnumber)
        {
            bool flagToggle = (aList[aRecordnumber - 1].Active) ? false : true;
            aList[aRecordnumber - 1].Active = flagToggle;
            totalInActiveRecords++;
            IO.WriteToFile<Person>(Program.filePeople, aList);
        }
        public static bool CheckIfActive<T>(List<T> aList, int aRecordsnumber) where T : Person
        {
            return aList[aRecordsnumber - 1].Active;
        }

        public static int GetTotalRecords()
        {
            return totalRecords;
        }

        public static int GetInactiveRecords()
        {
            return totalInActiveRecords;
        }

    }
}