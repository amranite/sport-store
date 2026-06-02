using Microsoft.AspNetCore.Http;
using SportStore.Application.Common.Repositories;
using SportStore.Domain.Entities;
using SportStore.Infrastructure.Extensions;

namespace SportStore.Infrastructure.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private const string ShoppingCartKey = "ShoppingCart";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ShoppingCart Get()
    {
        var session = GetSession();
        return session.GetObjectFromJson<ShoppingCart>(ShoppingCartKey) ?? new ShoppingCart();
    }

    public void Save(ShoppingCart shoppingCart)
    {
        var session = GetSession();
        session.SetObjectAsJson(ShoppingCartKey, shoppingCart);
    }

    public void Clear()
    {
        var session = GetSession();
        session.Remove(ShoppingCartKey);
    }

    private ISession GetSession()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("No active HttpContext is available.");

        return httpContext.Session;
    }
}
