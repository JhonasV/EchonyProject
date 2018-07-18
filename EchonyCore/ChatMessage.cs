using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EchonyCore
{
    public class ChatMessage
    {
        public ChatMessage(IHubContext<ChatHub> hubContext)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    hubContext.Clients.All.SendAsync("message", DateTime.Now.Ticks);
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
