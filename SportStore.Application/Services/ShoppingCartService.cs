using SportStore.Application.Common.Repositories;
using SportStore.Application.Services.Models;

namespace SportStore.Application.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IUnitOfWork unitOfWork)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _unitOfWork = unitOfWork;
    }

    public void AddToCart(int productId, int quantity)
    {
        if (quantity < 1 || _unitOfWork.Products.GetById(productId) is null)
        {
            return;
        }

        var shoppingCart = _shoppingCartRepository.Get();
        shoppingCart.AddCartLine(productId, quantity);
        _shoppingCartRepository.Save(shoppingCart);
    }

    public ShoppingCartSummary GetCart()
    {
        var shoppingCart = _shoppingCartRepository.Get();

        var lines = shoppingCart.CartLines
            .Select(line =>
            {
                var product = _unitOfWork.Products.GetById(line.ProductId);
                if (product is null)
                {
                    return null;
                }

                return new CartLineSummary
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = line.Quantity
                };
            })
            .Where(line => line is not null)
            .Cast<CartLineSummary>()
            .ToList();

        return new ShoppingCartSummary
        {
            Lines = lines,
            Total = lines.Sum(line => line.Subtotal)
        };
    }

    public void RemoveFromCart(int productId)
    {
        var shoppingCart = _shoppingCartRepository.Get();
        shoppingCart.RemoveCartLine(productId);
        _shoppingCartRepository.Save(shoppingCart);
    }

    public void ClearCart()
    {
        _shoppingCartRepository.Clear();
    }
}
