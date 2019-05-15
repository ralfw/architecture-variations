using System.Net;

namespace portfoliosimulation.contract.data.messages.commands
{
    public class CommandStatus {}
    
    public class Success : CommandStatus {}

    public class Failure : CommandStatus
    {
        public string Errormessage { get; }
        
        public Failure(string errormessage) { Errormessage = errormessage; }
    }


    public class HttpCommandStatus
    {
        public bool Success;
        public string Errormessage;

        public HttpCommandStatus(CommandStatus status)  {
            Success = status.GetType() == typeof(Success);
            if (Success is false) Errormessage = ((Failure) status).Errormessage;
        }

        public CommandStatus CommandStatus {
            get {
                if (Success) return new Success();
                return new Failure(Errormessage);
            }
        }
    }
}