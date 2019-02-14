using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleScraper
{
	internal class Program
    {
	    private static async Task Main(string[] args)
        {
	        var url = Console.ReadLine();
	        using (var httpClient = new HttpClient())
	        {
		        var output = await httpClient.GetStringAsync(url);
				Console.WriteLine(output);
	        }
        }
    }
}
