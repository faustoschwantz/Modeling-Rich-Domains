using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Services;

namespace PaymentContext.Shared.Mocks
{
    public class FakeEmailServices : IEmailService
    {
        public void Send(string to, string email, string subject, string body)
        {

        }
    }
}