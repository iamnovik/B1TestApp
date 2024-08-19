using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using B1TestApp.Data;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace B1TestApp.Repositories.Implementations;

public class BankAccountDataRepository : BaseRepository<BankAccountData, long>, IBankAccountDataRepository
{
    public BankAccountDataRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<BankAccountData>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var data = await _context.BankAccountsData
            .Include(bad => bad.IncomingBalance)
            .Include(bad => bad.OutcomingBalance)
            .Include(bad => bad.Turnover)
            .Include(bad => bad.Bank).ToListAsync(cancellationToken: cancellationToken);

        return data;
    }
}