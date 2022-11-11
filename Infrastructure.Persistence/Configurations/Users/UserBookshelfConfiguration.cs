using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Users
{
    public class UserBookshelfConfiguration : IEntityTypeConfiguration<UserBookshelf>
    {
        public void Configure(EntityTypeBuilder<UserBookshelf> builder)
        {
            builder.HasOne(x => x.Book)
                .WithMany()
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            
            builder.HasOne(x => x.User)
                .WithMany(x=>x.Bookshelf)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            
            builder.HasOne(x => x.CreatedByUser)
                .WithMany()
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            
            builder.HasOne(x => x.UpdatedByUser)
                .WithMany()
                .HasForeignKey(x => x.UpdatedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
            
            builder.HasIndex(b => new {b.UserId, b.BookId})
                .IsUnique();
        }
    }
}