using Application.Contracts.Infrastructure;

namespace Infrastructure.Email;

public class EmailSender : IEmailSender
{
    public Task<bool> SendEmailAsync(Application.Models.Email email)
    {
        throw new NotImplementedException();
    }
}
