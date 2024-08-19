using System;
using System.Threading;
using System.Threading.Tasks;
using B1TestApp.Data;
using B1TestApp.Repositories.Interfaces;

namespace B1TestApp.Repositories.Implementations;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private FileRepository? _FileRepository;

    public IFileRepository Files => _FileRepository ??= new FileRepository(context);
    
    private IncomingBalanceRepository? _IncomingBalanceRepository;

    public IIncomingBalanceRepository IncomingBalances =>
        _IncomingBalanceRepository ??= new IncomingBalanceRepository(context);

    public OutcomingBalanceRepository? _OutcomingBalanceRepository;

    public IOutcomingBalanceRepository OutcomingBalances =>
        _OutcomingBalanceRepository ??= new OutcomingBalanceRepository(context);

    private TurnoverRepository _TurnoverRepository;

    public ITurnOverRepository TurnOvers => _TurnoverRepository ??= new TurnoverRepository(context);

    private BankAccountDataRepository? _BankAccountRepository;

    public IBankAccountDataRepository BankAccountsData => _BankAccountRepository ??= new BankAccountDataRepository(context);

    private BankRepository? _BankRepository;

    public IBankRepository Banks => _BankRepository ??= new BankRepository(context);
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    private bool _disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }

            this._disposed = true;
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}