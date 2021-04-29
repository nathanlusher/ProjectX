using System;

namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// A transaction request.
    /// </summary>
    public class TransactionRequest
    {
        /// <summary>
        /// The shopper from which funds should be transferred.
        /// </summary>
        public CardHolder Shopper { get; set; }

        /// <summary>
        /// The merchant to which funds should be transferred.
        /// </summary>
        public AccountHolder Merchant { get; set; }

        /// <summary>
        /// The payment to be transferred.
        /// </summary>
        public Payment Payment { get; set; }

        /// <summary>
        /// The date of the transaction request.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
