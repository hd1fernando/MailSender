using MailSender.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailSender.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MailSenderController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> SendMail(MailViewModel mailViewModel){
        var mail = mailViewModel.ToModel();
        
        return  Ok();
    }
}