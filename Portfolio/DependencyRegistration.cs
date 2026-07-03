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

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}