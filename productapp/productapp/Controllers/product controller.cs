using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;
using System.Collections.Generic;
using ProductApp.Models;   // use your project namespace

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            // Sample product list
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 75000 },
                new Product { Id = 2, Name = "Smartphone", Price = 30000 },
                new Product { Id = 3, Name = "Headphones", Price = 2000 }
            };

            // Pass the list to the view
            return View(products);
        }
    }
}
