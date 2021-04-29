namespace CompanyX.ProjectX.Services
{
    /// <summary>
    /// Status messages for validation failures.
    /// </summary>
    public static class InvalidMessages
    {
        /// <summary>
        /// Validation message for invalid card number.
        /// </summary>
        public const string CardNumber = "The card number is invalid";

        /// <summary>
        /// Validation message for invalid card expiry month.
        /// </summary>
        public const string CardExpiryMonth = "The card expiry month is invalid";

        /// <summary>
        /// Validation message for invalid card expirt year.
        /// </summary>
        public const string CardExpiryYear = "The card expiry year is invalid";
    }
}
