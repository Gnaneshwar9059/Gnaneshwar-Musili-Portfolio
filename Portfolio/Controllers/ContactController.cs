using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.Services;
using Portfolio.Shared.Requests;

namespace Portfolio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    public async Task<IActionResult> Send(ContactRequest request)
    {
        var result = await _contactService.SendAsync(request);

        return Ok(new
        {
            success = result,
            message = "Contact request received."
        });
    }
}