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
        public async Task<TransactionResponse> ProcessTransactionAsync(TransactionRequest request)
        {
            Transaction transaction = CreateTransaction(request);

            ValidationResult validationResult = _requestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                transaction.Response.Status = TransactionStatus.DetailsInvalid;
                transaction.Response.StatusMessage = validationResult.Message;
                return transaction.Response;
            }

            BankTransactionRequestResponse response = await _bank.SubmitRequestAsync(request);
            transaction.Response.Id = response.TransactionId;
            transaction.Response.Status = GetStatusFromResponseCode(response.Code);
            transaction.Response.StatusMessage = response.Code;

            await SaveAsync(transaction);

            return transaction.Response;
        }

        private static Transaction CreateTransaction(TransactionRequest request)
        {
            return new Transaction
            {
                Request = request,
                Response = new TransactionResponse()
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
