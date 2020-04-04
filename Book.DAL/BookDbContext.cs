using Book.Core.Models;
using Book.DAL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Book.DAL
{
    public class BookDbContext : DbContext
    {
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }
        
        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new BookConfiguration());

            builder
                .ApplyConfiguration(new AuthorConfiguration());
        }
    }
}