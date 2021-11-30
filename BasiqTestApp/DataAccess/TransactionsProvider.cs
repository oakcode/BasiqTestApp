using BasiqTestApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BasiqTestApp.DataAccess
{
    public interface ITransactionsProvider
    {
        Task<Transactions> GetTransactionsAsync(string url, bool retryIfTokenExpired = true);
    }

    public class TransactionsProvider : ITransactionsProvider
    {
        private static JsonSerializer serializer = null;
        private IBasiqProxy basiqProxy = null;
        static TransactionsProvider()
        {
            serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
        }

        public TransactionsProvider(IBasiqProxy bp)
        {
            basiqProxy = bp;
        }

        public async Task<Transactions> GetTransactionsAsync(string url, bool retryIfTokenExpired = true)
        {
            var response = await basiqProxy.GetAsync(url);

            Transactions transactions = null;
            try
            {
                var stream = await response.Content.ReadAsStreamAsync();
                using (var reader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(reader))
                    transactions = serializer.Deserialize<Transactions>(jsonReader);

                if (retryIfTokenExpired && CheckIfTokenIsExpired(transactions))
                {
                    await basiqProxy.Reconfigure();
                    return await GetTransactionsAsync(url, false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Transactions();
            }

            return transactions;
        }

        private static bool CheckIfTokenIsExpired(Transactions transactions)
        {
            return transactions != null 
                && transactions.data.Count == 1 
                && transactions.data[0].type == "error" 
                && transactions.data[0].detail == "Token has expired";
        }
    }
}
