using Domain.Enums;

namespace Domain.Entities;

public class Subscription : BaseEntities
{
    public PlanType  PlanType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public int PaymentReference { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}