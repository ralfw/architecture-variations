namespace messagehandling
{
    public interface IMessage {}
    public interface IIncoming : IMessage {}
    public interface IOutgoing : IMessage {}
    
    public abstract class Request : IIncoming {}
    public abstract class Response : IOutgoing {}

    
    public abstract class Notification : IIncoming, IOutgoing {}
    
    public abstract class Command : Request {}
    public abstract class Query : Request {}
    
    public abstract class CommandStatus : Response {}
    
    public class Success : CommandStatus {}
    
    public class Failure : CommandStatus  {
        public string Errormessage { get; }
        public Failure(string errormessage) { Errormessage = errormessage; }
    }
    
    public abstract class QueryResult : Response {}
    
    
    public class NoResponse : Response {}
}