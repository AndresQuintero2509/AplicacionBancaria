using System;
using Domain;
using Domain.Events;

class Program
{
    static void Main(string[] args)
    {
        // Crear cuentas bancarias
        var sourceAccount = new BankAccount(Guid.NewGuid(), "John Doe", new Money(1000, "USD"));
        var targetAccount = new BankAccount(Guid.NewGuid(), "Jane Smith", new Money(500, "USD"));

        Console.WriteLine("Saldo inicial:");
        Console.WriteLine($"Cuenta origen ({sourceAccount.AccountHolder}): {sourceAccount.Balance.Amount} {sourceAccount.Balance.Currency}");
        Console.WriteLine($"Cuenta destino ({targetAccount.AccountHolder}): {targetAccount.Balance.Amount} {targetAccount.Balance.Currency}");
        Console.WriteLine();

        // Crear transferencia
        var amount = new Money(200, "USD");

        try
        {
            
            //Suscripción al evento antes de la transferencia
            TransferCompletedEvent? eventRaised = null;
            var transfer = new Transfer(sourceAccount, targetAccount, amount);

            // Suscribirse al evento
            transfer.TransferCompleted += (sender, e) =>
            {
                eventRaised = e;
                Console.WriteLine("Evento de transferencia completado:");
                Console.WriteLine($"- Transferencia ID: {e.TransferId}");
                Console.WriteLine($"- Cuenta origen ID: {e.SourceAccountId}");
                Console.WriteLine($"- Cuenta destino ID: {e.TargetAccountId}");
                Console.WriteLine($"- Monto transferido: {e.Amount} {sourceAccount.Balance.Currency}");
                Console.WriteLine($"- Fecha: {e.OccurredAt}");
                Console.WriteLine();
            };

            // Ejecutar transferencia
            Console.WriteLine("Ejecutando transferencia...");
            Console.WriteLine();
            transfer.CompleteTransfer();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Mostrar saldos finales
        Console.WriteLine("Saldo final:");
        Console.WriteLine($"Cuenta origen ({sourceAccount.AccountHolder}): {sourceAccount.Balance.Amount} {sourceAccount.Balance.Currency}");
        Console.WriteLine($"Cuenta destino ({targetAccount.AccountHolder}): {targetAccount.Balance.Amount} {targetAccount.Balance.Currency}");
    }
}