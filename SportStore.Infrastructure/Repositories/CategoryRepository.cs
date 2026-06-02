using SportStore.Application.Common.Repositories;
using SportStore.Domain.Entities;
using SportStore.Infrastructure.Data;

namespace SportStore.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> GetAll()
    {
        return _context.Categories.OrderBy(category => category.Name).ToList();
    }

    public Category? GetById(int id)
    {
        return _context.Categories.FirstOrDefault(category => category.Id == id);
    }
}
