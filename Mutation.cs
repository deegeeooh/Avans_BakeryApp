using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlaaieboer
{
    class Mutation
    {
        public int RecordCounter { get; set; }
        public DateTime MutationDate { get; set; }
        public string oldValue { get; set;} 
        public string newValue { get; set; }
    }
}
