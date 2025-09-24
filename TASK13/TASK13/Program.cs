using System;
using System.Threading.Tasks; 

class Program
{
    static async Task Main(string[] args) // Entry point of the program
    {
        Console.WriteLine("Fetching data, please wait..."); // Inform the user that data fetching is in progress

        string result = await GetDataAsync(); // Await the asynchronous method

        Console.WriteLine($"Data received: {result}"); // Output the result
    }

    static async Task<string> GetDataAsync() // Asynchronous method to simulate data fetching
    {
        
        await Task.Delay(2000); // Simulate a delay of 2 seconds
        return "Hello, this is your data!";
    }
}
