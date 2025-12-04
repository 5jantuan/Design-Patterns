using Pipeline.Interfaces;

namespace Pipeline.Contexts;

public sealed class OrderContext : ICanStop
{
    public string OrderId { get; set; } = "";
    public decimal Amount { get; set; }
    public bool IsDone { get; set; }
}
