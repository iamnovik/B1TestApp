using B1TestApp.Data;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Interfaces;

namespace B1TestApp.Repositories.Implementations;

public class TurnoverRepository : BaseRepository<Turnover,long>, ITurnOverRepository
{
    public TurnoverRepository(AppDbContext context) : base(context)
    {
    }
}