using AppointmentScheduler.Models;

namespace Optik.Services
{
    public interface IProductsService
    {
        public IEnumerable<Product> GetSearchedProducts(int minPrice, int maxPrice, string? searchQuery);
    }
}
