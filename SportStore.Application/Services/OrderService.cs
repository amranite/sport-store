using SportStore.Application.Common.Repositories;
using SportStore.Application.Services.Models;
using SportStore.Domain.Entities;

namespace SportStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IShoppingCartRepository shoppingCartRepository, IUnitOfWork unitOfWork)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _unitOfWork = unitOfWork;
    }

    public bool HasItemsInCart()
    {
        return _shoppingCartRepository.Get().CartLines.Count > 0;
    }

    public void PlaceOrder(PlaceOrderRequest request)
    {
        var shoppingCart = _shoppingCartRepository.Get();
        if (shoppingCart.CartLines.Count == 0)
        {
            return;
        }

        var order = new Order
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Street = request.Street,
            Number = request.Number,
            Zip = request.Zip,
            City = request.City,
            CustomerId = request.CustomerId,
            OrderDate = DateTime.UtcNow
        };

        foreach (var line in shoppingCart.CartLines)
        {
            var product = _unitOfWork.Products.GetById(line.ProductId);
            if (product is null)
            {
                continue;
            }

            order.OrderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = line.Quantity,
                UnitPrice = product.Price
            });
        }

        if (order.OrderItems.Count == 0)
        {
            return;
        }

        _unitOfWork.Orders.Add(order);
        _unitOfWork.Save();
        _shoppingCartRepository.Clear();
    }
}
