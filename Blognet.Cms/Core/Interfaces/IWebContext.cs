using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Blognet.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blognet.Cms.Core.Interfaces
{
    public interface IWebContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Album> Albums { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<Article> Articles { get; set; }
        DbSet<ArticleSettings> ArticleSettings { get; set; }
        DbSet<MenuItem> MenuItems { get; set; }
        DbSet<PageForward> PageForwards { get; set; }
        DbSet<ProjectSettings> ProjectSettings { get; set; }
        DbSet<Seo> Seo { get; set; }
        DbSet<TemplateVariable> TemplateVariables { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<IDbContextTransaction> BeginTransactionAsync();

        EntityEntry Entry(object entity);
    }
}
