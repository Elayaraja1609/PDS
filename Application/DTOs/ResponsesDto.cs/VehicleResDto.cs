namespace Application.DTOs.ResponsesDto.cs;

public class VehicleResDto
{
	public int Id { get; set; }
	public required string VehicleNumber { get; set; }
	public string Type { get; set; } // Small Van, Truck, etc.
	public double CapacityInKg { get; set; }

}
