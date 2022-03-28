namespace Juris.Api.IServices;

public interface IMailService
{
    public Task SendAsync(string to, string subject, string body);
}