using CompanyX.ProjectX.Domain.Models;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.Domain.Interfaces
{
    /// <summary>
    /// Reads transactions from a data store.
    /// </summary>
    public interface ITransactionReader
    {
        /// <summary>
        /// Gets the transaction from the data store with the supplied id.
        /// </summary>
        /// <param name="id">The id of the transaction to return.</param>
        /// <returns>The transaction with the supplied id, if found; othewise, null.</returns>
        Task<Transaction> GetTransactionAsync(string id);
    }
}
