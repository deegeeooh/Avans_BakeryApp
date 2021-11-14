using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace BakeryConsole
{
    class Employee : Person
    {
        //
        // 2 dimensional array with 3 columns per row: fieldNames index (for readability, not necessary), field max length, field min required length
        //

        private static int[,] fieldProperties = { { 0,  3,  1 },
                                                  { 1, 10, 10 },
                                                  { 2, 10,  0 },
                                                  { 3,  10, 0 } };

        private static String[] fieldNames =      { "Job Title:",                                  //0
                                                    "Date joined:",                                //1
                                                    "Date exit:",                                  //2
                                                    "Salary per month:" };                         //3
                                               
        public string JobTitle      { get; set; }
        public DateTime DateJoined  { get; set; }
        public DateTime DateExit    { get; set; }
        public int Salary           { get; set; }
        public bool IsEmployee      { get; set; }
        

        public Employee() : base ()           // Constructor method; gets executed whenever we call '= new Employee()'
        {
            //totalRecords++;
            //RecordCounter = totalRecords;
            JobTitle         = IO.GetInput(fieldNames[0],                    "", checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true,  fieldProperties[0, 2]);
            DateJoined       = IO.ParseToDateTime(IO.GetInput(fieldNames[1], "", checkinputStringDate,  lengthQuestionField, fieldProperties[1, 1], false, true, true, false, true, fieldProperties[1, 2]), false);
            DateExit         = IO.ParseToDateTime(IO.GetInput(fieldNames[2], "", checkinputStringDate,  lengthQuestionField, fieldProperties[2, 1], false, true, true, false, true, fieldProperties[2, 2]), false);
            string getSalary = IO.GetInput(fieldNames[3], "", checkinputStringNum, lengthQuestionField, fieldProperties[3, 1], true, true, true, true, true, fieldProperties[3, 2]);
            Salary = (getSalary != "") ? Int32.Parse(getSalary) : 0;

            //Salary      = Int32.Parse(IO.GetInput(fieldNames[3],        "", checkinputStringNum,   lengthQuestionField, fieldProperties[3, 1], true, true, true, true, true,   fieldProperties[3, 2]));
            // SickDays    = Int32.Parse(IO.GetInput(empFieldnames[4],        "", checkinputStringNum,   lengthQuestionField, empFieldProp[4, 1], true, true, true, true, true,   empFieldProp[4, 2]));
            IsEmployee  = true;
            
            
            //CheckMutations(this, " ", "[Created:]", "", 0);          // create a single mutation to indicate creation datestamp
        }

        public Employee(bool clearForm) : base (clearForm)
        {
            var cursor = Console.CursorTop;
            if (clearForm)
            {
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
                }
            }
        }

        public Employee(Employee anEmployee, bool displayOnly ) : base (anEmployee, displayOnly) //TODO: clear
        {
            if (!displayOnly)
            {

                JobTitle = IO.GetInput(fieldNames[0], anEmployee.JobTitle, checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2]);
                DateJoined = IO.ParseToDateTime(IO.GetInput(fieldNames[1], anEmployee.DateJoined.ToString("dd/MM/yyyy"), checkinputStringDate, lengthQuestionField, fieldProperties[1, 1], false, true, true, false, true, fieldProperties[1, 2]), false);
                DateExit = IO.ParseToDateTime(IO.GetInput(fieldNames[2], anEmployee.DateExit.ToString("dd/MM/yyyy"), checkinputStringDate, lengthQuestionField, fieldProperties[2, 1], false, true, true, false, true, fieldProperties[2, 2]), false);
                Salary = Int32.Parse(IO.GetInput(fieldNames[3], anEmployee.Salary.ToString(), checkinputStringNum, lengthQuestionField, fieldProperties[3, 1], true, true, true, true, true, fieldProperties[3, 2]));
                // SickDays    = Int32.Parse(IO.GetInput(empFieldnames[4], anEmployee.SickDays.ToString(), checkinputStringNum, lengthQuestionField, empFieldProp[4, 1], true, true, true, true, true, empFieldProp[4, 2]));
                IsEmployee = true;

                //if (anEmployee.Mutations == null)
                //{
                //    anEmployee.Mutations = new List<Mutation>();
                //}
                this.Mutations = anEmployee.Mutations;

                // check which values changed and store them in the Person.Mutations list

                CheckMutations(anEmployee, anEmployee.JobTitle, this.JobTitle, fieldNames[0], anEmployee.Mutations.Count);                          // we are using this with the new instanced value:
                CheckMutations(anEmployee, anEmployee.DateJoined.ToString(), this.DateJoined.ToString(), fieldNames[1], anEmployee.Mutations.Count);
                CheckMutations(anEmployee, anEmployee.DateExit.ToString(), this.DateExit.ToString(), fieldNames[2], anEmployee.Mutations.Count);
                CheckMutations(anEmployee, anEmployee.Salary.ToString(), this.Salary.ToString(), fieldNames[3], anEmployee.Mutations.Count);
                // CheckMutations(this, anEmployee.SickDays.ToString(), this.SickDays.ToString(), 5);
            }
            else
            {
                var cursor = Console.CursorTop;

                IO.PrintBoundaries(fieldNames[0], anEmployee.JobTitle, lengthQuestionField, fieldProperties[0, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[1], anEmployee.DateJoined.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[1, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[2], anEmployee.DateExit.ToString("dd/MM/yyyy"), lengthQuestionField, fieldProperties[2, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                IO.PrintBoundaries(fieldNames[3], anEmployee.Salary.ToString().PadLeft(fieldProperties[3, 1],' '), lengthQuestionField, fieldProperties[3, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                //IO.PrintBoundaries(empFieldnames[5], anEmployee.SickDays.ToString(), lengthQuestionField, empFieldProp[5, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                
            }
        }

        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Employee(string JUST4JSON_DontCall) : base(JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }


    }
}