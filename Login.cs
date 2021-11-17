using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace BakeryConsole
{
    class Login             // NICE: add login ID's and store users ID's in mutations
    {
        readonly string checkInputString    = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .,_!#$%^&*";
        private string passWord             = "bakker";
        public static bool validPassword;

        public Login()
        {
            string passWordInput = IO.GetInput("Enter password: ", "", checkInputString, 18, 56,false, true, false, false, true, 0);

            if (passWordInput == passWord)
            {
                IO.SystemMessage("Welcome, you have been logged in succesfully", false);
                validPassword = true;
                return;                             // exit if statement
            }
            else
            {
                IO.SystemMessage("Invalid password", true);
                validPassword = false;
            }

        }
    }

}
