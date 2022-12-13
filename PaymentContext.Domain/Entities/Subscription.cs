namespace PaymentContext.Domain.Entities;

public class Subscription 
{   
    public DateTime CrateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public bool Active { get; set; }
    public List<Payment> Payments { get; set; } = null!;
}