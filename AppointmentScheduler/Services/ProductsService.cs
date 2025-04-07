using AppointmentScheduler.Models;
using Microsoft.EntityFrameworkCore;
using Optik.Models;

namespace Optik.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext _context;

        public ProductsService(ApplicationDbContext dbContext)
        {
            this._context=dbContext;
        }
        public IEnumerable<Product> GetSearchedProducts(int minPrice, int maxPrice, string? searchQuery)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p =>
                    p.Title.Contains(searchQuery) ||
                    p.Description.Contains(searchQuery));
            }

            if (maxPrice > 0)
            {
                products = products.Where(p => p.Price <= maxPrice);
            }

            if (minPrice > 0)
            {
                products = products.Where(p => p.Price >= minPrice);
            }
            return products.ToList();
        }
    }
}
