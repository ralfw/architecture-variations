using messagehandling.pipeline.messagecontext;

namespace messagehandling.pipeline
{
    public interface IMessageProcessor {
        Output Process(IMessage input, IMessageContext model);
    }
}