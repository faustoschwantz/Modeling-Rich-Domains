using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repository;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
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
            // Fail Fast Validations
            command.Validate();
            AddNotifications(new Contract());

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // Verificar se CPF já existe
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso.");

            // Verificar se e-mail já existe
            if (_repository.DocumentExists(command.Email))
                AddNotification("Document", "Este Email já está em uso.");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // Gerar Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.paidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address, email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Adress, "Bem vindo!", "Você conseguiu");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso.");

        }

        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}