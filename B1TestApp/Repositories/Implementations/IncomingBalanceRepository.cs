using B1TestApp.Data;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Interfaces;

namespace B1TestApp.Repositories.Implementations;

public class IncomingBalanceRepository : BaseRepository<IncomingBalance, long>, IIncomingBalanceRepository
{
    public IncomingBalanceRepository(AppDbContext context) : base(context)
    {
    }
}