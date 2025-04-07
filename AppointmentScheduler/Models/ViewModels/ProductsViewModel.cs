using AppointmentScheduler.Models;
using System.ComponentModel.DataAnnotations;

namespace Optik.Models.ViewModels
{
    public class ProductsViewModel
    {
        public int StartPrice { get; set; }

        public int EndPrice { get; set; }

        public string searchQuery { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
