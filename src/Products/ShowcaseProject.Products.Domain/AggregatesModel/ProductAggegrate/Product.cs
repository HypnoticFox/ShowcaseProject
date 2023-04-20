
using ShowcaseProject.Products.Domain.Exceptions;
using ShowcaseProject.Shared.Extensions;

namespace ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;

public sealed class Product : TimeStampedEntity, IAggregateRoot
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int AmountInStock { get; private set; }
    public ProductStatus Status => ProductStatus.FromId(_productStatusId);
    private int _productStatusId = ProductStatus.Concept.Id;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Product(string name, string description)
    {
        Name = name.ThrowIfNullOrEmpty();
        Description = description.ThrowIfNullOrEmpty();
    }

    public Product(string name, string description, int amountInStock) : this(name, description)
    {
        AmountInStock = amountInStock.ThrowIfNegative();
    }

    private readonly Dictionary<int, int[]> _allowedFromStatuses = new()
    {
        {ProductStatus.Available.Id, new int[] { ProductStatus.Concept.Id, ProductStatus.Unavailable.Id } },
        {ProductStatus.Unavailable.Id, new int[] { ProductStatus.Concept.Id, ProductStatus.Available.Id } },
        {ProductStatus.Discontinued.Id, new int[] { ProductStatus.Available.Id, ProductStatus.Unavailable.Id } },
    };

    public void AddStock(int amountToAdd)
    {
        if (amountToAdd < 1) throw new ProductDomainException($"{nameof(amountToAdd)} has to be a positive integer.");
        if (_productStatusId == ProductStatus.Discontinued.Id) throw new ProductDomainException("The stock cannot be changed, because this product is no longer sold.");

        AmountInStock  += amountToAdd;
    }

    public void RemoveStock(int amountToRemove)
    {
        if (amountToRemove < 1) throw new ProductDomainException($"{nameof(amountToRemove)} has to be a positive integer.");
        if (_productStatusId == ProductStatus.Discontinued.Id) throw new ProductDomainException("The stock cannot be changed, because this product is no longer sold.");
        if (AmountInStock - amountToRemove < 0) throw new ProductDomainException($"Not enough stock! Amount currently in stock is {AmountInStock}, but amount to remove is {amountToRemove}.");
        
        AmountInStock -= amountToRemove;
    }

    public void SetAvailableStatus()
    {
        StatusChangeCheck(ProductStatus.Available);

        _productStatusId = ProductStatus.Available.Id;
    }

    public void SetUnavailableStatus()
    {
        StatusChangeCheck(ProductStatus.Unavailable);

        _productStatusId = ProductStatus.Unavailable.Id;
    }

    public void SetDiscontinuedStatus()
    {
        StatusChangeCheck(ProductStatus.Discontinued);

        _productStatusId = ProductStatus.Discontinued.Id;
    }

    private void StatusChangeCheck(ProductStatus newStatus)
    {
        var statusChangeAllowed = _allowedFromStatuses.GetValueOrDefault(newStatus.Id)?.Contains(_productStatusId) ?? false;
        if (!statusChangeAllowed)
        {
            StatusChangeException(newStatus);
        }
    }

    private void StatusChangeException(ProductStatus newStatus)
    {
        throw new ProductDomainException($"Is not possible to change the order status from {Status.Code} to {newStatus.Code}.");
    }
}
