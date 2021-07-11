using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using U_System.External;


namespace U_System
{
    public class SettingsSystem
    {
        public static string APP_VERSION { get
            {
                var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var version = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
                //version = SemanticVersion.FromAssembly(version);
                return version;
            } }
        public static Settings _Settings { get; set; }

        private static string SettingFile { get => Directory.GetCurrentDirectory() + "/settings.json"; }
        internal static void SAVE()
        {
            _Settings.Version = _Settings.Version;


            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            string json = JsonSerializer.Serialize(_Settings, options);
            File.WriteAllText(SettingFile, json);
        }
        internal static void LOAD()
        {
            if (File.Exists(SettingFile))
            {
                string json = File.ReadAllText(SettingFile);
                Settings settings = JsonSerializer.Deserialize<Settings>(json);
                _Settings = settings;
            }
            else
                LOAD_DEFAULT();
        }

        internal static Settings LOAD_DEFAULT()
        {
            Settings default_setting = new Settings();

            return default_setting;
        }
        internal static string SET_VERSION()
        {
            string version = string.Empty;
            string build = DateTime.Today.ToString("yy.MM.dd");
            string revision = DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm");
            version = string.Format("v.{0}-{1}", build, revision);
            return version;
        }
    }
    [Serializable]
    public class Settings
    {
        public string Version { get; set; }
    }
}
