using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BakeryConsole
{
    internal class Customer : Address
    {
        private static int lengthQuestionField      = 30;
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-@|' .,_";
        private static string telephoneString       = "0123456789+-";
        private static string zipCodeString         = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
        private static int[,] fieldProperties       = { { 0, 45, 0 },                //NICE : Use attributes instead
                                                        { 1, 45, 0 },
                                                        { 2,  1, 0 } };

        // user interface fields
        private static String[] fieldNames          = { "Representative:",                // 0
                                                        "Job Title:"     ,                // 1
                                                        "Customer Type:" };               // 2

        private static string _DescriptionFieldName = "Company Name";               // to set fieldname of Address class' generic Name Property
        public string MainContact   { get; set; }           // placeholder, this will be replaced by Person.ID in a list 
        public string Jobtitle      { get; set; }
        public string CustomerType  { get; set; }
        //public List<Person> Representatives { get; set; }
        

/*1st*/ public Customer() : base(_DescriptionFieldName)
        {
            var cursorRow = Console.CursorTop;

            MainContact  = IO.GetInput(fieldNames[0], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2]);
            Jobtitle     = IO.GetInput(fieldNames[1], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
            CustomerType = IO.GetInput(fieldNames[2], "", checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
            
/*2nd*/     GetAddressFields(new Address());                                       // clone instances field values to this

        }

/*1st*/ public Customer( bool clearForm ) : base(clearForm, _DescriptionFieldName, true)  // call Address, _1st part
        {
            var cursor = Console.CursorTop;
            if (clearForm)
            {
                for (int i = 0; i < fieldProperties.GetLength(0); i++)
                {
                    IO.PrintBoundaries(fieldNames[i], "", lengthQuestionField, fieldProperties[i, 1], cursor, false); Console.WriteLine(); cursor++;
                }
            }
/*2nd*/     _ = new Address(clearForm, _DescriptionFieldName, false);                     // call Address, _2nd part
        }

        public Customer(Customer aCustomer, bool displayOnly) : base(aCustomer, displayOnly, _DescriptionFieldName, true)    // Constructor for edit and display existing record
        {
/*1st*/     if (!displayOnly)  //EDIT
            {
                RecordCounter   = aCustomer.RecordCounter;
                MainContact     = IO.GetInput(fieldNames[0], aCustomer.MainContact,  checkinputStringAlpha, lengthQuestionField, fieldProperties[0, 1], false, true, true, true, true, fieldProperties[0, 2]);
                Jobtitle        = IO.GetInput(fieldNames[1], aCustomer.Jobtitle,     checkinputStringAlpha, lengthQuestionField, fieldProperties[1, 1], false, true, true, true, true, fieldProperties[1, 2]);
                CustomerType    = IO.GetInput(fieldNames[2], aCustomer.CustomerType, checkinputStringAlpha, lengthQuestionField, fieldProperties[2, 1], false, true, true, true, true, fieldProperties[2, 2]);
                
                this.Mutations  = aCustomer.Mutations;
                
                CheckMutations(aCustomer, aCustomer.MainContact,  this.MainContact,  fieldNames[0], aCustomer.Mutations.Count);
                CheckMutations(aCustomer, aCustomer.Jobtitle,     this.Jobtitle,     fieldNames[1], aCustomer.Mutations.Count);
                CheckMutations(aCustomer, aCustomer.CustomerType, this.CustomerType, fieldNames[2], aCustomer.Mutations.Count);

/*2nd*/         GetAddressFields(new Address(aCustomer, displayOnly, _DescriptionFieldName, false));   // clone instances field values to this

                CheckMutations(aCustomer, aCustomer.Street,       this.Street,      Address.fieldNames[0], aCustomer.Mutations.Count);
                CheckMutations(aCustomer, aCustomer.Zipcode,      this.Zipcode,     Address.fieldNames[1], aCustomer.Mutations.Count);
                CheckMutations(aCustomer, aCustomer.City,         this.City,        Address.fieldNames[2], aCustomer.Mutations.Count);
                CheckMutations(aCustomer, aCustomer.Country,      this.Country,     Address.fieldNames[3], aCustomer.Mutations.Count);
                CheckMutations(aCustomer, aCustomer.Telephone,    this.Telephone,   Address.fieldNames[4], aCustomer.Mutations.Count);
                CheckMutations(aCustomer, aCustomer.Email,        this.Email,       Address.fieldNames[5], aCustomer.Mutations.Count);
            }

/*1st*/     else              // DISPLAY ONLY
            {
                var cursorColumn = Console.CursorTop;

                IO.PrintBoundaries(fieldNames[0], aCustomer.MainContact,  lengthQuestionField, fieldProperties[0, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[1], aCustomer.Jobtitle,     lengthQuestionField, fieldProperties[1, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
                IO.PrintBoundaries(fieldNames[2], aCustomer.CustomerType, lengthQuestionField, fieldProperties[2, 1], cursorColumn, aCustomer.Active); Console.WriteLine(); cursorColumn++;
                
/*2nd*/         _ = new Address(aCustomer, displayOnly, _DescriptionFieldName, false);
            }

        }

        [JsonConstructor]                                               // for json, otherwise it will use the default() constructor when deserializing which we don't want here
        public Customer(Int64 JUST4JSON_DontCall) : base (JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        private void GetAddressFields(Address _newInstance)
        {
            this.Street    = _newInstance.Street;
            this.Zipcode   = _newInstance.Zipcode;
            this.City      = _newInstance.City;
            this.Country   = _newInstance.Country;
            this.Telephone = _newInstance.Telephone;
            this.Email     = _newInstance.Email;
        }

    }
}