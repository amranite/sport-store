using SportStore.Application.Services.Models;

namespace SportStore.Application.Services;

public interface IOrderService
{
    bool HasItemsInCart();
    void PlaceOrder(PlaceOrderRequest request);
}
