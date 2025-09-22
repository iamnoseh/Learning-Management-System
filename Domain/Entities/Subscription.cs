using Domain.Enums;

namespace Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public PlanType PlanType { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
    public string? PaymentReference { get; set; }
}


