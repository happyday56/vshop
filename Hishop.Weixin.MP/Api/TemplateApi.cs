namespace Hishop.Weixin.MP.Api
{
    using Hishop.Weixin.MP.Domain;
    using Hishop.Weixin.MP.Util;
    using System;
    using System.Text;
    using NewLife.Log;

    public class TemplateApi
    {
        public static void SendMessage(string accessTocken, TemplateMessage templateMessage)
        {
            StringBuilder builder = new StringBuilder("{");
            builder.AppendFormat("\"touser\":\"{0}\",", templateMessage.Touser);
            builder.AppendFormat("\"template_id\":\"{0}\",", templateMessage.TemplateId);
            builder.AppendFormat("\"url\":\"{0}\",", templateMessage.Url);
            builder.AppendFormat("\"topcolor\":\"{0}\",", templateMessage.Topcolor);
            builder.Append("\"data\":{");
            foreach (TemplateMessage.MessagePart part in templateMessage.Data)
            {
                builder.AppendFormat("\"{0}\":{{\"value\":\"{1}\",\"color\":\"{2}\"}},", part.Name, part.Value, part.Color);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append("}}");
            //XTrace.WriteLine("发送的微信内容：－－－" + builder.ToString());
            WebUtils utils = new WebUtils();
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessTocken;
            string str2 = utils.DoPost(url, builder.ToString());
            //XTrace.WriteLine("微信已发送，结果是：" + str2);
        }
    }
}

