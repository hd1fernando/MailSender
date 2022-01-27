namespace MailSender.Worker.Models;

public class MailSettingsOptions
{
    public string? Domain { get; set; }
    public int Port { get; set; }
    public string? UserEmail { get; set; }
    public string? UserPassword { get; set; }
}

public class MailViewModel
{
    public int Id { get; set; }
    public string? Subject { get; set; }

    public string? Content { get; set; }

    public string? Destiny { get; set; }
}