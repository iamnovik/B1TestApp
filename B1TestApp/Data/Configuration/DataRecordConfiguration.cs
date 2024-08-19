using B1TestApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B1TestApp.Data.Configuration
{
    public class DataRecordConfiguration : IEntityTypeConfiguration<DataRecord>
    {
        public void Configure(EntityTypeBuilder<DataRecord> builder)
        {
            builder.HasKey(dr => dr.Id);
        }
    }
}