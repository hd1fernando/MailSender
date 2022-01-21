using System.ComponentModel.DataAnnotations;
using MailSender.Api.Entities;

namespace MailSender.Api.Models;

public class MailViewModel
{
    [Required(ErrorMessage = "The {0} is required.")]
    [MaxLength(30, ErrorMessage = "{0} should be less than {1} characters.")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "The {0} is required.")]
    [MaxLength(5000, ErrorMessage = "{0} should be less than {1} characters")]
    public string? Content { get; set; }

    [Required(ErrorMessage = "The {0} is required.")]
    [EmailAddress(ErrorMessage = "{0} should be a valid email.")]
    public string? Destiny { get; set; }

    public MailEntity ToModel() => new MailEntity(Subject, Content, Destiny);
}
