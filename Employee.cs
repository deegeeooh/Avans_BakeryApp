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
        private static int surnameMaxLenght = 45; private static int surnameMinLenght = 1;

        private static int prefixMaxLength = 35; private static int prefixMinLenght = 0;
        private static int firstnameMaxLength = 30; private static int firstnameMinLength = 1;
        private static int doBMaxLength = 10; private static int doBMinLenght = 10;
        private static int addressMaxLenght = 45; private static int addressMinLenght = 0;
        private static int zipCodeMaxLenght = 6; private static int zipCodeMinLength = 0;
        private static int cityMaxLenght = 45; private static int cityMinLenght = 0;
        private static int telMaxLenght = 14; private static int telMinLenght = 0;
        private static int emailMaxLenght = 45; private static int emailMinLength = 1;
        private static int lengthQuestionField = 30;

        // input validation strings
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .,_";

        private static string checkinputStringDate = "0123456789/-";

        // public accessible total record counter
        public static int totalRecords = 0;

        private static String[] fieldNames = {"EmployeeID: ","Surname: " ,"Prefix:", "First Name:",
                                            "Date of Birth: (dd/mm/yyyy)", "Address: ", "Zipcode: (####ZZ)", "City: ",
                                            "Telephone: ", "Email: " };

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
        public string JobTitle { get; set; }                //TODO: jobtitle maintenance
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
                SurName = IO.GetInput("Surname:", "", checkinputStringAlpha, lengthQuestionField, surnameMaxLenght, true, true, true, true, surnameMinLenght);
                Prefix = IO.GetInput("Prefix:", "", checkinputStringAlpha, lengthQuestionField, prefixMaxLength, true, true, true, true, prefixMinLenght);
                FirstName = IO.GetInput("First Name:", "", checkinputStringAlpha, lengthQuestionField, firstnameMaxLength, true, true, true, true, firstnameMinLength);
                DateOfBirth = ParseToDateTime(IO.GetInput("Date of Birth: (dd/mm/yyyy)", "", checkinputStringDate, lengthQuestionField, doBMaxLength, true, true, false, true, doBMinLenght));
                Address = IO.GetInput("Address:", "", checkinputStringAlpha, lengthQuestionField, addressMaxLenght, true, true, true, true, addressMinLenght);
                Zipcode = IO.GetInput("Zipcode: (####ZZ)", "", checkinputStringAlpha, lengthQuestionField, zipCodeMaxLenght, true, true, true, true, zipCodeMinLength);
                City = IO.GetInput("City:", "", checkinputStringAlpha, lengthQuestionField, cityMaxLenght, true, true, true, true, cityMinLenght);
                Telephone = IO.GetInput("Telephone:", "", "0123456789+-", lengthQuestionField, telMaxLenght, true, true, true, true, telMinLenght);
                Email = IO.GetInput("Email:", "", checkinputStringAlpha, lengthQuestionField, emailMaxLenght, true, true, true, true, emailMinLength);

                // construct unique employee ID
                EmployeeID = ConstructID(this);
            }
        }

        private static string ConstructID(Employee anEmployee)
        {
            string a = anEmployee.RecordCounter.ToString("D5");        // make a string consisting of 5 decimals
            string b;
            if (anEmployee.SurName.Length >= 3)
            {
                b = anEmployee.SurName.Substring(0, 3).ToUpper();      // take first 3 chars in uppercase
            }                                               // TODO: remove whitespace if exists ("de Groot")
            else
            {
                b = anEmployee.SurName.Substring(0, anEmployee.SurName.Length)    // or build to 3 chars with added "A" chars
                    .ToUpper()
                    .PadRight(3, 'A');
            }

            return b + a;
        }

        public static void EditRecord(List<Employee> aList, int aRecord)
        {
            // start editing the first field
            aList[aRecord - 1].SurName = IO.GetInput("Surname:", aList[aRecord - 1].SurName, checkinputStringAlpha, lengthQuestionField, surnameMaxLenght, true, true, true, true, surnameMinLenght);
            aList[aRecord - 1].Prefix = IO.GetInput("Prefix:", aList[aRecord - 1].Prefix, checkinputStringAlpha, lengthQuestionField, prefixMaxLength, true, true, true, true, prefixMinLenght);
            aList[aRecord - 1].FirstName = IO.GetInput("First Name:", aList[aRecord - 1].FirstName, checkinputStringAlpha, lengthQuestionField, firstnameMaxLength, true, true, true, true, firstnameMinLength);
            aList[aRecord - 1].DateOfBirth = ParseToDateTime(IO.GetInput("Date of Birth: (dd/mm/yyyy)", aList[aRecord - 1].DateOfBirth.ToString("dd/MM/yyyy"), checkinputStringDate, lengthQuestionField, doBMaxLength, true, true, false, true, doBMinLenght));
            aList[aRecord - 1].Address = IO.GetInput("Address:", aList[aRecord - 1].Address, checkinputStringAlpha, lengthQuestionField, addressMaxLenght, true, true, true, true, addressMinLenght);
            aList[aRecord - 1].Zipcode = IO.GetInput("Zipcode: (####ZZ)", aList[aRecord - 1].Zipcode, checkinputStringAlpha, lengthQuestionField, zipCodeMaxLenght, true, true, true, true, zipCodeMinLength);
            aList[aRecord - 1].City = IO.GetInput("City:", aList[aRecord - 1].City, checkinputStringAlpha, lengthQuestionField, cityMaxLenght, true, true, true, true, cityMinLenght);
            aList[aRecord - 1].Telephone = IO.GetInput("Telephone:", aList[aRecord - 1].Telephone, "0123456789+-", lengthQuestionField, telMaxLenght, true, true, true, true, telMinLenght);
            aList[aRecord - 1].Email = IO.GetInput("Email:", aList[aRecord - 1].Email, checkinputStringAlpha, lengthQuestionField, emailMaxLenght, true, true, true, true, emailMinLength);

            aList[aRecord - 1].EmployeeID = ConstructID(aList[aRecord -1]);


            //IO.GetInput("Surname:", aList[aRecord].SurName, checkinputStringAlpha, lengthQuestionField, surnameMaxLenght, true, true, true, true, surnameMinLenght);
            //Prefix = IO.GetInput("Prefix:", "", checkinputStringAlpha, lengthQuestionField, prefixMaxLength, true, true, true, true, prefixMinLenght);
        }

        public static void DisplayRecord(List<Employee> aList, int aRecord, bool aClearform)             //TODO: Somehow make this compacter => probably via an interface?
        {
            var cursor = Console.CursorTop;
            if (aClearform)
            {
                IO.PrintBoundaries(fieldNames[0], "", lengthQuestionField, 8, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[1], "", lengthQuestionField, surnameMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[2], "", lengthQuestionField, prefixMaxLength, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[3], "", lengthQuestionField, firstnameMaxLength, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[4], "", lengthQuestionField, doBMaxLength, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[5], "", lengthQuestionField, addressMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[6], "", lengthQuestionField, zipCodeMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[7], "", lengthQuestionField, cityMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[8], "", lengthQuestionField, telMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[9], "", lengthQuestionField, emailMaxLenght, cursor); Console.WriteLine(); cursor++;
            }
            else
            {
                IO.PrintBoundaries(fieldNames[0], aList[aRecord - 1].EmployeeID, lengthQuestionField, 8, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[1], aList[aRecord - 1].SurName.ToString(), lengthQuestionField, surnameMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[2], aList[aRecord - 1].Prefix.ToString(), lengthQuestionField, prefixMaxLength, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[3], aList[aRecord - 1].FirstName.ToString(), lengthQuestionField, firstnameMaxLength, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[4], aList[aRecord - 1].DateOfBirth.ToString("dd/MM/yyyy"), lengthQuestionField, doBMaxLength, cursor); // Console.WriteLine(); // cursor++;
                Console.SetCursorPosition(lengthQuestionField + doBMaxLength + 5, cursor);
                Console.WriteLine("(Age: " + (CalculateAge(aList[aRecord - 1].DateOfBirth).ToString()) + ")    "); cursor++;
                IO.PrintBoundaries(fieldNames[5], aList[aRecord - 1].Address, lengthQuestionField, addressMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[6], aList[aRecord - 1].Zipcode.ToString(), lengthQuestionField, zipCodeMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[7], aList[aRecord - 1].City.ToString(), lengthQuestionField, cityMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[8], aList[aRecord - 1].Telephone.ToString(), lengthQuestionField, telMaxLenght, cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[9], aList[aRecord - 1].Email.ToString(), lengthQuestionField, emailMaxLenght, cursor); Console.WriteLine(); cursor++;
            }
        }

        public static void WriteToFile(string aFilename, List<Employee> aEmployeeList)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(aEmployeeList, Formatting.Indented);
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
            if (getemployeelist != null)                            // file Exists
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

        public static int CalculateAge(DateTime aDateTime)
        {
            var age = DateTime.Today.Year - aDateTime.Year;                   // not taking date into account eg. 2021-1997
            int nowMonthandDay = int.Parse(DateTime.Now.ToString("MMdd"));      // convert month and day to int
            int thenMonthandDay = int.Parse(aDateTime.ToString("MMdd"));
            if (nowMonthandDay < thenMonthandDay)
            {
                age--;                                                          // if current date (in MMdd) < date of birth subtract 1 year
            }
            return age;
        }

        private static DateTime ParseToDateTime(string aDateString)
        
        {
            DateTime parsedDateHelpstring;
            if (DateTime.TryParse(aDateString, out parsedDateHelpstring))   // Tryparse method passing back two values: bool and out var
            {
                if (CalculateAge(parsedDateHelpstring) > 100 || CalculateAge(parsedDateHelpstring) < 18)      //TODO: move age check to set; of DoB?
                {
                    IO.PrintOnConsole($"Invalid Age: {CalculateAge(parsedDateHelpstring)} ", 1, 34);
                    parsedDateHelpstring = DateTime.Parse("01/01/0001");
                }
                else
                {
                    IO.PrintOnConsole($"Parsed date string succesfully to {parsedDateHelpstring:dd-MM-yyyy}", 1, 34);
                }
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