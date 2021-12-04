using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace BakeryConsole
{

    internal class Prefs : WINDLL
    {

        public enum Color                              // this enum is for setting a color with Color()
        {                                           
            Input,
            MenuSelect,
            SystemMessage,
            Text,
            DefaultForeGround,
            DefaultBackGround,
            Inactive,
            Title,
            Inverted,
            Defaults
        }

        private static List<Prefs> userPrefs = new List<Prefs>();
        private static string settingsFile   = "settings.json";
        private static int minWinHeight      = 36;
        private static int minWinWidth       = 80;

        public ConsoleColor ForeGroundDefault { get; set; }
        public ConsoleColor BackGroundDefault { get; set; }
        public ConsoleColor MenuSelectDefault { get; set; }
        public ConsoleColor Title             { get; set; }
        public ConsoleColor TextHigh          { get; set; }
        public ConsoleColor InputText         { get; set; }
        public ConsoleColor WarningForeGround { get; set; }
        public ConsoleColor WarningBackGround { get; set; }
        public ConsoleColor ErrorForeGround   { get; set; }
        public ConsoleColor ErrorBackGround   { get; set; }
        public ConsoleColor SystemForeGround  { get; set; }
        public ConsoleColor SystemBackGround  { get; set; }
        public int WindowHeight               { get; set; }   
        public int WindowWidth                { get; set; }
        

        public Prefs(bool setStandard)               // set or reset to standard colors and windowsize
        {
            ForeGroundDefault = ConsoleColor.Gray;
            BackGroundDefault = ConsoleColor.DarkBlue;
            MenuSelectDefault = ConsoleColor.Cyan;
            Title             = ConsoleColor.Yellow;
            TextHigh          = ConsoleColor.White;
            InputText         = ConsoleColor.Green;
            WarningForeGround = ConsoleColor.White;
            WarningBackGround = ConsoleColor.DarkBlue;
            ErrorForeGround   = ConsoleColor.Red;
            ErrorBackGround   = ConsoleColor.White;
            SystemForeGround  = ConsoleColor.White;
            SystemBackGround  = ConsoleColor.DarkBlue;
            WindowHeight      = 36;                     // minimum values
            WindowWidth       = 80;
        }

        public Prefs()                               // reserved for future color sets
        {

        }

        [JsonConstructor]
        public Prefs(string JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        public static void InitializeColors()
        {
            if (!File.Exists(settingsFile))             // set standard colors and create file
            {
                userPrefs.Add(new Prefs(true));         // call constructor to set standards
                SetStandardColor();
                SaveColors();
                IO.SystemMessage("Setting standard colors and creating settings file", false);
            }
            else
            {
                userPrefs = JSON.PopulateList<Prefs>(settingsFile);
                SetColor (Color.Defaults);
                IO.SystemMessage("User preferences succesfully loaded", false);
            }
            Console.BackgroundColor = userPrefs[0].BackGroundDefault;
            ResizeConsoleWindow();
            IO.SetConsoleDimensions(userPrefs[0].WindowWidth, userPrefs[0].WindowHeight);
        }   

        public static void SetStandardColor()                           // for setting and resetting default color scheme
        {
            userPrefs[0] = new Prefs(true);

            Console.BackgroundColor = userPrefs[0].BackGroundDefault;   // set backgroudcolor for Console.Clear();
            IO.SystemMessage("Reset text colors to default settings", false);
        }

        public static void SaveColors()
        {
            JSON.WriteToFile<Prefs>(settingsFile, userPrefs, true);
        }

        public static int GetWindowHeight()
        {
            return userPrefs[0].WindowHeight;
        }

        public static int GetWindowWidth()
        {
            return userPrefs[0].WindowWidth;
        }


        public static void SetWindowSize(int aWidth, int aHeight)
        {
            if (userPrefs[0].WindowWidth  +  aWidth  >= minWinWidth & 
                userPrefs[0].WindowWidth  +  aWidth  <= Console.LargestWindowWidth) 
                userPrefs[0].WindowWidth  = aWidth;

            if (userPrefs[0].WindowHeight +  aHeight >= minWinHeight &
                userPrefs[0].WindowHeight +  aHeight <= Console.LargestWindowHeight) 
                userPrefs[0].WindowHeight = aHeight;
        }


        public static void ResizeConsoleWindow()
        {
             Console.SetWindowSize(userPrefs[0].WindowWidth,userPrefs[0].WindowHeight);
             
             //Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
             Console.SetBufferSize(userPrefs[0].WindowWidth,userPrefs[0].WindowHeight);
        }

        


        public static void SetWarningColor (bool aWarning)          // swap between warning and error colors for Color.SystemMessage
        {
            if (aWarning)
            {
                userPrefs[0].SystemForeGround = userPrefs[0].ErrorForeGround;
                userPrefs[0].SystemBackGround = userPrefs[0].ErrorBackGround;
            }
            else
            {
                userPrefs[0].SystemForeGround = userPrefs[0].WarningForeGround;
                userPrefs[0].SystemBackGround = userPrefs[0].WarningBackGround;
            }
        }

        public static void CycleColors(int aChoice, bool aRndBackground)
        {
            IO.SetWarningLength(300);
            int newColor;
            switch (aChoice)
            {
                case 0:     // input text color
                    
                    newColor = (int)userPrefs[0].InputText;
                    newColor = PickNextValidColor();
                    userPrefs[0].InputText = (ConsoleColor)newColor;
                    IO.SystemMessage("Set Input text color to " + (userPrefs[0].InputText.GetType()
                                    .GetEnumName(userPrefs[0].InputText)
                                    .ToString())
                                    ,false);
                    break;
                
                
                case 1:     //Text High

                    newColor = (int)userPrefs[0].TextHigh;                      // get the int value of enum ConsoleColor Usercolor[0].TextHigh;
                    newColor = PickNextValidColor();                            // get next color, != background
                    userPrefs[0].TextHigh = (ConsoleColor)newColor;             // set userColor[0] to new value;
                    IO.SystemMessage("Set Text_High color to " +
                       (userPrefs[0].TextHigh.GetType()                         // get the name of the enum constant
                        .GetEnumName(userPrefs[0].TextHigh)
                        .ToString()), false);
                    break;

                case 2:     //foreground

                    newColor = (int)userPrefs[0].ForeGroundDefault;
                    newColor = PickNextValidColor();
                    userPrefs[0].ForeGroundDefault = (ConsoleColor)newColor;
                    IO.SystemMessage("Set Foreground color to " + 
                      ( userPrefs[0].ForeGroundDefault.GetType()
                       .GetEnumName(userPrefs[0].ForeGroundDefault)
                       .ToString()), false);
                    break;

                case 3:     //background

                    newColor = (int)userPrefs[0].BackGroundDefault;
                    newColor = PickNextValidColor();
                    userPrefs[0].BackGroundDefault = (ConsoleColor)newColor;
                    Console.BackgroundColor = userPrefs[0].BackGroundDefault;   // set backgroundcolor here before Console.Clear() in main loop
                    IO.SystemMessage("Set Background color to " + 
                      ( userPrefs[0].BackGroundDefault.GetType()
                       .GetEnumName(userPrefs[0].BackGroundDefault)
                       .ToString()), false );
                    break;

                case 4:     //menu select 

                    newColor = (int)userPrefs[0].MenuSelectDefault;
                    newColor = PickNextValidColor();
                    userPrefs[0].MenuSelectDefault = (ConsoleColor)newColor;
                    IO.SystemMessage("Set Menu Select color to " + 
                       ( userPrefs[0].MenuSelectDefault.GetType()
                        .GetEnumName(userPrefs[0].MenuSelectDefault)
                        .ToString()), false );
                    break;

                case 5:     // title / license 

                    newColor = (int)userPrefs[0].Title;
                    newColor = PickNextValidColor();
                    userPrefs[0].Title = (ConsoleColor)newColor;
                    IO.SystemMessage("Set License text color to " + 
                       ( userPrefs[0].Title.GetType()
                        .GetEnumName(userPrefs[0].Title)
                        .ToString()), false );

                    break;

                case 6:     //randomize with or without including backgroundcolor

                    var rand = new Random();
                    if    (aRndBackground) 
                          { userPrefs[0].BackGroundDefault = (ConsoleColor)rand.Next(16);    }

                    do    { userPrefs[0].TextHigh          = (ConsoleColor)rand.Next(16);    }
                    while ( userPrefs[0].TextHigh          == userPrefs[0].BackGroundDefault );
                    
                    do    { userPrefs[0].ForeGroundDefault = (ConsoleColor)rand.Next(16);    } 
                    while ( userPrefs[0].ForeGroundDefault == userPrefs[0].BackGroundDefault );
                    
                    do    { userPrefs[0].MenuSelectDefault = (ConsoleColor)rand.Next(16);    } 
                    while ( userPrefs[0].MenuSelectDefault == userPrefs[0].BackGroundDefault 
                          | userPrefs[0].MenuSelectDefault == userPrefs[0].ForeGroundDefault );

                    do    { userPrefs[0].Title             = (ConsoleColor)rand.Next(16);    }
                    while ( userPrefs[0].Title             == userPrefs[0].BackGroundDefault );
                    
                    do    { userPrefs[0].InputText         = (ConsoleColor)rand.Next(16);    }
                    while ( userPrefs[0].InputText         == userPrefs[0].BackGroundDefault 
                          | userPrefs[0].InputText         == userPrefs[0].TextHigh 
                          | userPrefs[0].InputText         == userPrefs[0].ForeGroundDefault 
                          | userPrefs[0].InputText         == userPrefs[0].MenuSelectDefault );

                    IO.SystemMessage("Randomized text colors", false);

                    break;
            }
            if (Debugger.IsAttached)
            {
                IO.PrintOnConsole(((int)userPrefs[0].BackGroundDefault).ToString(), 1, 1, Prefs.Color.Defaults);
            }

            Console.BackgroundColor = userPrefs[0].BackGroundDefault;
            IO.SetWarningLength(Program.warningLenghtDefault);

            int PickNextValidColor()
            {
                newColor++;
                if (newColor == 16) { newColor = 0; }                                   // increase with 1 until 16, then reset to 0 (Usercolor has 0-15 value)
                if (newColor == (int)userPrefs[0].BackGroundDefault) { newColor++; }    // not same as current background?

                return newColor;
            }
        }

        public static void SetColor(Color textColor)                     // sets font and background color
        {
            
            
            Console.BackgroundColor = userPrefs[0].BackGroundDefault;
            
            switch (textColor)
            {
                case Color.Input:

                    Console.ForegroundColor = userPrefs[0].InputText;
                    break;

                case Color.MenuSelect:

                    Console.ForegroundColor = userPrefs[0].MenuSelectDefault;
                    break;

                case Color.SystemMessage:
                    Console.ForegroundColor = userPrefs[0].SystemForeGround;
                    Console.BackgroundColor = userPrefs[0].SystemBackGround;
                    break;

                case Color.Text:

                    Console.ForegroundColor = userPrefs[0].TextHigh;
                    break;

                case Color.DefaultForeGround:                                        // Standard foreground color

                    Console.ForegroundColor = userPrefs[0].ForeGroundDefault;
                    break;
                    
                case Color.DefaultBackGround:
                    Console.BackgroundColor = userPrefs[0].BackGroundDefault;
                    break;

                case Color.Inactive:
                    if (userPrefs[0].BackGroundDefault != ConsoleColor.DarkGray)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    break;

                case Color.Title:
                    Console.ForegroundColor = userPrefs[0].Title;
                    break;

                case Color.Inverted:
                    Console.ForegroundColor = userPrefs[0].BackGroundDefault;
                    Console.BackgroundColor = userPrefs[0].ForeGroundDefault;
                    break;

                case Color.Defaults:
                    Console.ForegroundColor = userPrefs[0].ForeGroundDefault;
                    Console.BackgroundColor = userPrefs[0].BackGroundDefault;

                    break;

                default:
                    break;
            }
        }
    }
}