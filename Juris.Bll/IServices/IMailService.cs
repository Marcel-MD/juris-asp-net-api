namespace Juris.Bll.IServices;

public interface IMailService
{
    /// <summary>
    ///     Sends an email using smtp. If there are no smtp setting specified it will do nothing.
    /// </summary>
    /// <param name="to">Destination email address.</param>
    /// <param name="subject">Subject of the email.</param>
    /// <param name="body">Body of the email.</param>
    public Task SendAsync(string to, string subject, string body);
}