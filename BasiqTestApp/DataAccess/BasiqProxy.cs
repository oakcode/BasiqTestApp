using BasiqTestApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BasiqTestApp.DataAccess
{
    public interface IBasiqProxy
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task Reconfigure();
    }

    public class BasiqProxy : IBasiqProxy
    {
        private static BasiqHttpClient basiqHttpDataClient = null;
        private static readonly BasiqHttpClient basiqHttpTokenClient = null;

        private static JsonSerializer serializer = null;

        static BasiqProxy()
        {
            serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            basiqHttpTokenClient = new BasiqHttpClient(5000, "Basic", Settings.AccessApiKey);
            basiqHttpTokenClient.DefaultRequestHeaders.Add("basiq-version", "2.0");
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            if (basiqHttpDataClient == null)
            {
                basiqHttpDataClient = await CreateHttpDataClientAsync();
            }

            return await basiqHttpDataClient.GetAsync(url);
        }

        public async Task Reconfigure()
        {
            Token token = await GetNewAccessTokenAsync();
            basiqHttpDataClient.UpdateAuthParams(token.token_type, token.access_token);
        }

        private async Task<BasiqHttpClient> CreateHttpDataClientAsync()
        {
            Token token = await GetNewAccessTokenAsync();
            var httpClient = new BasiqHttpClient(10000, token?.token_type, token?.access_token);
            return httpClient;
        }

        private async Task<Token> GetNewAccessTokenAsync()
        {
            Token token = null;
            try
            {
                HttpContent content = new StreamContent(new MemoryStream(0));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var response = await basiqHttpTokenClient.PostAsync(Settings.Url+"/token", content);
                var stream = await response.Content.ReadAsStreamAsync();
                using (var reader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(reader))
                    token = serializer.Deserialize<Token>(jsonReader);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get new access token. Reason: " + e.Message);
                return new Token() { access_token = "", token_type = "" };
            }

            return token;
        }
    }
}
