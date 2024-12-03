using System;

namespace Domain{
    public class BankAccount
    {
        public Guid Id { get; }
        public string AccountHolder { get; }
        private Money _balance;
        public Money Balance => _balance;

        public BankAccount(Guid id, string accountHolder, Money initialBalance)
        {
            Id = id;
            AccountHolder = accountHolder ?? throw new ArgumentNullException(nameof(accountHolder));
            _balance = initialBalance ?? throw new ArgumentNullException(nameof(initialBalance));
        }

        public void Deposit(Money amount)
        {
            if (amount == null) throw new ArgumentNullException(nameof(amount));
            _balance = _balance.Add(amount);
        }

        public void Withdraw(Money amount)
        {
            if (amount == null) throw new ArgumentNullException(nameof(amount));
            _balance = _balance.Subtract(amount);
        }
    }
}