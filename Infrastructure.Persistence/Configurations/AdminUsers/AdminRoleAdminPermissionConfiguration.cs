using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.AdminUsers;

namespace Infrastructure.Persistence.Configurations.AdminUsers
{
    public class AdminRoleAdminPermissionConfiguration : IEntityTypeConfiguration<AdminRoleAdminPermission>
    {
        public void Configure(EntityTypeBuilder<AdminRoleAdminPermission> builder)
        {
            builder.HasKey(bc => new {bc.AdminRoleId, bc.AdminPermissionId});

            builder.HasOne(bc => bc.AdminRole)
                .WithMany(bc => bc.AdminRoleAdminPermissions)
                .HasForeignKey(bc => bc.AdminRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bc => bc.AdminPermission)
                .WithMany(bc => bc.AdminRoleAdminPermissions)
                .HasForeignKey(bc => bc.AdminPermissionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CreatedByAdminUser)
                .WithMany()
                .HasForeignKey(x => x.CreatedByAdminUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.UpdatedByAdminUser)
                .WithMany()
                .HasForeignKey(x => x.UpdatedByAdminUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}