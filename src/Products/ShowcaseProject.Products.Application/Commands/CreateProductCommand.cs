
namespace ShowcaseProject.Products.Application.Commands;

public sealed class CreateProductCommand : ICommand<int>
{
    public CreateProductCommand() 
    {
        Name = "";
        Description= "";
    }
    public CreateProductCommand(string name, string description, int amountInStock = 0)
    {
        Name = name.ThrowIfNullOrEmpty("Name cannot be empty.");
        Description = description.ThrowIfNullOrEmpty("Description cannot be empty.");
        AmountInStock = amountInStock.ThrowIfNegative("Amount in stock cannot be lower than 0.");
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public int AmountInStock { get; set; }
}