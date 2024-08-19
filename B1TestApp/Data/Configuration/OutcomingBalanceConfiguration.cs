using B1TestApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestApp.Data.Configuration;

public class OutcomingBalanceConfiguration : IEntityTypeConfiguration<OutcomingBalance>
{
    public void Configure(EntityTypeBuilder<OutcomingBalance> builder)
    {
        builder.HasKey(ob => ob.Id);
    }
}