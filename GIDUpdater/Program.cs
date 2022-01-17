using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Net;
using Newtonsoft.Json;

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
                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
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

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Author
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Uploader
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Asset
    {
        public string url { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public object label { get; set; }
        public Uploader uploader { get; set; }
        public string content_type { get; set; }
        public string state { get; set; }
        public int size { get; set; }
        public int download_count { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string browser_download_url { get; set; }
    }

    public class Root
    {
        public string url { get; set; }
        public string assets_url { get; set; }
        public string upload_url { get; set; }
        public string html_url { get; set; }
        public int id { get; set; }
        public Author author { get; set; }
        public string node_id { get; set; }
        public string tag_name { get; set; }
        public string target_commitish { get; set; }
        public string name { get; set; }
        public bool draft { get; set; }
        public bool prerelease { get; set; }
        public DateTime created_at { get; set; }
        public DateTime published_at { get; set; }
        public List<Asset> assets { get; set; }
        public string tarball_url { get; set; }
        public string zipball_url { get; set; }
        public string body { get; set; }
    }


}
