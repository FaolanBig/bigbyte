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
                { "help", () => Console.WriteLine("help") },
                { "install", () => Console.WriteLine("install") },
                { "remove", () => Console.WriteLine("remove") },
                { "force-remove", () => Console.WriteLine("force-remove") },
                { "update", () => Console.WriteLine("update") },
                { "upgrade", () => Console.WriteLine("upgrade") },
                { "full-upgrade", () => Console.WriteLine("full-upgrade") },
                { "run", () => Console.WriteLine("run") },
                { "verify", () => Console.WriteLine("verify") },
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
    }
}
