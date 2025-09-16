using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    [Required]
    [StringLength(50, MinimumLength = 3,ErrorMessage = "First name must be between 3 and 50 characters")]
    public string FirstName { get; set; } = string.Empty;
    [StringLength(50, MinimumLength = 3,ErrorMessage = "Last name must be between 3 and 50 characters")]
    public string? LastName { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }  = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}