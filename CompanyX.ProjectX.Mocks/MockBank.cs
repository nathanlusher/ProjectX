using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.Mocks
{
    public class MockBank : IBank
    {
        public Task<BankTransactionRequestResponse> SubmitRequestAsync(TransactionRequest request)
        {
            BankTransactionRequestResponse response = new()
            {
                Code = "AOK",
                TransactionId = Guid.NewGuid().ToString()
            };

            return Task.FromResult(response);
        }
    }
}
