using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace U_System.API.GitHub
{
    public class Author
    {
        [JsonPropertyName("login")]
        public string Name { get; set; }
    }
}
