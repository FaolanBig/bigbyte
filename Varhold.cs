using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigbyte
{
    public static class VarHold
    {
        public static string CurrentMainPath {  get { return currentMainPath; } }
        private static string currentMainPath = AppContext.BaseDirectory;

        public static string ContentPath { get { return contentPath; } }
        private static string contentPath = currentMainPath += "";
        public static string LogFileNameInfo { get { return logFileNameInfo; } }
        private static string logFileNameInfo = "logfile";
        public static string LogFileNameError { get{ return logFileNameError; } }
        private static string logFileNameError = logFileNameInfo;
    }
}
