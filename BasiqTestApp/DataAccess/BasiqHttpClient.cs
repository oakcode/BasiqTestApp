using System;
using System.Net.Http;

namespace BasiqTestApp.DataAccess
{
    public class BasiqHttpClient : HttpClient
    {
        public BasiqHttpClient(int timeout, string authSchema, string authParam) : base()
        {
            this.Timeout = TimeSpan.FromMilliseconds(timeout);
            if(authSchema != null && authParam != null) 
                this.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authSchema, authParam);
        }

        public void UpdateAuthParams(string authSchema, string authParam)
        {
            this.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authSchema, authParam);
        }
    }
}
