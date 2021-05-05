using System;
using System.Collections.Generic;
using System.IO;
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
        private static string _TOKEN { get => Settings._GITHUB_ACCESS_TOKEN.Replace("KEY=", "").Replace("U-System.KEY-", "ghp_"); }
        private static string _URL_API_GITHUB { get => "https://api.github.com/"; }


        public static async Task<Repository> GetRepositoryAsync(int repositoryID)
        {
            string url = string.Format("{0}repositories/{1}", _URL_API_GITHUB, repositoryID);

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

        public static async Task<Stream> GetReleaseAssetAsync(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "U-System.APP");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token",string.Concat("", _TOKEN));
            
            byte[] content = await client.GetByteArrayAsync(url);
            Stream stream = new MemoryStream(content);
            return stream;
        }
    }
}
