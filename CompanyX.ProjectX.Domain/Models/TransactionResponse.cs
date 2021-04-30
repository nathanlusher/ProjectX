namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// The response to the processing of a transaction.
    /// </summary>
    public class TransactionResponse
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
    }
}
