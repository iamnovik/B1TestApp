namespace B1TestApp.Data.Entity;

public class IncomingBalance
{
    public long Id { get; set; }
    
    public decimal Assets { get; set; }
    
    public decimal Liabilities { get; set; }
    
    public long BankAccountId { get; set; }
    
    public BankAccountData BankAccountData { get; set; }
}