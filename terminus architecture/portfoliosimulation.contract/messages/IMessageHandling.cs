namespace portfoliosimulation.contract.messages
{
    public interface IMessageHandling<TMessage> where TMessage : IIncoming {}
    
    public interface IRequestHandling<TIncoming, out TOutgoing> : IMessageHandling<TIncoming> 
                     where TIncoming : Request where TOutgoing : Response {
        TOutgoing Handle(TIncoming request);
    }

    public interface ICommandHandling<TCommand> : IRequestHandling<TCommand, CommandStatus> 
                     where TCommand : Command {}
    public interface IQueryHandling<TQuery, out TQueryResult> : IRequestHandling<TQuery,TQueryResult> 
                     where TQuery : Query where TQueryResult : QueryResult {}

    
    public interface INotificationHandling<TNotification> : IMessageHandling<TNotification> 
                     where TNotification : Notification  {
        void Handle(TNotification notification);
    }
}