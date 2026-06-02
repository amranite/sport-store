namespace SportStore.Application.Services.Models;

public class ShoppingCartSummary
{
    public List<CartLineSummary> Lines { get; set; } = new();
    public decimal Total { get; set; }
}
