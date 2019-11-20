using System;
namespace Chat.Core.EventHandlers
{
    public class MessageEventArgs : IMessageEventArgs
    {

        public string User { get; }
        public string Message { get; }

        public MessageEventArgs(string user, string message)
        {
            User = user;
            Message = message;
        }
    }
}
