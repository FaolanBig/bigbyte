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
                "please report this and your logfile.txt to ";
            try
            {
                content = File.ReadAllText(path);
                ToLog.success();
            }
            catch (Exception ex)
            {
                ToLog.Err($"an error occurred when reading the contents of a file - file path: {path}");
            }
        }
    }
}
