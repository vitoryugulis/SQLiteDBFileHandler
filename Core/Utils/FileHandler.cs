using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public class FileHandler
    {
        public static string FileToBase64(string filePath)
        {
            var byteArray = File.ReadAllBytes(filePath);
            var base64File = Convert.ToBase64String(byteArray);
            return base64File;
        }

        public static bool Base64ToFile(string base64File, string pathToWriteFile)
        {
            try
            {
                var byteArray = Convert.FromBase64String(base64File);
                File.WriteAllBytes(pathToWriteFile, byteArray);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
