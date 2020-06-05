using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHanderTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailServices());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Fausto";
            command.LastName = "Schwantz";
            command.Email = "faustoschwantz@gmail.com";
            command.Document = "12345678910";
            command.BarCode = "123456891354";
            command.BoletoNumber = "65428161465";
            command.PaymentNumber = "65246145";
            command.paidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 100;
            command.TotalPaid = 100;
            command.Payer = "DEV CORP";
            command.PayerDocument = "564265441";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "faustoschwantz@gmail.com";
            command.Street = "Rua ABC";
            command.Number = "1010";
            command.Neighborhood = "Centro";
            command.City = "Trindade Do Sul";
            command.State = "Rio Grande Do Sul";
            command.Country = "Brasil";
            command.ZipCode = "99615000";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}