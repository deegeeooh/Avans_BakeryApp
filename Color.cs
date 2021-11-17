﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace BakeryConsole
{

    /// <summary>
    ///  Totally unnecessary but nicely working color stuff
    /// </summary>

    internal class Color
    {

        public enum TextColors                              // this enum is for setting a color with Color()
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

        private static List<Color> userColor = new List<Color>();
        private static string settingsFile   = "settings.json";

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


        public Color(bool setStandard)               // set or reset to standard colors
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
        }

        public Color()                               // reserved for future color sets
        {

        }

        [JsonConstructor]
        public Color(string JUST4JSON_DontCall)
        {
            //Console.WriteLine("Don't be a dick Jason dear"); Console.ReadKey();
        }

        public static void InitializeColors()
        {
            if (!File.Exists(settingsFile))             // set standard colors and create file
            {
                userColor.Add(new Color(true));
                SetStandardColor();
                SaveColors();
                IO.SystemMessage("Setting standard colors and creating settings file", false);
            }
            else
            {
                userColor = IO.PopulateList<Color>(settingsFile);
                SetColor (TextColors.Defaults);
            }
            Console.BackgroundColor = userColor[0].BackGroundDefault;
        }

        public static void SetStandardColor()                           // for setting and resetting default color scheme
        {
            userColor[0] = new Color(true);
            Console.BackgroundColor = userColor[0].BackGroundDefault;   // set backgroudcolor for Console.Clear();
            IO.SystemMessage("Reset text colors to default settings", false);
        }

        public static void SaveColors()
        {
            IO.WriteToFile<Color>(settingsFile, userColor, "");
        }

        public static void SetWarningColor (bool aWarning)          // swap between warning and error colors for Color.SystemMessage
        {
            if (aWarning)
            {
                userColor[0].SystemForeGround = userColor[0].ErrorForeGround;
                userColor[0].SystemBackGround = userColor[0].ErrorBackGround;
            }
            else
            {
                userColor[0].SystemForeGround = userColor[0].WarningForeGround;
                userColor[0].SystemBackGround = userColor[0].WarningBackGround;
            }
        }

        public static void CycleColors(int aChoice, bool aRndBackground)
        {
            IO.SetWarningLength(300);
            int newColor;
            switch (aChoice)
            {
                case 0:     //Text High
                                       
                    newColor = (int)userColor[0].TextHigh;                      // get the int value of enum ConsoleColor Usercolor[0].TextHigh;
                    newColor++; if (newColor == 16) { newColor = 0; }           // increase with 1 until 16, then reset to 0 (Usercolor has 0-15 value)
                    userColor[0].TextHigh = (ConsoleColor)newColor;             // set userColor[0] to new value;
                    IO.SystemMessage("Set Text_High color to "+ 
                       ( userColor[0].TextHigh.GetType()                        // get the name of the enum constant
                        .GetEnumName(userColor[0].TextHigh)
                        .ToString()), false);
                    break;


                case 1:     //foreground

                    newColor = (int)userColor[0].ForeGroundDefault;
                    newColor++; if (newColor == 16) { newColor = 0; }
                    userColor[0].ForeGroundDefault = (ConsoleColor)newColor;
                    IO.SystemMessage("Set Foreground color to " + 
                      ( userColor[0].ForeGroundDefault.GetType()
                       .GetEnumName(userColor[0].ForeGroundDefault)
                       .ToString()), false);
                    break;

                case 2:     //background

                    newColor = (int)userColor[0].BackGroundDefault;
                    newColor++; if (newColor == 16) { newColor = 0; }
                    userColor[0].BackGroundDefault = (ConsoleColor)newColor;
                    Console.BackgroundColor = userColor[0].BackGroundDefault;   // set backgroundcolor here before Console.Clear() in main loop
                    IO.SystemMessage("Set Background color to " + 
                      ( userColor[0].BackGroundDefault.GetType()
                       .GetEnumName(userColor[0].BackGroundDefault)
                       .ToString()), false );
                    break;

                case 3:     //menu select 

                    newColor = (int)userColor[0].MenuSelectDefault;
                    newColor++; if (newColor == 16) { newColor = 0; }
                    userColor[0].MenuSelectDefault = (ConsoleColor)newColor;
                    IO.SystemMessage("Set Menu Select color to " + 
                       ( userColor[0].MenuSelectDefault.GetType()
                        .GetEnumName(userColor[0].MenuSelectDefault)
                        .ToString()), false );
                    break;

                case 4:     // title / license 

                    newColor = (int)userColor[0].Title;
                    newColor++; if (newColor == 16) { newColor = 0; }
                    userColor[0].Title = (ConsoleColor)newColor;
                    IO.SystemMessage("Set License text color to " + 
                       ( userColor[0].Title.GetType()
                        .GetEnumName(userColor[0].Title)
                        .ToString()), false );

                    break;


                case 5:     //randomize with or without including backgroundcolor

                    var rand = new Random();
                    if    (aRndBackground) 
                          { userColor[0].BackGroundDefault = (ConsoleColor)rand.Next(16);    }

                    do    { userColor[0].TextHigh          = (ConsoleColor)rand.Next(16);    }
                    while ( userColor[0].TextHigh          == userColor[0].BackGroundDefault );
                    
                    do    { userColor[0].ForeGroundDefault = (ConsoleColor)rand.Next(16);    } 
                    while ( userColor[0].ForeGroundDefault == userColor[0].BackGroundDefault );
                    
                    do    { userColor[0].MenuSelectDefault = (ConsoleColor)rand.Next(16);    } 
                    while ( userColor[0].MenuSelectDefault == userColor[0].BackGroundDefault 
                          | userColor[0].MenuSelectDefault == userColor[0].ForeGroundDefault );

                    do    { userColor[0].Title             = (ConsoleColor)rand.Next(16);    }
                    while ( userColor[0].Title             == userColor[0].BackGroundDefault );
                    
                    do    { userColor[0].InputText         = (ConsoleColor)rand.Next(16);    }
                    while ( userColor[0].InputText         == userColor[0].BackGroundDefault 
                          | userColor[0].InputText         == userColor[0].TextHigh 
                          | userColor[0].InputText         == userColor[0].ForeGroundDefault 
                          | userColor[0].InputText         == userColor[0].MenuSelectDefault );
                            //userColor[0].ErrorBackGround   = ConsoleColor.White;
                            //userColor[0].ErrorForeGround   = ConsoleColor.Red;

                    IO.SystemMessage("Randomized text colors", false);

                    break;

                case 6:     // input text color
                    
                    newColor = (int)userColor[0].InputText;
                    newColor++; 
                    if (newColor == 16 ) 
                       { newColor = 0; }
                    userColor[0].InputText = (ConsoleColor)newColor;
                    
                    IO.SystemMessage("Set Input text color to " + (userColor[0].InputText.GetType()
                                    .GetEnumName(userColor[0].InputText)
                                    .ToString())
                                    ,false);
                    break;
            }
            if (Debugger.IsAttached)
            {
                IO.PrintOnConsole(((int)userColor[0].BackGroundDefault).ToString(), 1, 1, Color.TextColors.Defaults);
            }

            Console.BackgroundColor = userColor[0].BackGroundDefault;
            IO.SetWarningLength(Program.warningLenghtDefault);
        }

        public static void SetColor(TextColors textColor)                     // sets font and background color
        {
            
            
            Console.BackgroundColor = userColor[0].BackGroundDefault;
            
            switch (textColor)
            {
                case TextColors.Input:

                    Console.ForegroundColor = userColor[0].InputText;
                    break;

                case TextColors.MenuSelect:

                    Console.ForegroundColor = userColor[0].MenuSelectDefault;
                    break;

                case TextColors.SystemMessage:
                    Console.ForegroundColor = userColor[0].SystemForeGround;
                    Console.BackgroundColor = userColor[0].SystemBackGround;
                    break;

                case TextColors.Text:

                    Console.ForegroundColor = userColor[0].TextHigh;
                    break;

                case TextColors.DefaultForeGround:                                        // Standard foreground color

                    Console.ForegroundColor = userColor[0].ForeGroundDefault;
                    break;
                    
                case TextColors.DefaultBackGround:
                    Console.BackgroundColor = userColor[0].BackGroundDefault;
                    break;

                case TextColors.Inactive:
                    if (userColor[0].BackGroundDefault != ConsoleColor.DarkGray)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    break;

                case TextColors.Title:
                    Console.ForegroundColor = userColor[0].Title;
                    break;

                case TextColors.Inverted:
                    Console.ForegroundColor = userColor[0].BackGroundDefault;
                    Console.BackgroundColor = userColor[0].ForeGroundDefault;
                    break;

                case TextColors.Defaults:
                    Console.ForegroundColor = userColor[0].ForeGroundDefault;
                    Console.BackgroundColor = userColor[0].BackGroundDefault;

                    break;

                default:
                    break;
            }
        }
    }
}