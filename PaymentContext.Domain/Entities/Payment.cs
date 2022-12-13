namespace PaymentContext.Domain.Entities;

public abstract class Payment 
{
    public string Number { get; set; } = string.Empty;
    public DateTime PaidDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public decimal Total { get; set; }
    public decimal TotalPaid { get; set; }
    public string Payer { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class BoletoPayment : Payment
{
    public string BarCode { get; set; } = string.Empty;
    public string BoletoNumber { get; set; } = string.Empty;
}

public class CreditCardPayment : Payment
{
    public string CardHolderName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string LastTransactionNumber { get; set; } = string.Empty;
}

public class PaypalPayment : Payment
{
    public string TransactionCode { get; set; } = string.Empty;
}