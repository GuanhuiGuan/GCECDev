using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Chat.Core.EventHandlers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Core
{
    public class ChatService
    {
        public event EventHandler<MessageEventArgs> OnConnectionClosed;

        public event EventHandler<MessageEventArgs> OnEnterOrLeave;

        public event EventHandler<MessageEventArgs> OnReceiveMessage;

        HubConnection hubConnection;

        bool connected { get; set; }

        Dictionary<string, string> activeChannels { get; } = new Dictionary<string, string>();


        public void Init(string url, bool useHttps)
        {
            var port = string.Empty;
            // ios || android
            if (url.Equals("localhost") || url.Equals("10.0.2.2"))
            {
                port = useHttps ? ":5001" : ":5000";
            }
            string completeUrl = string.Format("http{0}://{1}{2}/hubs/chat",
                useHttps ? "s" : string.Empty, url, port);
            hubConnection = new HubConnectionBuilder().WithUrl(completeUrl).Build();

            // Closing connection
            hubConnection.Closed += async (error) =>
            {
                OnConnectionClosed?.Invoke(this, new MessageEventArgs("", "Closing connection"));
                connected = false;

                // Reconnect
                await Task.Delay(3000);
                try
                {
                    await ConnectAsync();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            };

            // Joining/leaving a group
            hubConnection.On<string>("EnterOrLeave", (message) =>
            {
                OnEnterOrLeave?.Invoke(this, new MessageEventArgs(message, message));
            });

            hubConnection.On<string>("Enter", (user) =>
            {
                OnEnterOrLeave?.Invoke(this, new MessageEventArgs(user, $"{user} joined"));
            });

            hubConnection.On<string>("Leave", (user) =>
            {
                OnEnterOrLeave?.Invoke(this, new MessageEventArgs(user, $"{user} left"));
            });

            // Receive message
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                OnReceiveMessage?.Invoke(this, new MessageEventArgs(user, message));
            });

        }

        public async Task ConnectAsync()
        {
            if (connected)
            {
                return;
            }
            await hubConnection.StartAsync();
            connected = true;
        }

        public async Task DisconnectAsync()
        {
            if (!connected)
            {
                return;
            }
            try
            {
                await hubConnection.DisposeAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            connected = false;
            activeChannels.Clear();
        }

        public async Task LeaveChannelAsync(string user, string channel)
        {
            if (!connected || !activeChannels.ContainsKey(channel))
            {
                return;
            }
            await hubConnection.SendAsync("RemoveFromChannel", user, channel);
            activeChannels.Remove(channel);
        }

        public async Task JoinChannelAsync(string user, string channel)
        {
            if (!connected || activeChannels.ContainsKey(channel))
            {
                return;
            }
            await hubConnection.SendAsync("JoinChannel", user, channel);
            activeChannels.Add(channel, user);
        }

        public async Task SendMsgAsync(string user, string channel, string msg)
        {
            if (!connected)
            {
                throw new InvalidOperationException("Disconnected");
            }
            await hubConnection.InvokeAsync("SendMessage", user, channel, msg);
        }


        // Temp
        public List<string> GetRooms()
        {
            return new List<string> { "IOS", "Android" };
        }
    }
}
