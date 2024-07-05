using GHotel.Domain.Entities;

namespace GHotel.Application.Repositories;

public interface IProjectRepository
{
    Task<Project> Create(Project project, CancellationToken cancellationToken);
    Task<Project?> GetByName(string name, CancellationToken cancellationToken);
    Task<bool> Exists(string name, CancellationToken cancellationToken);
    Task<Project> Update(Project project, CancellationToken cancellationToken);
}
