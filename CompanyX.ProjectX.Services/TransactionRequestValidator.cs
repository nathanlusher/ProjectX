using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Domain.Models;
using System;
using System.Text.RegularExpressions;

namespace CompanyX.ProjectX.Services
{
    /// <inheritdoc/>
    public class TransactionRequestValidator : IValidator<TransactionRequest>
    {
        /// <inheritdoc/>
        public ValidationResult Validate(TransactionRequest item)
        {
            ValidationResult result = ValidateCardNumber(item);

            if (result.IsValid)
            {
                result = ValidateCardExpiryMonth(item);
            }

            if (result.IsValid)
            {
                result = ValidateCardExpiryYear(item);
            }

            return result;
        }

        private static ValidationResult ValidateCardNumber(TransactionRequest item)
        {
            if (item?.Shopper?.Card?.Number == null)
            {
                return ValidationResult.Invalid(InvalidMessages.CardNumber);
            }

            string number = Regex.Replace(item.Shopper.Card.Number, "[^0-9]", "");

            if (number.Length != 16)
            {
                return ValidationResult.Invalid(InvalidMessages.CardNumber);
            }

            return ValidationResult.Valid();
        }

        private static ValidationResult ValidateCardExpiryMonth(TransactionRequest item)
        {
            if (item?.Shopper?.Card?.ExpiryMonth == null)
            {
                return ValidationResult.Invalid(InvalidMessages.CardNumber);
            }

            uint month = item.Shopper.Card.ExpiryMonth;

            if (month > 0 && month < 13)
            {
                return ValidationResult.Valid();
            }

            return ValidationResult.Invalid(InvalidMessages.CardExpiryMonth);
        }

        private static ValidationResult ValidateCardExpiryYear(TransactionRequest item)
        {
            if (item?.Shopper?.Card?.ExpiryYear == null)
            {
                return ValidationResult.Invalid(InvalidMessages.CardNumber);
            }

            uint year = item.Shopper.Card.ExpiryYear;

            if (year < DateTime.UtcNow.AddYears(10).Year && year > DateTime.UtcNow.AddYears(-10).Year)
            {
                return ValidationResult.Valid();
            }

            return ValidationResult.Invalid(InvalidMessages.CardExpiryYear);
        }
    }
}
