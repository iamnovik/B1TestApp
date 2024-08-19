using System.Collections.Generic;

namespace B1TestApp.Data.Entity;

public class Bank
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long FileId { get; set; }

    public Files File;
    
    public virtual IEnumerable<BankAccountData> BankAccounts { get; set; }
}