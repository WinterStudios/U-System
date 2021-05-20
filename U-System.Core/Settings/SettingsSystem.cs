using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace U_System.Core.Settings
{
    public class SettingsSystem
    {
        public static string SettingFile { get => Directory.GetCurrentDirectory() + "/settings.json"; }

        public static Settings Setting { get; set; }

        public static void Inicialize()
        {
            Load();
        }
        public static void Load()
        {
            if (!File.Exists(SettingFile))
                LOAD_DEFAULT();
            else
            {
                try
                {
                    JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

                    string json = File.ReadAllText(SettingFile);
                    Setting = JsonSerializer.Deserialize<Settings>(json, serializerOptions);
                }
                catch { }
            }
        }
        public static void Save()
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

            string json = JsonSerializer.Serialize<Settings>(Setting, serializerOptions);
            File.WriteAllText(SettingFile, json);

        }

        /// <summary>
        /// Load default settings and save it
        /// </summary>
        public static void LOAD_DEFAULT()
        {
            Setting = new Settings()
            {
                Theme = 0
            };
            Save();
        }
    }
}
