using GHotel.Application.Repositories;
using GHotel.Domain.Entities;
using GHotel.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace GHotel.Infrastructure.Repositories;

public class BusinessRepository : IBusinessRepository
{
    private readonly AppDBContext _context;

    public BusinessRepository(AppDBContext context) => _context = context;

    public async Task<List<Business>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Businesses!.Include(bu => bu.Image).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<Business?> GetByIdWithProjects(int id,CancellationToken cancellationToken)
    {
        return await _context.Businesses!
                            .Include(bu => bu.Image)
                            .Include(bu => bu.Projects)!
                                .ThenInclude(pr => pr.Images)
                            .SingleOrDefaultAsync(bu => bu.Id == id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Business?> GetByIdWithUser(int id,CancellationToken cancellation)
    {
        return await _context.Businesses!
                            .Include(bu => bu.User)
                            .SingleOrDefaultAsync(bu => bu.Id == id, cancellation).ConfigureAwait(false);
    }

    public async Task Update(Business business,CancellationToken cancellationToken)
    {
        _context.Businesses!.Update(business);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

}
