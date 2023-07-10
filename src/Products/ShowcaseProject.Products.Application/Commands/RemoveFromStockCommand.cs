
namespace ShowcaseProject.Products.Application.Commands;

public sealed class RemoveFromStockCommand : ICommand
{
    public RemoveFromStockCommand() { }
    public RemoveFromStockCommand(int productId, int amountToRemove)
    {
        ProductId = productId.ThrowIfNull();
        AmountToRemove = amountToRemove.ThrowIfNegative("AmountToRemove cannot be lower than 0.");
    }

    public int ProductId { get; set; }
    public int AmountToRemove { get; set; }
}