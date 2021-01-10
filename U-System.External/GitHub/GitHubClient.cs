using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


using U_System.External.Core;
using U_System.External.GitHub.Internal;

namespace U_System.External.GitHub
{
    public class GitHubClient
    {
        private static string _TOKEN { get => Settings._GITHUB_ACCESS_TOKEN; }
        private static string _URL_API_GITHUB { get => "https://api.github.com/"; }


        public static Repository[] GetRepositories(string user) => Task.Run(() => GetRepositoriesAsync(user)).Result;
        public static async Task<Repository[]> GetRepositoriesAsync(string user)
        {
            string url = string.Format("{0}user/repos", _URL_API_GITHUB, user);

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
            return JsonSerializer.Deserialize<Repository[]>(response, jsonSerializerOptions);

        }

    }
}
