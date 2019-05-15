using System;
using eventstore;
using messagehandling.pipeline;
using messagehandling.pipeline.messagecontext;

namespace messagehandling
{
    public interface IMessagePump
    {
        void Register<TMessage>(IMessageContextManager ctxManager, IMessageProcessor processor);

        void Register<TMessage>(Func<IMessage, IMessageContext> load,
                                Func<IMessage, IMessageContext, Output> process, 
                                Action<Event[]> update);

        IMessage Handle(IMessage inputMessage);
    }
}