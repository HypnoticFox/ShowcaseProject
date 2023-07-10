
namespace ShowcaseProject.Products.Application.Commands;

public sealed class SetDiscontinuedStatusCommand : ICommand
{
    public SetDiscontinuedStatusCommand() { }
    public SetDiscontinuedStatusCommand(int productId)
    {
        ProductId = productId.ThrowIfNull();
    }

    public int ProductId { get; set; }
}