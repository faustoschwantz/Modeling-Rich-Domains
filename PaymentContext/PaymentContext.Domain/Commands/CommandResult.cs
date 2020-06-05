using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {

        }

        public CommandResult(bool sucess, string messages)
        {
            Sucess = sucess;
            Messages = messages;
        }

        public bool Sucess { get; set; }
        public string Messages { get; set; }
    }
}