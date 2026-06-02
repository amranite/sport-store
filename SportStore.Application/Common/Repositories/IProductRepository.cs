using SportStore.Domain.Entities;

namespace SportStore.Application.Common.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
}
