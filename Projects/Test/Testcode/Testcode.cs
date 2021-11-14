using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;


class Program
{
    private static readonly object ConsoleLock = new object();
    private static readonly int ConsoleColumns = Console.WindowWidth;
    private static int _cursorPosX = 0;
    private static int _cursorPosY = 6;
    private static string _keysRead;

    public static void Main(string[] args)
    {
        var timerMain = new Thread(TimerThread) { IsBackground = true };
        timerMain.Start();

        // Main application.
        GetInput();
    }


    private static void TimerThread()
    {
        for (int i = 1, mins = -1; i <= 1860; i++)
        {
            lock (ConsoleLock)
            {
                Console.SetCursorPosition(0, 35);
                if (i % 60 == 1)
                {
                    mins++;
                }
                Console.WriteLine("Timer: " + mins + " minute(s) and " + i % 60 + " seconds elapsed");
                Console.SetCursorPosition(_cursorPosX, _cursorPosY);
            }
            Thread.Sleep(1000);
        }
    }

    private static void ConsoleMoveCursorForward()
    {
        _cursorPosX++;
        if (_cursorPosX != ConsoleColumns) return;
        _cursorPosX = 0;
        _cursorPosY++;
    }

    private static void ConsoleMoveCursorBack()
    {
        _cursorPosX--;
        if (_cursorPosX != -1) return;
        _cursorPosX = ConsoleColumns - 1;
        _cursorPosY--;
    }

    /// <summary>
    /// Reads a line from console, without echo.
    /// </summary>
    /// <returns></returns>
    private static string ReadLine()
    {
        var builder = new StringBuilder();
        while (true)
        {
            char keyRead = Console.ReadKey(true).KeyChar;
            switch (keyRead)
            {
                // <Backspace> key.
                case '\b':
                    builder = builder.Remove(builder.Length - 1, 1);
                    lock (ConsoleLock)
                    {
                        ConsoleMoveCursorBack();
                        Console.SetCursorPosition(_cursorPosX, _cursorPosY);
                        Console.Write(' ');
                        Console.SetCursorPosition(_cursorPosX, _cursorPosY);
                    }
                    continue;
                // <Enter> key.
                case '\n':
                case '\r':
                    if ((keyRead == 13) && Console.KeyAvailable && (Console.In.Peek() == 10))
                    {
                        Console.ReadKey(true);
                    }
                    return builder.ToString();
            }
            builder.Append(keyRead);
            lock (ConsoleLock)
            {
                Console.Write(keyRead);
                //_cursorPosX++;
                ConsoleMoveCursorForward();
            }
        }
    }

    public static void GetInput()
    {
        // Read a line from console.
        _keysRead = ReadLine();

        // TODO: do something with that text.
    }

    
}