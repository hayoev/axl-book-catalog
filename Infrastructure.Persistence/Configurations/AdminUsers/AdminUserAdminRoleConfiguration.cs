using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.AdminUsers;
using Domain.Enums.AdminUsers;

namespace Infrastructure.Persistence.Configurations.AdminUsers
{
    public class AdminUserAdminRoleConfiguration : IEntityTypeConfiguration<AdminUserAdminRole>
    {
        public void Configure(EntityTypeBuilder<AdminUserAdminRole> builder)
        {
            builder.HasKey(bc => new {bc.AdminUserId, bc.AdminRoleId});

            builder.HasOne(bc => bc.AdminUser)
                .WithMany(bc => bc.AdminUserAdminRoles)
                .HasForeignKey(bc => bc.AdminUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bc => bc.AdminRole)
                .WithMany(bc => bc.AdminUserAdminRoles)
                .HasForeignKey(bc => bc.AdminRoleId)
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

            Seed(builder);
        }

        private static void Seed(EntityTypeBuilder<AdminUserAdminRole> builder)
        {
            var data = new List<AdminUserAdminRole>()
            {
                new AdminUserAdminRole()
                {
                    Id = Guid.Parse("0e4ce9f0-95d1-11eb-9f9c-244bfee059a7"),
                    AdminUserId = AdminUserEnum.Sadmin.Value,
                    AdminRoleId = AdminRoleEnum.Sadmin.Value,
                    CreatedDateTime = DateTime.Now,
                    CreatedByAdminUserId = AdminUserEnum.System.Value
                }
            };

            builder.HasData(data);
        }
    }
}