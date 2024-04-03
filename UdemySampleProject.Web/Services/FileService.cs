using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;
using UdemySampleProject.Web.Models;

namespace UdemySampleProject.Web.Services
{
    public class FileService(AppDbContext context,IHttpContextAccessor httpContextAccessor,UserManager<IdentityUser> userManager,Channel<(string userId,List<Product> products)> channel)
    {

       

        public async Task<bool> AddMessageToQueue()
        {

            var userId = userManager.GetUserId(httpContextAccessor!.HttpContext!.User);


            var products = await context.Products.Where(x => x.UserId == userId).ToListAsync();


            return channel.Writer.TryWrite((userId, products));


        }


    }
}
