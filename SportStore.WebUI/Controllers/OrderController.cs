using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application.Services;
using SportStore.Application.Services.Models;
using SportStore.WebUI.ViewModels;

namespace SportStore.WebUI.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (!_orderService.HasItemsInCart())
        {
            return RedirectToAction("Index", "Cart");
        }

        return View(new OrderFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(OrderFormViewModel viewModel)
    {
        if (!_orderService.HasItemsInCart())
        {
            return RedirectToAction("Index", "Cart");
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(customerId))
        {
            return Forbid();
        }

        _orderService.PlaceOrder(new PlaceOrderRequest
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Street = viewModel.Street,
            Number = viewModel.Number,
            Zip = viewModel.Zip,
            City = viewModel.City,
            CustomerId = customerId
        });

        return RedirectToAction(nameof(Confirmation));
    }

    [HttpGet]
    public IActionResult Confirmation()
    {
        return View();
    }
}
