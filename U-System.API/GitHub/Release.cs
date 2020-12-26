using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace U_System.API.GitHub
{
    public class Release
    {
        public string Name { get; set; }
        [JsonPropertyName("tag_name")]
        public string Tag { get; set; }
        public bool PreRelease { get; set; }
        public Asset[] Assets { get; set; }
    }
}
