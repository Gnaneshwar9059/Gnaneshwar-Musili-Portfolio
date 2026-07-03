using Microsoft.Extensions.Logging;
using Portfolio.Infrastructure.Email;
using Portfolio.Shared.Requests;

namespace Portfolio.Services.Services;

public interface IContactService
{
    Task<bool> SendAsync(ContactRequest request);
}

public class ContactService : IContactService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<ContactService> _logger;

    public ContactService(
        IEmailService emailService,
        ILogger<ContactService> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<bool> SendAsync(ContactRequest request)
    {
        var body = EmailTemplateBuilder.Build(
     request.Name,
     request.Email,
     request.Phone ?? "",
     request.Role,
     request.Subject,
     request.Message);

        _logger.LogInformation(
    "Received contact request from {Email}",
    request.Email);

        try
        {
            await _emailService.SendAsync(
           $"Portfolio Contact - {request.Subject}",
           body);

        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Email sending failed.");

            throw;
        }

        _logger.LogInformation(
    "Email sent successfully.");

        return true;
    }
}