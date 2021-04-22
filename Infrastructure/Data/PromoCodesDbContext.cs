using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using TheRoom.PromoCodes.ApplicationCore.SharedKernel;

namespace TheRoom.PromoCodes.Infrastructure.Data
{
    public class PromoCodesDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public PromoCodesDbContext(DbContextOptions<PromoCodesDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Service> Services { get; set; }

        public DbSet<UserBonus> UserBonuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_mediator == null) return result;

            // dispatch events only if save was successful
            BaseEntity[] entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (BaseEntity entity in entitiesWithEvents)
            {
                BaseDomainEvent[] events = entity.Events.ToArray();
                entity.Events.Clear();

                foreach (BaseDomainEvent domainEvent in events)
                {
                    await _mediator.Publish(domainEvent).ConfigureAwait(false);
                }
            }

            return result;

        }
    }
}
