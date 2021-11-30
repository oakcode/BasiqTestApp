using BasiqTestApp.DataAccess;
using BasiqTestApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasiqTestApp.Utils
{
    public class Manager
    {
        private static BasiqProxy basiqProxy;
        private static TransactionsProvider transactionsProvider;
        private static TransactionsRepository transactionsRepository;
        private static Calculator calculator;
        static Manager()
        {
            //initialize components
            basiqProxy = new BasiqProxy();
            transactionsProvider = new TransactionsProvider(basiqProxy);
            transactionsRepository = new TransactionsRepository(transactionsProvider);
            calculator = new Calculator();
        }

        public async Task<float> GetAverageValueAsync(string userId, int code)
        {
            List<Transaction> transactions = await transactionsRepository.GetAllTransactionsForUserAsync(userId);
            float average = calculator.CalculateAverageSpendingPerCategory(transactions, code);
            return average;
        }

    }
}
