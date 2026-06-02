using Microsoft.EntityFrameworkCore;
using SportStore.Application.Common.Repositories;
using SportStore.Domain.Entities;
using SportStore.Infrastructure.Data;

namespace SportStore.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products
            .Include(product => product.Category)
            .OrderBy(product => product.Name)
            .ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products
            .Include(product => product.Category)
            .FirstOrDefault(product => product.Id == id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }
}
