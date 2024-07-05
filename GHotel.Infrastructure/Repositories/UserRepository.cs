using GHotel.Application.Repositories;
using GHotel.Domain.Entities;
using GHotel.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace GHotel.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDBContext _context;

    public UserRepository(AppDBContext context) => _context = context;

    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        var result = await _context.Users!.AddAsync(user, cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return result.Entity;
    }

    public async Task<User?> GetByEmail(string email,CancellationToken cancellationToken)
    {
        return await _context.Users!.FirstOrDefaultAsync(x => x.Email == email, cancellationToken).ConfigureAwait(false);
    }

    public async Task<bool> Exists(string email,CancellationToken cancellationToken)
    {
        return await _context.Users!.AnyAsync(x => x.Email == email, cancellationToken).ConfigureAwait(false);
    }
}
