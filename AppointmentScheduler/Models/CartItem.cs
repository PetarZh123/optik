namespace AppointmentScheduler.Models;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string UserId { get; set; } //tied to the logged-in user
    public int Quantity { get; set; }
}