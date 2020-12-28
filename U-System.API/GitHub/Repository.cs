using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace U_System.API.GitHub
{
    public class Repository
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public bool Private { get; set; }
        public string Description { get; set; }



        [JsonPropertyName("owner")]
        public Author Author { get; set; }
        internal Release[] Releases { get; set; }
    }
}
