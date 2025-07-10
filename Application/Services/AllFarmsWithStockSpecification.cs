using Domain.Entities;

namespace Application.Services;

public class AllFarmsWithStockSpecification : BaseSpecification<Farm>
{
	public AllFarmsWithStockSpecification()
		: base(f => f.FarmStocks.Any(fs => fs.QuantityAvailableInKg > 0))
	{
		AddInclude(f => f.FarmStocks); // eager load FarmStocks
	}
}