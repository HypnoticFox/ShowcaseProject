
namespace ShowcaseProject.Products.Application.Commands;

public sealed class SetUnavailableStatusCommand : ICommand
{
    public SetUnavailableStatusCommand() { }
    public SetUnavailableStatusCommand(int productId)
    {
        ProductId = productId.ThrowIfNull();
    }

    public int ProductId { get; set; }
}