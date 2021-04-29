namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// A payment in a particular currency.
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// The currency of the payment.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The payment value.
        /// </summary>
        public decimal Value { get; set; }
    }
}
