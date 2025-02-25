namespace Chapter8Strategy.Models;

public class PaypalPayment : IPaymentStrategy
{
    public async Task ProcessPaymentAsync(decimal amount)
    {
        // Logic for processing PayPal payment
        Console.WriteLine($"Processing PayPal payment for {amount:C}");
    }
}
