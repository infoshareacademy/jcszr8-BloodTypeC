using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodTypeC.DAL.Models;
using BloodTypeC.Logic.Services.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BloodTypeC.Logic.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;

        public MailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public string CreateMailTemplate(string mailHeadline, string mailBody, string mailFooter)
        {
            var mail = Consts.mailTemplate
                .Replace("{MAILHEADLINE}", mailHeadline)
                .Replace("{MAILBODY}", mailBody)
                .Replace("{MAILFOOTER}", mailFooter);
            return mail;
        }

        public async Task<bool> SendAsync(MailData mailData, CancellationToken ct)
        {
            try
            {
                var mail = new MimeMessage();

                // Sender
                mail.From.Add(new MailboxAddress(_settings.DisplayName, mailData.From ?? _settings.From));
                mail.Sender = new MailboxAddress(mailData.DisplayName ?? _settings.DisplayName,
                    mailData.From ?? _settings.From);

                // Receivers
                foreach (string mailAddress in mailData.To)
                {
                    mail.To.Add(MailboxAddress.Parse(mailAddress));
                }

                if (!string.IsNullOrEmpty(mailData.ReplyTo) && !string.IsNullOrWhiteSpace(mailData.ReplyTo))
                {
                    mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));
                }

                if (mailData.Bcc != null)
                {
                    foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                    }
                }

                if (mailData.Cc != null)
                {
                    foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrEmpty(x)))
                    {
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                    }
                }

                // Content
                var body = new BodyBuilder();
                mail.Subject = mailData.Subject;
                body.HtmlBody = mailData.Body;
                mail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailData.Body };// body.ToMessageBody();

                // Send mail
                using var smtp = new SmtpClient();

                if (_settings.UseSSL)
                {
                    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
                }
                else if (_settings.UseStartTls)
                {
                    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.Auto, ct);
                }
;
                await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
