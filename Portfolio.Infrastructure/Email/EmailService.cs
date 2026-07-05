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
        try
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_settings.From));
            email.To.Add(MailboxAddress.Parse(_settings.Username));

            email.Subject = subject;

            email.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            // Timeout after 10 seconds instead of hanging forever
            smtp.Timeout = 10000;

            Console.WriteLine("========== EMAIL CONFIG ==========");
            Console.WriteLine($"HOST: {_settings.Host}");
            Console.WriteLine($"PORT: {_settings.Port}");
            Console.WriteLine($"FROM: {_settings.From}");
            Console.WriteLine($"USER: {_settings.Username}");
            Console.WriteLine($"PASSWORD EMPTY: {string.IsNullOrWhiteSpace(_settings.Password)}");
            Console.WriteLine("==================================");

            Console.WriteLine("Connecting to SMTP server...");

            await smtp.ConnectAsync(
                _settings.Host,
                _settings.Port,
                SecureSocketOptions.StartTls);

            Console.WriteLine("Connected successfully.");

            Console.WriteLine("Authenticating...");

            await smtp.AuthenticateAsync(
                _settings.Username,
                _settings.Password);

            Console.WriteLine("Authenticated successfully.");

            Console.WriteLine("Sending email...");

            await smtp.SendAsync(email);

            Console.WriteLine("Email sent successfully.");

            Console.WriteLine("Disconnecting...");

            await smtp.DisconnectAsync(true);

            Console.WriteLine("Disconnected successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("========== EMAIL ERROR ==========");
            Console.WriteLine(ex.ToString());
            Console.WriteLine("=================================");

            throw;
        }
    }
}