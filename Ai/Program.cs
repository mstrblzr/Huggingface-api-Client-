using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace HuggingFaceApiClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            var Secret = config["apikey"];

            HttpClient httpClient = new HttpClient();
           
            IHuggingFaceClient huggingFace = new HuggingFaceClient(Secret, httpClient);
            var response = huggingFace.Query("What is the capital of France?").Result;
            Console.WriteLine(response);
        }
    }
}