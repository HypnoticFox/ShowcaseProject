
namespace ShowcaseProject.Products.Infrastructure.Idempotency;

internal sealed class ClientRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Time { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ClientRequest() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ClientRequest(Guid id, string name, DateTime time)
    {
        Id = id.ThrowIfNull();
        Name = name.ThrowIfNullOrEmpty();
        Time = time;
    }
}