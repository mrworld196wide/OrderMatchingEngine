
namespace OrderMatchingEngine.Models;

public class Position
{
    public int NetQuantity { get; set; }
    public decimal AvgPrice { get; set; }
    public decimal RealizedPnL { get; set; }
    public decimal UnrealizedPnL { get; set; }
}