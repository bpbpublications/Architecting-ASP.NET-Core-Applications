namespace Chapter8Strategy.Models;

public class CreditCardPayment : IPaymentStrategy
{
    public async Task ProcessPaymentAsync(decimal amount)
    {
        // Logic for processing credit card payment
        Console.WriteLine($"Processing credit card payment for {amount:C}");
    }
}
