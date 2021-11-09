using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlaaieboer
{
    class Mutation              //NICE create separate mutations file
    {
        public int      RecordCounter { get; set; }
        public DateTime MutationDate  { get; set; }
        public string   FieldName     { get; set; }
        public string   OldValue      { get; set; } 
        public string   Changes       { get; set; }
        public string   NewValue      { get; set; }

        public Mutation(int aCounter, DateTime aDate, string aFieldName, string anOldString, string aChange, string aNewString)
        {
            RecordCounter = aCounter;
            MutationDate = aDate;
            FieldName = aFieldName;
            OldValue = anOldString;
            Changes = aChange;
            NewValue = aNewString;
        }
    }
}



