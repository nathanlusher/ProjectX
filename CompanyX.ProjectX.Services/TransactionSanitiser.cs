using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;

namespace CompanyX.ProjectX.Services
{
    /// <inheritdoc/>
    public class TransactionSanitiser : ISanitiser<Transaction>
    {
        /// <inheritdoc/>
        public Transaction Sanitise(Transaction item)
        {
            Card card = item.Request.Shopper.Card;

            card.Number = $"****{card.Number[^4..]}";
            card.SecurityCode = null;

            return item;
        }
    }
}
