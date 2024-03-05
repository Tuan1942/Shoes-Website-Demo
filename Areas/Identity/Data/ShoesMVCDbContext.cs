using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesMVC.Areas.Identity.Data;

namespace ShoesMVC.Areas.Identity.Data;

public class ShoesMVCDbContext : IdentityDbContext<ShoesMVCUser>
{
    public ShoesMVCDbContext(DbContextOptions<ShoesMVCDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ShoesMVCUserEntityConfiguration());
    }
}

public class ShoesMVCUserEntityConfiguration : IEntityTypeConfiguration<ShoesMVCUser>
{
    public void Configure(EntityTypeBuilder<ShoesMVCUser> builder)
    {
        builder.Property(u => u.firstName).HasMaxLength(255);
        builder.Property(u => u.lastName).HasMaxLength(255);
    }
}
