using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltoHttp;
using System.IO;
using System.IO.Compression;
namespace updater
{
    class Program
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static string zipPath = Path.Combine(path, "altotestmanager.zip");
        static string url = "https://codeload.github.com/aalitor/AltoTestManager/zip/refs/heads/main";
        static string appPath = Path.Combine(path, "app");

        static void Main(string[] args)
        {
            Console.WriteLine("Downloading package...");

            var downloader = new HttpDownloader(url, zipPath);
            downloader.DownloadCompleted += downloader_DownloadCompleted;
            downloader.ProgressChanged += downloader_ProgressChanged;
            downloader.ErrorOccured += downloader_ErrorOccured;
            downloader.Start();

            Console.ReadLine();
        }

        static void downloader_ErrorOccured(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.GetException().Message);
            Console.WriteLine(e.GetException().StackTrace);

        }

        static void downloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        static void downloader_DownloadCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Extracting package... " + appPath);
            try
            {
                using (var strm = File.OpenRead(zipPath))
                using (ZipArchive a = new ZipArchive(strm))
                {
                    ExtractToDirectory(a, appPath, true);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("Done");
        }

        public static void ExtractToDirectory(ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }

            DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
            string destinationDirectoryFullPath = di.FullName;

            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

                if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                {
                    throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                }

                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }
    }
}
