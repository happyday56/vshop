namespace ASPNET.WebControls
{
    using System;
    using System.Web;

    public static class Utils
    {
        public static bool IsUrlAbsolute(string url)
        {
            if (url != null)
            {
                string[] strArray = new string[] { "about:", "file:///", "ftp://", "gopher://", "http://", "https://", "javascript:", "mailto:", "news:", "res://", "telnet://", "view-source:" };
                foreach (string str in strArray)
                {
                    if (url.StartsWith(str))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string ApplicationPath
        {
            get
            {
                string applicationPath = "/";
                if (HttpContext.Current != null)
                {
                    applicationPath = HttpContext.Current.Request.ApplicationPath;
                }
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                return applicationPath;
            }
        }
    }
}

