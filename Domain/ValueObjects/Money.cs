using System;

namespace Domain{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount must be positive.");
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required.");

            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot add amounts in different currencies.");
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot subtract amounts in different currencies.");
            if (Amount < other.Amount)
                throw new InvalidOperationException("Insufficient funds.");
            return new Money(Amount - other.Amount, Currency);
        }
    }
}