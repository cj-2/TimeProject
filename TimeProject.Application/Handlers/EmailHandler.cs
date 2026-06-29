using System.Net.Mail;
using TimeProject.Application.Dtos.Emails;
using TimeProject.Application.Interfaces.Handlers;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Handlers;

public class EmailHandler(ICustomSmtp customSmtp) : IEmailHandler
{
    public void Send(EmailPayloadDto emailPayloadDto)
    {
        var mailMessage = new MailMessage();

        mailMessage.To.Add(new MailAddress(emailPayloadDto.To));
        mailMessage.Subject = string.IsNullOrEmpty(emailPayloadDto.Subject) ? "Default Subject" : emailPayloadDto.Subject;
        mailMessage.Body = string.IsNullOrEmpty(emailPayloadDto.Body) ? "Default Body" : emailPayloadDto.Body;
        mailMessage.IsBodyHtml = emailPayloadDto.IsHtml;
        customSmtp.Send(mailMessage);
    }
}