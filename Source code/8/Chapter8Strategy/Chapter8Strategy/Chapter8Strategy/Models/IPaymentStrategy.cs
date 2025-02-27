namespace Chapter8Strategy.Models;

public interface IPaymentStrategy
{
    Task ProcessPaymentAsync(decimal amount);
}