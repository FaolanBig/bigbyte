﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigbyte
{
    internal class Appearance
    {
    }
    internal class PrintIn
    {
        public static void blue(string toPrint)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(toPrint);
            Console.ResetColor();
        }
        public static void red(string toPrint)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(toPrint);
            Console.ResetColor();
        }
        public static void green(string toPrint)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(toPrint);
            Console.ResetColor();
        }
        public static void yellow(string toPrint)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(toPrint);
            Console.ResetColor();
        }
        public static void WigglyStarInBorders(int sleepingDuration = 100, int runs = 3)
        {
            Console.WriteLine();
            for (int ii = 0; ii < runs; ii++)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[*  ]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[** ]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[***]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[ **]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[  *]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[   ]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[  *]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[ **]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[***]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[** ]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[*  ]");
                Thread.Sleep(sleepingDuration);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                PrintIn.yellow("[   ]");
                Thread.Sleep(sleepingDuration);
            }
        }
    }
}
