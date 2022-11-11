using Domain.Entities.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Authors
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(b => b.Fullname)
                .HasMaxLength(255)
                .IsRequired();
            
            builder.Property(b => b.Bio)
                .HasMaxLength(1000)
                .IsRequired(false);
            
            builder.Property(b => b.BirthDate)
                .IsRequired(false);
            
            builder.Property(b => b.DeathDate)
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
        }
    }
}