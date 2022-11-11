using Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Books
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Name)
                .HasMaxLength(255)
                .IsRequired();
            
            builder.Property(b => b.Description)
                .HasMaxLength(255)
                .IsRequired();
            
            builder.HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.Author)
                .OnDelete(DeleteBehavior.Restrict)
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
            
        }
    }
}