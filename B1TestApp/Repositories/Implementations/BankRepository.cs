using B1TestApp.Data;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Interfaces;

namespace B1TestApp.Repositories.Implementations;

public class BankRepository : BaseRepository<Bank, long>, IBankRepository
{
    public BankRepository(AppDbContext context) : base(context)
    {
    }
}