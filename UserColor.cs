using System;

namespace Vlaaieboer
{
    internal class UserColor
    {
        public ConsoleColor ForeGroundDefault { get; set; }
        public ConsoleColor BackGroundDefault { get; set; }
        public ConsoleColor MenuSelectDefault { get; set; }
        public ConsoleColor Title { get; set; }
        public ConsoleColor TextHigh { get; set; }
        public ConsoleColor InputText { get; set; }

        public UserColor()
        {
            ForeGroundDefault = ConsoleColor.Gray;
            BackGroundDefault = ConsoleColor.Black;
            MenuSelectDefault = ConsoleColor.Cyan;
            Title             = ConsoleColor.Yellow;
            TextHigh          = ConsoleColor.White;
            InputText         = ConsoleColor.Green;
        }
    }
}