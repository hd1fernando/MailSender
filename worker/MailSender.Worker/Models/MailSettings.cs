namespace MailSender.Worker.Models;

public class MailSettingsOptions
{
    public string? Domain { get; set; }
    public int Port { get; set; }
    public string? UserEmail { get; set; }
    public string? UserPassword { get; set; }
}
