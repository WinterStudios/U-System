using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.API
{
    public class File
    {
        public static void SaveStreamToFile(Stream stream, string pathToSave)
        {

            byte[] buffer = new byte[16345];
            FileStream fileStream = new FileStream(pathToSave, FileMode.Create, FileAccess.Write);

            int read = 0;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, read);
            }
        }
    }
}
