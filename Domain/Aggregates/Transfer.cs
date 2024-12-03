namespace Domain
{
    public class Transfer
    {
        public event EventHandler<Events.TransferCompletedEvent>? TransferCompleted;
        public Guid Id { get; }
        public BankAccount SourceAccount { get; }
        public BankAccount TargetAccount { get; }
        public Money Amount { get; }
        public DateTime TransferDate { get; }

        public Transfer(BankAccount sourceAccount, BankAccount targetAccount, Money amount)
        {

            if (sourceAccount == null) throw new ArgumentNullException(nameof(sourceAccount));
            if (targetAccount == null) throw new ArgumentNullException(nameof(targetAccount));
            if (amount == null) throw new ArgumentNullException(nameof(amount));
            if (sourceAccount.Balance.Currency != targetAccount.Balance.Currency)
                throw new InvalidOperationException("Accounts must use the same currency.");
            if (sourceAccount.Balance.Amount < amount.Amount)
                throw new InvalidOperationException("Insufficient funds in source account.");

            Id = Guid.NewGuid();
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
            Amount = amount;
            TransferDate = DateTime.UtcNow;

            // Aplicar lógica de transferencia
            SourceAccount.Withdraw(amount);
            TargetAccount.Deposit(amount);
        }

        public void CompleteTransfer()
        {
            // Disparar evento
            if (TransferCompleted != null)
            {
                TransferCompleted.Invoke(this, new Events.TransferCompletedEvent(
                    Id, SourceAccount.Id, TargetAccount.Id, Amount.Amount, TransferDate));
            }
            else
            {
                Console.WriteLine("No hay suscriptores para TransferCompleted.");
            }
        }
    }
}