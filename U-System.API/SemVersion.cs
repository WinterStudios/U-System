using System;
using System.Collections.Generic;
using System.Text;

namespace U_System.API
{
    public class SemVersion
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public REVISION Revision { get; set; }




        public override string ToString()
        {
            string version = string.Format("v.{0}.{1}.{2}-{3}", Major, Minor, Build, Revision);
            return version;
        }
        public struct REVISION
        {
            public Release Release { get; set; }
            public int Revision { get; set; }
            public override string ToString()
            {
                string v = string.Format("{0}.{1}", Release, Revision);
                return v;
            }
        }
        public enum Release
        {
            f = 0,
            rc = 1,
            preview = 2,
            alpha,
            beta
        }
    }



}
