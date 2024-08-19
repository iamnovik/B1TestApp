namespace B1TestApp.Data.Entity;

public class Turnover
{
    public long Id { get; set; }
    
    public decimal Debit { get; set; }
    
    public decimal Credit { get; set; }
    
    public long BankAccountId { get; set; }
    
    public BankAccountData BankAccountData { get; set; }
}