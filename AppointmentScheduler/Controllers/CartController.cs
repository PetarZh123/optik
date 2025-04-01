using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User);
        var cartItems = _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToList();
        return View(cartItems);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId)
    {
        var userId = _userManager.GetUserId(User);
        var cartItem = _context.CartItems
            .FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

        if (cartItem != null)
        {
            cartItem.Quantity++;
        }
        else
        {
            _context.CartItems.Add(new CartItem
            {
                ProductId = productId,
                UserId = userId,
                Quantity = 1
            });
        }
        
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int cartItemId)
    {
        var cartItem = _context.CartItems.Find(cartItemId);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
