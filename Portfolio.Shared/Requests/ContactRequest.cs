using System.ComponentModel.DataAnnotations;

namespace Portfolio.Shared.Requests;

public class ContactRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Phone { get; set; }
    [StringLength(100)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [StringLength(3000)]
    public string Message { get; set; } = string.Empty;
}