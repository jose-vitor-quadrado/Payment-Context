using Flunt.Notifications;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Services;
using PaymentContext.Doamin.Repositories;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : 
    Notifiable<Notification>, 
    IHandler<CreateBoletoSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar sua assinatura");
        }

        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");

        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este email já está em uso");

        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(
            command.Street, 
            command.Number, 
            command.Neighborhood, 
            command.City, 
            command.State, 
            command.Country, 
            command.ZipCode);

        var student = new Student(name, document, email, address);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(
            command.BarCode, 
            command.BoletoNumber, 
            command.PaidDate, 
            command.ExpireDate, 
            command.Total,
            command.TotalPaid,
            command.Payer,
            document,
            address,
            email);

        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        AddNotifications(name, document, email, address, student, subscription, payment);

        _repository.CreateSubscription(student);

        _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao balta.io", "Sua assinatura foi criada");

        return new CommandResult(true, "Assinatura realizada com sucesso");
    }
}