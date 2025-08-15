using Application.DTOs.ResponsesDto.cs;

namespace Application.DTOs;

public class DeliveryDetailsDto
{
	public int DeliveryId { get; set; }
	public DateTime? DeliveryDate { get; set; }
	public string? Area { get; set; }
	public double? TotalWeightLoaded { get; set; }
	public double? RemainingWeight { get; set; }
	public int? VehicleId { get; set; }
	public VehicleResDto? Vehicle { get; set; }
	public int? DriverId { get; set; }
	public DriverResDto? Driver { get; set; }
	public int? FarmStockId { get; set; }
	public FarmStockResDto? FarmStock { get; set; }

	public IList<OrderResDto>? Orders { get; set; }
	public IList<MiscExpenseDto>? MiscExpenses { get; set; }
	public IList<TransportCostDto>? TransportCosts { get; set; }
}
