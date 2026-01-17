using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using System.IO;
namespace Ai
{
    public class HuggingfaceApi : HuggingfaceInterface
    {

        private string token { get; set; } 
        private string url = "https://router.huggingface.co/v1/chat/completions";

        public HuggingfaceApi(string api)
        {
            token = api;

        }
        public async Task<string> Query(string query)

        {
            using var client = new HttpClient();
            // Setup headers
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var payload = new
            {
                messages = new[]
                {
                 new { role = "user", content = query }
                },
                model = "deepseek-ai/DeepSeek-R1", 
                stream = false
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                //send and recieve http request
                var response = await client.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                //extract the response from the json document
                using JsonDocument doc = JsonDocument.Parse(responseBody);
                string assistantReply = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString()!;
                //cleans the output to remove all the thought proccess and only show the answer hardcoded so might not work for other models
               int answerBeggins = assistantReply.LastIndexOf("</think>");
               string finalAnswer = assistantReply.Substring(answerBeggins +8).Trim();

                if (response.IsSuccessStatusCode)
                {
                    // save the full response to a json file for debugging 
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string jsonString = JsonSerializer.Serialize(doc, options);
                    File.WriteAllText("Queryresponse.json", jsonString);

                    return finalAnswer;
                }
                else
                {
                    return response.StatusCode + responseBody;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
