
using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;
using ShowcaseProject.Products.Domain.SeedWork;
using ShowcaseProject.Products.Infrastructure.EntityConfigurations;

namespace ShowcaseProject.Products.Infrastructure;

public sealed class ProductsContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "products";
    public DbSet<Product> Products { get; init; }
    public DbSet<ProductStatus> ProductStatus { get; init; }

    private readonly IDomainMediator _mediator;

    // Commands use the Repeatable Read Isolation Level.
    // Queries use the Snapshot Isolation Level.
    private IDbContextTransaction? _currentTransaction;
    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public ProductsContext(DbContextOptions<ProductsContext> options, IDomainMediator mediator) : base(options)
    {
        _mediator = mediator.ThrowIfNull();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyBaseEntityConfigurations();

        modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductStatusEntityTypeConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await _mediator.DispatchDomainEventsAsync(this);

        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction is not null) return _currentTransaction;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        transaction.ThrowIfNull();
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            if (ChangeTracker.HasChanges())
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}