
namespace ShowcaseProject.Products.Application.Commands;

public sealed class SetAvailableStatusCommand : ICommand
{
    public SetAvailableStatusCommand() { }
    public SetAvailableStatusCommand(int productId)
    {
        ProductId = productId.ThrowIfNull();
    }

    public int ProductId { get; set; }
}