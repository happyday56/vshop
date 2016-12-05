
using System;
using System.IO;
using System.Web;

namespace Hidistro.UI.Web.Handler
{
    public class flashupload : IHttpHandler
    {

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            context.Response.Expires = -1;
            try
            {
                HttpPostedFile item = context.Request.Files["Filedata"];
                string str = "/Storage/temp/";
                string str1 = "/Storage/temp/";
                FileInfo fileInfo = new FileInfo(item.FileName);
                DateTime now = DateTime.Now;
                string str2 = now.ToString("yyyyMM");
                string str3 = string.Concat(context.Server.MapPath(str), str2, "/");
                if (!Directory.Exists(str3))
                {
                    Directory.CreateDirectory(str3);
                }
                string lower = fileInfo.Extension.ToLower();
                if (lower == ".gif" || lower == ".jpg" || lower == ".png")
                {
                    object[] millisecond = new object[] { "product_", null, null, null };
                    DateTime dateTime = DateTime.Now;
                    millisecond[1] = dateTime.ToString("HHmmss");
                    millisecond[2] = DateTime.Now.Millisecond;
                    millisecond[3] = fileInfo.Extension;
                    string str4 = string.Concat(millisecond);
                    if (File.Exists(string.Concat(str3, str4)))
                    {
                        File.Delete(string.Concat(str3, str4));
                    }
                    item.SaveAs(string.Concat(str3, str4));
                    context.Response.StatusCode = 200;
                    context.Response.Write(string.Concat(str1, str2, "/", str4));
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                context.Response.StatusCode = 200;
                context.Response.Write(exception.ToString());
            }
        }
    }
}