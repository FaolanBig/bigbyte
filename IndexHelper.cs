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
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bigbyte
{
    public class IndexHelper
    {
        private string pathToJSON = "";
        private PackageRepository index;

        public IndexHelper(string path) 
        {
            this.pathToJSON = path;
            this.loadINDEX();
        }

        private void loadINDEX()
        {
            //PrintIn.blue("loading index file");
            string jsonFileContents = "";
            try
            {
                //jsonFileContents = File.ReadAllText(this.pathToJSON);
                jsonFileContents = File.ReadAllText(this.pathToJSON, new System.Text.UTF8Encoding(false));
            }
            catch (Exception ex)
            {
                VarHold.GlobalErrorLevel = 001001003;
                ToLog.Err($"an error occurred when reading INDEX_remote.json at {this.pathToJSON} - error: {ex.Message}");
                Exit.auto();
            }

            try
            {
                this.index = JsonSerializer.Deserialize<PackageRepository>(jsonFileContents);
            }
            catch (Exception ex)
            {
                VarHold.GlobalErrorLevel = 001002001;
                ToLog.Err($"an error occurred when deserializing an IndexFile at {this.pathToJSON} - error: {ex.Message}");
                Exit.auto();
            }
        }
        internal void searchForPackage(string packageName)
        {
            ToLog.Inf($"performing package search for {packageName} in {this.pathToJSON}");

            var foundPackages = this.index.Packages
            .Where(p => p.Name.Contains(packageName, StringComparison.OrdinalIgnoreCase))
            .ToList();

            if (foundPackages.Any())
            {
                PrintIn.green($"{foundPackages.Count} packages found");
                Console.WriteLine();

                int count = 0;
                foreach (var package in foundPackages)
                {
                    string count_string = Convert.ToString(count);

                    Console.WriteLine($"[{++count}] -> name: {package.Name}\n" +
                        $"{new string(' ', count_string.Length + 2)} -> version: {package.Version}\n" +
                        $"{new string(' ', count_string.Length + 2)} -> describtion: {package.Description}");
                    Console.WriteLine();
                }
            }
            else
            {
                PrintIn.red($"no packages found containing string {packageName}");
                VarHold.GlobalErrorLevel = 004001001;
                Exit.auto();
            }
        }
    }
    public class Package
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("license")]
        public string License { get; set; }

        [JsonPropertyName("dependencies")]
        public List<string> Dependencies { get; set; }

        [JsonPropertyName("download_url")]
        public Dictionary<string, string> DownloadUrl { get; set; }

        [JsonPropertyName("checksum")]
        public Dictionary<string, string> Checksum { get; set; }

        [JsonPropertyName("size_download_MB")]
        public int SizeDownloadMB { get; set; }

        [JsonPropertyName("size_file_MB")]
        public int SizeFileMB { get; set; }

        [JsonPropertyName("install_instructions")]
        [JsonConverter(typeof(InstallInstructionsConverter))]
        public List<string> InstallInstructions { get; set; }

        [JsonPropertyName("executable")]
        public Dictionary<string, string> Executable { get; set; }

        [JsonPropertyName("help")]
        public string Help { get; set; }
    }
    public class InstallInstructionsConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String) { return new List<string> { reader.GetString() }; }
            else if (reader.TokenType == JsonTokenType.StartArray) { return JsonSerializer.Deserialize<List<string>>(ref reader, options); }
            else { throw new JsonException(); }
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
    public class PackageRepository
    {
        [JsonPropertyName("repository")]
        public string Repository { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("packages")]
        public List<Package> Packages { get; set; }
    }
}
