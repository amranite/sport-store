using SportStore.Application.Common.Repositories;
using SportStore.Infrastructure.Data;

namespace SportStore.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IProductRepository? _productRepository;
    private ICategoryRepository? _categoryRepository;
    private IOrderRepository? _orderRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
    public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);
    public IOrderRepository Orders => _orderRepository ??= new OrderRepository(_context);

    public void Save()
    {
        _context.SaveChanges();
    }
}
