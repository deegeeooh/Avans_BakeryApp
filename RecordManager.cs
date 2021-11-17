using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BakeryConsole
{
    class RecordManager
    {
        public int RecordCounter                       { get; set; }    // NOT static, this is a record propery of the  class    
        public static int TotalRecords                 { get; set; }    // static,this a class property        
        public bool Active                             { get; set; }    // flag for record deletion/inactive
        public List<Mutation> Mutations                { get; set; }    // just as PoC; every record stores all mutations 
                                                                        // in practice, store in separate file.
        public RecordManager()
        {
            TotalRecords++;
            RecordCounter   = TotalRecords;
            Active          = true;

            CheckMutations(this, " ", "[Created:]", "", 0);             // set creation date in mutation at initial record creation
        }

        public RecordManager(bool dontDoShit) { }                       // DUMMY

        public RecordManager(RecordManager anInheritor,bool dontDoShit) { }     

        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public RecordManager(Int64 JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        public static void CheckMutations<T>(T anInheritor, string old, string newVal, string fieldName, int existingNumberOfMutations) where T : RecordManager                   // NICE: make method generic and store mutations in separate file
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
                                                                          //newVal.Replace(old, ""),     // TODO: old cannot be empty, throws exception
                                           newVal);
                
                anInheritor.Mutations.Add(newMutation);                       // needs object reference when = null;
            }
        }

        public static void ToggleDeletionFlag<T>(T anInheritor, int aRecordnumber) where T : RecordManager
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

        public static void SetTotalRecords(int aRecord)
        {
            TotalRecords = aRecord;
        }

        public static void ResetRecordCounter()
        {
            TotalRecords = 0;
        }

        public static void IfActive<T>(T anInheritor,  int onCursorColumn , int onCursorRow ) where T : RecordManager
        {
            if (!anInheritor.Active)
            {
                IO.PrintOnConsole(" *Inactive* ", onCursorColumn, onCursorRow, Color.TextColors.Inverted);
            }
            else
            {
                IO.PrintOnConsole("".PadRight(12, ' '), onCursorColumn, onCursorRow, Color.TextColors.Defaults);
            }
        }

    }
}
