﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;
using ConsoleLibrary;

namespace BakeryConsole
{
    class Login             // NICE: add login ID's and store users ID's in mutations
    {
        readonly string checkInputString    = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-@|.,_!#$%^&*";
        private string passWord             = "Bakery";
        public static bool validPassword;
        string inputString;

        public Login()
        {
            inputString = "Enter password: " + "(debug: " + passWord + ") ";
            string passWordInput = IO.GetInput(inputString, "", checkInputString, inputString.Length, 40,false, true, false, false, true, 0, 1);

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

        [JsonConstructor]                                              
        public Login(Int64 JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

    }

}
