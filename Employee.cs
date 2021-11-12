using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace BakeryConsole
{
    class Employee : Person
    {
        private static int lengthQuestionField = 30;

        // input validation strings
        //private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";

        //private static string checkinputStringDate = "0123456789/-";

        // public accessible total record counter
        // public static int totalRecords = 0;

        //
        // 2 dimensional array with 3 columns per row: fieldNames index (for readability, not necessary), field max length, field min required length
        //

        private static int[,] empFieldProp = {    { 0,  3,  1 },
                                                  { 1, 10, 10 },
                                                  { 2, 10,  0 },
                                                  { 3,  10, 0 } };

        private static String[] empFieldnames = { "Job Title:",                                  //0
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
            JobTitle    = IO.GetInput(empFieldnames[0],                    "", checkinputStringAlpha, lengthQuestionField, empFieldProp[0, 1], false, true, true, true, true,  empFieldProp[0, 2]);
            DateJoined  = IO.ParseToDateTime(IO.GetInput(empFieldnames[1], "", checkinputStringDate,  lengthQuestionField, empFieldProp[1, 1], false, true, true, false, true, empFieldProp[1, 2]), false);
            DateExit    = IO.ParseToDateTime(IO.GetInput(empFieldnames[2], "", checkinputStringDate,  lengthQuestionField, empFieldProp[2, 1], false, true, true, false, true, empFieldProp[2, 2]), false);
            Salary      = Int32.Parse(IO.GetInput(empFieldnames[3],        "", checkinputStringNum,   lengthQuestionField, empFieldProp[3, 1], true, true, true, true, true,   empFieldProp[3, 2]));
            // SickDays    = Int32.Parse(IO.GetInput(empFieldnames[4],        "", checkinputStringNum,   lengthQuestionField, empFieldProp[4, 1], true, true, true, true, true,   empFieldProp[4, 2]));
            IsEmployee  = true;
            
            //CheckMutations(this, " ", "[Created:]", 1);          // create a single mutation to indicate creation datestamp

        }

        public Employee(bool clearForm) : base (clearForm)
        {
            var cursor = Console.CursorTop;
            if (clearForm)
            {
                for (int i = 0; i < empFieldProp.GetLength(0); i++)
                {
                    IO.PrintBoundaries(empFieldnames[i], "", lengthQuestionField, empFieldProp[i, 1], cursor, false); Console.WriteLine(); cursor++;
                }
            }
        }


        public Employee(Employee anEmployee, bool displayOnly, bool clearForm) : base (anEmployee,displayOnly,clearForm)
        {
            if (!displayOnly)
            {

                JobTitle    = IO.GetInput(empFieldnames[0], anEmployee.JobTitle, checkinputStringAlpha, lengthQuestionField, empFieldProp[0, 1], false, true, true, true, true, empFieldProp[0, 2]);
                DateJoined  = IO.ParseToDateTime(IO.GetInput(empFieldnames[1], anEmployee.DateJoined.ToString("dd/MM/yyyy"), checkinputStringDate, lengthQuestionField, empFieldProp[1, 1], false, true, true, false, true, empFieldProp[1, 2]), false);
                DateExit    = IO.ParseToDateTime(IO.GetInput(empFieldnames[2], anEmployee.DateExit.ToString("dd/MM/yyyy"), checkinputStringDate, lengthQuestionField, empFieldProp[2, 1], false, true, true, false, true, empFieldProp[2, 2]), false);
                Salary      = Int32.Parse(IO.GetInput(empFieldnames[3], anEmployee.Salary.ToString(), checkinputStringNum, lengthQuestionField, empFieldProp[3, 1], true, true, true, true, true, empFieldProp[3, 2]));
               // SickDays    = Int32.Parse(IO.GetInput(empFieldnames[4], anEmployee.SickDays.ToString(), checkinputStringNum, lengthQuestionField, empFieldProp[4, 1], true, true, true, true, true, empFieldProp[4, 2]));
                IsEmployee  = true;
                
                // check which values changed and store them in the Person.Mutations list

                CheckMutations(this, anEmployee.JobTitle, this.JobTitle, 1);                // we are using this with the new instanced value:
                CheckMutations(this, anEmployee.DateJoined.ToString(), this.DateJoined.ToString(), 2);
                CheckMutations(this, anEmployee.DateExit.ToString(), this.DateExit.ToString(), 3);
                CheckMutations(this, anEmployee.Salary.ToString(), this.Salary.ToString(), 4);
               // CheckMutations(this, anEmployee.SickDays.ToString(), this.SickDays.ToString(), 5);
            }
            else
            {
                var cursor = Console.CursorTop;
                if (clearForm)
                {
                    for (int i = 0; i < empFieldProp.GetLength(0); i++)
                    {
                        IO.PrintBoundaries(empFieldnames[i], "", lengthQuestionField, empFieldProp[i, 1], cursor, false); Console.WriteLine(); cursor++;
                    }
                }
                else
                {
                    IO.PrintBoundaries(empFieldnames[0], anEmployee.JobTitle, lengthQuestionField, empFieldProp[0, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(empFieldnames[1], anEmployee.DateJoined.ToString("dd/MM/yyyy"), lengthQuestionField, empFieldProp[1, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(empFieldnames[2], anEmployee.DateExit.ToString("dd/MM/yyyy"), lengthQuestionField, empFieldProp[2, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                    IO.PrintBoundaries(empFieldnames[3], anEmployee.Salary.ToString(), lengthQuestionField, empFieldProp[3, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                    //IO.PrintBoundaries(empFieldnames[5], anEmployee.SickDays.ToString(), lengthQuestionField, empFieldProp[5, 1], cursor, anEmployee.Active); Console.WriteLine(); cursor++;
                }  
            }
        }

        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Employee(string JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }


    }
}