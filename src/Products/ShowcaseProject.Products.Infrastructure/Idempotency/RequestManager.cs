
using ShowcaseProject.Products.Domain.Exceptions;
using ShowcaseProject.Shared.Idempotency;

namespace ShowcaseProject.Products.Infrastructure.Idempotency;

public sealed class RequestManager : IRequestManager
{
    private readonly ProductsContext _context;

    public RequestManager(ProductsContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public async Task<bool> ExistAsync(Guid id)
    {
        var request = await _context.
            FindAsync<ClientRequest>(id);

        return request != null;
    }

    public async Task CreateRequestForCommandAsync<T>(Guid id)
    {
        var exists = await ExistAsync(id);

        if (exists) throw new ProductDomainException($"Request with {id} already exists");

        var request = new ClientRequest(id, typeof(T).Name, DateTime.UtcNow);

        _context.Add(request);

        await _context.SaveChangesAsync();
    }
}
