using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ConsoleLibrary;
namespace BakeryConsole
{
    class RecordManager
    {

        private static int lengthQuestionField      = 30;
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-@|' .,_";

        private static int[,] fieldProperties       = { { 0,   8,  1 },             //NICE : Use attributes instead
                                                        { 1,  45,  1 } };

        public static String[] fieldNames           = { "Record ID:",               // 0
                                                        "Name:"        };           // 1

        //public String searchString;
        public string ID                { get; set; }                  // unique record ID
        public string Description       { get; set; }                  // Default record search string
        public int RecordCounter        { get; set; }                  // NOT static, this is a record propery of the  class    
        public static int TotalRecords  { get; set; }                  // static,this a class property        
        public bool Active              { get; set; }                  // flag for record deletion/inactive
        public List<Mutation> Mutations { get; set; }                  // just as PoC; every record stores all mutations 

        /// <summary>
        /// Constructor for creating new objects
        /// </summary>
        /// <param name="aStringFor_Name">pass class specific name for description field or empty for 'Name'</param>
        public RecordManager(string aStringFor_Name)
        {
            fieldNames[1]   = (aStringFor_Name == "") ? "Name" : aStringFor_Name;       
            TotalRecords++;
            RecordCounter   = TotalRecords;
            Description     = IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2], 1);
            ID              = ConstructID(this);
            Active          = true;

            CheckMutations(this, " ", "[Created:]", "", 0);             // set creation date in mutation at initial record creation
        }

        public RecordManager(bool _clearForm, string aStringFor_Name, bool _First)
        {
            if (_First)
            {
                fieldNames[1]   = aStringFor_Name;
                var cursor      = Console.CursorTop;

                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], "", "", lengthQuestionField, fieldProperties[i, 1], cursor, 1, false); Console.WriteLine(); cursor++;
                }
            }
        }

        public RecordManager( RecordManager anInheritor, string aHighLight, bool displayOnly, string aStringFor_Name, bool _ExecuteConstructor )
        {
            if (_ExecuteConstructor)
            {
                if (!displayOnly)           //EDIT
                {
                    fieldNames[1]   = aStringFor_Name;
                    RecordCounter   = anInheritor.RecordCounter;
                    Description     = IO.GetInput(fieldNames[1], anInheritor.Description, checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2], 1);
                    ID              = ConstructID(this);
                    this.Mutations  = anInheritor.Mutations;
                    Active = true;

                    CheckMutations(anInheritor, anInheritor.ID, this.ID, fieldNames[0], anInheritor.Mutations.Count);
                    CheckMutations(anInheritor, anInheritor.Description, this.Description, fieldNames[1], anInheritor.Mutations.Count);
                }
                else               // DISPLAY ONLY
                {
                    fieldNames[1]   = aStringFor_Name;
                    int cursorColumn = Console.CursorTop;
                    IfActive(anInheritor, lengthQuestionField + fieldProperties[0, 1] + 5, cursorColumn);
                    IO.PrintBoundaries(fieldNames[0], anInheritor.ID,          aHighLight, lengthQuestionField, fieldProperties[0, 1], cursorColumn, 1, anInheritor.Active); Console.WriteLine(); cursorColumn++;
                    IO.PrintBoundaries(fieldNames[1], anInheritor.Description, aHighLight, lengthQuestionField, fieldProperties[1, 1], cursorColumn, 1, anInheritor.Active); Console.WriteLine(); cursorColumn++;

                }
            }
        }


        public RecordManager( bool dontDoShit ) { }                      // dummy 
        //public RecordManager(RecordManager anInheritor, bool dontDoShit) { }     

        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public RecordManager( Int64 JUST4JSON_DontCall )
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        public static void CheckMutations<T>( T anInheritor, string old, string newVal, string fieldName, int existingNumberOfMutations ) where T : RecordManager                   // NICE: make method generic and store mutations in separate file
        {
            if (old != newVal)
            {
                if (anInheritor.Mutations == null)
                {
                    anInheritor.Mutations = new List<Mutation>();
                }

                Mutation newMutation = new Mutation(existingNumberOfMutations + 1,
                                           DateTime.Now,
                                           fieldName,
                                           old,
                                           "",                            // placeholder because:
                                                                          //newVal.Replace(old, "") // TODO: old cannot be empty, throws exception
                                           newVal);

                anInheritor.Mutations.Add(newMutation);                       // needs object reference when = null;
            }
        }

        public static void ToggleDeletionFlag<T>( T anInheritor, int aRecordnumber ) where T : RecordManager
        {
            bool flagToggle = anInheritor.Active ? false : true;
            anInheritor.Active = flagToggle;

            if (anInheritor.Active)
            {
                IO.SystemMessage("Record has been set to Active, changes written to file", false);
            }
            else
            {
                IO.SystemMessage("Record has been marked for Deletion, changes written to file", false);
            }
        }

        public static void SetTotalRecords( int aRecord )
        {
            TotalRecords = aRecord;
        }

        public static string GetSearchString<T>(T anInheritor) where T : RecordManager
        {
            string searchString = anInheritor.ConstructSearchString();
            return searchString;
        }

        public virtual string ConstructSearchString()
        {
            string searchString = this.ID + "\r" + this.Description;
            return searchString;
        }
        
        public void IfActive<T>( T anInheritor, int onCursorColumn, int onCursorRow ) where T : RecordManager
        {
            if (!anInheritor.Active) 
            {
                IO.PrintOnConsole(" *Inactive* ", onCursorColumn, onCursorRow, Prefs.Color.Inverted);
            }
            else
            {
                IO.PrintOnConsole("".PadRight(12, ' '), onCursorColumn, onCursorRow, Prefs.Color.Defaults);
            }
        }

        private string ConstructID( RecordManager aRecord )
            {
            string a = RecordCounter.ToString("D5");                                // make a string consisting of 5 decimals
            string b;
            if (aRecord.Description.Length >= 3)
            {
                b = aRecord.Description.Substring(0, 3).ToUpper();                  // take first 3 chars in uppercase
            }                                                                       // TODO: remove whitespace if exists ("de Groot")
            else
            {
                b = aRecord.Description.Substring(0, aRecord.Description.Length)    // or build to 3 chars with added "A" chars
                    .ToUpper()
                    .PadRight(3, 'A');
            }
            return b + a;
        }
    }
}
