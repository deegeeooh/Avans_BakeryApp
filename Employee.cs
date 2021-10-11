using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Vlaaieboer
{
    class Employee
    {

        // intialize field parameters
        readonly int surnameMaxLenght = 45; readonly int surnameMinLenght = 1;
        readonly int prefixMaxLength = 35; readonly int prefixMinLenght = 0;
        readonly int firstnameMaxLength = 30; readonly int firstnameMinLength = 1;
        readonly int doBMaxLength = 10; readonly int doBMinLenght = 10;
        readonly int addressMaxLenght = 45; readonly int addressMinLenght = 0;
        readonly int zipCodeMaxLenght = 6; readonly int zipCodeMinLength = 0;
        readonly int cityMaxLenght = 45; readonly int cityMinLenght = 0;
        readonly int telMaxLenght = 14; readonly int telMinLenght = 0;
        readonly int emailMaxLenght = 50; readonly int emailMinLength = 1;
        // input validation strings
        readonly string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .,_";
        readonly string checkinputStringDate = "0123456789/-";
        // public accessible total record counter
        public static int totalRecords = 0;

        //private string firstName;

        /*

        {get; set;} is shorthand for:

        private string name;                // this is the variable
        public String Name                  // this is a class property (which is why it has a Capital, it's not a variable
        {
            get
            {
                return this.name;     
            }
            set
            {
                this.name = value;       
            }
        }

       */
        public int RecordCounter { get; set; }
        public string EmployeeID { get; set; }
        // public string JobTitle { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Prefix { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public Employee(bool getInputFromConsole, bool readFromFile)           // Constructor method; gets executed whenever we call '= new Employee()'
        {
            if (getInputFromConsole)
            {
                                
                RecordCounter = 1;              // TODO: get recordcounter from # records in file when initialising program           
                SurName = IO.GetInput("Surname:", checkinputStringAlpha, 30, surnameMaxLenght, true, true, true, true, surnameMinLenght);
                Prefix = IO.GetInput("Prefix", checkinputStringAlpha, 30, prefixMaxLength, true, true, true, true, prefixMinLenght);
                FirstName = IO.GetInput("First Name:", checkinputStringAlpha, 30, firstnameMaxLength, true, true, true, true, firstnameMinLength);
                DateOfBirth= ParseToDateTime(IO.GetInput("Date of Birth (dd/mm/yyyy):",checkinputStringDate, 30, doBMaxLength, true, true, false, true, doBMinLenght));
                Address = IO.GetInput("Address:", checkinputStringAlpha, 30, addressMaxLenght, true, true, true, true, addressMinLenght);
                Zipcode = IO.GetInput("Zipcode: (####ZZ)", checkinputStringAlpha, 30, zipCodeMaxLenght, true, true, true, true, zipCodeMinLength);
                City = IO.GetInput("City:", checkinputStringAlpha, 30, cityMaxLenght, true, true, true, true, cityMinLenght);
                Telephone = IO.GetInput("Telephone:", "0123456789+-", 30, telMaxLenght, true, true, true, true, telMinLenght);
                Email = IO.GetInput("Email:", checkinputStringAlpha, 30, emailMaxLenght, true, true, true, true, emailMinLength);

                // construct unique employee ID
                string a = RecordCounter.ToString("D5");        // make a string consisting of 5 decimals
                string b;
                if (SurName.Length >= 3)
                {
                    b = SurName.Substring(0, 3).ToUpper();      // take first 3 chars in uppercase
                }
                else
                {
                    b = SurName.Substring(0, SurName.Length)    // or build to 3 chars with added "A" chars
                        .ToUpper()
                        .PadRight(3 - SurName.Length, 'A');
                }
                EmployeeID = b + a;
            }
        }

        public void WriteToFile(string aFilename)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(this, options);

                using (StreamWriter sw = File.AppendText(aFilename))
                {
                    sw.WriteLine(jsonString);
                }
            }
            catch (Exception)
            {
                IO.PrintOnConsole($"Error opening file {aFilename}", 1, 34);
            }
        }

        public int CalculateAge()
        {
            var age = DateTime.Today.Year - DateOfBirth.Year;                   // not taking date into account eg. 2021-1997
            int nowMonthandDay = int.Parse(DateTime.Now.ToString("MMdd"));      // convert month and day to int
            int thenMonthandDay = int.Parse(DateOfBirth.ToString("MMdd"));
            if (nowMonthandDay < thenMonthandDay)
            {
                age--;                                                          // if current date (in MMdd) < date of birth subtract 1 year
            }
            //
            return age;
        }

        private static DateTime ParseToDateTime(string aDateString)
        {
            DateTime parsedDateHelpstring;
            if (DateTime.TryParse(aDateString, out parsedDateHelpstring))   // Tryparse method passing back two values: bool and out var
            {
                IO.PrintOnConsole($"Parsed date string succesfully to {parsedDateHelpstring:dd-MM-yyyy}", 1, 34);
            }
            else
            {
                // if invalid date, DateTime remains at initialised 01/01/01 value
                IO.PrintOnConsole($"Could not parse date string, set to {parsedDateHelpstring:dd-MM-yy}", 1, 34);
            }

            return parsedDateHelpstring;
        }

    }
}
