namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// A transaction.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// The request that initiated the transaction.
        /// </summary>
        public TransactionRequest Request { get; set; }

        /// <summary>
        /// The response to the processing of the transaction.
        /// </summary>
        public TransactionResponse Response { get; set; }
    }
}
