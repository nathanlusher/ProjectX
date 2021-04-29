namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// The type of credit or debit card.
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// MasterCard credit card.
        /// </summary>
        MasterCard,

        /// <summary>
        /// VISA credit or debit card.
        /// </summary>
        VISA,

        /// <summary>
        /// American express credit card.
        /// </summary>
        Amex,

        /// <summary>
        /// Discover credit card.
        /// </summary>
        Discover,

        /// <summary>
        /// Other credit or debit card.
        /// </summary>
        Other
    }
}
