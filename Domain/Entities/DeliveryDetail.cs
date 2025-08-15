namespace Domain.Entities;

public class DeliveryDetail : BaseEntity
{
    public int DeliveryId { get; set; }
    public Delivery Delivery { get; set; }
    public int ShopId { get; set; }
    public Shop Shop { get; set; }
    public double QuantityDelivered { get; set; }
    public double PricePerKg { get; set; }
    public double TotalAmount { get; set; }
    public DateTime DeliveryTime { get; set; }
    public string Status { get; set; } // Pending, Delivered, Cancelled
    public string? Notes { get; set; }
    public string? Signature { get; set; } // For delivery confirmation
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
