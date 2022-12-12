using Microsoft.EntityFrameworkCore;
using Whs.Application.Interfaces;
using Whs.Domain;
using Whs.Persistence.EntityTypeConfigurations;

namespace Whs.Persistence
{
    public class WhsDbContext : DbContext, IWhsDbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Product> Products { get; set; }

        public WhsDbContext(DbContextOptions<WhsDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NoteTypeConfiguration());
            builder.ApplyConfiguration(new ProductTypeConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
