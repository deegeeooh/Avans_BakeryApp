using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Vlaaieboer
{
    internal class Person
    {
        private static int lengthQuestionField = 30;

        // input validation strings
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";

        private static string checkinputStringDate = "0123456789/-";

        // public accessible total record counter
        public static int totalRecords = 0;

        //
        // 2 dimensional array with 3 columns per row: fieldNames index (for readability, not necessary), field max length, field min required length
        //

        private static int[,] fieldProperties = { { 0, 8, 1 },
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
                                              { 11, 45, 0 },
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
        public List<Mutation> Mutations { get; set; }           //TODO: probably not a good idea :P
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
                SurName = IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
                Prefix = IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
                FirstName = IO.GetInput(fieldNames[3], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
                Gender = IO.GetInput(fieldNames[4], "", "MmFfXx", lengthQuestionField, fieldProperties[4, 1], true, true, true, true, true, fieldProperties[4, 2]);
                RelationType = IO.GetInput(fieldNames[5], "", "YyNn", lengthQuestionField, fieldProperties[5, 1], true, true, true, true, true, fieldProperties[5, 2]);
                JobTitle = IO.GetInput(fieldNames[6], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[6, 1], true, true, true, true, true, fieldProperties[6, 2]);
                DateOfBirth = ParseToDateTime(IO.GetInput(fieldNames[7], "", checkinputStringDate, lengthQuestionField, fieldProperties[7, 1], false, true, true, false, true, fieldProperties[7, 2]));
                Address = IO.GetInput(fieldNames[8], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[8, 1], false, true, true, true, true, fieldProperties[8, 2]);
                Zipcode = IO.GetInput(fieldNames[9], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[9, 1], false, true, true, true, true, fieldProperties[9, 2]);
                City = IO.GetInput(fieldNames[10], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[10, 1], false, true, true, true, true, fieldProperties[10, 2]);
                Country = IO.GetInput(fieldNames[11], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[11, 1], false, true, true, true, true, fieldProperties[11, 2]);
                Telephone = IO.GetInput(fieldNames[12], "", "0123456789+-", lengthQuestionField, fieldProperties[12, 1], false, true, true, true, true, fieldProperties[12, 2]);
                Email = IO.GetInput(fieldNames[13], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[13, 1], false, true, true, true, true, fieldProperties[13, 2]);

                CheckMutations (this," ","[Created:]", 1);          // create a single mutation to indicate creation datestamp

                // construct unique employee ID
                PersonID = ConstructID(this);
                Active = true;
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

            var old = aList[aRecord - 1].SurName;
            aList[aRecord - 1].SurName = IO.GetInput(
                                         fieldNames[1],
                                         aList[aRecord - 1].SurName,
                                         checkinputStringAlpha,
                                         lengthQuestionField,
                                         fieldProperties[1, 1],
                                         false,
                                         true,
                                         true,
                                         true,
                                         true,
                                         fieldProperties[1, 2]);
            var newVal = aList[aRecord - 1].SurName;
            CheckMutations(aList[aRecord - 1], old, newVal, 1);

            aList[aRecord - 1].Prefix = IO.GetInput(fieldNames[2], aList[aRecord - 1].Prefix, checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
            aList[aRecord - 1].FirstName = IO.GetInput(fieldNames[3], aList[aRecord - 1].FirstName, checkinputStringAlpha, lengthQuestionField, fieldProperties[3, 1], false, true, true, true, true, fieldProperties[3, 2]);
            aList[aRecord - 1].Gender = IO.GetInput(fieldNames[4], aList[aRecord - 1].Gender, "mMfFxX", lengthQuestionField, fieldProperties[4, 1], true, true, true, true, true, fieldProperties[4, 2]);
            aList[aRecord - 1].RelationType = IO.GetInput(fieldNames[5], aList[aRecord - 1].RelationType, "YyNn", lengthQuestionField, fieldProperties[5, 1], true, true, true, true, true, fieldProperties[5, 2]);         //TODO: select from array from EmployeeRoles
            aList[aRecord - 1].JobTitle = IO.GetInput(fieldNames[6], aList[aRecord - 1].JobTitle, checkinputStringAlpha, lengthQuestionField, fieldProperties[6, 1], true, true, true, true, true, fieldProperties[6, 2]);
            aList[aRecord - 1].DateOfBirth = ParseToDateTime(IO.GetInput(fieldNames[7], aList[aRecord - 1].DateOfBirth.ToString("dd/MM/yyyy"), checkinputStringDate, lengthQuestionField, fieldProperties[7, 1], false, true, true, false, true, fieldProperties[7, 2]));
            aList[aRecord - 1].Address = IO.GetInput(fieldNames[8], aList[aRecord - 1].Address, checkinputStringAlpha, lengthQuestionField, fieldProperties[8, 1], false, true, true, true, true, fieldProperties[8, 2]);
            aList[aRecord - 1].Zipcode = IO.GetInput(fieldNames[9], aList[aRecord - 1].Zipcode, checkinputStringAlpha, lengthQuestionField, fieldProperties[9, 1], false, true, true, true, true, fieldProperties[9, 2]);
            aList[aRecord - 1].City = IO.GetInput(fieldNames[10], aList[aRecord - 1].City, checkinputStringAlpha, lengthQuestionField, fieldProperties[10, 1], false, true, true, true, true, fieldProperties[10, 2]);
            aList[aRecord - 1].Country = IO.GetInput(fieldNames[11], aList[aRecord - 1].Country, checkinputStringAlpha, lengthQuestionField, fieldProperties[11, 1], false, true, true, true, true, fieldProperties[11, 2]);
            aList[aRecord - 1].Telephone = IO.GetInput(fieldNames[12], aList[aRecord - 1].Telephone, "0123456789+-", lengthQuestionField, fieldProperties[12, 1], false, true, true, true, true, fieldProperties[12, 2]);
            aList[aRecord - 1].Email = IO.GetInput(fieldNames[13], aList[aRecord - 1].Email, checkinputStringAlpha, lengthQuestionField, fieldProperties[13, 1], false, true, true, true, true, fieldProperties[13, 2]);

            aList[aRecord - 1].PersonID = ConstructID(aList[aRecord - 1]);
            aList[aRecord - 1].Active = true;
        }

        private static void CheckMutations(Person aPerson, string old, string newVal, int aFieldnumber)
        {
            if (old != newVal)
            {
                int len;
                if (aPerson.Mutations != null)
                {
                    len = aPerson.Mutations.Count;
                }
                else
                {
                    aPerson.Mutations = new List<Mutation>();            // set object reference so we can use Mutations.Add
                    len = 0;
                }

                Mutation a = new Mutation(len + 1,
                                          DateTime.Now,
                                          fieldNames[aFieldnumber],
                                          old,
                                          newVal.Replace(old, ""),
                                          newVal);

                aPerson.Mutations.Add(a);                                // needs object reference when = null;
            }
        }

        public static void DisplayRecord(List<Person> aList, int aRecord, bool clearform)             //TODO: Somehow make this compacter => probably via an interface?
        {
            var cursor = Console.CursorTop;
            if (clearform)
            {
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor); Console.WriteLine(); cursor++;
                }
            }
            else
            {
                IO.PrintBoundaries(fieldNames[0], aList[aRecord - 1].PersonID, lengthQuestionField, fieldProperties[0, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[1], aList[aRecord - 1].SurName, lengthQuestionField, fieldProperties[1, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[2], aList[aRecord - 1].Prefix, lengthQuestionField, fieldProperties[2, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[3], aList[aRecord - 1].FirstName, lengthQuestionField, fieldProperties[3, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[4], aList[aRecord - 1].Gender, lengthQuestionField, fieldProperties[4, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[5], aList[aRecord - 1].RelationType, lengthQuestionField, fieldProperties[5, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[6], aList[aRecord - 1].JobTitle, lengthQuestionField, fieldProperties[6, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[7], aList[aRecord - 1].DateOfBirth.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[7, 1], cursor); // Console.WriteLine(); // cursor++;
                Console.SetCursorPosition(lengthQuestionField + fieldProperties[7, 1] + 5, cursor);
                Console.WriteLine("(Age: " + (CalculateAge(aList[aRecord - 1].DateOfBirth).ToString()) + ")    "); cursor++;
                IO.PrintBoundaries(fieldNames[8], aList[aRecord - 1].Address, lengthQuestionField, fieldProperties[8, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[9], aList[aRecord - 1].Zipcode, lengthQuestionField, fieldProperties[9, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[10], aList[aRecord - 1].City, lengthQuestionField, fieldProperties[10, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[11], aList[aRecord - 1].Country, lengthQuestionField, fieldProperties[11, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[12], aList[aRecord - 1].Telephone, lengthQuestionField, fieldProperties[12, 1], cursor); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[13], aList[aRecord - 1].Email, lengthQuestionField, fieldProperties[13, 1], cursor); Console.WriteLine(); cursor++;
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
            if (File.Exists(aFilename))                                       // and causes object not defined error
            {                                                                 // when calling employeeList.add from main()
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