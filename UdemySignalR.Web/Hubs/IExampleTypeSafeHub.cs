namespace UdemySignalR.Web.Hubs
{
    public interface IExampleTypeSafeHub
    {
        Task ReceiveMessageForAllClient(string message);

        Task ReceiveTypedMessageForAllClient(UserDto user);

        Task ReceiveConnectedClientCountAllCLient(int clientCount);

        Task ReceiveMessageForCallerClient(string message);

        Task ReceiveMessageForOthersClient(string message);

        Task ReceiveMessageForIndividualClient(string message);

        Task ReceiveMessageForGroupClients(string message);
    }
}