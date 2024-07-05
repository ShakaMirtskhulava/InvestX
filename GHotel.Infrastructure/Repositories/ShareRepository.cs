using GHotel.Application.Repositories;
using GHotel.Domain.Entities;
using GHotel.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace GHotel.Infrastructure.Repositories;

public class ShareRepository : IShareRepository
{
    private readonly AppDBContext _appDBContext;

    public ShareRepository(AppDBContext appDBContext)
    {
        _appDBContext = appDBContext;
    }

    public async Task<List<Share>> GetSharesForUserAndProject(string email,string projectName,CancellationToken cancellationToken)
    {
        var shares = await _appDBContext.Shares!
                                .Include(sh => sh.User)
                                .Where(sh => sh.ProjectName == projectName && sh.User!.Email == email)
                                .ToListAsync(cancellationToken)
                                .ConfigureAwait(false);
        return shares;
    }
}
