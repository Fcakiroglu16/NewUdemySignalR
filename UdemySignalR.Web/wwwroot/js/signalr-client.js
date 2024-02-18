$(document).ready(function () {

    const broadcastMessageToAllClientHubMethodCall = "BroadcastMessageToAllClient";
    const ReceiveMessageForAllClientClientMethodCall = "ReceiveMessageForAllClient";
    const receiveConnectedClientCountAllCLient = "ReceiveConnectedClientCountAllCLient";
    const connection = new signalR.HubConnectionBuilder().withUrl("/exampleTypeSafeHub").configureLogging(signalR.LogLevel.Information).build();


    function start() {
        connection.start().then(() => console.log("Hub ile bağlantı kuruldu"));
    }

    try {
        start();
    }
    catch
    {
        setTimeout(() => start(), 5000)
    }


    //subscribe
    connection.on(ReceiveMessageForAllClientClientMethodCall, (message) => {

        console.log("Gelen Mesaj", message);
    })



    var span_client_count = $("#span-connected-client-count");
    connection.on(receiveConnectedClientCountAllCLient, (count) => {
        span_client_count.text(count);
        console.log("connected client count", count);
    })


    $("#btn-send-message-all-client").click(function () {

        const message = "hello world";
        connection.invoke(broadcastMessageToAllClientHubMethodCall, message).catch(err => console.error("hata", err))

    })




})