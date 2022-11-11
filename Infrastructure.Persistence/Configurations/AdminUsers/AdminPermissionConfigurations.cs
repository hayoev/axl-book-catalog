using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.AdminUsers;

namespace Infrastructure.Persistence.Configurations.AdminUsers
{
    public class AdminPermissionConfigurations : IEntityTypeConfiguration<AdminPermission>
    {
        public void Configure(EntityTypeBuilder<AdminPermission> builder)
        {
            builder.Property(b => b.Code)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(b => b.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.Description)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.HasIndex(b => b.Code)
                .IsUnique();
            
        }
    }
}
