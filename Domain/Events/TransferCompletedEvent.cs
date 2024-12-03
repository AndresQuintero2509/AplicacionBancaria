namespace Domain.Events
{
    public class TransferCompletedEvent: EventArgs
    {
        public Guid TransferId { get; }
        public Guid SourceAccountId { get; }
        public Guid TargetAccountId { get; }
        public decimal Amount { get; }
        public DateTime OccurredAt { get; }

        public TransferCompletedEvent(Guid transferId, Guid sourceAccountId, Guid targetAccountId, decimal amount, DateTime occurredAt)
        {
            TransferId = transferId;
            SourceAccountId = sourceAccountId;
            TargetAccountId = targetAccountId;
            Amount = amount;
            OccurredAt = occurredAt;
        }
    }
}