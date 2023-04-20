
using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;

namespace ShowcaseProject.Products.Infrastructure.EntityConfigurations;

internal sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> productConfiguration)
    {
        productConfiguration.ToTable("products", ProductsContext.DEFAULT_SCHEMA);

        productConfiguration
            .Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired(false);

        productConfiguration
            .Property(p => p.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        productConfiguration
            .Property(p => p.AmountInStock)
            .IsRequired();

        productConfiguration
            .Property<int>("_productStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ProductStatusId")
            .IsRequired();

        productConfiguration
            .HasOne<ProductStatus>()
            .WithMany()
            .HasForeignKey("_productStatusId");
    }
}