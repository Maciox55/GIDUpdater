using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace GIDUpdater
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();

            var version = AssemblyName.GetAssemblyName(@"GETIID.exe").Version;
            Console.WriteLine(String.Format("Current GETIID Version: {0}", version));
            //string version = versionInfo.FileVersion;
            IEnumerable<String> files = Directory.GetFiles(path);
            backupZIP("update_backup.zip", files); // Zip the GETIID version before downloading the 
            
        }
        static void backupZIP(string fileName, IEnumerable<string> files)
        {

            var zip = ZipFile.Open(fileName, ZipArchiveMode.Create);

            foreach (var file in files)
            {
                zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
            }

            zip.Dispose();

        }
    }
}
