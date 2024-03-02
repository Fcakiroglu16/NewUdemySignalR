using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.WorkerService
{
    public class Worker(IConfiguration configuration) : BackgroundService
    {
        private HubConnection? _hubConnection;


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(configuration["SignalRHub"]!)
                .Build();


            _hubConnection.StartAsync(cancellationToken).ContinueWith(x =>
            {
                Console.WriteLine(x.IsCompletedSuccessfully
                    ? "Hub ile bağlanti kuruldu"
                    : "Hub ile bağlanti kurulamadı.");
            }, cancellationToken);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("çalıştı");
            _hubConnection?.On<User>("ReceiveTypedMessageForAllClient",
                (message) => Console.WriteLine($"{message.UserName} - {message.Email}"));

            return Task.CompletedTask;
            ;
        }
    }
}