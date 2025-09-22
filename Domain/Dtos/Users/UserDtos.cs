using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dtos.Users;

public class UserCreateDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 3)]
    public string? LastName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    public string? PhoneNumber { get; set; }

    [Required]
    public DateTime Birthday { get; set; }

    public Gender Gender { get; set; }

    public string Address { get; set; } = string.Empty;

    public string? Role { get; set; } = "Student";
}

public class UserUpdateDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 3)]
    public string? LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    public string? PhoneNumber { get; set; }

    public DateTime Birthday { get; set; }

    public Gender Gender { get; set; }

    public string Address { get; set; } = string.Empty;
}

public class UserResultDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateOnly Birthday { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}


