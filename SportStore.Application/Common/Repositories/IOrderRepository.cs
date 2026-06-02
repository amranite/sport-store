using SportStore.Domain.Entities;

namespace SportStore.Application.Common.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
}
