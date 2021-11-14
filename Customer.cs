using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BakeryConsole
{
    internal class Customer
    {

        // initialize field properties
        private static int nameMaxLenght = 45; private static int nameMinLenght = 1;
        private static int addressMaxLenght = 45; private static int addressMinLenght = 1;
        private static int zipMaxLenght = 45; private static int zipMinLenght = 1;
        private static int cityMaxLenght = 45; private static int cityMinLenght = 1;
        private static int telMaxLenght = 45; private static int telMinLenght = 0;
        private static int emailMaxLenght = 45; private static int emailMinLenght = 1;
        private static int lengthQuestionField = 30;
        
        
        
        private static string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@|' .,_";
        // public accessible recordcounter
        private static int totalRecords = 0;
        //private static string[] personID;

        public int RecordCounter            { get; set; }
        public string ID                    { get; set; }
        public bool Active                  { get; set; }
        public List<Mutation> Mutations     { get; set; }
        public List<Person> Representatives { get; set; }
        public string CompanyName           { get; set; }
        public string Address               { get; set; }
        public string Zipcode               { get; set; }
        public string City                  { get; set; }
        public string Telephone             { get; set; }
        public string Email                 { get; set; }

        public Customer()
        {
            // TODO: finish customer
            RecordCounter++;         
            //Name = IO.GetInput("Name: ", "", checkinputStringAlpha, lengthQuestionField, nameMaxLenght, true, true, true, true, nameMinLenght);
            //Address = IO.GetInput("Adress: ", "", checkinputStringAlpha, lengthQuestionField, addressMaxLenght, true, true, true, true, addressMinLenght);
            //Zipcode = IO.GetInput("Zipcode: ", "", checkinputStringAlpha, lengthQuestionField, zipMaxLenght, true, true, true, true, zipMinLenght);
            //City = IO.GetInput("City: ", "", checkinputStringAlpha, lengthQuestionField, cityMaxLenght, true, true, true, true, cityMinLenght);
            //Telephone = IO.GetInput("Telephone: ", "", checkinputStringAlpha, lengthQuestionField, telMaxLenght, true, true, true, true, telMinLenght);
            //Email = IO.GetInput("Email: ", "", checkinputStringAlpha, lengthQuestionField, emailMaxLenght, true, true, true, true, emailMinLenght);
                        
            totalRecords++;
        }
    }
}