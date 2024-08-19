using B1TestApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestApp.Data.Configuration;

public class FileConfiguration : IEntityTypeConfiguration<Files>
{
    public void Configure(EntityTypeBuilder<Files> builder)
    {
        builder.HasKey(f => f.Id);

        builder.HasMany(f => f.Banks)
            .WithOne(ba => ba.File)
            .HasForeignKey(ba => ba.FileId);
    }
}