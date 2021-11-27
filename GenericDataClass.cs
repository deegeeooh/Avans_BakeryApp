using Newtonsoft.Json;
using System;


namespace BakeryConsole
{
    internal class GenericDataClass : RecordManager
    { 
        private static int lengthQuestionField = 30;

        // input validation string
        public static string checkinputStringAlpha  = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| '.,_";
        private static string _DescriptionFieldName = "Description";

        private static int[,] fieldProperties;       


        private static new String[] fieldNames;      
                                               
        
        public string[] StrVal { get; set; }

        // public string Description { get; set; }

        public GenericDataClass() : base(_DescriptionFieldName)
        {

            StrVal = new string[fieldProperties.GetLength(0)];      //instantiate StrVal with number of entries

            for (int i = 0; i < fieldProperties.GetLength(0); i++)
            {
                StrVal[i] = IO.GetInput(fieldNames[i],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[i, 1], false, true, true, true, true, fieldProperties[i, 2]);
            }
                       
            //Code             = IO.GetInput(fieldNames[0],  "", checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2]);
        }
        public GenericDataClass(bool clearForm, bool _Activatordummy) : base (clearForm, _DescriptionFieldName,  true)                                   // Constructor for displaying clear input form
        {
            var cursor = Console.CursorTop;
            for (int i = 0; i < fieldProperties.GetLength(0); i++)
            {
                IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
            }
        }

        public GenericDataClass( GenericDataClass anObject, bool displayOnly ) : base(anObject, displayOnly, _DescriptionFieldName, true)
        {
            if (!displayOnly)            //Edit
            {
                StrVal = new string[fieldProperties.GetLength(0)]; 
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    StrVal[i] = IO.GetInput(fieldNames[i],  anObject.StrVal[i], checkinputStringAlpha, lengthQuestionField, fieldProperties[i, 1], false, true, true, true, true, fieldProperties[i, 2]);
                    CheckMutations(anObject, anObject.StrVal[i], this.StrVal[i], fieldNames[i], anObject.Mutations.Count);
                }
                 //CheckMutations(anEmployeeRole, anEmployeeRole.Code, this.Code, fieldNames[0], anEmployeeRole.Mutations.Count);
            }
            else                        // Display Only
            {
                int cursorColumn = Console.CursorTop;
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], anObject.StrVal[i], lengthQuestionField, fieldProperties[i, 1], cursorColumn, anObject.Active); Console.WriteLine(); cursorColumn++;
                }
                //IO.PrintBoundaries(fieldNames[0], anEmployeeRole.Code, lengthQuestionField, fieldProperties[0, 1], cursorColumn, anEmployeeRole.Active); Console.WriteLine(); cursorColumn++;
            }
        }

        [JsonConstructor]                                              
        public GenericDataClass(Int64 JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }



        /// <summary>
        /// This method sets the field properties for the configurable fields
        /// </summary>
        /// <param name="aFieldLength">Set the fieldlenght of the code field</param>
        public static void SetCodeFieldLength(int aFieldLength)
        {
            fieldProperties[0, 1] = aFieldLength;
        }


        /// <summary>
        /// Sets the fieldnames arrray for this class
        /// </summary>
        /// <param name="AFieldNameDescription">name of the generic description field</param>
        public static void SetNameFieldName (string aFieldNameDescription)
        {
            _DescriptionFieldName = aFieldNameDescription;
        }

        public static void SetFieldNamesArray (String[] _fieldNames)
        {
            fieldNames = _fieldNames;
        }

        public static void SetFieldPropertiesArray( int[,] _fieldProperties )
        {
            fieldProperties = _fieldProperties;
        }


    }
}