namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// A transaction.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// The transaction id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The transaction status.
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// A message associated with the transaction status.
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// The request that initiated the transaction.
        /// </summary>
        public TransactionRequest Request { get; set; }
    }
}
