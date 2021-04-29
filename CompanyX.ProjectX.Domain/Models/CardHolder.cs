namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// A credit or debit card holder.
    /// </summary>
    public class CardHolder
    {
        /// <summary>
        /// The contact associated with the card.
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// The credit or debit card.
        /// </summary>
        public Card Card { get; set; }
    }
}
