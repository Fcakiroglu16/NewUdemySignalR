using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClientWorkerServiceApp
{
    public class Worker(ILogger<Worker> logger,IConfiguration configuration) : BackgroundService
    {
        //private readonly ILogger<Worker> _logger;
        //private readonly IConfiguration configuration;
        //public Worker(ILogger<Worker> logger, IConfiguration configuration)
        //{
        //    _logger = logger;
        //    this.configuration = configuration;
        //}
        private HubConnection? connection;
        public override Task StartAsync(CancellationToken cancellationToken)
        {

            connection= new HubConnectionBuilder().WithUrl(configuration.GetSection("SignalR")["Hub"]!).Build();



            connection?.StartAsync().ContinueWith((result) =>
            {
                logger.LogInformation(result.IsCompletedSuccessfully ? "Connected" : "Connection failed");
            });

            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await connection!.StopAsync(cancellationToken);
            await connection!.DisposeAsync();

           base.StopAsync(cancellationToken);
        }

        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {

            connection!.On<Product>("ReceiveTypedMessageForAllClient",
    (product) => { logger.LogInformation($"Received message: {product.Id}-{product.Name}-{product.Price}"); });

            return Task.CompletedTask;


        }
    }
}
