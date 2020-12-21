using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API
{
    class Paths
    {
        public static string PluginsDirectory {
            get {
                string path = AppDomain.CurrentDomain.BaseDirectory + "Plugins\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            } }

    }
}
