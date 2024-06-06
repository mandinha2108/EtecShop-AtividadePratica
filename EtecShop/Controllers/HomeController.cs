using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EtecShop.Models;
using EtecShop.Data;
using EtecShop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EtecShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(
      ILogger<HomeController> logger,
      AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        HomeVM homeVM = new()
        {
            Categorias = _context.Categorias.ToList(),
            Produtos = _context.Produtos.Include(p => p.Categoria).ToList()
        };
        return View(homeVM);
    }
    public IActionResult Produto(int id)
    {
        Produto produto = _context.Produtos
          .Include(p => p.Categoria)
          .FirstOrDefault(p => p.Id == id);
        return View(produto);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
