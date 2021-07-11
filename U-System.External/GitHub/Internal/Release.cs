using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace U_System.External.GitHub.Internal
{
    public class Release
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("tag_name")]
        public string Tag { get; set; }
        [JsonPropertyName("prerelease")]
        public bool PreRelease { get; set; }
        public Asset[] Assets { get; set; }
        [JsonPropertyName("published_at")]
        public DateTime PublishedDate { get; set; }
        [JsonPropertyName("body")]
        public string ReleaseNotes { get; set; }

        [JsonPropertyName("target_commitish")]
        public string Branch { get; set; }

    }
}
