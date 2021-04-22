using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TheRoom.PromoCodes.Infrastructure.Identity
{
    public class PromoCodesIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public PromoCodesIdentityDbContext(DbContextOptions<PromoCodesIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(au =>
            {
                au.ToTable("Users");
            });

            builder.Entity<IdentityUserClaim<string>>(uc =>
            {
                uc.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(ul =>
            {
                ul.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserToken<string>>(ut =>
            {
                ut.ToTable("UserTokens");
            });

            builder.Entity<IdentityRole>(r =>
            {
                r.ToTable("Roles");
            });

            builder.Entity<IdentityRoleClaim<string>>(rc =>
            {
                rc.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserRole<string>>(ur =>
            {
                ur.ToTable("UserRoles");
            });
        }
    }
}
