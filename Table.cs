using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryConsole
{
    internal class Table

    {
        public List<Window> Columns { get; set; }

        public Table(List<Window> columns)
        {
            Columns = columns;
        }
    }
}
