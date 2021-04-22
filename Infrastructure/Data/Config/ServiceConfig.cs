using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheRoom.PromoCodes.ApplicationCore.Entities;

namespace TheRoom.PromoCodes.Infrastructure.Data.Config
{
    internal class ServiceConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.Description)
                .HasMaxLength(180)
                .IsRequired();

            // Added index to make service names unique
            builder.HasIndex(s => s.Description)
                .IsUnique();

            // Events should be excluded from the model
            builder.Ignore(s => s.Events);
        }
    }
}
