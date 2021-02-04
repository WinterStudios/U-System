using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U_System.Core
{
    public class FileSystem
    {
        /// <summary>
        /// Convert a stream to a file on a system
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="pathToSave"></param>
        /// <param name="bufferSize">Default size off 16KB</param>
        public static void SaveStreamToFile(Stream stream, string pathToSave, int bufferSize = 16345)
        {

            byte[] buffer = new byte[bufferSize];   
            FileStream fileStream = new FileStream(pathToSave, FileMode.Create, FileAccess.Write);

            int read = 0;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, read);
            }
            fileStream.Close();
        }
    }
}
