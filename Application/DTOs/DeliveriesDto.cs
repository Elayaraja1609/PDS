namespace Application.DTOs;

public class DeliveriesDto
{
	public int id { get; set; }
	public DateTime deliverydate { get; set; }
	public string area { get; set; }
	public double totalweightloaded { get; set; }
	public double remainingweight { get; set; }
	public int vehicleid { get; set; }
	public int driverid { get; set; }
	public int farmstockid { get; set; }
}
