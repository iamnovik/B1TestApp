using B1TestApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestApp.Data.Configuration;

public class BankConfiguration : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.HasMany(f => f.BankAccounts)
            .WithOne(ba => ba.Bank)
            .HasForeignKey(ba => ba.BankId);
    }
}