using GHotel.Domain.Entities;

namespace GHotel.Application.Repositories;

public interface IBusinessRepository
{
    Task<List<Business>> GetAll(CancellationToken cancellationToken);
    Task<Business?> GetByIdWithProjects(int id, CancellationToken cancellationToken);
    Task<Business?> GetByIdWithUser(int id, CancellationToken cancellation);
    Task Update(Business business, CancellationToken cancellationToken);
}
