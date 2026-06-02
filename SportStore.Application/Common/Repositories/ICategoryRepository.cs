using SportStore.Domain.Entities;

namespace SportStore.Application.Common.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    Category? GetById(int id);
}
