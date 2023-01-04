using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

// [TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;

    public StudentTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("34855372099", EDocumentType.CPF);
        _email = new Email("batman@dc.com");
        _address = new Address("Veigar", "123", "Void", "Runeterra", "GP", "SR", "151617");
        _student = new Student(_name, _document, _email, _address);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription() 
    {
        var subscription = new Subscription(null);
        var payment = new PaypalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);

        subscription.AddPayment(payment); 

        _student.AddSubscription(subscription);
        _student.AddSubscription(subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
        var subscription = new Subscription(null);
        _student.AddSubscription(subscription);

        Assert.IsFalse(_student.IsValid);
    }

    //[TestMethod]
    //public void ShouldReturnSuccessWhenAddSubscription()
    //{
    //    var subscription = new Subscription(null);
    //    var payment = new PaypalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);

    //    subscription.AddPayment(payment);

    //    _student.AddSubscription(subscription);

    //    Assert.IsTrue(_student.IsValid);
    //}
}