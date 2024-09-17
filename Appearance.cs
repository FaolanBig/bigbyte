/*
    bigbyte serves as a package manager.
    It serves as a management tool for downloading, installing, updating, upgrading, removing and running various software aplications from the command line.

    The latest source code can be found on https://github.com/FaolanBig/bigbyte

    Copyright (C) 2024  Carl Öttinger (Carl Oettinger)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as published
    by the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program. If not, see <https://www.gnu.org/licenses/>.

    You can contact me in the following ways:
        EMail: oettinger.carl@web.de, big-programming@web.de
 */


using System;
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
