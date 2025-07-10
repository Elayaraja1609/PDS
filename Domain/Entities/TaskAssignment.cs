namespace Domain.Entities;

public class TaskAssignment:BaseEntity
{
	public Guid LaborId { get; set; }
	public required Labor Labor { get; set; }

	public string? TaskDescription { get; set; }
	public DateTime AssignedAt { get; set; }
	public bool IsCompleted { get; set; }
}
