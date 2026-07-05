using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Portfolio.Infrastructure.Email;

public interface IEmailService
{
    Task SendAsync(string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly HttpClient _http;
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        HttpClient http,
        IOptions<EmailSettings> options,
        ILogger<EmailService> logger)
    {
        _http = http;
        _settings = options.Value;
        _logger = logger;

        _http.BaseAddress = new Uri("https://api.resend.com/");
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _settings.ApiKey);
    }

    public async Task SendAsync(string subject, string body)
    {
        var payload = new
        {
            from = _settings.From,
            to = new[] { _settings.To },
            subject,
            html = body
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _logger.LogInformation("Sending email via Resend...");

        var response = await _http.PostAsync("emails", content);

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(
                "Resend API call failed. Status: {Status}, Body: {Body}",
                response.StatusCode,
                responseBody);

            throw new HttpRequestException(
                $"Resend API returned {response.StatusCode}: {responseBody}");
        }

        _logger.LogInformation("Email sent successfully via Resend.");
    }
}