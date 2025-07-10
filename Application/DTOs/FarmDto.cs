using Domain.Entities;

namespace Application.DTOs;

public class FarmDto
{
	public int FramId { get; set; }
	public string Name { get; set; }
	public string Location { get; set; }
	public string ContactPerson { get; set; }
	public string PhoneNumber { get; set; }
	//public ICollection<FarmStockDto>? FarmStocks { get; set; }
}
