using BlogPostWebApi.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace BlogPostWebApi.Services;

public class EmailService(IConfiguration _config) : IEmailService
{
    private readonly IConfiguration config = _config.GetSection("Jwt");

    public async Task SendMessageAsync(string to, string subject, string message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["EmailAddress"]));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = message };


        var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config["Host"], 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_config["EmailAddress"], _config["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
