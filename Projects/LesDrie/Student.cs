using System;

namespace LesDrie
{
    internal class Student : Person
    {
        // Class data members
        //public string Name;
        //public string DateOfBirth;
        //public int Phone;
        //public string Address;
        
        public int Grade;
        public static int aantalRecords;

        public Student()
        {
            Console.WriteLine("Geef cijfer van de student: ");
            Grade = int.Parse(Console.ReadLine());

            //Console.WriteLine("Geef naam van de student: ");
            //Name = Console.ReadLine();

            //Console.WriteLine("Geef tel nummer van de student: ");
            //Phone = int.Parse(Console.ReadLine());

            //Console.WriteLine("Geef geboortedatum van de student: ");
            //DateOfBirth = Console.ReadLine();

            //Console.WriteLine("Geef adres van de student: ");
            //Address = (Console.ReadLine());

            Student.aantalRecords++;
        }

        public override void definieerLogo()
        {

        }

        //public override void SendMessage()
        //{
        //    base.SendMessage();
        //}



    }
}