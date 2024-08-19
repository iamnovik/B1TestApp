using B1TestApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestApp.Data.Configuration;

public class TurnoverConfiguration : IEntityTypeConfiguration<Turnover>
{
    public void Configure(EntityTypeBuilder<Turnover> builder)
    {
        builder.HasKey(t => t.Id);
    }
}