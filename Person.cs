using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Vlaaieboer
{
    internal class Person
    {
        // intialize field parameters
        //private static int surnameMaxLenght = 45; private static int surnameMinLenght = 1;

        //private static int prefixMaxLength = 35; private static int prefixMinLenght = 0;
        //private static int firstnameMaxLength = 30; private static int firstnameMinLength = 1;
        //private static int genderMaxLength = 1; private static int genderMinLenght = 1;
        //private static int relationMaxLength = 1; private static int relationMinLength = 1;
        //private static int jobTitleMaxLenght = 3; private static int jobTitleMinLenght = 0;
        //private static int doBMaxLength = 10; private static int doBMinLenght = 10;
        //private static int addressMaxLenght = 45; private static int addressMinLenght = 0;
        //private static int zipCodeMaxLenght = 6; private static int zipCodeMinLength = 0;
        //private static int cityMaxLenght = 45; private static int cityMinLenght = 0;
        //private static int countryMaxLength = 45; private static int countryMinLength = 0;
        //private static int telMaxLenght = 14; private static int telMinLenght = 0;
        //private static int emailMaxLenght = 45; private static int emailMinLength = 1;
        private static int lengthQuestionField = 30;

        // input validation strings
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";

        private static string checkinputStringDate = "0123456789/-";

        // public accessible total record counter
        public static int totalRecords = 0;

        //
        // 2 dimensional array with 3 columns per row: fieldNames index, field max length, field min required length
        //
        private static int[,] fieldsArray = { { 0, 8, 1 },
                                               { 1, 45, 1 },
                                               { 2, 35, 0 },
                                               { 3, 30, 1 },
                                               { 4, 1, 1 },
                                               { 5, 1, 1 },
                                               { 6, 3, 0 },
                                               { 7, 10, 10 },
                                               { 8, 45, 0 },
                                               { 9, 6, 0 },
                                               { 10, 45, 0 },
                                               { 11, 45, 1 },
                                               { 12, 14, 0 },
                                               { 13, 45, 1 } };

        private static String[] fieldNames = { "PersonID: ",                                  //0
                                               "Surname: ",                                   //1
                                               "Prefix:",                                     //2
                                               "First Name:",                                 //3
                                               "Gender: (M/F/X)",                             //4
                                               "Employee? (Y/N)",                             //5
                                               "Job title:",                                  //6
                                               "Date of Birth: (dd/mm/yyyy)",                 //7
                                               "Address: ",                                   //8
                                               "Zipcode: (####ZZ)",                           //9
                                               "City: ",                                      //10
                                               "Country: ",                                   //11
                                               "Telephone: ",                                 //12
                                               "Email: " };                                   //13

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
        public string PersonID { get; set; }
        public bool Active { get; set; }
        public Mutation[] Mutations { get; set; }
        public string Gender { get; set; }
        public string RelationType { get; set; }
        public string JobTitle { get; set; }                //TODO: jobtitle maintenance
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Prefix { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public Person(bool input)           // Constructor method; gets executed whenever we call '= new Employee()'
        {
            if (input)                      // bool input not used atm
            {
                totalRecords++;

                RecordCounter = totalRecords;
                SurName = IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldsArray[1 ,1], false, true, true, true, true, fieldsArray[1 ,2]);
                Prefix = IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldsArray[2, 1], false, true, true, true, true, fieldsArray[2, 2]);
                FirstName = IO.GetInput(fieldNames[3], "", checkinputStringAlpha, lengthQuestionField, fieldsArray[3, 1], false, true, true, true, true, fieldsArray[3, 2]);
                Gender = IO.GetInput(fieldNames[4], "", "MmFfXx", lengthQuestionField, fieldsArray[4, 1], true, true, true, true, true, fieldsArray[4, 2]);
                RelationType = IO.GetInput(fieldNames[5], "", "YyNn", lengthQuestionField, fieldsArray[5, 1], true, true, true, true, true, fieldsArray[5, 2]);
                JobTitle = IO.GetInput(fieldNames[6], "", checkinputStringAlpha, lengthQuestionField, fieldsArray[6, 1], true, true, true, true, true, fieldsArray[6, 2]);
                DateOfBirth = ParseToDateTime(IO.GetInput(fieldNames[7], "", checkinputStringDate, lengthQuestionField, fieldsArray[7, 1], false, true, true, false, true, fieldsArray[7, 2]));
                Address = IO.GetInput(fieldNames[8], "", checkinputStringAlpha, lengthQuestionField, fieldsArray[8, 1], false, true, true, true, true, fieldsArray[8, 2]);
                Zipcode = IO.GetInput(fieldNames[9], "", checkinputStringAlpha, lengthQuestionField, fieldsArray[9, 1], false, true, true, true, true, fieldsArray[9, 2]);
                City = IO.GetInput(fieldNames[10], "", checkinputStringAlpha, lengthQuestionField, fieldsArray[10, 1], false, true, true, true, true, fieldsArray[10, 2]);
                Country = IO.GetInput(fieldNames[11], "", checkinputStringAlpha, lengthQuestionField, fieldsArray[11, 1], false, true, true, true, true, fieldsArray[11, 2]);
                Telephone = IO.GetInput(fieldNames[12], "", "0123456789+-", lengthQuestionField, fieldsArray[12, 1], false, true, true, true, true, fieldsArray[12, 2]);
                Email = IO.GetInput(fieldNames[13], "", checkinputStringAlpha, lengthQuestionField, fieldsArray[13, 1], false, true, true, true, true, fieldsArray[13, 2]);

                // construct unique employee ID
                PersonID = ConstructID(this);
            }
        }

        private static string ConstructID(Person anEmployee)
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

        public static void EditRecord(List<Person> aList, int aRecord)
        {
            // start editing the first field
            aList[aRecord - 1].SurName = IO.GetInput(fieldNames[1], aList[aRecord - 1].SurName, checkinputStringAlpha, lengthQuestionField, fieldsArray[1, 1], false, true, true, true, true, fieldsArray[1, 2]);
            aList[aRecord - 1].Prefix = IO.GetInput(fieldNames[2], aList[aRecord - 1].Prefix, checkinputStringAlpha, lengthQuestionField, fieldsArray[2, 1], false, true, true, true, true, fieldsArray[2, 2]);
            aList[aRecord - 1].FirstName = IO.GetInput(fieldNames[3], aList[aRecord - 1].FirstName, checkinputStringAlpha, lengthQuestionField, fieldsArray[3, 1], false, true, true, true, true, fieldsArray[3, 2]);
            aList[aRecord - 1].Gender = IO.GetInput(fieldNames[4], aList[aRecord - 1].Gender, "mMfFxX", lengthQuestionField, fieldsArray[4, 1], true, true, true, true, true, fieldsArray[4, 2]);
            aList[aRecord - 1].RelationType = IO.GetInput(fieldNames[5], aList[aRecord - 1].RelationType, "YyNn", lengthQuestionField, fieldsArray[5, 1], true, true, true, true, true, fieldsArray[5, 2]);         //TODO: select from array from EmployeeRoles
            aList[aRecord - 1].JobTitle = IO.GetInput(fieldNames[6], aList[aRecord - 1].JobTitle, checkinputStringAlpha, lengthQuestionField, fieldsArray[6, 1], true, true, true, true, true, fieldsArray[6, 2]);
            aList[aRecord - 1].DateOfBirth = ParseToDateTime(IO.GetInput(fieldNames[7], aList[aRecord - 1].DateOfBirth.ToString("dd/MM/yyyy"), checkinputStringDate, lengthQuestionField, fieldsArray[7, 1], false, true, true, false, true, fieldsArray[7, 2]));
            aList[aRecord - 1].Address = IO.GetInput(fieldNames[8], aList[aRecord - 1].Address, checkinputStringAlpha, lengthQuestionField, fieldsArray[8, 1], false, true, true, true, true, fieldsArray[8, 2]);
            aList[aRecord - 1].Zipcode = IO.GetInput(fieldNames[9], aList[aRecord - 1].Zipcode, checkinputStringAlpha, lengthQuestionField, fieldsArray[9, 1], false, true, true, true, true, fieldsArray[9, 2]);
            aList[aRecord - 1].City = IO.GetInput(fieldNames[10], aList[aRecord - 1].City, checkinputStringAlpha, lengthQuestionField, fieldsArray[10, 1], false, true, true, true, true, fieldsArray[10, 2]);
            aList[aRecord - 1].Country = IO.GetInput(fieldNames[11], aList[aRecord - 1].Country, checkinputStringAlpha, lengthQuestionField, fieldsArray[11, 1], false, true, true, true, true, fieldsArray[11, 2]);
            aList[aRecord - 1].Telephone = IO.GetInput(fieldNames[12], aList[aRecord - 1].Telephone, "0123456789+-", lengthQuestionField, fieldsArray[12, 1], false, true, true, true, true, fieldsArray[12, 2]);
            aList[aRecord - 1].Email = IO.GetInput(fieldNames[13], aList[aRecord - 1].Email, checkinputStringAlpha, lengthQuestionField, fieldsArray[13, 1], false, true, true, true, true, fieldsArray[13, 2]);

            aList[aRecord - 1].PersonID = ConstructID(aList[aRecord - 1]);
            aList[aRecord - 1].Active = true;

            //IO.GetInput("Surname:", aList[aRecord].SurName, checkinputStringAlpha, lengthQuestionField, surnameMaxLenght, true, true, true, true, surnameMinLenght);
            //Prefix = IO.GetInput("Prefix:", "", checkinputStringAlpha, lengthQuestionField, prefixMaxLength, true, true, true, true, prefixMinLenght);
        }

        public static void DisplayRecord(List<Person> aList, int aRecord, bool clearform)             //TODO: Somehow make this compacter => probably via an interface?
        {
            var cursor = Console.CursorTop;
            if (clearform)
            {
                for (int i = 0; i < fieldsArray.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldsArray[i, 1], cursor); Console.WriteLine(); cursor++;
                }

                //IO.PrintBoundaries(fieldNames[0], "", lengthQuestionField, 8, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[1], "", lengthQuestionField, surnameMaxLenght, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[2], "", lengthQuestionField, prefixMaxLength, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[3], "", lengthQuestionField, firstnameMaxLength, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[4], "", lengthQuestionField, genderMaxLength, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[5], "", lengthQuestionField, relationMaxLength, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[6], "", lengthQuestionField, jobTitleMaxLenght, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[7], "", lengthQuestionField, doBMaxLength, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[8], "", lengthQuestionField, addressMaxLenght, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[9], "", lengthQuestionField, zipCodeMaxLenght, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[10], "", lengthQuestionField, cityMaxLenght, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[11], "", lengthQuestionField, countryMaxLength, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[12], "", lengthQuestionField, telMaxLenght, cursor); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(fieldNames[13], "", lengthQuestionField, emailMaxLenght, cursor); Console.WriteLine(); cursor++;
            }
            else
            {
                IO.PrintBoundaries(fieldNames[0], aList[aRecord - 1].PersonID, lengthQuestionField, fieldsArray[0, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[1], aList[aRecord - 1].SurName, lengthQuestionField, fieldsArray[1, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[2], aList[aRecord - 1].Prefix, lengthQuestionField, fieldsArray[2, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[3], aList[aRecord - 1].FirstName, lengthQuestionField, fieldsArray[3, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[4], aList[aRecord - 1].Gender, lengthQuestionField, fieldsArray[4, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[5], aList[aRecord - 1].RelationType, lengthQuestionField, fieldsArray[5, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[6], aList[aRecord - 1].JobTitle, lengthQuestionField, fieldsArray[6, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[7], aList[aRecord - 1].DateOfBirth.ToString("dd/MM/yyyy"), lengthQuestionField, fieldsArray[7, 1], cursor); // Console.WriteLine(); // cursor++;
                Console.SetCursorPosition(lengthQuestionField + fieldsArray[7, 1] + 5, cursor);
                Console.WriteLine("(Age: " + (CalculateAge(aList[aRecord - 1].DateOfBirth).ToString()) + ")    "); cursor++;
                IO.PrintBoundaries(fieldNames[8], aList[aRecord - 1].Address, lengthQuestionField, fieldsArray[8, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[9], aList[aRecord - 1].Zipcode, lengthQuestionField, fieldsArray[9, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[10], aList[aRecord - 1].City, lengthQuestionField, fieldsArray[10, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[11], aList[aRecord - 1].Country, lengthQuestionField, fieldsArray[11, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[12], aList[aRecord - 1].Telephone, lengthQuestionField, fieldsArray[12, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[13], aList[aRecord - 1].Email, lengthQuestionField, fieldsArray[13, 1], cursor); Console.WriteLine(); cursor++;
            }
        }

        public static void WriteToFile(string aFilename, List<Person> aEmployeeList)
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

        public static List<Person> PopulateList(string aFilename)
        {
            var getemployeelist = DeserializeJSONfile(aFilename);
            if (getemployeelist != null)                            // file Exists
            {
                Person.totalRecords = getemployeelist.Count;      // set total Record static field in Employee Class
            }

            return getemployeelist;
        }

        private static List<Person> DeserializeJSONfile(string aFilename)
        {
            var listFromJason = new List<Person>();                           // define here so method doesn't return NULL
            if (File.Exists(aFilename))                                         // and causes object not defined error
            {                                                                   // when calling employeeList.add from main()
                try
                {
                    listFromJason = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(aFilename));
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
                    IO.PrintOnConsole($"Invalid Age: {CalculateAge(parsedDateHelpstring)} ".PadRight(30, ' '), 1, 34);
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