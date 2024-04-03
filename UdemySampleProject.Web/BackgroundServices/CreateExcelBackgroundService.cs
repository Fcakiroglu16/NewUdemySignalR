
using ClosedXML.Excel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileProviders;
using System.Data;
using System.Threading.Channels;
using UdemySampleProject.Web.Hubs;
using UdemySampleProject.Web.Models;

namespace UdemySampleProject.Web.BackgroundServices
{
    public class CreateExcelBackgroundService(Channel<(string userId, List<Product> products)> channel,IFileProvider fileProvider,IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await channel.Reader.WaitToReadAsync(stoppingToken))
            {


                await Task.Delay(4000);


                var (userId,products) = await channel.Reader.ReadAsync(stoppingToken);

                var wwwrootFolder = fileProvider.GetDirectoryContents("wwwroot");

                var files = wwwrootFolder.Single(x => x.Name == "files");


                var newExcelFileName = $"product-list-{Guid.NewGuid()}.xlsx";

                var newExcelFilePath= Path.Combine(files.PhysicalPath, newExcelFileName);


                var wb = new XLWorkbook();

                var ds = new DataSet();

                ds.Tables.Add(GetTable("Product List", products));

                wb.Worksheets.Add(ds);


                await using var excelFileStream = new FileStream(newExcelFilePath, FileMode.Create);

                wb.SaveAs(excelFileStream);


                using (var scope= serviceProvider.CreateScope())
                {
                    var appHub = scope.ServiceProvider.GetRequiredService<IHubContext<AppHub>>();

  
                    await appHub.Clients.User(userId).SendAsync("AlertCompleteFile", $"/files/{newExcelFileName}", stoppingToken);



                }

             



            }




        }



        private DataTable GetTable(string tableName, List<Product> products)
        {
            var table = new DataTable { TableName = tableName };

            foreach (var item in typeof(Product).GetProperties()) table.Columns.Add(item.Name, item.PropertyType);


            products.ForEach(x => { table.Rows.Add(x.Id, x.Name, x.Price, x.Description,x.UserId); });

            return table;
        }
    }
}
