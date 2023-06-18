using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace EmailService
{
    public class MailData
    {
        // Receiver
        public List<string> To { get; }
        public List<string> Bcc { get; }
        public List<string> Cc { get; }

        // Sender
        public string? From { get; }

        public MailData(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select((x, y)=> new MailboxAddress(x,y)));
            Subject = subject;
            Content = content;
        }
    }
}
