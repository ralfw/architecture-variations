namespace messagehandling.pipeline
{
    public class NotificationOutput : Output {
        public Command[] Commands { get; }

        public NotificationOutput(Command[] commands) => Commands = commands;
    }
}