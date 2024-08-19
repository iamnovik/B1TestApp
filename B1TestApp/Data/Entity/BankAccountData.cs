using System;

namespace B1TestApp.Data.Entity;

public class BankAccountData
{
    public long Id;
    
    public long BankAccountNumber { get; set; }
    
    public long ReportYear { get; set; }

    public IncomingBalance IncomingBalance { get; set; }

    public OutcomingBalance OutcomingBalance { get; set; }

    public Turnover Turnover { get; set; }

    public long BankId { get; set; }

    public Bank Bank;
}