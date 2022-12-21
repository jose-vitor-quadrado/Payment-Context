using Flunt.Validations;
using Flunt.Notifications;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{   
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsLowerOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
            .IsLowerOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres")
            .IsGreaterThan(FirstName, 40, "Name.FirstName", "Nome deve conter no máximo 40 caracteres"));
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}