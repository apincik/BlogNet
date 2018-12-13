using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class WebContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleSettings> ArticleSettings { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<PageForward> PageForwards { get; set; }
        public DbSet<ProjectSettings> ProjectSettings { get; set; }
        public DbSet<Seo> Seo { get; set; }
        public DbSet<TemplateVariable> TemplateVariables { get; set; }

        private ITime _timeService;

        public WebContext(DbContextOptions<WebContext> options, ITime timeService) : base(options)
        {
            _timeService = timeService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        private void SaveAudit()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));
            DateTime now = _timeService.GetCurrentDateTime();

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }

                entity.UpdatedAt = now;
            }
        }

        public override int SaveChanges()
        {
            SaveAudit();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SaveAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
