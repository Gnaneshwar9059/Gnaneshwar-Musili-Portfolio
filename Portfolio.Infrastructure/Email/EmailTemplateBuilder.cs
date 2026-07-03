namespace Portfolio.Infrastructure.Email;

public static class EmailTemplateBuilder
{
    public static string Build(
        string name,
        string email,
        string phone,
        string role,
        string subject,
        string message)
    {
        return $"""
<!DOCTYPE html>
<html>

<body style="font-family:Segoe UI,Arial,sans-serif;background:#f5f5f5;padding:20px;">

<div style="max-width:700px;margin:auto;background:white;border-radius:10px;padding:30px;">

<h2 style="color:#2563eb;">
📩 New Portfolio Contact
</h2>

<hr/>

<p><strong>Name</strong><br/>{name}</p>

<p><strong>Email</strong><br/>{email}</p>

<p><strong>Phone</strong><br/>{phone}</p>

<p><strong>Role / Company</strong><br/>{role}</p>

<p><strong>Subject</strong><br/>{subject}</p>

<hr/>

<h3>Message</h3>

<p>{message}</p>

<hr/>

<p style="color:gray;font-size:12px;">
Generated from Gnaneshwar's Portfolio Website.
</p>

</div>

</body>

</html>
""";
    }
}