using Domain.Entities;

namespace Application.DTOs;

public class DeliveryDetailDto
{
    public int Id { get; set; }
    public int DeliveryId { get; set; }
    public int ShopId { get; set; }
    public string ShopName { get; set; }
    public double QuantityDelivered { get; set; }
    public double PricePerKg { get; set; }
    public double TotalAmount { get; set; }
    public DateTime DeliveryTime { get; set; }
    public string Status { get; set; }
    public string? Notes { get; set; }
    public string? Signature { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateDeliveryDetailDto
{
    public int ShopId { get; set; }
    public double QuantityDelivered { get; set; }
    public double PricePerKg { get; set; }
    public string? Notes { get; set; }
}
