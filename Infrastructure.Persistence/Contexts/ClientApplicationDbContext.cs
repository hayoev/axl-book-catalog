using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Client.Common.Interfaces;
using Domain.Common.BaseEntities;
using Domain.Common.SoftDeletes;
using Domain.Entities.Authors;
using Domain.Entities.Books;
using Domain.Entities.Categories;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts
{
    public class ClientApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public ClientApplicationDbContext(DbContextOptions<ClientApplicationDbContext> options,
            IAuthenticatedUserService authenticatedUserService) : base(options)
        {
            _authenticatedUserService = authenticatedUserService;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBookshelf> UserBookshelfs { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDateTime = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDateTime = DateTime.Now;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<ClientAuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDateTime = DateTime.Now;
                        entry.Entity.CreatedByUserId = _authenticatedUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDateTime = DateTime.Now;
                        entry.Entity.UpdatedByUserId = _authenticatedUserService.UserId;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<ISoftDeleted>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.DeletedDateTime = DateTime.Now;
                    entry.Entity.DeletedByUserId = _authenticatedUserService.UserId;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Author>()
                .HasQueryFilter(p => !p.DeletedDateTime.HasValue);

            modelBuilder.Entity<Book>()
                .HasQueryFilter(p => !p.DeletedDateTime.HasValue);
        }
    }
}