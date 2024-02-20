using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;

namespace ShowcaseProject.Products.Application.Tests;
public class CreateProductHandlerTests
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
}
