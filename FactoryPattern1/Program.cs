namespace FactoryPattern1;

//https://www.youtube.com/watch?v=SLEu6rNdJj0
internal class Program
{
    static void Main(string[] args)
    {
        // this payment method will normally be selected by the user. so it will be passed in as a variable.
        // so it demonstrates the pattern where in real life this would not need to change the code because of the passed in variable.
        IPayable payment = PaymentFactory.Create(Payment.apple); 
        payment.Pay(100);
        payment = PaymentFactory.Create(Payment.google);
        payment.Pay(100);
    }
}
public static class PaymentFactory
{
    public static IPayable Create(Payment paymentMethod)
    {
        switch(paymentMethod)
        {
            case Payment.apple:
                return new ApplePay();
                break;
            case Payment.google:
                return new GooglePay();
                break;
            case Payment.card:
                return new Card();
                break;
            default:
                return null;
        }
    }
}
public interface IPayable
{
    void Pay(decimal amount);
}
public enum Payment
{
    google,
    apple,
    card
}

public class GooglePay : IPayable
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"paid {amount} with google");
    }
}
public class ApplePay : IPayable
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"paid {amount} with apple");
    }
}
public class Card : IPayable
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"paid {amount} with card");
    }
}
