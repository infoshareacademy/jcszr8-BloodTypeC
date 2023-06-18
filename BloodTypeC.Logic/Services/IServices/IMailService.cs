using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodTypeC.DAL.Models;

namespace BloodTypeC.Logic.Services.IServices
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData, CancellationToken ct);
        string CreateMailTemplate(string header, string body, string footer);

    }
}
