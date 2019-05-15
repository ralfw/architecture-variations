namespace portfoliosimulation.contract.messages
{
    public interface IMessage {}
    public interface IIncoming : IMessage {}
    public interface IOutgoing : IMessage {}
    
    public class Request : IIncoming {}
    public class Response : IOutgoing {}
    
    public class Notification : IIncoming, IOutgoing {}
    
    public class Command : Request {}
    public class Query : Request {}
    
    public class CommandStatus : Response {}
    
    public class Success : CommandStatus {}
    
    public class Failure : CommandStatus  {
        public string Errormessage { get; }
        public Failure(string errormessage) { Errormessage = errormessage; }
    }
    
    public class QueryResult : Response {}


    public class HttpCommandStatus
    {
        public readonly bool Success;
        public readonly string Errormessage;

        public HttpCommandStatus(CommandStatus commandStatus)  {
            Success = commandStatus.GetType() == typeof(Success);
            if (Success is false) Errormessage = ((Failure) commandStatus).Errormessage;
        }

        public CommandStatus CommandStatus {
            get {
                if (Success) return new Success();
                return new Failure(Errormessage);
            }
        }
    }
}