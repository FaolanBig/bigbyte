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




using Serilog.Parsing;
using System.Net.NetworkInformation;


namespace bigbyte
{
    internal static class NetworkAgent
    {
        public static bool GetInternetConnectionAvailable(string ipv4ToTry = "8.8.8.8", string urlToTry = "https://www.google.com")
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(ipv4ToTry, 3000); // Google's public DNS server
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                try
                {
                    using (Ping ping = new Ping())
                    {
                        PingReply reply = ping.Send(urlToTry, 3000);
                        return reply.Status == IPStatus.Success;
                    }
                }
                catch (Exception e2)
                {
                    Console.Error.WriteLine(e2);
                    return false;
                }
            }

        }
        /*public static bool GetInternetConnectionAvailable(string urlToTry = "https://www.google.com", string ipv4ToTry = "8.8.8.8")
        {
            
        }*/
        internal static async Task<bool> CheckInternetConnection(string urlToTry = "https://www.github.com")
        {
            int timeout = 3;
            ToLog.Inf($"checking internet connection to {urlToTry} with timeout: {timeout}s");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(timeout);
                    HttpResponseMessage response = await client.GetAsync(urlToTry);
                    ToLog.Inf("connection established successfully");
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                ToLog.Err("connection failed");
                return false;
            }
        }
    }
    internal class DownloadAgent
    {
        protected string destinationDirectory = "";
        public string DestinationDirectory 
        {
            get { return destinationDirectory; }
            set
            {
                if (value != destinationDirectory)
                {
                    ToLog.Inf($"DowloadAgent - destination path set to {destinationDirectory}");
                    destinationDirectory = value;
                }
            }
        }
        protected string targetURL = "";
        public string TargetURL
        {
            get { return targetURL; }
            set
            {
                if (value != targetURL)
                {
                    ToLog.Inf($"DowloadAgent - destination path set to {targetURL}");
                    targetURL = value;
                }
            }
        }
        protected string fileName = "";
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (value != targetURL)
                {
                    ToLog.Inf($"DowloadAgent - fileName set to {targetURL}");
                    targetURL = value;
                }
            }
        }
        public DownloadAgent() { }
        
        public void downloadFromTarget_singleFileRaw()
        {
            if (string.IsNullOrEmpty(targetURL) || string.IsNullOrEmpty(destinationDirectory) || string.IsNullOrEmpty(fileName))
            {
                VarHold.GlobalErrorLevel = 005001001;
                Exit.auto();
            }
            try
            {
                ToLog.Inf($"fetching file from {TargetURL} to {destinationDirectory}");
                Task.Run(async () => await DownloadFileAsync()).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

            }
        }
        async Task DownloadFileAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(TargetURL);
                response.EnsureSuccessStatusCode();

                if (!Directory.Exists(DestinationDirectory)) { Directory.CreateDirectory(DestinationDirectory); }

                string filePath = Path.Combine(DestinationDirectory, FileName);

                await using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }
        }
    }
}
