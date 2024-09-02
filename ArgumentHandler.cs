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
using System.Net.NetworkInformation;
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
                { "search", () => argumentSearch(args) },
                { "s", () => argumentSearch(args) },
                { "details", () => argumentDetails(args) },
                { "detail", () => argumentDetails(args) },
                { "d", () => argumentDetails(args) },
                { "add", () => argumentAddPackage(args) },
                { "a", () => argumentAddPackage(args) }

            };

            if (!acceptedArguments.ContainsKey(args[0]))
            {
                PrintIn.red($"bad argument: {args[0]} | try 'bigbyte help' to see accepted arguments");
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
            //PrintIn.blue("running: help - display help message");
            Console.WriteLine(Helper.GetFileContentsTxt(VarHold.helpFilePath));
            Exit.auto();
        }
        private static void argumentInstall(string[] args) 
        { 
            ToLog.Inf("running: install");
            //PrintIn.blue("running: install");
            ArgumentHandler_errors.MissingPackageName(args);
        }
        private static void argumentRemove(string[] args) 
        {
            ToLog.Inf("running: remove");
            //PrintIn.blue("running: remove");
            ArgumentHandler_errors.MissingPackageName(args);
        }
        private static void argumentForceRemove(string[] args) 
        {
            ToLog.Inf("running: force-remove");
            PrintIn.blue("running: force-remove");
            ArgumentHandler_errors.MissingPackageName(args);
        }
        private static void argumentUpdate(string[] args) 
        {
            ToLog.Inf("running: update");
            //PrintIn.blue("running: update");
        }
        private static void argumentUpgrade(string[] args) 
        {
            ToLog.Inf("running: upgrade");
            //PrintIn.blue("running: upgrade");
        }
        private static void argumentFullUpgrade(string[] args) 
        {
            ToLog.Inf("running: full-upgrade");
            //PrintIn.blue("running: full-upgrade");
        }
        private static void argumentRun(string[] args) 
        {
            ToLog.Inf("running: run");
            //PrintIn.blue("running: run");
            ArgumentHandler_errors.MissingPackageName(args);
        }
        private static void argumentVerify(string[] args) 
        {
            ToLog.Inf("running: verify");
            //PrintIn.blue("running: verify");
            ArgumentHandler_errors.MissingPackageName(args);
        }
        private static void argumentSearch(string[] args)
        {
            ToLog.Inf($"running: search - searching for {args[1]}");
            //PrintIn.blue("running: search");
            ArgumentHandler_errors.MissingPackageName(args);

            if (args.Length == 3)
            {
                if (args[2] == "local" || args[2] == "l" || args[2] == "installed" || args[2] == "i")
                {
                    IndexHelper index = new IndexHelper(VarHold.IndexFile_local);
                    index.searchForPackage(args[1]);
                }
                else if (args[2] == "remote" || args[2] == "r" || args[2] == "all" || args[2] == "a")
                {
                    IndexHelper index = new IndexHelper(VarHold.IndexFile_remote);
                    index.searchForPackage(args[1]);
                }
            }
            else
            {
                IndexHelper index = new IndexHelper(VarHold.IndexFile_remote);
                index.searchForPackage(args[1]);
            }

        }
        private static void argumentDetails(string[] args)
        {
            ToLog.Inf("running: details");
            //PrintIn.blue("running: details");
            ArgumentHandler_errors.MissingPackageName(args);
        }
        private static void argumentAddPackage(string[] args)
        {
            ToLog.Inf("running: add");
            //PrintIn.blue("running: add");
        }
    }
    internal class ArgumentHandler_errors()
    {
        internal static void MissingPackageName(string[] args, int length = 1)
        {
            if (args.Length <= length)
            {
                PrintIn.red("missing argument: package name");
                VarHold.GlobalErrorLevel = 003001001;
                Exit.auto();
            }
        }
    }
}
