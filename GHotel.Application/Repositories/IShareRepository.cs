using GHotel.Domain.Entities;

namespace GHotel.Application.Repositories;

public interface IShareRepository
{
    Task<List<Share>> GetSharesForUserAndProject(string email, string projectName, CancellationToken cancellationToken);
}
