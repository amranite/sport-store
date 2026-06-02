using Microsoft.AspNetCore.Mvc;
using SportStore.Application.Services;
using SportStore.WebUI.ViewModels;

namespace SportStore.WebUI.Controllers;

public class CartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    public CartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public IActionResult Index()
    {
        var cart = _shoppingCartService.GetCart();
        var viewModel = new CartIndexViewModel
        {
            Lines = cart.Lines.Select(line => new CartLineViewModel
            {
                ProductId = line.ProductId,
                ProductName = line.ProductName,
                Price = line.Price,
                Quantity = line.Quantity
            }).ToList(),
            Total = cart.Total
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(int productId, int quantity)
    {
        _shoppingCartService.AddToCart(productId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int productId)
    {
        _shoppingCartService.RemoveFromCart(productId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        _shoppingCartService.ClearCart();
        return RedirectToAction(nameof(Index));
    }
}
