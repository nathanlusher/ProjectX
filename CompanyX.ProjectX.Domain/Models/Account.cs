namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// A bank account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Name on account.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Account number.
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Account sort code.
        /// </summary>
        public string SortCode { get; set; }
    }
}
