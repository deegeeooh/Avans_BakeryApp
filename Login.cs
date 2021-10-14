using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Vlaaieboer
{
    class Login
    {
        readonly string checkInputString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789//-@| .,_!#$%^&*";
        private string passWord = "bakker";
        public static bool validPassword;

        public Login()
        {
            string passWordInput = IO.GetInput("Enter password: ", "", checkInputString, 18, 56, true, false, false, true, 0);

            if (passWordInput == this.passWord)
            {
                validPassword = true;
                IO.PrintOnConsole("You have been logged in succesfully", 1, 34);
                Thread.Sleep(500);
                return;                             // exit if statement
            }
            else
            {
                Console.WriteLine("Invalid password");
                validPassword = false;
                //Log.validPassword = false;
                Thread.Sleep(500);
            }

        }
    }

}




