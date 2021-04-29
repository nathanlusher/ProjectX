namespace CompanyX.ProjectX.Domain.Models
{
    /// <summary>
    /// The result of a validation process.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// A flag indicating whether the validation succeeded or failed.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// A message relating to the validation result.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Creates a new instance of a validation result with a valid status.
        /// </summary>
        /// <returns>A new instance of a validation result.</returns>
        public static ValidationResult Valid()
        {
            return new ValidationResult { IsValid = true };
        }

        /// <summary>
        /// Creates a new instance of a validation result with an invalid status.
        /// </summary>
        /// <returns>A new instance of a validation result.</returns>
        public static ValidationResult Invalid(string message)
        {
            return new ValidationResult { IsValid = false, Message = message };
        }
    }
}
