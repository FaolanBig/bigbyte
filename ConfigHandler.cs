using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            ToLog.Inf("initializing default config (main)");

            ConfigStruct defaultConfig = new ConfigStruct()
            {
                ////////////////////
            };
        }
    }

    public class ConfigStruct
    {
        public string IndexDir { get; set; }
        public string installPath_programs { get; set; }
        public string tempDirectory { get; set; }
        public string helpFilePath { get; set; }
    }
}