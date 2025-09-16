namespace Domain.Dtos.Account;
using System.ComponentModel.DataAnnotations;


public class ChangePasswordDto
{
    [DataType(DataType.Password)] 
    public required string OldPassword { get; set; }
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}