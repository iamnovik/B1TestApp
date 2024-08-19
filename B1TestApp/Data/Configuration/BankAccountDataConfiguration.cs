using B1TestApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestApp.Data.Configuration;

public class BankAccountDataConfiguration : IEntityTypeConfiguration<BankAccountData>
{
    public void Configure(EntityTypeBuilder<BankAccountData> builder)
    {
        builder.HasKey(ba => ba.Id);
        
        builder.HasOne(b => b.IncomingBalance)
            .WithOne(i => i.BankAccountData)
            .HasForeignKey<IncomingBalance>(i => i.BankAccountId);
        
        builder.HasOne(b => b.OutcomingBalance)
            .WithOne(i => i.BankAccountData)
            .HasForeignKey<OutcomingBalance>(i => i.BankAccountId);
        
        builder.HasOne(b => b.Turnover)
            .WithOne(i => i.BankAccountData)
            .HasForeignKey<Turnover>(i => i.BankAccountId);
        
    }
}