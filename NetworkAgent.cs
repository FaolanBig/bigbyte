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
using System;
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
    public class DownloadAgent_singleFile
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
        public DownloadAgent_singleFile() { }
        
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
                Task.Run(DownloadFileAsync).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

            }
        }
        /*async Task DownloadFileAsync()
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
        }*/
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
    public class DownloadAgent_multiFile
    {
        public List<string> targetURLs = new List<string>();
        public List<string> fileNames = new List<string>();
        public List<string> packageNames = new List<string>();
        public List<string> destinationDirectories = new List<string>();
        public List<string> tempDirectories = new List<string>();
        public bool displayPackageNameAtProgress = true; //displays the package name at the progress bar instead of the file name
        public DownloadAgent_multiFile() { }
        public void invokeDownload()
        {
            Console.Clear();
            Task.Run(startDownload).GetAwaiter().GetResult();
        }
        private async Task startDownload()
        {
            int numberOfDownloads = targetURLs.Count;
            if (fileNames.Count != numberOfDownloads || packageNames.Count != numberOfDownloads || destinationDirectories.Count != numberOfDownloads)
            {
                ToLog.Err($"download failed: wrong quantity of parameters set");
                PrintIn.red("error: download failed due to an internal error");
                VarHold.GlobalErrorLevel = 005001002;
                Exit.auto();
            }
            Task[] downloadTasks = new Task[numberOfDownloads];

            for (int i = 0; i < numberOfDownloads; i++)
            {
                downloadTasks[i] = DownloadFileWithProgressAsync(targetURLs[i], destinationDirectories[i], fileNames[i], packageNames[i], i + 1, i + 1, displayPackageNameAtProgress);
            }
            await Task.WhenAll(downloadTasks);
        }
        private static async Task DownloadFileWithProgressAsync(string url, string destinationDirectory, string fileName, string packageName, int downloadNumber, int consoleLine, bool displayPackageNameAtProgress)
        {
            ToLog.Inf($"download {downloadNumber} started - fileName: {fileName}, package: {packageName}, destination: {destinationDirectory}, url: {url}");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();

                    long totalBytes = response.Content.Headers.ContentLength ?? -1L;
                    long receivedBytes = 0L;

                    if (!Directory.Exists(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }

                    string filePath = Path.Combine(destinationDirectory, fileName);

                    await using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using (var httpStream = await response.Content.ReadAsStreamAsync())
                        {
                            byte[] buffer = new byte[8192];  // 8 KB Puffer
                            int bytesRead;

                            if (displayPackageNameAtProgress) { DisplayProgress(downloadNumber, packageName, 0, totalBytes, consoleLine); }
                            else { DisplayProgress(downloadNumber, fileName, 0, totalBytes, consoleLine); }

                            while ((bytesRead = await httpStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                await fileStream.WriteAsync(buffer, 0, bytesRead);
                                receivedBytes += bytesRead;

                                if (displayPackageNameAtProgress) { DisplayProgress(downloadNumber, packageName, receivedBytes, totalBytes, consoleLine); }
                                else { DisplayProgress(downloadNumber, fileName, receivedBytes, totalBytes, consoleLine); }
                            }
                        }
                    }
                }
                ToLog.Inf($"download {downloadNumber} finished successfully - fileName: {fileName}, package: {packageName}, destination: {destinationDirectory}, url: {url}");
            }
            catch (Exception ex)
            {
                ToLog.Err($"download failed - fileName: {fileName}, package: {packageName}, destination: {destinationDirectory}, url: {url} - error: {ex.Message}");
                PrintIn.red("error: download failed due to an networking error\n" +
                    $"       visit {VarHold.WikiURL_Troubleshooting} for some help");
                VarHold.GlobalErrorLevel = 006001001;
                Exit.auto();
            }
        }

        /*static void DisplayProgress(int downloadNumber, string fileName, long receivedBytes, long totalBytes)
        {
            int progressBarWidth = 20;
            double progressPercentage = (totalBytes > 0) ? (double)receivedBytes / totalBytes : 0;
            int progressBlocks = (int)(progressPercentage * progressBarWidth);

            string progressBar = new string('=', progressBlocks) + (progressBlocks < progressBarWidth ? ">" : "") + new string(' ', progressBarWidth - progressBlocks);

            Console.Write($"\r[{downloadNumber}]({fileName})[{progressBar}] | {progressPercentage:P2}");
        }*/
        private static void DisplayProgress(int downloadNumber, string packageName, long receivedBytes, long totalBytes, int consoleLine)
        {
            int progressBarWidth = 20;  // Breite des Fortschrittsbalkens
            double progressPercentage = (totalBytes > 0) ? (double)receivedBytes / totalBytes : 0;
            int progressBlocks = (int)(progressPercentage * progressBarWidth);

            // Erstelle den Fortschrittsbalken
            string progressBar = new string('=', progressBlocks) + (progressBlocks < progressBarWidth ? ">" : "") + new string(' ', progressBarWidth - progressBlocks);

            // Cursor an die richtige Position setzen und Fortschritt anzeigen
            lock (Console.Out)  // Synchronisation für Konsolenausgabe
            {
                int currentLine = Console.CursorTop;  // Aktuelle Zeile speichern
                Console.SetCursorPosition(0, consoleLine);  // Cursor zu der Zeile des aktuellen Downloads setzen
                Console.Write($"[{downloadNumber}]({packageName})[{progressBar}] | {progressPercentage:P2}");
                Console.SetCursorPosition(0, currentLine);  // Cursor zurücksetzen
            }
        }
    }
}
