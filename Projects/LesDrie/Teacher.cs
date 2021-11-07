using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesDrie
{
    class Teacher:Person
    {
        
        public static int aantalRecords;
        public string vakgebied;

        public Teacher()
        {

                Console.WriteLine("Geef vakgebied van de docent: ");
                vakgebied = Console.ReadLine();

                Teacher.aantalRecords++;



        }

        public override void definieerLogo()
        {

        }

        public void Display()
        {

        }


    }
}
