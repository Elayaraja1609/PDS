using Application.DTOs;

namespace Application.Interfaces;

public interface IInventoryService
{
    Task<double> GetCurrentInventoryAsync();
    Task<IEnumerable<ChickenBatchDto>> GetAvailableBatchesAsync();
    Task<IEnumerable<ChickenBatchDto>> GetBatchesByFIFOAsync(double requiredQuantity);
    Task<bool> AllocateFromInventoryAsync(double quantity, int deliveryId);
    Task<double> GetInventoryValueAsync();
    Task<Dictionary<string, double>> GetInventoryBreakdownAsync();
    Task<IEnumerable<ChickenBatchDto>> GetExpiringBatchesAsync(int daysThreshold = 7);
}
