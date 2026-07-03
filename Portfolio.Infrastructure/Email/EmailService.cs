using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Portfolio.Infrastructure.Email;

public interface IEmailService
{
    Task SendAsync(string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendAsync(string subject, string body)
    {
        Console.WriteLine("EmailService reached.");

        await Task.Delay(1000);

        Console.WriteLine("EmailService completed.");

        return;
    }
}