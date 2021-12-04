﻿using System;
using System.Runtime.InteropServices;

namespace BakeryConsole
{
    internal class WINDLL
    {
        public const int SC_CLOSE       = 0xF060;
        public const int SC_MINIMIZE    = 0xF020;
        public const int SC_MAXIMIZE    = 0xF030;
        public const int SC_SIZE        = 0xF000;
        /// <summary>
        ///  calls to disable resizing and closing/minimizing console window
        /// </summary>

        private const int MF_BYCOMMAND  = 0x00000000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        public static void SetConsoleWindowProperties()
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
    }
}