using B1TestApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestApp.Data.Configuration;

public class IncomingBalanceConfiguration : IEntityTypeConfiguration<IncomingBalance>
{
    public void Configure(EntityTypeBuilder<IncomingBalance> builder)
    {
        builder.HasKey(ib => ib.Id);
    }
}