﻿/*
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
using System.Text;
using System.Threading.Tasks;

namespace bigbyte
{
    internal static class Helper
    {
        public static string GetFileContentsTxt(string path)
        {
            ToLog.Inf($"reading contents from file: {path}");
            string content = "an unexpected error occurred" +
                $"please report this and your logfile.txt to {VarHold.IssueURL}" +
                $"you can then try to find help on {VarHold.WikiURL_Troubleshooting}";
            try
            {
                content = File.ReadAllText(path);
                ToLog.success();
            }
            catch (Exception ex)
            {
                ToLog.Err($"an error occurred when reading the contents of a file - file path: {path}");
            }
            return content;
        }
    }
}