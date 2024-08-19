using System.Threading;
using System.Threading.Tasks;

namespace B1TestApp.Repositories.Interfaces;

public interface IUnitOfWork
{
    IFileRepository Files { get; }
    
    IBankAccountDataRepository BankAccountsData { get; }
    
    IIncomingBalanceRepository IncomingBalances { get; }
    
    IOutcomingBalanceRepository OutcomingBalances { get; }
    
    ITurnOverRepository TurnOvers { get; }
    
    IBankRepository Banks { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}