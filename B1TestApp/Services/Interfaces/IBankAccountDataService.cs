using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using B1TestApp.Data.Entity;

namespace B1TestApp.Services.Interfaces;

public interface IBankAccountDataService
{
    Task<IEnumerable<BankAccountData>> GetBankAccountDataAsync(
        CancellationToken cancellationToken = default);
}