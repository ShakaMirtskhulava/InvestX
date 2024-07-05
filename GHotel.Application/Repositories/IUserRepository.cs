using GHotel.Domain.Entities;

namespace GHotel.Application.Repositories;

public interface IUserRepository
{
    Task<User> Create(User user, CancellationToken cancellationToken);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<bool> Exists(string email, CancellationToken cancellationToken);
}
