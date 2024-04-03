using Microsoft.EntityFrameworkCore;

namespace UdemySampleProject.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;


        [Precision(18,2)] 
        public decimal Price { get; set; }
        public string Description { get; set; } = default!;

        public string UserId { get; set; } = default!;
    }
}
