using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;
using CompanyX.ProjectX.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CompanyX.ProjectX.Tests.Unit
{
    public class TransactionProcessorTests
    {
        private readonly Mock<IBank> _mockBank;
        private readonly Mock<IRepository<Transaction>> _mockRepo;
        private readonly Mock<IValidator<TransactionRequest>> _mockValidator;

        public TransactionProcessorTests()
        {
            _mockBank = new Mock<IBank>();

            _mockBank
                .Setup(b => b.SubmitRequestAsync(It.IsAny<TransactionRequest>()))
                .ReturnsAsync(new TransactionRequestResponse { TransactionId = TestData.NewGuid, Code = "AOK" });

            _mockRepo = new Mock<IRepository<Transaction>>();

            _mockValidator = new Mock<IValidator<TransactionRequest>>();
            _mockValidator
                .Setup(v => v.Validate(It.IsAny<TransactionRequest>()))
                .Returns(ValidationResult.Valid());
        }

        [Fact]
        public async Task IdIsPopulatedOnProcessTransactionAsync()
        {
            TransactionRequest request = TestData.CreateRequest();
            ITransactionProcessor processor = CreateProcessor();
            Transaction transaction = await processor.ProcessTransactionAsync(request);

            Assert.NotNull(transaction.Id);
        }

        [Theory]
        [InlineData("AOK", TransactionStatus.Successful)]
        [InlineData("ANF", TransactionStatus.AccountNotFound)]
        [InlineData("DIN", TransactionStatus.DetailsInvalid)]
        [InlineData("REJ", TransactionStatus.Rejected)]
        [InlineData("ERR", TransactionStatus.Unknown)]
        [InlineData("XXX", TransactionStatus.Unknown)]
        public async Task TransactionStatusIsUpdatedAsync(string code, TransactionStatus expectedStatus)
        {
            TransactionRequest request = TestData.CreateRequest();

            _mockBank
                .Setup(b => b.SubmitRequestAsync(request))
                .ReturnsAsync(new TransactionRequestResponse { TransactionId = TestData.NewGuid, Code = code });

            ITransactionProcessor processor = CreateProcessor();

            Transaction transaction = await processor.ProcessTransactionAsync(request);

            Assert.Equal(expectedStatus, transaction.Status);
            Assert.Equal(code, transaction.StatusMessage);
        }

        [Fact]
        public async Task TransactionIsWrittenToDatabaseAsync()
        {
            TransactionRequest request = TestData.CreateRequest();
            string transactionId = TestData.NewGuid;

            _mockBank
                .Setup(b => b.SubmitRequestAsync(request))
                .ReturnsAsync(new TransactionRequestResponse { TransactionId = transactionId, Code = TestData.NewGuid });

            Transaction savedTransaction = null;

            _mockRepo
                .Setup(r => r.SaveItemAsync(It.Is<Transaction>(t => t.Request == request)))
                .Callback((Transaction t) => savedTransaction = t);

            ITransactionProcessor processor = CreateProcessor();
            Transaction transaction = await processor.ProcessTransactionAsync(request);

            Assert.NotNull(savedTransaction);
            Assert.Equal(request, savedTransaction.Request);
            Assert.Equal(transactionId, savedTransaction.Id);
        }

        [Fact]
        public async Task SecurityCodeIsNotWrittenToDatabaseAsync()
        {
            TransactionRequest request = TestData.CreateRequest();

            _mockBank
                .Setup(b => b.SubmitRequestAsync(request))
                .ReturnsAsync(new TransactionRequestResponse());

            Transaction savedTransaction = null;

            _mockRepo
                .Setup(r => r.SaveItemAsync(It.Is<Transaction>(t => t.Request == request)))
                .Callback((Transaction t) => savedTransaction = t);

            ITransactionProcessor processor = CreateProcessor();
            Transaction transaction = await processor.ProcessTransactionAsync(request);

            Assert.NotNull(savedTransaction);
            Assert.Null(savedTransaction.Request.Shopper.Card.SecurityCode);
        }

        [Fact]
        public async Task TransactionRequestIsValidated()
        {
            TransactionRequest request = TestData.CreateRequest();

            string statusMessage = TestData.NewGuid;

            _mockValidator
                .Setup(v => v.Validate(request))
                .Returns(ValidationResult.Invalid(statusMessage));

            ITransactionProcessor processor = CreateProcessor();
            Transaction transaction = await processor.ProcessTransactionAsync(request);

            Assert.Equal(statusMessage, transaction.StatusMessage);
            Assert.Equal(TransactionStatus.DetailsInvalid, transaction.Status);
        }

        private ITransactionProcessor CreateProcessor()
        {
            return new TransactionProcessor(_mockRepo.Object, _mockValidator.Object, _mockBank.Object);
        }
    }
}
