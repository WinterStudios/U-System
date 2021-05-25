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

        public static void Initialize()
        {
            Debug.Log.LogMessage("Initializing", typeof(SettingsSystem));
            Load();
        }
        public static void Load()
        {
            if (!File.Exists(SettingFile))
            {
                Debug.Log.LogMessage(string.Format("File Not Found .: {0}",SettingFile), typeof(SettingsSystem), Debug.LogMessageType.Warning);
                Debug.Log.LogMessage("Trying load default settings", typeof(SettingsSystem));
                LOAD_DEFAULT();
            }
            else
            {
                try
                {
                    Debug.Log.LogMessage("Trying loading settings file", typeof(SettingsSystem));
                    JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

                    string json = File.ReadAllText(SettingFile);
                    Setting = JsonSerializer.Deserialize<Settings>(json, serializerOptions);
                    Debug.Log.LogMessage("Load success", typeof(SettingsSystem));
                }
                catch (Exception ex) { Debug.Log.LogMessage(ex.Message, typeof(SettingsSystem), Debug.LogMessageType.Error); }
            }
        }
        public static void Save()
        {
            Debug.Log.LogMessage("Starting Saving", typeof(SettingsSystem));
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

            string json = JsonSerializer.Serialize<Settings>(Setting, serializerOptions);
            File.WriteAllText(SettingFile, json);
            Debug.Log.LogMessage("Saving success", typeof(SettingsSystem));
        }

        /// <summary>
        /// Load default settings and save it
        /// </summary>
        public static void LOAD_DEFAULT()
        {
            Debug.Log.LogMessage("Loading Default", typeof(SettingsSystem));
            Setting = new Settings()
            {
                Theme = 0
            };
            Debug.Log.LogMessage("Load Default", typeof(SettingsSystem));
            Save();
        }
    }
}
