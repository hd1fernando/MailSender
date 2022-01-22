using MailSender.Api.Models;
using MailSender.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailSender.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MailSenderController : ControllerBase
{
    private readonly IMailSenderService MailSenderService;

    public MailSenderController(IMailSenderService mailSenderService)
    {
        MailSenderService = mailSenderService;
    }

    [HttpPost]
    public async Task<ActionResult> SendMail(MailViewModel mailViewModel, CancellationToken cancellationToken = default)
    {
        var mail = mailViewModel.ToModel();

        await MailSenderService.SendMailAsync(mail, cancellationToken);

        return Ok();
    }
}