using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les_Vier
{
    class ReferenceTypes
    {
        static void aMethod()
        {
            testRefTypesClass p = new testRefTypesClass();      // reference type Object wordt bewaard op de heap
            int a = 10;                     // value type wordt op stack bewaard
            p.naam = "Reza";
            Check (p, ref a);
            Console.WriteLine(p.naam);
            Console.WriteLine(a);

        }

        public static void Check(testRefTypesClass t, ref int b)
        {
            // if you want to pass the value from a method for a value (primitive) type, use 
            
            b = 20;                 // value type;
            t.naam = "Jan";         // reference type ; value wordt doorgegeven
        }
    }

    public class testRefTypesClass
    {
        public string naam;
    }
}
