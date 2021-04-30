using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CompanyX.ProjectX.Tests.Integration
{
    public class TransactionControllerTests : WebApiTest
    {
        private const string RelativeUrl = "/transaction";

        [Fact]
        public async Task GetByIdReturnsExpectedTransactionAsync()
        {
            Transaction transaction = new()
            {
                Response = new TransactionResponse
                {
                    Id = NewGuid,
                    Status = TransactionStatus.AccountNotFound,
                    StatusMessage = NewGuid
                }
            };

            IRepository<Transaction> repo = Resolve<IRepository<Transaction>>();
            await repo.SaveItemAsync(transaction);

            HttpResponseMessage response = await Client.GetAsync($"{RelativeUrl}?id={transaction.Response.Id}");

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonSerializer.Serialize(transaction, DefaultJsonOptions), responseBody);
        }

        [Fact]
        public async Task GetByIdReturnsNotFoundAsync()
        {
            HttpResponseMessage response = await Client.GetAsync($"{RelativeUrl}?id=909090");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ProcessTransactionSavesToDatabaseAsync()
        {
            TransactionRequest request = new()
            {
                Date = DateTime.UtcNow,
                Shopper = new CardHolder
                {
                    Card = new Card
                    {
                        ExpiryMonth = 3,
                        ExpiryYear = (uint)DateTime.UtcNow.AddYears(1).Year,
                        Number = "1234 1234 1234 1234"
                    }
                }
            };

            HttpResponseMessage response = await Client.PostAsJsonAsync($"{RelativeUrl}", request);

            response.EnsureSuccessStatusCode();

            TransactionResponse transactionResponse = await response.Content.ReadFromJsonAsync<TransactionResponse>(DefaultJsonOptions);

            Assert.NotNull(transactionResponse);
            Assert.True(!string.IsNullOrWhiteSpace(transactionResponse.Id));
            Assert.Equal(TransactionStatus.Successful, transactionResponse.Status);

            Transaction transaction = await Resolve<IRepository<Transaction>>().GetItemAsync(transactionResponse.Id);

            Assert.NotNull(transactionResponse);
            Assert.Equal(JsonSerializer.Serialize(request), JsonSerializer.Serialize(transaction.Request));
        }

        [Fact]
        public async Task ProcessTransactionReturnsProblemDetailsAsync()
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync($"{RelativeUrl}", new TransactionRequest());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
