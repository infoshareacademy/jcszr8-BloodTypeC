using BloodTypeC.DAL.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData, CancellationToken ct);
    }
}
