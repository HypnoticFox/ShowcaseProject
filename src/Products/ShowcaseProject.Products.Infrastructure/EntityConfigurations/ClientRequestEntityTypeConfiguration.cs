
using ShowcaseProject.Products.Infrastructure.Idempotency;

namespace ShowcaseProject.Products.Infrastructure.EntityConfigurations;

internal sealed class ClientRequestEntityTypeConfiguration
    : IEntityTypeConfiguration<ClientRequest>
{
    public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
    {
        requestConfiguration.ToTable("requests", ProductsContext.DEFAULT_SCHEMA);
        requestConfiguration.HasKey(cr => cr.Id);
        requestConfiguration.Property(cr => cr.Name).IsRequired();
        requestConfiguration.Property(cr => cr.Time).IsRequired();
    }
}