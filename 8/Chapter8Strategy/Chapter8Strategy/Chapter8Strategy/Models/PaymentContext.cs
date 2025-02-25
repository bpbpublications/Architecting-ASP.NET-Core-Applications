namespace Chapter8Strategy.Models;

public class PaymentContext
{
    private IPaymentStrategy paymentStrategy;

    public PaymentContext(IPaymentStrategy paymentStrategy)
    {
        this.paymentStrategy = paymentStrategy;
    }

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        this.paymentStrategy = paymentStrategy;
    }

    public async Task ExecutePayment(decimal amount)
    {
        await paymentStrategy.ProcessPaymentAsync(amount);
    }

    public string GetPaymentProcess()
    {
        var ret = string.Empty;
        if(paymentStrategy is CreditCardPayment)
        {
            ret = "Credit card";
        }
        if (paymentStrategy is PaypalPayment)
        {
            ret = "PayPal";
        }
        return ret;
    }
}