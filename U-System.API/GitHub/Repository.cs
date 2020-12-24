using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API.GitHub
{
    public class Repository
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public Author Author { get; set; }
        internal Release[] Releases { get; set; }
    }
}
