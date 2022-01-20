using System.Net.Mail;

namespace MailSender.Api.Entities;
public class MailEntity
{
    public int Id { get; }
    public string? Subject { get; }

    public string? Content { get; }

    public string? Destiny { get; }

    public MailEntity(string? subject, string? content, string? destiny)
    {
        PreConditions(subject, content, destiny);
        Subject = subject;
        Content = content;
        Destiny = destiny;
    }

    private void PreConditions(string? subject, string? content, string? destiny)
    {
        if (string.IsNullOrEmpty(subject)) throw new Exception($"{nameof(subject)} is required");
        if (string.IsNullOrEmpty(content)) throw new Exception($"{nameof(content)} is required");
        if (string.IsNullOrEmpty(destiny)) throw new Exception($"{nameof(destiny)} is required");
        ValidateEmail(destiny);
    }

    private void ValidateEmail(string email)
    {
        try
        {
            var address = new MailAddress(email.Trim());

        }
        catch
        {
            throw new Exception("Invalid email format");
        }

    }
}