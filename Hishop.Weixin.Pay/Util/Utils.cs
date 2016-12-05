namespace Hishop.Weixin.Pay.Util
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Hishop.Weixin.Pay.Notify;

    internal class Utils
    {
        private static readonly DateTime BaseTime = new DateTime(0x7b2, 1, 1);
        private static Dictionary<string, XmlSerializer> parsers = new Dictionary<string, XmlSerializer>();

        public static DateTime ConvertSecondsToDateTime(long seconds)
        {
            return BaseTime.AddSeconds((double) seconds).AddHours(8.0);
        }

        public static string CreateNoncestr()
        {
            return DateTime.Now.ToString("fffffff");
        }

        public static long GetCurrentTimeSeconds()
        {
            TimeSpan span = (TimeSpan) (DateTime.UtcNow - BaseTime);
            return (long) span.TotalSeconds;
        }

        public static T GetNotifyObject<T>(string xml) where T: NotifyObject
        {
            Type type = typeof(T);
            string fullName = type.FullName;
            XmlSerializer serializer = null;
            if (!(parsers.TryGetValue(fullName, out serializer) && (serializer != null)))
            {
                XmlAttributes attributes = new XmlAttributes {
                    XmlRoot = new XmlRootAttribute("xml")
                };
                XmlAttributeOverrides overrides = new XmlAttributeOverrides();
                overrides.Add(type, attributes);
                serializer = new XmlSerializer(type, overrides);
                parsers[fullName] = serializer;
            }
            object obj2 = null;
            try
            {
                using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    obj2 = serializer.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
            return (obj2 as T);
        }

        public static long GetTimeSeconds(DateTime dt)
        {
            TimeSpan span = (TimeSpan) (dt.ToUniversalTime() - BaseTime);
            return (long) span.TotalSeconds;
        }

        public static string GetToken(string appid, string secret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
            string str2 = new WebUtils().DoGet(url);
            if (!string.IsNullOrEmpty(str2))
            {
                Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(str2);
                if ((dictionary != null) && dictionary.ContainsKey("access_token"))
                {
                    return dictionary["access_token"];
                }
            }
            return string.Empty;
        }
    }
}

