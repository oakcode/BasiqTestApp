using BasiqTestApp.DataAccess;
using BasiqTestApp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace BasiqTestApp
{
    public class TransactionsRepository
    {
        private ITransactionsProvider transactionsProvider = null;

        public TransactionsRepository(ITransactionsProvider tp)
        {
            transactionsProvider = tp;
        }

        public async Task<List<Transaction>> GetAllTransactionsForUserAsync(string userId)
        {
            string initialUrl = CreateURL(userId);

            Transactions transactions = await transactionsProvider.GetTransactionsAsync(initialUrl);
            List<Transaction> allTransactions = new List<Transaction>(transactions.size);

            if (transactions.data?.Count > 0)
            {
                allTransactions.AddRange(transactions.data);
            }

            while(!string.IsNullOrEmpty(transactions?.links?.next))
            {
                transactions = await transactionsProvider.GetTransactionsAsync(transactions.links.next);
                if (transactions.data?.Count > 0)
                {
                    allTransactions.AddRange(transactions.data);
                }
            }

            return allTransactions;
        }

        private string CreateURL(string userId)
        {
            var builder = new UriBuilder(Settings.Url);
            builder.Path = $"users/{userId}/transactions";
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["filter"] = "transaction.class.eq('payment')";
            builder.Query = query.ToString();
            string url = builder.ToString();
            return url;
        }
    }
}
