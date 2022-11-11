using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.AdminUsers;
using Domain.Entities.Authors;
using Domain.Entities.Books;
using Domain.Entities.Categories;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Admin.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<BookCategory> BookCategories { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<AdminUser> AdminUsers { get; set; }
        DbSet<AdminUserAdminRole> AdminUserAdminRoles { get; set; }
        DbSet<AdminRole> AdminRoles { get; set; }
        DbSet<AdminRoleAdminPermission> AdminRoleAdminPermissions { get; set; }
        DbSet<AdminPermission> AdminPermissions { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserBookshelf> UserBookshelfs { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade Database { get; }
    }
}