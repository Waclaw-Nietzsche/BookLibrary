using Book.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.DAL.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorModel>
    {
        public void Configure(EntityTypeBuilder<AuthorModel> builder)
        {
            builder
                .HasKey(artmodel => artmodel.Id);

            builder
                .Property(model => model.Id)
                .UseIdentityColumn();

            builder
                .Property(model => model.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .ToTable("Authors");
        }
    }
}