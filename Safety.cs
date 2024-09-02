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




using Serilog;

namespace bigbyte
{
    internal static class ToLog
    {
        public static void Inf(string toLog)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(VarHold.LogFileNameInfo)
                .CreateLogger();

            Log.Information(toLog);
            Log.CloseAndFlush();
        }
        public static void Err(string toLog)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(VarHold.LogFileNameError)
                .CreateLogger();

            Log.Error(toLog);
            Log.CloseAndFlush();
        }
        public static void success()
        {
            Inf("operation finished successfully");
        }
    }
    internal static class Exit
    {
        internal static void success() 
        {
            ToLog.Inf($"exiting bigbyte with exit code 0 - see {VarHold.WikiURL_ExitCodes} for more information on exit codes");
            Environment.Exit(0); 
        }
        internal static void errored(int exitCode = 1) 
        {
            ToLog.Inf($"exiting bigbyte with exit code {exitCode} - see {VarHold.WikiURL_ExitCodes} for more information on exit codes");
            Environment.Exit(exitCode); 
        }
        internal static void auto() 
        {
            ToLog.Inf($"exiting bigbyte with exit code {VarHold.GlobalErrorLevel} - see {VarHold.WikiURL_ExitCodes} for more information on exit codes");
            if (VarHold.GlobalErrorLevel == 0) { /*PrintIn.green("bigbyte exit: success");*/ }
            else { PrintIn.red($"bigbyte exit: code {VarHold.GlobalErrorLevel} - visit {VarHold.WikiURL_ExitCodes} for more information"); }
            Environment.Exit(VarHold.GlobalErrorLevel); 
            
        }
    }
}