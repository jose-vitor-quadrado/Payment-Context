using PaymentContext.Domain.Entities;

namespace PaymentContext.Doamin.Repositories;

public interface IStudentRepository
{
    bool DocumentExists(string document);
    bool EmailExists(string email);
    void CreateSubscription(Student student);
}