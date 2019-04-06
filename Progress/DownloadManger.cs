using System.Threading.Tasks;
using System;
using System.IO;
using PShared;

namespace Progress
{
    public class DownloadManger
    {

        public DownloadManger()
        {
        }

        public static async Task Main()
        {
            //string APIString = "https://rss.itunes.apple.com/api/v1/us/apple-music/hot-tracks/all/100/explicit.json";
            string APIString = "https://sample-videos.com/zip/10mb.zip";

            var downloadFileUrl = APIString;

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var destinationFilePath = Path.Combine(documents, "explicit.json");

            try
            {
                
                using (var client = new HttpClientDownloadWithProgress(downloadFileUrl, destinationFilePath))
                {
                    client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) => {
                        Console.WriteLine($"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
                    };

                    await client.StartDownload();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception - ");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
