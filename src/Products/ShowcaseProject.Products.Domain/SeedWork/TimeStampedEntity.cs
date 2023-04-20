
namespace ShowcaseProject.Products.Domain.SeedWork;

public abstract class TimeStampedEntity : Entity
{
    public byte[]? TimeStamp { get; }
}
