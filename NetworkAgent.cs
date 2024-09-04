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
using System.IO.Compression;
using System.Net.NetworkInformation;
using SharpCompress.Archives;
using SharpCompress.Archives.GZip;
using SharpCompress.Archives.Tar;
using SharpCompress.Common;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;


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
        public List<string> tempFileNames = new List<string>();
        public bool displayPackageNameAtProgress = true; //displays the package name at the progress bar instead of the file name
        public DownloadAgent_multiFile() { }
        public void invokeDownload()
        {
            for (int i = 0; i < targetURLs.Count; i++)
            {
                //tempFileNames.Add(new Random().Next(100_000_000, 1_000_000_000).ToString("D10")); //maybe add '.zip'
                tempFileNames.Add("DB-Matcher-v5.tar.gz"); //maybe add '.zip'
            }
            Console.Clear();
            Console.WriteLine("downloads:");
            Task.Run(startDownload).GetAwaiter().GetResult();
            Console.WriteLine("extracting");
            for (int i = 0; i < targetURLs.Count; i++)
            {
                //ExtractZipWithProgress(Path.Combine(VarHold.tempDirectory_downloads, tempFileNames[i]), destinationDirectories[i], i + 1);
                //ExtractTarGzWithProgress(Path.Combine(VarHold.tempDirectory_downloads, tempFileNames[i]), destinationDirectories[i], i + 1);
                //ExtractTarGzWithProgress(Path.Combine(VarHold.tempDirectory_downloads, "DB-Matcher-v5.tar.gz"), destinationDirectories[i], i + 1);
                //ExtractTarGz(Path.Combine(VarHold.tempDirectory_downloads, tempFileNames[i]), destinationDirectories[i]);
                //ExtractTarGz(Path.Combine(VarHold.tempDirectory_downloads, "DB-Matcher-v5.tar.gz"), destinationDirectories[i]);
                ExtractTarGz_alternative(Path.Combine(VarHold.tempDirectory_downloads, "DB-Matcher-v5.tar.gz"), destinationDirectories[i]);
            }
            Console.WriteLine("finished");
        }
        protected async Task startDownload()
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
                downloadTasks[i] = DownloadFileWithProgressAsync(targetURLs[i], tempFileNames[i], packageNames[i], i + 1, i + 1, displayPackageNameAtProgress);
            }
            await Task.WhenAll(downloadTasks);
        }
        protected static async Task DownloadFileWithProgressAsync(string url, string fileName, string packageName, int downloadNumber, int consoleLine, bool displayPackageNameAtProgress)
        {
            ToLog.Inf($"download {downloadNumber} started - fileName: {fileName}, package: {packageName}, url: {url}");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();

                    long totalBytes = response.Content.Headers.ContentLength ?? -1L;
                    long receivedBytes = 0L;

                    string filePath = Path.Combine(VarHold.tempDirectory_downloads, fileName);

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
                            if (displayPackageNameAtProgress) { DisplayProgress(downloadNumber, packageName, totalBytes, totalBytes, consoleLine); }
                            else { DisplayProgress(downloadNumber, fileName, totalBytes, totalBytes, consoleLine); }
                        }
                    }
                }
                ToLog.Inf($"download {downloadNumber} finished successfully - fileName: {fileName}, package: {packageName}, url: {url}");
            }
            catch (Exception ex)
            {
                ToLog.Err($"download failed - fileName: {fileName}, package: {packageName}, url: {url} - error: {ex.Message}");
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
        protected static void DisplayProgress(int downloadNumber, string packageName, long receivedBytes, long totalBytes, int consoleLine)
        {
            int progressBarWidth = 50;  // Breite des Fortschrittsbalkens
            double progressPercentage = (totalBytes > 0) ? (double)receivedBytes / totalBytes : 0;
            int progressBlocks = (int)(progressPercentage * progressBarWidth);

            string progressBar = new string('=', progressBlocks) + (progressBlocks < progressBarWidth ? ">" : "") + new string(' ', progressBarWidth - progressBlocks);

            lock (Console.Out)
            {
                int currentLine = Console.CursorTop;
                Console.SetCursorPosition(0, consoleLine);
                if (receivedBytes != totalBytes) { Console.Write($"[{downloadNumber}] -> ({packageName}):[{progressBar}] | {progressPercentage:P2}"); }
                else { Console.Write($"fetching: [{downloadNumber}] -> ({packageName}):[{progressBar}] | {progressPercentage:P2} - finished"); }
                Console.SetCursorPosition(0, currentLine);
            }
        }
        protected void ExtractZipWithProgress(string zipPath, string extractPath, int number)
        {
            //Console.WriteLine();
            ToLog.Inf($"extracting {zipPath} to {extractPath}");
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                int totalFiles = archive.Entries.Count;
                int extractedFiles = 0;

                foreach (var entry in archive.Entries)
                {
                    if (!string.IsNullOrEmpty(entry.Name))
                    {
                        string destinationPath = Path.Combine(extractPath, entry.FullName);
                        if (!Directory.Exists(destinationPath)) { Helper.createDir(destinationPath); }
                        //Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                        entry.ExtractToFile(destinationPath, true);

                        extractedFiles++;
                        DisplayProgress_extract(extractedFiles, totalFiles, entry.FullName, number);
                    }
                }
            }
        }
        protected void ExtractTarGzWithProgress(string tarGzPath, string extractPath, int number)
        {
            //  Console.WriteLine();
            ToLog.Inf($"extracting {tarGzPath} to {extractPath}");
            using (var stream = File.OpenRead(tarGzPath))
            using (var gzipArchive = GZipArchive.Open(stream))
            {
                var tarStream = gzipArchive.Entries.First().OpenEntryStream();
                using (var tarArchive = SharpCompress.Archives.Tar.TarArchive.Open(tarStream))
                {
                    int totalFiles = tarArchive.Entries.Count;
                    int extractedFiles = 0;

                    foreach (var entry in tarArchive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            string destinationPath = Path.Combine(extractPath, entry.Key);
                            if (!Directory.Exists(destinationPath)) { Helper.createDir(destinationPath); }
                            //Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                            entry.WriteToFile(destinationPath, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });

                            extractedFiles++;
                            DisplayProgress_extract(extractedFiles, totalFiles, entry.Key, number);
                        }
                    }
                }
            }
        }
        protected void ExtractTarGz(string tarGzPath, string extractPath)
        {
            using (var stream = File.OpenRead(tarGzPath))
            using (var gzipArchive = GZipArchive.Open(stream)) // Zuerst .gz-Archiv öffnen
            {
                var tarEntry = gzipArchive.Entries.FirstOrDefault(); // Die TAR-Datei aus dem GZ-Archiv erhalten
                if (tarEntry == null)
                {
                    throw new InvalidOperationException("Keine TAR-Datei im GZip-Archiv gefunden.");
                }
                
                using (var tarStream = tarEntry.OpenEntryStream()) // TAR-Dateistream öffnen
                using (var tarArchive = SharpCompress.Archives.Tar.TarArchive.Open(tarStream)) // TAR-Archiv öffnen
                {
                    foreach (var entry in tarArchive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            Console.WriteLine($"Extrahiere: {entry.Key}");
                            entry.WriteToDirectory(extractPath, new ExtractionOptions { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                }
            }
        }
        static void ExtractTarGz_alternative(string tarGzPath, string extractPath) // that's the shit that works - all others are fucked
        {
            using (FileStream fs = File.OpenRead(tarGzPath))
            using (GZipInputStream gzipStream = new GZipInputStream(fs))
            using (TarInputStream tarStream = new TarInputStream(gzipStream))
            {
                TarEntry entry;
                while ((entry = tarStream.GetNextEntry()) != null)
                {
                    if (!entry.IsDirectory)
                    {
                        string outPath = Path.Combine(extractPath, entry.Name);
                        Directory.CreateDirectory(Path.GetDirectoryName(outPath));

                        using (FileStream outFile = File.Create(outPath))
                        {
                            tarStream.CopyEntryContents(outFile);
                        }
                    }
                }
            }
        }
        protected void DisplayProgress_extract(int extracted, int total, string currentFile, int consoleLine)
        {
            int percentage = (int)((double)extracted / total * 100);
            int progressBarWidth = 20;
            int progressBlocks = (int)(percentage / 100.0 * progressBarWidth);

            int currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, consoleLine);
            Console.Write($"extracting: ({currentFile}) [{extracted}/{total}] [");
            Console.Write(new string('=', progressBlocks));
            Console.Write((progressBlocks < progressBarWidth ? ">" : ""));
            Console.Write(new string(' ', progressBarWidth - progressBlocks));
            Console.Write($"] | {percentage}% ");
            Console.SetCursorPosition(0, currentLine);
        }
    }
}
