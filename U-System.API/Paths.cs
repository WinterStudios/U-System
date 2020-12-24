using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>All Paths from Directory Incluides "\\"</remarks>
    public class Paths
    {
        public enum Storage
        {
            AppDataLocation = 0,
            ExeLocation = 1
        }
        /// <summary>
        /// Where the everthing is store
        /// </summary>
        /// <value>Default Value: Setting.AppDataLocation</value>
        public static Storage StorageFolder { get; set; } = Storage.AppDataLocation;
        public static string STORAGE_DIRECTORY { 
            get {
                switch (StorageFolder)
                {   
                    case Storage.AppDataLocation:
                        return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\U-System\\";
                    case Storage.ExeLocation:
                        return AppDomain.CurrentDomain.BaseDirectory + "Config\\";
                    default:
                        return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\U-System\\";
                }
            } }

        public static class SETTINGS
        {
            /// <summary>
            /// Retun the App Settings Path 
            /// </summary>
            private static string SETTINGS_DIRECTORY {
                get {
                    string _path = STORAGE_DIRECTORY + "Settings\\";
                    if (!Directory.Exists(_path))
                        Directory.CreateDirectory(_path);
                    return _path;
                }
            }

            /// <summary>
            /// Plugin File
            /// </summary>
            public static string PLUGINS_SETTINGS { get => STORAGE_DIRECTORY + "plugin.json"; }
        }        
        public class PLUGINS
        {
            public static string PLUGIN_DIRECTORY { get {
                    string _path = STORAGE_DIRECTORY + "Plugins\\";
                    if (!Directory.Exists(_path))
                        Directory.CreateDirectory(_path);
                    return _path;
            } }
        }
        public static class GITHUB
        {
            public static string ACCESS_TOKEN { get => File.ReadAllText(STORAGE_DIRECTORY + "_TOKEN.token"); }
        }
    }
}
