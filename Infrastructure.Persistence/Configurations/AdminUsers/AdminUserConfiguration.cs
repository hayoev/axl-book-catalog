using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.AdminUsers;
using Domain.Enums.AdminUsers;

namespace Infrastructure.Persistence.Configurations.AdminUsers
{
    public class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.Property(b => b.Username)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(b => b.Password)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(b => b.PasswordSalt)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(b => b.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.LastName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(b => b.MiddleName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.IsActive)
                .IsRequired();

            builder.Property(b => b.RefreshToken)
                .IsRequired(false);

            builder.HasOne(c => c.CreatedByAdminUser)
                .WithMany()
                .HasForeignKey(x => x.CreatedByAdminUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(c => c.UpdatedByAdminUser)
                .WithMany()
                .HasForeignKey(x => x.UpdatedByAdminUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasIndex(b => b.Email)
                .IsUnique();

            builder.HasIndex(b => b.Username)
                .IsUnique();

            Seed(builder);
        }

        private static void Seed(EntityTypeBuilder<AdminUser> builder)
        {
            var data = new List<AdminUser>()
            {
                new AdminUser()
                {
                    Id = AdminUserEnum.System.Value,
                    Email = "systemib@eskhata.tj",
                    Password = "123",
                    Username = "system",
                    FirstName = "system",
                    MiddleName = "system",
                    IsActive = true,
                    CreatedByAdminUserId = null
                },
                new AdminUser()
                {
                    Id = AdminUserEnum.Sadmin.Value,
                    Email = "adminib@eskhata.tj",
                    Password = "123",
                    Username = "admin",
                    FirstName = "admin",
                    MiddleName = "admin",
                    IsActive = true,
                    CreatedByAdminUserId = AdminUserEnum.System.Value
                }
            };

            builder.HasData(data);
        }
    }
}