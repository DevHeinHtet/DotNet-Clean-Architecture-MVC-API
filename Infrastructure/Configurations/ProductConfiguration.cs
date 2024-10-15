using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(p =>
            {
                p.HasCheckConstraint("CK_Product_Price", $"{nameof(Product.Price)} > 0");
                p.HasCheckConstraint("CK_Product_Quantity", $"{nameof(Product.Quantity)} >= 0");
            });

            builder.HasKey(p => p.ID);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("NVARCHAR(255)");

            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(16, 4);

            builder.Property(p => p.Quantity)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
