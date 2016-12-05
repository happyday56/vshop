namespace Hidistro.UI.ControlPanel.Utility
{
    using Hidistro.Core;
    using Hidistro.UI.Common.Controls;
    using Hishop.Components.Validation;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Text;
    using System.Web.UI;
    

    public class AdminPage : Page
    {
        private void CheckPageAccess()
        {
            if (Globals.GetCurrentManagerUserId() == 0)
            {
                
                this.Page.Response.Redirect(Globals.ApplicationPath + "/admin/Login.aspx", true);
            }
        }

        protected virtual void CloseWindow()
        {
            string str = "var win = art.dialog.open.origin; win.location.reload();";
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("ServerMessageScript"))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ServerMessageScript", "<script language='JavaScript' defer='defer'>" + str + "</script>");
            }
        }

        protected string CutWords(object obj, int length)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            string str = obj.ToString();
            if (str.Length > length)
            {
                return (str.Substring(0, length) + "......");
            }
            return str;
        }

        private string GenericReloadUrl(NameValueCollection queryStrings)
        {
            if ((queryStrings == null) || (queryStrings.Count == 0))
            {
                return base.Request.Url.AbsolutePath;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(base.Request.Url.AbsolutePath).Append("?");
            foreach (string str2 in queryStrings.Keys)
            {
                string str = queryStrings[str2].Trim().Replace("'", "");
                if (!string.IsNullOrEmpty(str) && (str.Length > 0))
                {
                    builder.Append(str2).Append("=").Append(base.Server.UrlEncode(str)).Append("&");
                }
            }
            queryStrings.Clear();
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        protected int GetFormIntParam(string name)
        {
            string str = base.Request.Form.Get(name);
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            try
            {
                return Convert.ToInt32(str);
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        protected bool GetUrlBoolParam(string name)
        {
            string str = base.Request.QueryString.Get(name);
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            try
            {
                return Convert.ToBoolean(str);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        protected int GetUrlIntParam(string name)
        {
            string str = base.Request.QueryString.Get(name);
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            try
            {
                return Convert.ToInt32(str);
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        protected void GotoResourceNotFound()
        {
            base.Response.Redirect(Globals.GetAdminAbsolutePath("ResourceNotFound.aspx"));
        }

        protected override void OnInit(EventArgs e)
        {
            if (ConfigurationManager.AppSettings["Installer"] != null)
            {
                base.Response.Redirect(Globals.ApplicationPath + "/installer/default.aspx", false);
            }
            else
            {
                this.RegisterFrameScript();
                this.CheckPageAccess();
                base.OnInit(e);
            }
        }

        protected virtual void RegisterFrameScript()
        {
            string key = "admin-frame";
            string script = string.Format("<script>if(window.parent.frames.length == 0) window.location.href=\"{0}\";</script>", Globals.ApplicationPath + "/admin/default.html");
            ClientScriptManager clientScript = this.Page.ClientScript;
            if (!clientScript.IsClientScriptBlockRegistered(key))
            {
                clientScript.RegisterClientScriptBlock(base.GetType(), key, script);
            }
        }

        protected void ReloadPage(NameValueCollection queryStrings)
        {
            base.Response.Redirect(this.GenericReloadUrl(queryStrings));
        }

        protected void ReloadPage(NameValueCollection queryStrings, bool endResponse)
        {
            base.Response.Redirect(this.GenericReloadUrl(queryStrings), endResponse);
        }

        protected virtual void ShowMsg(ValidationResults validateResults)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ValidationResult result in (IEnumerable<ValidationResult>) validateResults)
            {
                builder.Append(Formatter.FormatErrorMessage(result.Message));
            }
            this.ShowMsg(builder.ToString(), false);
        }

        protected virtual void ShowMsg(string msg, bool success)
        {
            string str = string.Format("ShowMsg(\"{0}\", {1})", msg, success ? "true" : "false");
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("ServerMessageScript"))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ServerMessageScript", "<script language='JavaScript' defer='defer'>setTimeout(function(){" + str + "},300);</script>");
            }
        }

        protected virtual void ShowMsgAndReUrl(string msg, bool success, string url)
        {
            string str = string.Format("ShowMsgAndReUrl(\"{0}\", {1}, \"{2}\")", msg, success ? "true" : "false", url);
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("ServerMessageScript"))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ServerMessageScript", "<script language='JavaScript' defer='defer'>setTimeout(function(){" + str + "},300);</script>");
            }
        }

        protected virtual void ShowMsgAndReUrl(string msg, bool success, string url, string target)
        {
            string str = string.Format("ShowMsgAndReUrl(\"{0}\", {1}, \"{2}\",\"{3}\")", new object[] { msg, success ? "true" : "false", url, target });
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("ServerMessageScript"))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ServerMessageScript", "<script language='JavaScript' defer='defer'>setTimeout(function(){" + str + "},300);</script>");
            }
        }
    }
}

