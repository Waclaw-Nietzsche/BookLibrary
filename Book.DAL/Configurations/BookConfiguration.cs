using Book.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.DAL.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            builder
                .HasKey(model => model.Id);

            builder
                .Property(model => model.Id)
                .UseIdentityColumn();

            builder
                .Property(model => model.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasOne(model => model.Author)
                .WithMany(artmodel => artmodel.Books)
                .HasForeignKey(model => model.AuthorId);

            builder
                .ToTable("Books");

        }
    }
}