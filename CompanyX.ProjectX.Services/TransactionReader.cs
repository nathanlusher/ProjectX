using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.Services
{
    /// <inheritdoc/>
    public class TransactionReader : ITransactionReader
    {
        private readonly IRepository<Transaction> _transactionRepo;
        private readonly ISanitiser<Transaction> _transactionSanitiser;

        /// <summary>
        /// Initialises the instance with the supplied parameters.
        /// </summary>
        /// <param name="transactionRepo">The repository from which transactions are retrieved.</param>
        /// <param name="transactionSanitiser">The sanitiser with which to remove sensitive information before returning transactions.</param>
        public TransactionReader(IRepository<Transaction> transactionRepo, ISanitiser<Transaction> transactionSanitiser)
        {
            _transactionRepo = transactionRepo;
            _transactionSanitiser = transactionSanitiser;
        }

        /// <inheritdoc/>
        public async Task<Transaction> GetTransactionAsync(string id)
        {
            Transaction transaction = await _transactionRepo.GetItemAsync(id);

            if (transaction == null)
            {
                return null;
            }

            return _transactionSanitiser.Sanitise(transaction);
        }
    }
}
