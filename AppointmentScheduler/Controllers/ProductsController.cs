using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Optik.Models;

namespace AppointmentScheduler.Controllers;

//ensures only authenticated users can access this controller
[Authorize]
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    //allows both Admin and Patient to access Index
    [Authorize(Roles = "Admin,Patient")]
    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products);
    }

    //restricts create to Admins only
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    //restricts create to Admins only
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(product);
    }
}