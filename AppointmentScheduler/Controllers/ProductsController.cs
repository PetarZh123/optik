using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Optik.Models;
using Optik.Models.ViewModels;
using Optik.Services;

namespace AppointmentScheduler.Controllers;

//ensures only authenticated users can access this controller
[Authorize]
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IProductsService productsService;

    public ProductsController(ApplicationDbContext context, IProductsService productsService)
    {
        _context = context;
        this.productsService=productsService;
    }

    //allows both Admin and Patient to access Index
    [Authorize(Roles = "Admin,Patient")]
    [HttpGet]
    public IActionResult Index()
    {
        ProductsViewModel productsViewModel = new ProductsViewModel();
        productsViewModel.Products = _context.Products.ToList();
        return View(productsViewModel);
    }

    [Authorize(Roles = "Admin,Patient")]
    [HttpPost]
    public IActionResult Index(ProductsViewModel productsViewModel)
    {
        //ProductsViewModel productsViewModel = new ProductsViewModel();
        //productsViewModel.Products = _context.Products.ToList();
        Console.WriteLine(productsViewModel.StartPrice);
        Console.WriteLine(productsViewModel.EndPrice);
        Console.WriteLine(productsViewModel.searchQuery);
        productsViewModel.Products = productsService.GetSearchedProducts(productsViewModel.StartPrice, productsViewModel.EndPrice, productsViewModel.searchQuery);
        return View(productsViewModel);
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