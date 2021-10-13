using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlaaieboer
{
    class Customer
    {

        readonly int nameMaxLenght = 45; readonly int nameMinLenght = 1;
        readonly int addressMaxLenght = 45; readonly int addressMinLenght = 1;
        readonly int zipMaxLenght = 45; readonly int zipMinLenght = 1;
        readonly int cityMaxLenght = 45; readonly int cityMinLenght = 1;
        readonly int telMaxLenght = 45; readonly int telMinLenght = 0;
        readonly int emailMaxLenght = 45; readonly int emailMinLenght = 1;
        readonly int lengthQuestionField = 30;
        readonly string checkinputStringAlpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .,_";

        static int totalRecords = 0;            // public accessible recordcounter

        public int RecordCounter { get; set; }
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public Customer()
        {
            // TODO: Getinput customer
            RecordCounter++;         // TODO read from file
            Name = IO.GetInput("Name: ", checkinputStringAlpha, lengthQuestionField, nameMaxLenght, true, true, true, true, nameMinLenght);
            Address = IO.GetInput("Adress: ", checkinputStringAlpha, lengthQuestionField, addressMaxLenght, true, true, true, true, addressMinLenght);
            Zipcode = IO.GetInput("Zipcode: ", checkinputStringAlpha, lengthQuestionField, zipMaxLenght, true, true, true, true, zipMinLenght);
            City = IO.GetInput("City: ", checkinputStringAlpha, lengthQuestionField, cityMaxLenght, true, true, true, true, cityMinLenght);
            Telephone = IO.GetInput("Telephone: ", checkinputStringAlpha, lengthQuestionField, telMaxLenght, true, true, true, true, telMinLenght);
            Email = IO.GetInput("Email: ", checkinputStringAlpha, lengthQuestionField, emailMaxLenght, true, true, true, true, emailMinLenght);

            // TODO: build customer ID
            // TODO; store record
            totalRecords++;

        }


    }
}
