namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// Enumeration for the transaction status.
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// Transaction was successful.
        /// </summary>
        Successful,

        /// <summary>
        /// Transaction failed due to invalid details.
        /// </summary>
        DetailsInvalid,

        /// <summary>
        /// Transaction failed due to payment being rejected.
        /// </summary>
        Rejected,

        /// <summary>
        /// Transaction failed due to account not being found.
        /// </summary>
        AccountNotFound,

        /// <summary>
        /// Transaction status is unknown.
        /// </summary>
        Unknown
    }
}
