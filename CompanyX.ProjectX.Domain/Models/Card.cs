namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// A credit or debit card.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// The type of credit or debit card.
        /// </summary>
        public CardType Type { get; set; }

        /// <summary>
        /// The long card number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The security code, or CVV.
        /// </summary>
        public uint? SecurityCode { get; set; }

        /// <summary>
        /// The name as written on the card.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The month of expiration.
        /// </summary>
        public uint ExpiryMonth { get; set; }

        /// <summary>
        /// The year of expiration.
        /// </summary>
        public uint ExpiryYear { get; set; }

        /// <summary>
        /// The (optional) issue number.
        /// </summary>
        public uint IssueNumber { get; set; }
    }
}
