namespace MailSender.Worker.Models;

public class MailViewModel
{
    public int Id { get; set; }
    public string? Subject { get; set; }

    public string? Content { get; set; }

    public string? Destiny { get; set; }
}