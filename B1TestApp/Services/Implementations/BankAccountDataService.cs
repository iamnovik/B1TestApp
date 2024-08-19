using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Interfaces;
using B1TestApp.Services.Interfaces;

namespace B1TestApp.Services.Implementations;

public class BankAccountDataService(IUnitOfWork unitOfWork) : IBankAccountDataService
{
    public async Task<IEnumerable<BankAccountData>> GetBankAccountDataAsync(CancellationToken cancellationToken = default)
    {
        var data = await unitOfWork.BankAccountsData.GetAllAsync(cancellationToken);

        return data;
    }
}