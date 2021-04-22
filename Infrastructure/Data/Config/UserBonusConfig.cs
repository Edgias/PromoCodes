using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheRoom.PromoCodes.ApplicationCore.Entities;

namespace TheRoom.PromoCodes.Infrastructure.Data.Config
{
    internal class UserBonusConfig : IEntityTypeConfiguration<UserBonus>
    {
        public void Configure(EntityTypeBuilder<UserBonus> builder)
        {
            // Create a composite key of UserId and ServiceId.
            builder.HasKey(ub => new { ub.UserId, ub.ServiceId });

            // Excluded the Id property from the base class since we created a composite key.
            builder.Ignore(ub => ub.Id);

            // Events should be excluded from the model.
            builder.Ignore(ub => ub.Events);
        }
    }
}
