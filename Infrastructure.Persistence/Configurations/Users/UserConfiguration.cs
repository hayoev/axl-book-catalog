using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.Username)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.MiddleName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(b => b.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(b => b.Password)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(b => b.PasswordSalt)
                .HasMaxLength(255)
                .IsRequired(false);


            builder.Property(b => b.LastLoginDateTime)
                .IsRequired(false);


            builder.Property(b => b.IsActive)
                .IsRequired();

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

            builder.HasIndex(b => b.Email)
                .IsUnique();

            builder.HasIndex(b => b.Username)
                .IsUnique();
        }
    }
}