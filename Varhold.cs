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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bigbyte
{
    internal static class VarHold
    {
        public static int GlobalErrorLevel
        {
            get { return globalErrorLevel; }
            set
            {
                if (globalErrorLevel != value)
                {
                    globalErrorLevel = value;
                    ToLog.Inf($"global error level changed to value {value} - GlobalErrorLevel_setTo:{value}");
                    ToLog.Err($"global error level changed to value {value} - GlobalErrorLevel_setTo:{value}");
                }
            }
        }
        private static int globalErrorLevel = 0;
        public static string baseDirectory { get; } = AppDomain.CurrentDomain.BaseDirectory;
        public static string filesCommon = Path.Combine(baseDirectory, "common");
        public static string IndexDir = Path.Combine(filesCommon, "index");
        public static string IndexFile_remote_name = "INDEX_remote.json";
        public static string IndexFile_remote_URL = "https://raw.githubusercontent.com/FaolanBig/bigbyte_indexData/main/INDEX_remote.json";
        public static string IndexFile_remote = Path.Combine(IndexDir, IndexFile_remote_name);
        public static string IndexFile_local_name = "INDEX_local.json";
        public static string IndexFile_local = Path.Combine(IndexDir, IndexFile_local_name);
        public static string installPath_programs = Path.Combine(filesCommon, "installed");
        public static string tempDirectory = Path.Combine(filesCommon, "temp");
        public static string tempDirectory_downloads = Path.Combine(tempDirectory, "downloads");
        public static string OS = "";
        public static string OS_version = "";
        public static bool OS_isWindows = false;
        public static bool OS_isLinux= false;
        public static string helpFilePath { get; } = Path.Combine(baseDirectory, "showHelp.txt");
        public static string RepoURL { get; } = "https://github.com/FaolanBig/bigbyte";
        public static string IssueURL { get; } = "https://github.com/FaolanBig/bigbyte/issues/new";
        public static string WikiURL { get; } = "https://github.com/FaolanBig/bigbyte/wiki";
        public static string WikiURL_Troubleshooting { get; } = "https://github.com/FaolanBig/bigbyte/wiki/Troubleshooting";
        public static string WikiURL_ExitCodes { get; } = "https://github.com/FaolanBig/bigbyte/wiki/Exit-codes";
        public static string IndexDataURL { get; } = "https://github.com/FaolanBig/bigbyte_indexData";
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
