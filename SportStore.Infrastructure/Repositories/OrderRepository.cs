using SportStore.Application.Common.Repositories;
using SportStore.Domain.Entities;
using SportStore.Infrastructure.Data;

namespace SportStore.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }
}
