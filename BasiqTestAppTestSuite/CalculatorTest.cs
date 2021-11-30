using BasiqTestApp.Model;
using BasiqTestApp.Utils;
using System.Collections.Generic;
using Xunit;

namespace BasiqTestAppTestSuite
{
    public class CalculatorTest
    {
        private ICalculator calculator = null;

        [Fact]
        public void CalculateAverageSpendingPerCategory_TransactionListIsNull()
        {
            calculator = new Calculator();
            var result = calculator.CalculateAverageSpendingPerCategory(null, 0);

            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateAverageSpendingPerCategory_TransactionListIsEmpty()
        {
            calculator = new Calculator();
            var result = calculator.CalculateAverageSpendingPerCategory(new System.Collections.Generic.List<BasiqTestApp.Model.Transaction>(0), 0);

            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateAverageSpendingPerCategory_NoTransactionWithCode()
        {
            var transactions = new List<BasiqTestApp.Model.Transaction>(3)
                {
                    new BasiqTestApp.Model.Transaction() { amount = -1.2f, subClass = new SubClass() { code = 400, title = "Test data 1" } },
                    new BasiqTestApp.Model.Transaction() { amount = -55.45f, subClass = new SubClass() { code = 200, title = "Test data 2" } },
                    new BasiqTestApp.Model.Transaction() { amount = -700.33f, subClass = new SubClass() { code = 400, title = "Test data 3" } }
                };

            calculator = new Calculator();
            var result = calculator.CalculateAverageSpendingPerCategory(transactions, 300);

            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateAverageSpendingPerCategory_TransactionsWithCodeExist()
        {
            var transactions = new List<BasiqTestApp.Model.Transaction>(3)
                {
                    new BasiqTestApp.Model.Transaction() { amount = -100.2f, subClass = new SubClass() { code = 400, title = "Test data 1" } },
                    new BasiqTestApp.Model.Transaction() { amount = -55.45f, subClass = new SubClass() { code = 200, title = "Test data 2" } },
                    new BasiqTestApp.Model.Transaction() { amount = -700.3f, subClass = new SubClass() { code = 400, title = "Test data 3" } }
                };

            calculator = new Calculator();
            var result = calculator.CalculateAverageSpendingPerCategory(transactions, 400);

            Assert.Equal(400.25f, result);
        }
    }
}
