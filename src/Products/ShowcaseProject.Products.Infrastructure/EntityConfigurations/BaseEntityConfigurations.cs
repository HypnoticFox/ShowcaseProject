
using Microsoft.EntityFrameworkCore;
using ShowcaseProject.Products.Domain.SeedWork;
using System.Runtime.CompilerServices;

namespace ShowcaseProject.Products.Infrastructure.EntityConfigurations;

internal static class BaseEntityConfigurations
{
    public static ModelBuilder ApplyBaseEntityConfigurations(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
            {
                var entityConfiguration = modelBuilder.Entity(entityType.ClrType);

                entityConfiguration
                    .HasKey(nameof(Entity.Id));

                entityConfiguration
                    .Property(nameof(Entity.Id))
                    .ValueGeneratedOnAdd();

                entityConfiguration
                    .Ignore(nameof(Entity.DomainEvents));

                if (typeof(TimeStampedEntity).IsAssignableFrom(entityType.ClrType))
                {
                    entityConfiguration
                        .Property(nameof(TimeStampedEntity.TimeStamp))
                        .IsRowVersion();
                }
            }
        }

        return modelBuilder;
    }
}