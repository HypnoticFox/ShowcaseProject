
using ShowcaseProject.Products.Domain.AggregatesModel.ProductAggegrate;

namespace ShowcaseProject.Products.Application.Commands;

public sealed class AddToStockCommand : ICommand
{
    public AddToStockCommand() { }

    public AddToStockCommand(int productId, int amountToAdd)
    {
        ProductId = productId.ThrowIfNull();
        AmountToAdd = amountToAdd.ThrowIfNegative("AmountToAdd cannot be lower than 0.");
    }

    public int ProductId { get; set; }
    public int AmountToAdd { get; set; }
}