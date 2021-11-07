using System;

namespace Consoleapp
{
    class Program
    {

        //
        // uitleg Reza tweede les
        //

        static void Main(string[] args)
        {

            var studentencijfers = new int[3];
            var studentnamen = new string[3];

            for (int i = 0; i < studentencijfers.Length; i++)
            {
                Console.WriteLine("Geef aub naam van de student {1}", i + 1);
                studentnamen[i] = Console.ReadLine();
                Console.WriteLine("geef aub {0}e cijfer van de student", i + 1);
                try
                {
                    studentencijfers[i] = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Je hebt geen cijfer opgegeven");
                    throw;
                }
            }

            for (int i = 0; i < studentencijfers.Length; i++)
            {
                Console.WriteLine("De naam van de student is {0) en het cijfer is {1}", studentnamen[i], studentencijfers[i]);
            }






        }

        static string Check(string vraag)
        {

            var vraag = Console.ReadLine();
            if (vraag == "")
            {
                Console.Write("je hebt niets ingevoerd. Probeer het nog een keer: ";
                vraag = Console.ReadLine();

            }
            

            return vraag;
        }


    }
}