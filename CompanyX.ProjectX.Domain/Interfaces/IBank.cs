using CompanyX.ProjectX.Domain.Models;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.Domain.Interfaces
{
    /// <summary>
    /// Represents the acquiring bank for processing transaction requests.
    /// </summary>
    public interface IBank
    {
        /// <summary>
        /// Submits a transaction request to the bank for processing.
        /// </summary>
        /// <param name="request">The transaction request to process.</param>
        /// <returns>The response to the transaction request.</returns>
        Task<BankTransactionRequestResponse> SubmitRequestAsync(TransactionRequest request);
    }
}
