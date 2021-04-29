namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// The response to a transaction request.
    /// </summary>
    public class TransactionRequestResponse
    {
        /// <summary>
        /// The response code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The transaction id.
        /// </summary>
        public string TransactionId { get; set; }
    }
}
