namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// An account holder.
    /// </summary>
    public class AccountHolder
    {
        /// <summary>
        /// The contact associated with the account.
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// The account.
        /// </summary>
        public Account Account { get; set; }
    }
}
