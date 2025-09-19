using Domain.Dtos.Email;

namespace Infrastructure.Interfaces;

public interface IEmailService
{
    Task SendAsync(SendEmailDto dto);
}



