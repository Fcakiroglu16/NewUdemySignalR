var toastTimeout;

$(document).ready(function () {

    const connection = new window.signalR.HubConnectionBuilder().withUrl("/hub").build();


    connection.start().then(() => {console.log("Bağlantı sağlandı.") })


    connection.on("AlertCompleteFile", (downloadPath) => {

        clearTimeout(toastTimeout);

        $(".toast-body").html(`<p>Excel oluşturma işlemi tamamlanmıştır. Aşağıdaki link ile excel dosyasını indirebilirsiniz<p>
        <a href="${downloadPath}">indir</a>
        `);

        $("#liveToast").show();

    })










})
