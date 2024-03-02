// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;
using SignalR.ConsoleApp;

Console.WriteLine("Hello, World!");


var hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7020/exampleTypeSafeHub")
    .Build();


hubConnection.StartAsync().ContinueWith(x =>
{
    Console.WriteLine(x.IsCompletedSuccessfully
        ? "Hub ile bağlanti kuruldu"
        : "Hub ile bağlanti kurulamadı.");
});

hubConnection.On<User>("ReceiveTypedMessageForAllClient",
    (message) => Console.WriteLine($"{message.UserName} - {message.Email}"));
Console.ReadLine();