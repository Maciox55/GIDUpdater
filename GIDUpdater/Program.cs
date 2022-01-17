using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Net;


namespace GIDUpdater
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                var path = Directory.GetCurrentDirectory();

                var localVersion = AssemblyName.GetAssemblyName(@"C:\GETIID SOURCE CODE REPOSITORY\MicrosoftOfficeActivationUtility\GETIID\bin\Debug\GETIID.exe").Version;
                
                Console.WriteLine(String.Format("Current GETIID Version: {0}", localVersion));
                //string version = versionInfo.FileVersion;
                IEnumerable<String> files = Directory.GetFiles(path);

                WebRequest request = WebRequest.Create("https://api.github.com/repos/Maciox55/MicrosoftOfficeActivationUtility/releases");
                request.Headers.Add("User-Agent", "request");
                WebResponse response = request.GetResponse();

                //Print API to console
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd(); // Contains JSON data from API call response, needs to be parsed
                    
                    Console.WriteLine(result);
                }


                Version newestVersion = Version.Parse("2.8.1.99999");
                Console.WriteLine(String.Format("Newest GETIID Version: {0}", newestVersion));

                if (localVersion < newestVersion)
                {
                    backupZIP("update_backup.zip", files); // Zip the GETIID version before downloading the 
                }
                else {

                    Console.WriteLine("You are on the latest release!");
                }

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
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

        static void unzip(string newZIP, bool overwrite)
        {
            ZipFile.ExtractToDirectory(newZIP, Directory.GetCurrentDirectory(),overwrite);
        }
    }
}
