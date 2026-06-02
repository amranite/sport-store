using SportStore.Application.Services.Models;

namespace SportStore.Application.Services;

public interface IShoppingCartService
{
    void AddToCart(int productId, int quantity);
    ShoppingCartSummary GetCart();
    void RemoveFromCart(int productId);
    void ClearCart();
}
