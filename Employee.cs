using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Text.Json;
using Newtonsoft.Json;
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
        readonly int emailMaxLenght = 45; readonly int emailMinLength = 1;
        readonly int lengthQuestionField = 30;
        // input validation strings
        readonly string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .,_";
        readonly string checkinputStringDate = "0123456789/-";
        // public accessible total record counter
        public static int totalRecords = 0;
        readonly string fileEmployees = "employee.json";

        // TODO: can't use list here
        List<Employee> employees = new List<Employee>();

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

        public Employee()           // Constructor method; gets executed whenever we call '= new Employee()'
        {

            // ReadFromFile(fileEmployees);
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
               
            // employees.Add(this);

            

            //var count = employees.Count();
            //IO.PrintOnConsole(count.ToString(), 1, 34);
            //Console.ReadKey();
            //WriteToFile(fileEmployees);
           
        }

        public void WriteToFile(string aFilename)
        {
            try
            {

                //var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonConvert.SerializeObject(employees);
                //Console.Clear();
                //Console.Write(jsonString);
                //Console.ReadKey();
               // File.WriteAllText(aFilename, jsonString);
                
                //using (StreamWriter sw = File.WriteAllText(aFilename, jsonString))
                //{
                //    sw.WriteLine(jsonString);
                //}

            }
            catch (Exception e)
            {
                IO.PrintOnConsole($"Error opening file {aFilename} {e}", 1, 34);
            }
        }

        public void ReadFromFile(string aFilename)
        {
            try
            {
                string json = System.IO.File.ReadAllText(aFilename);
                employees = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Employee>>(json);
                var count = employees.Count();
                totalRecords = count;
                IO.PrintOnConsole(count.ToString(), 1, 34);
                Console.ReadKey();

            }
            catch (Exception)
            {
                IO.PrintOnConsole("Error reading from file", 1, 34);
                totalRecords = 0;
                //Console.ReadKey();
                
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
