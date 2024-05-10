namespace BlogPostWebApi.Interfaces.Services;

public interface IEmailService
{
    Task SendMessageAsync(string to, string subject, string message);
}
