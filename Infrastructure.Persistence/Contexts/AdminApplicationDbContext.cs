using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Interfaces;
using Domain.Common.BaseEntities;
using Domain.Common.SoftDeletes;
using Domain.Entities.AdminUsers;
using Domain.Entities.Authors;
using Domain.Entities.Books;
using Domain.Entities.Categories;
using Domain.Entities.Users;
using Domain.Enums.AdminUsers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts
{
    public class AdminApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public AdminApplicationDbContext(DbContextOptions<AdminApplicationDbContext> options,
            IAuthenticatedUserService authenticatedUserService) : base(options)
        {
            _authenticatedUserService = authenticatedUserService;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AdminUserAdminRole> AdminUserAdminRoles { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<AdminRoleAdminPermission> AdminRoleAdminPermissions { get; set; }
        public DbSet<AdminPermission> AdminPermissions { get; set; }
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

            foreach (var entry in ChangeTracker.Entries<AdminAuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDateTime = DateTime.Now;
                        entry.Entity.CreatedByAdminUserId = _authenticatedUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDateTime = DateTime.Now;
                        entry.Entity.UpdatedByAdminUserId = _authenticatedUserService.UserId;
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