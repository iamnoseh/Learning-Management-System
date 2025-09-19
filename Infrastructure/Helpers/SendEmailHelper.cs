using Domain.Dtos.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastructure.Helpers;

public static class SendEmailHelper
{
    public static async Task SendEmail(SendEmailDto emailDto, EmailSettings settings)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(settings.FromName, settings.FromEmail));
        message.To.Add(MailboxAddress.Parse(emailDto.To));
        message.Subject = emailDto.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = emailDto.Body
        };
        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            if (settings.UseSsl)
            {
                await client.ConnectAsync(settings.Host, settings.Port, SecureSocketOptions.SslOnConnect);
            }
            else if (settings.UseStartTls)
            {
                await client.ConnectAsync(settings.Host, settings.Port, SecureSocketOptions.StartTls);
            }
            else
            {
                await client.ConnectAsync(settings.Host, settings.Port, SecureSocketOptions.None);
            }

            if (!string.IsNullOrWhiteSpace(settings.Username))
            {
                await client.AuthenticateAsync(settings.Username, settings.Password);
            }

            await client.SendAsync(message);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}