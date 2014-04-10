using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;

namespace BookIt.Api.Providers
{
    public class BearerTokenProvider
    {
        class TokenResponseModel
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("userName")]
            public string Username { get; set; }

            [JsonProperty(".issued")]
            public string IssuedAt { get; set; }

            [JsonProperty(".expires")]
            public string ExpiresAt { get; set; }
        }

        public async Task<string> GetBearerToken(string username, string password)
        {
            var baseUrl = "http://bookit.local/";
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            var body = "grant_type=password&username=" + username + "&password=" + password;
            HttpContent requestContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

            var responseMessage = await client.PostAsync("Token", requestContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsAsync<TokenResponseModel>();
                return response.AccessToken;
            }
            return null;
        }
    }
}