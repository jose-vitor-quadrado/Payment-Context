using Flunt.Validations;
using Flunt.Notifications;
using PaymentContext.Shared.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities;

public class Student : Entity
{
    private IList<Subscription> _subscriptions;

    public Student(Name name, Document document, Email email, Address address)
    {
        Name = name;
        Document = document;
        Email = email;
        Address = address;
        _subscriptions = new List<Subscription>();

        AddNotifications(name, document, email);
    }

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

    public void AddSubscription(Subscription subscription) 
    {
        var hasSubscriptionActive = false;
        foreach (var sub in _subscriptions)
            if (sub.Active)
                hasSubscriptionActive = true;

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Voc� j� tem uma assinatura ativa")
            .AreNotEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "Esta assinatura n�o possui pagamentos"));

        if (IsValid)
            _subscriptions.Add(subscription);
    }
}