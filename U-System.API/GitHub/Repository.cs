using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace U_System.API.GitHub
{
    public class Repository
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public bool Private { get; set; }
        public string Description { get; set; }
        


        [JsonPropertyName("owner")] public Author Author { get; set; }
        [JsonPropertyName("url")] public string URL { get; set; }

        public Release[] Releases { get; set ; }

    }
}
