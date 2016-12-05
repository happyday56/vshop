namespace Hidistro.Messages
{
    using Hidistro.Core.Configuration;
    using Hidistro.Core.Entities;
    using Hidistro.SqlDal.Store;
    using Hishop.Plugins;
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Threading;

    public static class Emails
    {
        internal static void EnqueuEmail(MailMessage email, SiteSettings settings)
        {
            if (((email != null) && (email.To != null)) && (email.To.Count > 0))
            {
                new EmailQueueDao().QueueEmail(email);
            }
        }

        public static void SendQueuedEmails(int failureInterval, int maxNumberOfTries, SiteSettings settings)
        {
            if (settings != null)
            {
                HiConfiguration config = HiConfiguration.GetConfig();
                Dictionary<Guid, MailMessage> dictionary = new EmailQueueDao().DequeueEmail();
                IList<Guid> list = new List<Guid>();
                EmailSender sender = Messenger.CreateEmailSender(settings);
                if (sender != null)
                {
                    int num = 0;
                    short smtpServerConnectionLimit = config.SmtpServerConnectionLimit;
                    foreach (Guid guid in dictionary.Keys)
                    {
                        if (Messenger.SendMail(dictionary[guid], sender))
                        {
                            new EmailQueueDao().DeleteQueuedEmail(guid);
                            if ((smtpServerConnectionLimit != -1) && (++num >= smtpServerConnectionLimit))
                            {
                                Thread.Sleep(new TimeSpan(0, 0, 0, 15, 0));
                                num = 0;
                            }
                        }
                        else
                        {
                            list.Add(guid);
                        }
                    }
                    if (list.Count > 0)
                    {
                        new EmailQueueDao().QueueSendingFailure(list, failureInterval, maxNumberOfTries);
                    }
                }
            }
        }
    }
}

