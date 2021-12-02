using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BakeryConsole
{
    internal class GenericDataClass : RecordManager
    { 
        private static int lengthQuestionField = 30;

        // input validation string
        public static string[] checkinputStringAlpha  =  {"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-@| '.,_",         // 0, alphanumeric input
                                                          "0123456789" + NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator,             // 1, numeric input with decimals
                                                          "0123456789",                                                                     // 2, integer input
                                                          "YyNn",                                                                           // 3, Yes/no
                                                          "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-@|.,_!#$%^&*" };  // 4, Password

        bool _toUpper;
        bool _showInput;
        bool _trim;

        private static string _DescriptionFieldName = "Description";

        private static int[,] fieldProperties;          // { 0 arrayindex,
                                                        //   1 fieldlength,
                                                        //   2 minimum input length,
                                                        //   3 inputString to use
                                                        //   4 showInput,
                                                        //   5 to upper
                                                        //   6 trim }
        private static new String[] fieldNames;      
                                               
        
        public string[] StrVal { get; set; }

        public int[,] FieldProperties { get; set; }

        public string[] FieldNames { get; set; }


        // public string Description { get; set; }

        public GenericDataClass() : base(_DescriptionFieldName)
        {

            StrVal          = new string[fieldProperties.GetLength(0)];      //instantiate StrVal with number of entries
            //FieldProperties = fieldProperties;                             // to store arrays in file
            //FieldNames      = fieldNames;    

            for (int i = 0; i < fieldProperties.GetLength(0); i++)
            {
                Checkbooleans(i);
                StrVal[i] = IO.GetInput(fieldNames[i], "", checkinputStringAlpha[fieldProperties[i, 3]], lengthQuestionField, fieldProperties[i, 1], _toUpper, true, _showInput, _trim, true, fieldProperties[i, 2]);
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
                    Checkbooleans(i);
                    StrVal[i] = IO.GetInput(fieldNames[i],  anObject.StrVal[i], checkinputStringAlpha[fieldProperties[i,3]], lengthQuestionField, fieldProperties[i, 1], _toUpper, true, _showInput, _trim, true, fieldProperties[i, 2]);
                    CheckMutations(anObject, anObject.StrVal[i], this.StrVal[i], fieldNames[i], anObject.Mutations.Count);
                }
                 //CheckMutations(anEmployeeRole, anEmployeeRole.Code, this.Code, fieldNames[0], anEmployeeRole.Mutations.Count);
            }
            else                        // Display Only
            {
                int cursorColumn = Console.CursorTop;
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    if (fieldProperties[i,3] == 1)              // numeric field
                    {
                        IO.PrintBoundaries(fieldNames[i], anObject.StrVal[i].PadLeft(fieldProperties[i,1],' '), lengthQuestionField, fieldProperties[i, 1], cursorColumn, anObject.Active); Console.WriteLine(); cursorColumn++;
                    }else
                    {
                        IO.PrintBoundaries(fieldNames[i], anObject.StrVal[i], lengthQuestionField, fieldProperties[i, 1], cursorColumn, anObject.Active); Console.WriteLine(); cursorColumn++;
                    }
                    
                }
                
            }
        }

        [JsonConstructor]                                              
        public GenericDataClass(Int64 JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }
        
        void Checkbooleans(int anInt)
            {
                _showInput  = (fieldProperties[anInt, 4] == 1) ? true : false;
                _toUpper    = (fieldProperties[anInt, 5] == 1) ? true : false;
                _trim       = (fieldProperties[anInt, 6] == 1) ? true : false;
            }

        public static void SetNameFieldName (string aFieldNameDescription)
        {
            _DescriptionFieldName = aFieldNameDescription;
        }

        public static void SetFieldNamesArray (String[] _fieldNames)
        {
            fieldNames = _fieldNames;
        }

        /// <summary>
        /// Set field properties with array {0,1,2,3,4,5,6,7}
        /// </summary>
        /// <param name="_fieldProperties"> arrayindex, fieldlength, minimum input length, inputString (0 alpha, 1 Num, 2 YN, 3 PW), showInput, to upper, trim </param>
        public static void SetFieldPropertiesArray( int[,] _fieldProperties )
        {
            fieldProperties = _fieldProperties;
        }

    }
}