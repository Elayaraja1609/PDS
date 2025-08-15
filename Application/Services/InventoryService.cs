using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class InventoryService : IInventoryService
{
    private readonly IGenericRepo<ChickenBatch> _batchRepo;
    private readonly IGenericRepo<Delivery> _deliveryRepo;

    public InventoryService(
        IGenericRepo<ChickenBatch> batchRepo,
        IGenericRepo<Delivery> deliveryRepo)
    {
        _batchRepo = batchRepo;
        _deliveryRepo = deliveryRepo;
    }

    public async Task<double> GetCurrentInventoryAsync()
    {
        var activeBatches = await _batchRepo.GetAsync(x => x.IsActive && x.Status == "InStock");
        return activeBatches.Sum(x => x.RemainingQuantityInKg);
    }

    public async Task<IEnumerable<ChickenBatchDto>> GetAvailableBatchesAsync()
    {
        var activeBatches = await _batchRepo.GetAsync(x => x.IsActive && x.Status == "InStock" && x.RemainingQuantityInKg > 0);
        var dtos = new List<ChickenBatchDto>();
        
        foreach (var batch in activeBatches)
        {
            dtos.Add(await MapToDto(batch));
        }
        
        return dtos.OrderBy(x => x.CreatedAt); // FIFO order
    }

    public async Task<IEnumerable<ChickenBatchDto>> GetBatchesByFIFOAsync(double requiredQuantity)
    {
        var availableBatches = await GetAvailableBatchesAsync();
        var selectedBatches = new List<ChickenBatchDto>();
        var remainingQuantity = requiredQuantity;

        foreach (var batch in availableBatches)
        {
            if (remainingQuantity <= 0) break;

            var quantityToTake = Math.Min(remainingQuantity, batch.RemainingQuantityInKg);
            selectedBatches.Add(batch);
            remainingQuantity -= quantityToTake;
        }

        return selectedBatches;
    }

    public async Task<bool> AllocateFromInventoryAsync(double quantity, int deliveryId)
    {
        var availableBatches = await _batchRepo.GetAsync(x => x.IsActive && x.Status == "InStock" && x.RemainingQuantityInKg > 0);
        var orderedBatches = availableBatches.OrderBy(x => x.CreatedAt).ToList();
        
        var remainingQuantity = quantity;
        var allocatedBatches = new List<ChickenBatch>();

        foreach (var batch in orderedBatches)
        {
            if (remainingQuantity <= 0) break;

            var quantityToTake = Math.Min(remainingQuantity, batch.RemainingQuantityInKg);
            batch.RemainingQuantityInKg -= quantityToTake;
            remainingQuantity -= quantityToTake;

            if (batch.RemainingQuantityInKg <= 0)
            {
                batch.Status = "Dispatched";
                batch.IsActive = false;
            }

            allocatedBatches.Add(batch);
        }

        if (remainingQuantity > 0)
        {
            return false; // Insufficient inventory
        }

        // Update all allocated batches
        foreach (var batch in allocatedBatches)
        {
            await _batchRepo.UpdateAsync(batch);
        }

        return true;
    }

    public async Task<double> GetInventoryValueAsync()
    {
        var activeBatches = await _batchRepo.GetAsync(x => x.IsActive && x.Status == "InStock");
        // This would need to be enhanced with actual pricing logic
        return activeBatches.Sum(x => x.RemainingQuantityInKg);
    }

    public async Task<Dictionary<string, double>> GetInventoryBreakdownAsync()
    {
        var activeBatches = await _batchRepo.GetAsync(x => x.IsActive && x.Status == "InStock");
        
        return activeBatches
            .GroupBy(x => x.Type)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.RemainingQuantityInKg));
    }

    public async Task<IEnumerable<ChickenBatchDto>> GetExpiringBatchesAsync(int daysThreshold = 7)
    {
        var thresholdDate = DateTime.UtcNow.AddDays(daysThreshold);
        var expiringBatches = await _batchRepo.GetAsync(x => 
            x.IsActive && 
            x.Status == "InStock" && 
            x.CollectionDate.AddDays(30) <= thresholdDate); // Assuming 30-day shelf life
        
        var dtos = new List<ChickenBatchDto>();
        
        foreach (var batch in expiringBatches)
        {
            dtos.Add(await MapToDto(batch));
        }
        
        return dtos;
    }

    private async Task<ChickenBatchDto> MapToDto(ChickenBatch batch)
    {
        return new ChickenBatchDto
        {
            Id = batch.Id,
            CollectionDate = batch.CollectionDate,
            NumberOfChickens = batch.NumberOfChickens,
            QuantityInKg = batch.QuantityInKg,
            RemainingQuantityInKg = batch.RemainingQuantityInKg,
            Type = batch.Type,
            Notes = batch.Notes,
            Status = batch.Status,
            FarmerId = batch.FarmerId,
            FarmerName = batch.FarmerName,
            CollectionWeight = batch.CollectionWeight,
            CreatedAt = batch.CreatedAt,
            IsActive = batch.IsActive
        };
    }
}
