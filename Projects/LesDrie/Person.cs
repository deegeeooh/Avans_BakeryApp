using System;

namespace LesDrie
{
    public abstract class Person                // can only be used as a child class
    {
        private string _name;
        private string _dateOfBirth;
        private int _phone;
        private string _address;

        public string Name
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name;
            }
        }

        public string DateOfBirth
        {
            set
            {
                _dateOfBirth = value;
            }
            get
            {
                return _dateOfBirth;
            }
        }

        public int Phone
        {
            set
            {
                _phone = value;
            }
            get
            {
                return _phone;
            }
        }

        public string Address
        {
            set
            {
                _address = value;
            }
            get
            {
                return _address;
            }
        }

        public Person()
        {
            Console.WriteLine("Geef naam van de persoon: ");
            Name = Console.ReadLine();

            Console.WriteLine("Geef tel nummer van de persoon: ");
            Phone = int.Parse(Console.ReadLine());

            Console.WriteLine("Geef geboortedatum van de persoon: ");
            DateOfBirth = Console.ReadLine();

            Console.WriteLine("Geef adres van de persoon: ");
            Address = (Console.ReadLine());
        }

        public abstract void definieerLogo();       // every class which inherits Person needs to have this method with override

        public virtual void SendMessage()
        {
            Console.WriteLine("ik ben hier geweest");
        }
    }
}