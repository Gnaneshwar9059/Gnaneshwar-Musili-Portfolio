namespace Portfolio.Infrastructure.Email;

public class EmailSettings
{
    public string ApiKey { get; set; } = "";
    public string From { get; set; } = "";   // e.g. "Portfolio <contact@yourdomain.com>"
    public string To { get; set; } = "";     // your own inbox
}