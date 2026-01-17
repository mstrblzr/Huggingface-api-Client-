using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Ai
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            var Secret = config["apikey"];
            //Console.WriteLine(Secret);
            IHuggingFaceClient huggingFace = new HuggingFaceClient(Secret);
            var response = huggingFace.Query("What is the capital of France?").Result;
            Console.WriteLine(response);
        }
    }
}