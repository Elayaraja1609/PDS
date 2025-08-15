namespace Application.DTOs.ResponsesDto.cs;

public class DeliveryListResDto
{
	public int Id { get; set; }
	public DateTime DeliveryDate { get; set; }
	public double TotalWeightDelivered { get; set; }
	public string VehicleNo { get; set; }
	public string DriverName { get; set; }
}
