using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace bigbyte
{
    internal static class ConfigHandler
    {
        internal static void loadConfig_Main()
        {
            if (!File.Exists(VarHold.configMain))
            {
                ToLog.Wrn($"no config file (main) found at {VarHold.configMain} under {VarHold.configDirectory}");
                ConfigHandler_init.initConfiguration_default();
            }
        }
    }

    internal static class ConfigHandler_init
    {
        internal static void initConfiguration_default()
        {
            ToLog.Inf($"initializing default config (main) to {VarHold.configMain}");

            ConfigStruct defaultConfig = new ConfigStruct()
            {
                Destinations = new Destinations
                {
                    IndexDir = VarHold.IndexDir,
                    installPath_programs = VarHold.installPath_programs,
                    tempDirectory = VarHold.tempDirectory,
                    helpFilePath = VarHold.helpFilePath
                },
                Sources = new Sources
                {
                    IndexDataURL = VarHold.IndexDataURL
                },
                Appearance = new Appearance
                {
                    Colors = new Colors
                    {
                        progressBarDone = ConsoleColor.Green
                    }
                }
            };
        }
    }

    public class ConfigStruct
    {
        public Destinations Destinations { get; set; }
        public Sources Sources { get; set; }
        public Appearance Appearance { get; set; }
    }
    public class Destinations
    {
        public string IndexDir { get; set; }
        public string installPath_programs { get; set; }
        public string tempDirectory { get; set; }
        public string helpFilePath { get; set; }
    }
    public class Sources
    {
        public string IndexDataURL { get; set; }
    }
    public class Appearance
    {
        public Colors Colors { get; set; }
    }
    public class Colors
    {
        public ConsoleColor progressBarDone { get; set; }
    }
}