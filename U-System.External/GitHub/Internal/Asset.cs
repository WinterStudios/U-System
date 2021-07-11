using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace U_System.External.GitHub.Internal
{
    public class Asset
    {
        public int ID { get; set; }
        [JsonPropertyName("url")]
        public string URL { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("browser_download_url")]
        public string DownloadURL { get; set; }
    }
}
