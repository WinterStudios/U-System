using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace U_System.API.GitHub
{
    public class GitHub
    {
        private static string _TOKEN { get => Paths.GITHUB.ACCESS_TOKEN; }
        private static string _URL_API_GITHUB { get => "https://api.github.com/"; }


        public static async Task<Repository[]> GetRepositoriesAsync(string user)
        {
            string url = string.Format("{0}user/repos", _URL_API_GITHUB, user);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent","U-System.APP");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3.raw"));
            client.DefaultRequestHeaders.Add("Authorization", string.Concat("token ", _TOKEN));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(string.Concat("token ", _TOKEN));
            Console.WriteLine("dd");
            string response = await client.GetStringAsync(url);

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<Repository[]>(response, jsonSerializerOptions);
            
        }

        public static async Task<Repository> GetRepositoryAsync(int id)
        {
            string url = string.Format("{0}repositories/{1}", _URL_API_GITHUB, id);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "U-System.APP");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3.raw"));
            client.DefaultRequestHeaders.Add("Authorization", string.Concat("token ", _TOKEN));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(string.Concat("token ", _TOKEN));
            Console.WriteLine("dd");
            string response = await client.GetStringAsync(url);

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<Repository>(response, jsonSerializerOptions);
        }

        public static async Task<Release[]> GetReleasesAsync(Repository repository)
        {
            string url = string.Format("{0}repos/{1}/{2}/releases", _URL_API_GITHUB, repository.Author.Name, repository.Name);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "U-System.APP");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3.raw"));
            client.DefaultRequestHeaders.Add("Authorization", string.Concat("token ", _TOKEN));
            
            string response = await client.GetStringAsync(url);

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<Release[]>(response, jsonSerializerOptions);
        }
    }
}
