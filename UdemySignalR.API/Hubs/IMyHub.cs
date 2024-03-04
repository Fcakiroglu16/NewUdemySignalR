namespace UdemySignalR.API.Hubs
{
    public interface IMyHub
    {

        Task ReceiveMessageForAllClient(string message);
    }
}
