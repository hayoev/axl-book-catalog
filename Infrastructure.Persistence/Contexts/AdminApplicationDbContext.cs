using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Admin.Common.Interfaces;
using Domain.Common.BaseEntities;
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
        public AdminApplicationDbContext(DbContextOptions<AdminApplicationDbContext> options) : base(options)
        {
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
                        entry.Entity.CreatedByAdminUserId = AdminUserEnum.Sadmin.Value; //todo after authorization;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDateTime = DateTime.Now;
                        entry.Entity.UpdatedByAdminUserId = AdminUserEnum.Sadmin.Value; //todo after authorization
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}