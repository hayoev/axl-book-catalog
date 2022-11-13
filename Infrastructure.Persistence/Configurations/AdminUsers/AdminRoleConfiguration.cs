using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.AdminUsers;
using Domain.Enums.AdminUsers;

namespace Infrastructure.Persistence.Configurations.AdminUsers
{
    public class AdminRoleConfiguration : IEntityTypeConfiguration<AdminRole>
    {
        public void Configure(EntityTypeBuilder<AdminRole> builder)
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

            builder.HasIndex(b => b.Code)
                .IsUnique();

            Seed(builder);
        }

        private static void Seed(EntityTypeBuilder<AdminRole> builder)
        {
            var data = new List<AdminRole>()
            {
                new AdminRole()
                {
                    Id = AdminRoleEnum.Sadmin.Value,
                    Code = "SADMIN",
                    Name = "Администратор",
                    CreatedDateTime = new DateTime(2022, 11, 13, 8, 47, 31, 256, DateTimeKind.Local).AddTicks(7253),
                    CreatedByAdminUserId = AdminUserEnum.System.Value
                }
            };

            builder.HasData(data);
        }
    }
}