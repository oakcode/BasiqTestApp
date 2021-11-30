using BasiqTestApp.Model;
using System;
using System.Collections.Generic;

namespace BasiqTestApp.Utils
{
    public interface ICalculator
    {
        float CalculateAverageSpendingPerCategory(List<Transaction> transactions, int category);
    }

    public class Calculator : ICalculator
    {
        public float CalculateAverageSpendingPerCategory(List<Transaction> transactions, int category)
        {
            if(transactions == null || transactions.Count == 0)
            {
                return 0;
            }

            float sum = 0;
            int count = 0;
            foreach (Transaction transaciton in transactions)
            {
                if (transaciton.subClass?.code == category)
                {
                    count++;
                    sum += Math.Abs(transaciton.amount);
                }
            }

            if (count == 0) return 0;

            return sum / count;
        }
    }
}
