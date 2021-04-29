using CompanyX.ProjectX.Domain.Models;
using System;

namespace CompanyX.ProjectX.Tests.Unit
{
    internal static class TestData
    {
        public static string NewGuid => Guid.NewGuid().ToString();

        public static Transaction CreateTransaction()
        {
            return new Transaction
            {
                Id = NewGuid,
                Request = CreateRequest(),
                Status = TransactionStatus.Successful
            };
        }

        public static TransactionRequest CreateRequest()
        {
            return new TransactionRequest
            {
                Shopper = CreateShopper(),
                Merchant = CreateMerchant(),
                Payment = CreatePayment(),
                Date = DateTime.UtcNow
            };
        }

        public static CardHolder CreateShopper()
        {
            return new CardHolder
            {
                Contact = CreateContact(),
                Card = new Card
                {
                    Type = CardType.VISA,
                    Name = NewGuid,
                    Number = "1111-2222-3333-4444",
                    SecurityCode = 123,
                    ExpiryMonth = 10,
                    ExpiryYear = (uint)DateTime.UtcNow.AddYears(2).Year,
                    IssueNumber = 2,
                }
            };
        }

        public static AccountHolder CreateMerchant()
        {
            return new AccountHolder
            {
                Contact = CreateContact(),
                Account = new Account
                {
                    Name = NewGuid,
                    Number = NewGuid,
                    SortCode = NewGuid
                }
            };
        }

        public static Contact CreateContact()
        {
            return new Contact
            {
                FirstName = NewGuid,
                LastName = NewGuid,
                AddressLine1 = NewGuid,
                AddressLine2 = NewGuid,
                CompanyName = NewGuid,
                Country = NewGuid,
                County = NewGuid,
                Email = NewGuid,
                Postcode = NewGuid
            };
        }

        public static Payment CreatePayment()
        {
            return new Payment
            {
                Currency = new Currency
                {
                    Name = NewGuid,
                    Symbol = NewGuid
                },
                Value = 200.78M
            };
        }
    }
}
