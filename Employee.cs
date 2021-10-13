using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Vlaaieboer
{
    internal class Employee
    {
        // intialize field parameters
        private readonly int surnameMaxLenght = 45; private readonly int surnameMinLenght = 1;

        private readonly int prefixMaxLength = 35; private readonly int prefixMinLenght = 0;
        private readonly int firstnameMaxLength = 30; private readonly int firstnameMinLength = 1;
        private readonly int doBMaxLength = 10; private readonly int doBMinLenght = 10;
        private readonly int addressMaxLenght = 45; private readonly int addressMinLenght = 0;
        private readonly int zipCodeMaxLenght = 6; private readonly int zipCodeMinLength = 0;
        private readonly int cityMaxLenght = 45; private readonly int cityMinLenght = 0;
        private readonly int telMaxLenght = 14; private readonly int telMinLenght = 0;
        private readonly int emailMaxLenght = 45; private readonly int emailMinLength = 1;
        private readonly int lengthQuestionField = 30;

        // input validation strings
        private readonly string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .,_";

        private readonly string checkinputStringDate = "0123456789/-";

        // public accessible total record counter
        public static int totalRecords = 0;

        //private readonly string fileEmployees = "employee.json";

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

        public Employee(bool input)           // Constructor method; gets executed whenever we call '= new Employee()'
        {
            if (input)
            {

                totalRecords++;

                RecordCounter = totalRecords;
                SurName = IO.GetInput("Surname:", checkinputStringAlpha, lengthQuestionField, surnameMaxLenght, true, true, true, true, surnameMinLenght);
                Prefix = IO.GetInput("Prefix", checkinputStringAlpha, lengthQuestionField, prefixMaxLength, true, true, true, true, prefixMinLenght);
                FirstName = IO.GetInput("First Name:", checkinputStringAlpha, lengthQuestionField, firstnameMaxLength, true, true, true, true, firstnameMinLength);
                DateOfBirth = ParseToDateTime(IO.GetInput("Date of Birth (dd/mm/yyyy):", checkinputStringDate, lengthQuestionField, doBMaxLength, true, true, false, true, doBMinLenght));
                Address = IO.GetInput("Address:", checkinputStringAlpha, lengthQuestionField, addressMaxLenght, true, true, true, true, addressMinLenght);
                Zipcode = IO.GetInput("Zipcode: (####ZZ)", checkinputStringAlpha, lengthQuestionField, zipCodeMaxLenght, true, true, true, true, zipCodeMinLength);
                City = IO.GetInput("City:", checkinputStringAlpha, lengthQuestionField, cityMaxLenght, true, true, true, true, cityMinLenght);
                Telephone = IO.GetInput("Telephone:", "0123456789+-", lengthQuestionField, telMaxLenght, true, true, true, true, telMinLenght);
                Email = IO.GetInput("Email:", checkinputStringAlpha, lengthQuestionField, emailMaxLenght, true, true, true, true, emailMinLength);

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

        public static void WriteToFile(string aFilename, List<Employee> aList)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(aList, Formatting.Indented);
                File.WriteAllText(aFilename, jsonString);
            }
            catch (Exception e)
            {
                IO.Color(3);
                IO.PrintOnConsole($"Error writing to file {aFilename} {e}", 1, 34);
                IO.Color(5);
                Thread.Sleep(500);
            }
        }

        public static List<Employee> PopulateList(string aFilename)
        {
            var getemployeelist = DeserializeJSONfile(aFilename);
            if (getemployeelist != null)                           // file Exists
            {
                Employee.totalRecords = getemployeelist.Count;      // set total Record static field in Employee Class
            }

            return getemployeelist;
        }

        private static List<Employee> DeserializeJSONfile(string aFilename)
        {
            var listFromJason = new List<Employee>();                           // define here so method doesn't return NULL 
            if (File.Exists(aFilename))                                         // and causes object not defined error
            {                                                                   // when calling employeeList.add from main()
                
                try
                {
                    listFromJason = JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(aFilename));
                    return listFromJason;
                }
                catch (Exception e)
                {
                    IO.Color(3);
                    IO.PrintOnConsole($"Error parsing json file{aFilename} {e}", 1, 1);
                    Thread.Sleep(500);
                    IO.Color(5);
                }
            }
            else
            {
                IO.PrintOnConsole($"File {aFilename} doesn't exist, creating new file ", 1, 34);
                Thread.Sleep(750);
                IO.PrintOnConsole($"".PadRight(80, ' '), 1, 34);
            }
            return listFromJason;

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