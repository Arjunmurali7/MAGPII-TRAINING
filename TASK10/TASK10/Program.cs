using System;
using System.Threading;
// delegate
public delegate void Download(string fileName);

public class FileDownloader
{
    // event using deligate
    public event Download DownloadCompleted; // event to notify when download is completed

    // raise the event
    public void DownloadFile(string fileName) // method to download file
    {
        Console.WriteLine($"Downloading {fileName}...");
        Thread.Sleep(1000);
        Console.WriteLine($"{fileName} download finished.");

        // Raise the event
        DownloadCompleted?.Invoke(fileName); // Notify subscribers that download is completed
    }
}

// method to handles the event
public class User // Subscriber
{
    public void DownloadCompleted(string fileName)// event handler method 
    {
        Console.WriteLine($"Notification: {fileName} has been downloaded."); // Notify user that download is completed
    }
}

class Program
{
    static void Main()
    {
        // Create objects
        FileDownloader downloader = new FileDownloader(); // Publisher
        User user = new User(); // Subscriber

        // Subscribe method to the event
        downloader.DownloadCompleted += user.DownloadCompleted;

        // Start download
        downloader.DownloadFile("class report");
    }
}