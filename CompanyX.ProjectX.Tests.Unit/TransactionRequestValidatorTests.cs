using CompanyX.ProjectX.Services;
using System;
using Xunit;

namespace CompanyX.ProjectX.Tests.Unit
{
    public class TransactionRequestValidatorTests
    {
        [Theory]
        [InlineData("1234 5678 1234 5678", true)]
        [InlineData("1234-5678-1234-5678", true)]
        [InlineData("1234567812345678", true)]
        [InlineData("1234 5678 1234 567", false)]
        [InlineData("123456781234567", false)]
        [InlineData("123456781234567W", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void CreditCardNumberIsValidated(string number, bool valid)
        {
            Domain.Models.TransactionRequest transaction = TestData.CreateRequest();
            transaction.Shopper.Card.Number = number;

            TransactionRequestValidator validator = new();
            Domain.Models.ValidationResult result = validator.Validate(transaction);

            Assert.Equal(valid, result.IsValid);
            Assert.Equal(valid ? null : InvalidMessages.CardNumber, result.Message);
        }

        [Theory]
        [InlineData(12, true)]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(13, false)]
        public void ExpiryMonthIsValidated(uint month, bool valid)
        {
            Domain.Models.TransactionRequest transaction = TestData.CreateRequest();
            transaction.Shopper.Card.ExpiryMonth = month;

            TransactionRequestValidator validator = new();
            Domain.Models.ValidationResult result = validator.Validate(transaction);

            Assert.Equal(valid, result.IsValid);
            Assert.Equal(valid ? null : InvalidMessages.CardExpiryMonth, result.Message);
        }

        [Theory]
        [InlineData(2000)]
        [InlineData(1900)]
        [InlineData(12)]
        [InlineData(0)]
        public void ExpiryYearIsValidated(uint year)
        {
            Domain.Models.TransactionRequest transaction = TestData.CreateRequest();
            transaction.Shopper.Card.ExpiryYear = (uint)DateTime.UtcNow.AddYears(11).Year;

            TransactionRequestValidator validator = new();
            Domain.Models.ValidationResult result = validator.Validate(transaction);

            bool valid = year < DateTime.UtcNow.AddYears(10).Year && year > DateTime.UtcNow.AddYears(-10).Year;

            Assert.Equal(valid, result.IsValid);
            Assert.Equal(valid ? null : InvalidMessages.CardExpiryYear, result.Message);
        }
    }
}
