﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API.GitHub
{
    public class Release
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public Asset[] Assets { get; set; }
    }
}
