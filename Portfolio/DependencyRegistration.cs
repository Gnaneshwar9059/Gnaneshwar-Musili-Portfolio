using Portfolio.Infrastructure.Email;
using Portfolio.Services.Services;

namespace Portfolio;

public static class DependencyRegistration
{
    public static IServiceCollection AddPortfolioServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(
            configuration.GetSection("EmailSettings"));

        services.AddScoped<IContactService, ContactService>();

        // Registers IEmailService with a typed HttpClient
        // (reuses connections instead of creating a new HttpClient per request)
        services.AddHttpClient<IEmailService, EmailService>();

        return services;
    }
}