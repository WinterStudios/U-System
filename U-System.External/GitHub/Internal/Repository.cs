using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace U_System.External.GitHub.Internal
{
    public class Repository
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public bool Private { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Author of the repository
        /// </summary>
        [JsonPropertyName("owner")]
        public Author Author { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [JsonPropertyName("url")] 
        public string URL { get; set; }
        
        [JsonPropertyName("releases_url")]
        public string ReleaseURL { get; set; }

        public Release[] Releases { get; set; }
    }
}
