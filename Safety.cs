﻿using Serilog;

namespace bigbyte
{
    public static class ToLog
    {
        public static void Inf(string toLog)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(VarHold.LogFileNameInfo)
                .CreateLogger();

            Log.Information(toLog);
            Log.CloseAndFlush();
        }
        public static void Err(string toLog)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(VarHold.LogFileNameError)
                .CreateLogger();

            Log.Error(toLog);
            Log.CloseAndFlush();
        }
    }
}