using System.Net.Http.Headers;
using GHotel.Application.Repositories;
using GHotel.Domain.Entities;
using GHotel.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace GHotel.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDBContext _context;

    public ProjectRepository(AppDBContext context) => _context = context;

    public async Task<Project> Create(Project project, CancellationToken cancellationToken)
    {
        var result = await _context.Projects!.AddAsync(project,cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return result.Entity;
    }

    public async Task<Project> Update(Project project,CancellationToken cancellationToken)
    {
        var result = _context.Projects!.Update(project);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return result.Entity;
    }

    public async Task<Project?> GetByName(string name,CancellationToken cancellationToken)
    {
        return await _context.Projects!
                                .Include(pr => pr.Images)
                                .Include(pr => pr.Business)
                                    .ThenInclude(bu => bu.User)
                                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken).ConfigureAwait(false);
    }

    public async Task<bool> Exists(string name, CancellationToken cancellationToken)
    {
        return await _context.Projects!.AnyAsync(x => x.Name == name,cancellationToken).ConfigureAwait(false);
    }

}
