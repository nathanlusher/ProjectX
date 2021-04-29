using CompanyX.ProjectX.Domain.Models;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.Domain.Interfaces
{
    /// <summary>
    /// Processes transaction requests.
    /// </summary>
    public interface ITransactionProcessor
    {
        /// <summary>
        /// Processes the supplied transaction request.
        /// </summary>
        /// <param name="request">The transaction request to process.</param>
        /// <returns>The transaction resulting from processing the request.</returns>
        Task<Transaction> ProcessTransactionAsync(TransactionRequest request);
    }
}
