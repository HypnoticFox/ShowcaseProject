
using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;

namespace ShowcaseProject.Products.Infrastructure.EntityConfigurations;

internal sealed class ProductStatusEntityTypeConfiguration
    : IEntityTypeConfiguration<ProductStatus>
{
    public void Configure(EntityTypeBuilder<ProductStatus> productStatusConfiguration)
    {
        productStatusConfiguration.ToTable("productstatus", ProductsContext.DEFAULT_SCHEMA);

        productStatusConfiguration.HasKey(o => o.Id);

        productStatusConfiguration.HasIndex(o => o.Code)
            .IsUnique();

        productStatusConfiguration.Property(o => o.Id)
            .ValueGeneratedNever()
            .IsRequired();

        productStatusConfiguration.Property(o => o.Code)
            .HasMaxLength(200)
            .IsRequired();

        productStatusConfiguration.Property(o => o.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}