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
using Spectre.Console;
using System.ComponentModel;

namespace bigbyte
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*************************
             ******** TESTING ********
             *** REMOVE TO PUBLISH ***
             ************************/

            /*AnsiConsole.Progress()
                .Columns(new ProgressColumn[]
                {
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new RemainingTimeColumn(),
                    new SpinnerColumn(),
                    new TransferSpeedColumn()
                })
                .Start(ctx =>
                {
                    var task1 = ctx.AddTask("task1");
                    var task2 = ctx.AddTask("task2");
                    var task3 = ctx.AddTask("task3");

                    while (!ctx.IsFinished)
                    {
                        task1.Increment(10);
                        Thread.Sleep(100);
                        task2.Increment(8.5);
                        task3.Increment(31);
                        Thread.Sleep(400);
                        var task4 = ctx.AddTask("added");
                        Thread.Sleep(150);
                        task4.Increment(8);
                    }
                });*/
            var totalBytes = 100_000_000; // Gesamtgröße (in Bytes)
            var downloadedBytes = 0; // Bereits heruntergeladene Bytes
            var startTime = DateTime.Now;

            AnsiConsole.Progress()
                .Columns(
                    new TaskDescriptionColumn(), // Beschreibung der Aufgabe
                    new ProgressBarColumn(), // Fortschrittsbalken
                    new PercentageColumn(), // Prozentsatz
                    new RemainingTimeColumn(), // Verbleibende Zeit
                    new ElapsedTimeColumn(), // Verstrichene 
                    new TransferSpeedColumn() // Übertragungsgeschwindigkeit
                )
                .Start(ctx =>
                {
                    var task = ctx.AddTask("Herunterladen...", maxValue: totalBytes);

                    while (!ctx.IsFinished)
                    {
                        // Simuliere den Download
                        var chunkSize = 1_000_000; // 1 MB pro Schritt
                        downloadedBytes += chunkSize;
                        task.Increment(chunkSize);

                        Thread.Sleep(100); // Simulierte Verzögerung
                    }
                });



            Exit.auto();

            /*************************
             ****** END TESTING ******
             ************************/

            ToLog.Inf("... bigbyte started ...");
            Helper.SetOperatingSystem_inVarHold();
            Helper.Check_ifDataDirectoriesAreAvailable_orCreateThem();

            string argumentsOutputHold = "";
            foreach (var arg in args)
            {
                argumentsOutputHold += arg + " ";
            }
            if (!string.IsNullOrEmpty(argumentsOutputHold))
            {
                ToLog.Inf($"program (bigbyte) started with arguments: {argumentsOutputHold}");
                ArgumentHandler.analyzer(args);
            }
            else
            {
                ToLog.Inf("program (bigbyte) started without arguments - showing help");
                string[] argsTempToHelp = { "help" };
                ArgumentHandler.analyzer(argsTempToHelp);
            }
            Exit.auto();
        }
    }
}
