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
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    You can contact me in the following ways:
        EMail: oettinger.carl@web.de, big-programming@web.de
 */




using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigbyte
{
    internal class ArgumentHandler
    {
        public static void analyzer(string[] args)
        {
            List<string> switchesWithoutArguments = new List<string>()
            {
                "f", //force
                "h",
            };

            List<string> switchesArgumentExpected = new List<string>()
            {
                "r"
            };

            List<KeyValuePair<string, string>> acceptedArgumentsToSwitches = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("update", "u"),
                new KeyValuePair<string, string>("upgrade", "g"),
                new KeyValuePair<string, string>("exe", "e"),
                new KeyValuePair<string, string>("execute", "e"),
                new KeyValuePair<string, string>("install", "i"),
                new KeyValuePair<string, string>("remove", "r"),
                new KeyValuePair<string, string>("help", "h"),
                new KeyValuePair<string, string>("verify", "v")
            };

            List<KeyValuePair<string, string>> switchPairs = [];

            /*List<string> acceptedArguments = new List<string>()
            {
                "help",
                "install",
                "remove",
                "force-remove",
                "update",
                "upgrade",
                "full-upgrade",
                "run",
                "verify"
            };*/

            Dictionary<string, Action> acceptedArguments = new Dictionary<string, Action>
            {
                { "help", () => argumentHelp(args) },
                { "h", () => argumentHelp(args) },
                { "install", () => argumentInstall(args) },
                { "i", () => argumentInstall(args) },
                { "remove", () => argumentRemove(args) },
                { "r", () => argumentRemove(args) },
                { "force-remove", () => argumentForceRemove(args) },
                { "forceremove", () => argumentForceRemove(args) },
                { "fr", () => argumentForceRemove(args) },
                { "update", () => argumentUpdate(args) },
                { "u", () => argumentUpdate(args) },
                { "upgrade", () => argumentUpgrade(args) },
                { "g", () => argumentUpgrade(args) },
                { "full-upgrade", () => argumentFullUpgrade(args) },
                { "fullupgrade", () => argumentFullUpgrade(args) },
                { "f", () => argumentFullUpgrade(args) },
                { "run", () => argumentRun(args) },
                { "execute", () => argumentRun(args) },
                { "exe", () => argumentRun(args) },
                { "e", () => argumentRun(args) },
                { "verify", () => argumentVerify(args) },
            };

            if (!acceptedArguments.ContainsKey(args[0]))
            {
                ToLog.Err($"bad argument: {args[0]} | try help to see accepted arguments");
            }
            else
            {
                acceptedArguments[args[0]].Invoke();
            }

            /*for (int i = 0; i < args.Length; i++)
            {
                string argHold = args[i];



                if (argHold.StartsWith("-"))
                {
                    argHold.Replace("-", "");

                    if (i == args.Length - 1 || args[i + 1].StartsWith("-"))
                    {
                        if (switchesArgumentExpected.Contains(argHold))
                        {
                            ToLog.Err($"ignored argument: {argHold}");  
                        }
                        else
                        {

                        }
                    }
                }
            }*/
        }

        private static void argumentHelp(string[] args) 
        {
            ToLog.Inf("running: help - display help message");
            Console.WriteLine(Helper.GetFileContentsTxt(VarHold.helpFilePath));
            Exit.auto();
        }
        private static void argumentInstall(string[] args) 
        { 
            ToLog.Inf("running: install");
        }
        private static void argumentRemove(string[] args) 
        {
            ToLog.Inf("running: remove");
        }
        private static void argumentForceRemove(string[] args) 
        {
            ToLog.Inf("running: force-remove");
        }
        private static void argumentUpdate(string[] args) 
        {
            ToLog.Inf("running: update");
        }
        private static void argumentUpgrade(string[] args) 
        {
            ToLog.Inf("running: upgrade");
        }
        private static void argumentFullUpgrade(string[] args) 
        {
            ToLog.Inf("running: full-upgrade");
        }
        private static void argumentRun(string[] args) 
        {
            ToLog.Inf("running: run");
        }
        private static void argumentVerify(string[] args) 
        {
            ToLog.Inf("running: verify");
        }
    }
}
