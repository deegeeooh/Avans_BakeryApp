using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BakeryConsole
{
    class Mutation              //NICE create separate mutations file
    {
        public int RecordCounter { get; set; }
        public DateTime MutationDate { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string Changes { get; set; }
        public string NewValue { get; set; }

        public Mutation(int aCounter, DateTime aDate, string aFieldName, string anOldString, string aChange, string aNewString)
        {
            RecordCounter = aCounter;
            MutationDate = aDate;
            FieldName = aFieldName;
            OldValue = anOldString;
            Changes = aChange;
            NewValue = aNewString;
        }

        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Mutation(string JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

    }

    



}







