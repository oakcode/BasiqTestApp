using BasiqTestApp;
using BasiqTestApp.DataAccess;
using BasiqTestApp.Model;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BasiqTestAppTestSuite
{
    public class TransactionsRepositoryTest
    {
        private Mock<ITransactionsProvider> transactionProviderMock = new Mock<ITransactionsProvider>(MockBehavior.Strict);
        private TransactionsRepository targetUnit = null;

        [Fact]
        public async Task GetAllTransactionsForUserAsync_NoData()
        {
            Transactions transactions = new Transactions() { };

            transactionProviderMock.Setup(x => x.GetTransactionsAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(transactions));
            targetUnit = new TransactionsRepository(transactionProviderMock.Object);
            List<BasiqTestApp.Model.Transaction> retVal = await targetUnit.GetAllTransactionsForUserAsync("userId");

            Assert.NotNull(retVal);
            Assert.Empty(retVal);
        }

        [Fact]
        public async Task GetAllTransactionsForUserAsync_HasDataButOnlyOnePage()
        {
            Transactions transactions = new Transactions() { size = 3, data = new List<BasiqTestApp.Model.Transaction>(3)
            {
                new BasiqTestApp.Model.Transaction() { amount = -1.2f, subClass = new SubClass() { code = 400, title = "Test data 1" } },
                new BasiqTestApp.Model.Transaction() { amount = -55.45f, subClass = new SubClass() { code = 200, title = "Test data 2" } },
                new BasiqTestApp.Model.Transaction() { amount = -700.33f, subClass = new SubClass() { code = 400, title = "Test data 3" } }
            }
            };

            transactionProviderMock.Setup(x => x.GetTransactionsAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(transactions));
            targetUnit = new TransactionsRepository(transactionProviderMock.Object);
            List<BasiqTestApp.Model.Transaction> retVal = await targetUnit.GetAllTransactionsForUserAsync("userId");

            Assert.NotEmpty(retVal);
            Assert.True(retVal.Count == 3);
        }

        [Fact]
        public async Task GetAllTransactionsForUserAsync_TwoPages()
        {
            Transactions transactions = new Transactions()
            {
                size = 3,
                data = new List<BasiqTestApp.Model.Transaction>(3)
                {
                    new BasiqTestApp.Model.Transaction() { amount = -1.2f, subClass = new SubClass() { code = 400, title = "Test data 1" } },
                    new BasiqTestApp.Model.Transaction() { amount = -55.45f, subClass = new SubClass() { code = 200, title = "Test data 2" } },
                    new BasiqTestApp.Model.Transaction() { amount = -700.33f, subClass = new SubClass() { code = 400, title = "Test data 3" } }
                },
                links = new Links()
                {
                    next = "nextUrl"
                }
            };

            Transactions transactions2 = new Transactions()
            {
                size = 3,
                data = new List<BasiqTestApp.Model.Transaction>(3)
                {
                    new BasiqTestApp.Model.Transaction() { amount = -1.2f, subClass = new SubClass() { code = 400, title = "Test data 1" } },
                    new BasiqTestApp.Model.Transaction() { amount = -55.45f, subClass = new SubClass() { code = 200, title = "Test data 2" } },
                    new BasiqTestApp.Model.Transaction() { amount = -700.33f, subClass = new SubClass() { code = 400, title = "Test data 3" } }
                }
            };

            transactionProviderMock.Setup(x => x.GetTransactionsAsync(It.Is<string>(x => x != "nextUrl"), It.IsAny<bool>()))
                .Returns(Task.FromResult(transactions));

            transactionProviderMock.Setup(x => x.GetTransactionsAsync(It.Is<string>(x => x == "nextUrl"), It.IsAny<bool>()))
                .Returns(Task.FromResult(transactions2));

            targetUnit = new TransactionsRepository(transactionProviderMock.Object);
            List<BasiqTestApp.Model.Transaction> retVal = await targetUnit.GetAllTransactionsForUserAsync("userId");

            Assert.NotEmpty(retVal);
            Assert.True(retVal.Count == 6);
        }
    }
}
