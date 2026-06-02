namespace SportStore.Domain.Entities;

public class ShoppingCart
{
    public List<CartLine> CartLines { get; set; } = new();

    public void AddCartLine(int productId, int quantity)
    {
        var existingLine = CartLines.FirstOrDefault(line => line.ProductId == productId);
        if (existingLine is null)
        {
            CartLines.Add(new CartLine
            {
                ProductId = productId,
                Quantity = quantity
            });

            return;
        }

        existingLine.Quantity += quantity;
    }

    public void RemoveCartLine(int productId)
    {
        var existingLine = CartLines.FirstOrDefault(line => line.ProductId == productId);
        if (existingLine is not null)
        {
            CartLines.Remove(existingLine);
        }
    }

    public void Clear()
    {
        CartLines.Clear();
    }
}
