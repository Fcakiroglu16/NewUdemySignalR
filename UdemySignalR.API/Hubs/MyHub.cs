using Microsoft.AspNetCore.SignalR;

namespace UdemySignalR.API.Hubs
{
    public class MyHub:Hub<IMyHub>
    {

        public async Task BroadcastMessageToAllClient(string message)
        {
            await Clients.All.ReceiveMessageForAllClient(message);
        }

    }
}
