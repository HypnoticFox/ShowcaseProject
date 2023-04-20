using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;
using ShowcaseProject.Products.Domain.Exceptions;

namespace ShowcaseProject.Products.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void AddStock_AddPositiveAmount_UpdatesAmountInStock()
    {
        // Arrange
        var product = new Product("Test Product", "A test product");

        // Act
        product.AddStock(10);

        // Assert
        Assert.Equal(10, product.AmountInStock);
    }

    [Fact]
    public void AddStock_AddNegativeAmount_ThrowsException()
    {
        // Arrange
        var product = new Product("Test Product", "A test product");

        // Act & Assert
        Assert.Throws<ProductDomainException>(() => product.AddStock(-10));
    }

    [Fact]
    public void AddStock_ProductIsDiscontinued_ThrowsException()
    {
        // Arrange
        var product = new Product("Test Product", "A test product");
        product.SetAvailableStatus();
        product.SetDiscontinuedStatus();

        // Act & Assert
        Assert.Throws<ProductDomainException>(() => product.AddStock(10));
    }

    [Fact]
    public void RemoveStock_RemovePositiveAmount_UpdatesAmountInStock()
    {
        // Arrange
        var product = new Product("Test Product", "A test product", 10);

        // Act
        product.RemoveStock(5);

        // Assert
        Assert.Equal(5, product.AmountInStock);
    }

    [Fact]
    public void RemoveStock_RemoveNegativeAmount_ThrowsException()
    {
        // Arrange
        var product = new Product("Test Product", "A test product", 10);

        // Act & Assert
        Assert.Throws<ProductDomainException>(() => product.RemoveStock(-5));
    }

    [Fact]
    public void RemoveStock_NotEnoughStock_ThrowsException()
    {
        // Arrange
        var product = new Product("Test Product", "A test product", 10);

        // Act & Assert
        Assert.Throws<ProductDomainException>(() => product.RemoveStock(20));
    }

    [Fact]
    public void RemoveStock_ProductIsDiscontinued_ThrowsException()
    {
        // Arrange
        var product = new Product("Test Product", "A test product", 10);
        product.SetAvailableStatus();
        product.SetDiscontinuedStatus();

        // Act & Assert
        Assert.Throws<ProductDomainException>(() => product.RemoveStock(5));
    }

    [Fact]
    public void SetAvailableStatus_ProductStatusChangesToAvailable()
    {
        // Arrange
        var product = new Product("Test Product", "A test product");

        // Act
        product.SetAvailableStatus();

        // Assert
        Assert.Equal(ProductStatus.Available, product.Status);
    }

    [Fact]
    public void SetAvailableStatus_InvalidStatusChange_ThrowsException()
    {
        // Arrange
        var product = new Product("Test Product", "A test product");
        product.SetUnavailableStatus();
        product.SetDiscontinuedStatus();

        // Act & Assert
        Assert.Throws<ProductDomainException>(() => product.SetAvailableStatus());
    }

    [Fact]
    public void SetUnavailableStatus_ProductStatusChangesToUnavailable()
    {
        // Arrange
        var product = new Product("Test Product", "A test product");
        product.SetAvailableStatus();

        // Act
        product.SetUnavailableStatus();

        // Assert
        Assert.Equal(ProductStatus.Unavailable, product.Status);
    }

    [Fact]
    public void SetUnavailableStatus_InvalidStatusChange_ThrowsException()
    {
        // Arrange
        var product = new Product("Test Product", "A test product");
        product.SetAvailableStatus();
        product.SetDiscontinuedStatus();

        // Act & Assert
        Assert.Throws<ProductDomainException>(() => product.SetUnavailableStatus());
    }

    [Fact]
    public void SetDiscontinuedStatus_SetsProductStatusToDiscontinued()
    {
        // Arrange
        var product = new Product("Test Product", "A test product description");
        product.SetAvailableStatus();

        // Act
        product.SetDiscontinuedStatus();

        // Assert
        Assert.Equal(ProductStatus.Discontinued, product.Status);
    }

    [Fact]
    public void SetNoLongerSoldStatus_InvalidStatusChange_ThrowsException()
    {
        // Arrange
        var product = new Product("Product A", "Description A", 10);

        // Act & Assert
        var ex = Assert.Throws<ProductDomainException>(() => product.SetDiscontinuedStatus());
    }
}
