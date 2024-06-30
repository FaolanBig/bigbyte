using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigbyte
{
    internal static class VarHold
    {
        public static string CurrentMainPath {  get { return currentMainPath; } }
        private static string currentMainPath = AppContext.BaseDirectory;

        public static string ContentPath { get { return contentPath; } }
        private static string contentPath = currentMainPath += "";
        public static string LogFileNameInfo { get { return logFileNameInfo; } }
        private static string logFileNameInfo = "logfile.txt";
        public static string LogFileNameError { get { return logFileNameError; } }
        private static string logFileNameError = logFileNameInfo;
        public static bool ForceDeletion { get { return forceDeletion; } set { forceDeletion = value; } }
        private static bool forceDeletion = false;
        public static string ActiveArgument { get { return activeArgument; } set { activeArgument = value; } }
        private static string activeArgument = "";
        public static string ActiveArgumentContent { get { return activeArgumentContent; } set { activeArgumentContent = value; } }
        private static string activeArgumentContent = "";
    }
}
