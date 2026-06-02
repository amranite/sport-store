using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application.Common.Repositories;
using SportStore.WebUI.Models;
using SportStore.WebUI.ViewModels;

namespace SportStore.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var products = _unitOfWork.Products.GetAll()
            .Select(product => new ProductCatalogItemViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            })
            .ToList();

        return View(products);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
