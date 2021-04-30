using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;
using CompanyX.ProjectX.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CompanyX.ProjectX.Tests.Unit
{
    public class TransactionReaderTests
    {
        [Fact]
        public async Task ReaderReturnsTransactionByIdAsync()
        {
            Transaction transaction = TestData.CreateTransaction();

            Mock<IRepository<Transaction>> mockRepo = CreateMockRepo(transaction);
            Mock<ISanitiser<Transaction>> mockSanitiser = CreateMockSanitiser(transaction);

            ITransactionReader reader = CreateReader(mockRepo.Object, mockSanitiser.Object);
            Transaction returnedTransaction = await reader.GetTransactionAsync(transaction.Response.Id);

            Assert.Equal(transaction, returnedTransaction);
        }

        [Fact]
        public async Task ReaderReturnsMaskedCardNumberAsync()
        {
            Transaction transaction = TestData.CreateTransaction();
            transaction.Request.Shopper.Card.Number = "1234 5678 1234 5678";

            Mock<IRepository<Transaction>> mockRepo = CreateMockRepo(transaction);

            ITransactionReader reader = CreateReader(mockRepo.Object, new TransactionSanitiser());
            Transaction returnedTransaction = await reader.GetTransactionAsync(transaction.Response.Id);

            Assert.Equal("****5678", returnedTransaction.Request.Shopper.Card.Number);
        }

        [Fact]
        public async Task NullIsReturnedIfTransactionIsNotFound()
        {
            string id = TestData.NewGuid;

            Mock<IRepository<Transaction>> mockRepo = new();
            mockRepo
                .Setup(r => r.GetItemAsync(id))
                .ReturnsAsync((Transaction)null);

            ITransactionReader reader = CreateReader(mockRepo.Object, new Mock<ISanitiser<Transaction>>().Object);
            Transaction returnedTransaction = await reader.GetTransactionAsync(id);

            Assert.Null(returnedTransaction);
        }

        private static Mock<IRepository<Transaction>> CreateMockRepo(Transaction transaction)
        {
            Mock<IRepository<Transaction>> mockRepo = new();

            mockRepo
                .Setup(r => r.GetItemAsync(transaction.Response.Id))
                .ReturnsAsync(transaction);

            return mockRepo;
        }

        private static Mock<ISanitiser<Transaction>> CreateMockSanitiser(Transaction transaction)
        {
            Mock<ISanitiser<Transaction>> mockSanitiser = new();

            mockSanitiser
                .Setup(r => r.Sanitise(transaction))
                .Returns(transaction);

            return mockSanitiser;
        }

        private static ITransactionReader CreateReader(IRepository<Transaction> transactionRepo, ISanitiser<Transaction> sanitiser)
        {
            return new TransactionReader(transactionRepo, sanitiser);
        }
    }
}
