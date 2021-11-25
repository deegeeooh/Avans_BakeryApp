using Newtonsoft.Json;
using System;


namespace BakeryConsole
{
    internal class EmployeeRoles : RecordManager
    { 
        public static int lengthQuestionField = 30;

        // input validation string
        public static string checkinputStringAlpha  = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";
        private static string _DescriptionFieldName = "Description";

        private static int[,] fieldProperties       = { { 0, 3, 1 } };


        private static String[] fieldNames          = { "Code: " };                                      //0
                                               

        public string Code { get; set; }
        // public string Description { get; set; }

        public EmployeeRoles() : base(_DescriptionFieldName)
        {
            Code             = IO.GetInput(fieldNames[0],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], true, true, true, true, true, fieldProperties[0, 2]);
        }
        public EmployeeRoles(bool clearForm, bool _Activatordummy) : base (clearForm, _DescriptionFieldName,  true)                                   // Constructor for displaying clear input form
        {
            var cursor = Console.CursorTop;
            for (int i = 0; i < fieldProperties.GetLength(0); i++)
            {
                IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
            }
        }

        public EmployeeRoles( EmployeeRoles anEmployeeRole, bool displayOnly ) : base(anEmployeeRole, displayOnly, _DescriptionFieldName, true)
        {
            if (!displayOnly)            //Edit
            {
                 Code             = IO.GetInput(fieldNames[0],  anEmployeeRole.Code, checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], true, true, true, true, true, fieldProperties[0, 2]);
                 CheckMutations(anEmployeeRole, anEmployeeRole.Code, this.Code, fieldNames[0], anEmployeeRole.Mutations.Count);
            }
            else                        // Display Only
            {
                int cursorColumn = Console.CursorTop;
                IO.PrintBoundaries(fieldNames[0], anEmployeeRole.Code, lengthQuestionField, fieldProperties[0, 1], cursorColumn, anEmployeeRole.Active); Console.WriteLine(); cursorColumn++;
            }
        }

        [JsonConstructor]                                              
        public EmployeeRoles(Int64 JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

    }
}