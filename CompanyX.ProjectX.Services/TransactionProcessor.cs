using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyX.ProjectX.Services
{
    /// <inheritdoc/>
    public class TransactionProcessor : ITransactionProcessor
    {
        private static readonly Dictionary<string, TransactionStatus> StatusMappings = new()
        {
            ["AOK"] = TransactionStatus.Successful,
            ["ANF"] = TransactionStatus.AccountNotFound,
            ["DIN"] = TransactionStatus.DetailsInvalid,
            ["REJ"] = TransactionStatus.Rejected,
            ["ERR"] = TransactionStatus.Unknown,
        };

        private readonly IRepository<Transaction> _transactionRepo;
        private readonly IValidator<TransactionRequest> _requestValidator;
        private readonly IBank _bank;

        /// <summary>
        /// Initialises the instance with the supplied parameters.
        /// </summary>
        /// <param name="transactionRepo">The repository to which transactions are saved.</param>
        /// <param name="requestValidator">The validator with which requests should be validated.</param>
        /// <param name="bank">The acquiring bank by which transactions are processed.</param>
        public TransactionProcessor(IRepository<Transaction> transactionRepo, IValidator<TransactionRequest> requestValidator, IBank bank)
        {
            _transactionRepo = transactionRepo;
            _requestValidator = requestValidator;
            _bank = bank;
        }

        /// <inheritdoc/>
        public async Task<Transaction> ProcessTransactionAsync(TransactionRequest request)
        {
            Transaction transaction = CreateTransaction(request);

            ValidationResult validationResult = _requestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                transaction.Status = TransactionStatus.DetailsInvalid;
                transaction.StatusMessage = validationResult.Message;
                return transaction;
            }

            TransactionRequestResponse response = await _bank.SubmitRequestAsync(request);
            transaction.Id = response.TransactionId;
            transaction.Status = GetStatusFromResponseCode(response.Code);
            transaction.StatusMessage = response.Code;

            await SaveAsync(transaction);

            return transaction;
        }

        private static Transaction CreateTransaction(TransactionRequest request)
        {
            return new Transaction
            {
                Request = request
            };
        }

        private async Task SaveAsync(Transaction transaction)
        {
            transaction.Request.Shopper.Card.SecurityCode = null;

            await _transactionRepo.SaveItemAsync(transaction);
        }

        private static TransactionStatus GetStatusFromResponseCode(string code)
        {
            if (code != null && StatusMappings.ContainsKey(code))
            {
                return StatusMappings[code];
            }

            return TransactionStatus.Unknown;
        }
    }
}
