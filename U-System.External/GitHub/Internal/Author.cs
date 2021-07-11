using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace U_System.External.GitHub.Internal
{
    public class Author
    {
        [JsonPropertyName("login")]
        public string Name { get; set; }
    }
}
