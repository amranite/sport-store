using SportStore.Domain.Entities;

namespace SportStore.Application.Common.Repositories;

public interface IShoppingCartRepository
{
    ShoppingCart Get();
    void Save(ShoppingCart shoppingCart);
    void Clear();
}
