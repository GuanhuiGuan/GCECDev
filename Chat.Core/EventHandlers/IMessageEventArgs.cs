using System;
namespace Chat.Core.EventHandlers
{
    public interface IMessageEventArgs
    {
        string Message { get; }
    }
}
