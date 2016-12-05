namespace Hidistro.Messages
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal.Store;
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;

    public static class MessageTemplateHelper
    {
        private const string CacheKey = "Message-{0}";
        private const string DistributorCacheKey = "Message-{0}-{1}";

        internal static MailMessage GetEmailTemplate(MessageTemplate template, string emailTo)
        {
            if (((template == null) || !template.SendEmail) || string.IsNullOrEmpty(emailTo))
            {
                return null;
            }
            MailMessage message = new MailMessage {
                IsBodyHtml = true,
                Priority = MailPriority.High,
                Body = template.EmailBody.Trim(),
                Subject = template.EmailSubject.Trim()
            };
            message.To.Add(emailTo);
            return message;
        }

        public static MessageTemplate GetMessageTemplate(string messageType)
        {
            if (string.IsNullOrEmpty(messageType))
            {
                return null;
            }
            return new MessageTemplateDao().GetMessageTemplate(messageType);
        }

        public static IList<MessageTemplate> GetMessageTemplates()
        {
            return new MessageTemplateDao().GetMessageTemplates();
        }

        internal static MessageTemplate GetTemplate(string messageType)
        {
            messageType = messageType.ToLower();
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
            string key = string.Format("Message-{0}", messageType);
            MessageTemplate messageTemplate = HiCache.Get(key) as MessageTemplate;
            if (messageTemplate == null)
            {
                messageTemplate = GetMessageTemplate(messageType);
                if (messageTemplate != null)
                {
                    HiCache.Max(key, messageTemplate);
                }
            }
            return messageTemplate;
        }

        public static void UpdateSettings(IList<MessageTemplate> templates)
        {
            if ((templates != null) && (templates.Count != 0))
            {
                new MessageTemplateDao().UpdateSettings(templates);
                foreach (MessageTemplate template in templates)
                {
                    HiCache.Remove(string.Format("Message-{0}", template.MessageType.ToLower()));
                }
            }
        }

        public static void UpdateTemplate(MessageTemplate template)
        {
            if (template != null)
            {
                new MessageTemplateDao().UpdateTemplate(template);
                HiCache.Remove(string.Format("Message-{0}", template.MessageType.ToLower()));
            }
        }
    }
}

