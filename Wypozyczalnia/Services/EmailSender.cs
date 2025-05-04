namespace Wypozyczalnia.Services;

using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MimeKit;

public class CustomEmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public CustomEmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;
        message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config["EmailSettings:SmtpHost"], int.Parse(_config["EmailSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_config["EmailSettings:From"], _config["EmailSettings:Password"]);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}