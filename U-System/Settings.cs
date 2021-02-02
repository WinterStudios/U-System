using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace U_System
{
    public class Settings
    {
        public static string Version { get; set; }
        public string m_version { get; set; }



        private static string SettingFile { get => Directory.GetCurrentDirectory() + "/settings.json"; }

        private static string SET_VERSION()
        {
            string version = string.Empty;
            string build = DateTime.Today.ToString("yy.MM.dd");
            string revision = DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm");
            version = string.Format("v.{0}-{1}", build, revision);
            return version;
        }

        internal static void Save()
        {
            Settings settings = new Settings();
            settings.m_version = Version;


            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(SettingFile, json);
        }
        internal static void Load()
        {
            if (File.Exists(SettingFile))
            {
                string json = File.ReadAllText(SettingFile);
                Settings settings = JsonSerializer.Deserialize<Settings>(json);
#if DEBUG
                settings.m_version = Version = SET_VERSION();
                Save();
#endif
            }
            else
                Save();
        }
    }
}
