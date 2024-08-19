using B1TestApp.Data;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Interfaces;

namespace B1TestApp.Repositories.Implementations;

public class OutcomingBalanceRepository : BaseRepository<OutcomingBalance, long>, IOutcomingBalanceRepository
{
    public OutcomingBalanceRepository(AppDbContext context) : base(context)
    {
    }
}