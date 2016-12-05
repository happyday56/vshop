using System;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.WebControls
{
    /// <summary>
    /// 分页大小
    /// </summary>
    public class PageSize : WebControl
    {

        #region 字段
        StringBuilder urlFormat = new StringBuilder();
        #endregion

        #region 构造函数
        public PageSize()
        {
            this.SelectedSizeCss = "selectthis";
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.urlFormat.Clear();

            this.urlFormat.Append(this.Context.Request.RawUrl);

            if (this.Context.Request.QueryString.Count > 0)
            {



                this.urlFormat.Replace(this.Context.Request.Url.Query, "?");


                string keyVal = "";



                foreach (string key in this.Context.Request.QueryString.Keys)
                {


                    if ((string.Compare(key, "pagesize", true) != 0) && (string.Compare(key, "pageindex", true) != 0))
                    {


                        keyVal = this.Context.Request.QueryString[key];

                        if (!string.IsNullOrWhiteSpace(keyVal))
                        {
                            this.urlFormat.AppendFormat("{0}={1}&", key, Page.Server.UrlEncode(keyVal));
                        }
                        else
                        {
                            this.urlFormat.AppendFormat("{0}={1}&", key, keyVal);
                        }

                        //string urlFormat = this.urlFormat;
                        //this.urlFormat = urlFormat + str + "=" + this.Page.Server.UrlEncode(this.Context.Request.QueryString[str]) + "&";
                    }

                }


            }

            // this.urlFormat = this.urlFormat + (this.urlFormat.Contains("?") ? "pagesize=" : "?pagesize=");

            if (urlFormat.ToString().Contains("?"))
            {
                urlFormat.Append("pagesize=");
            }
            else
            {
                urlFormat.Append("?pagesize=");
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.RenderButton(writer);
        }

        private void RenderButton(HtmlTextWriter writer)
        {
            WebControl control = new WebControl(HtmlTextWriterTag.A);
            control.Controls.Add(new LiteralControl("10"));
            control.Attributes.Add("href", this.urlFormat + "10");
            if (this.SelectedSize == 10)
            {
                control.Attributes.Add("class", this.SelectedSizeCss);
            }
            control.RenderControl(writer);
            WebControl control2 = new WebControl(HtmlTextWriterTag.A);
            control2.Controls.Add(new LiteralControl("20"));
            control2.Attributes.Add("href", this.urlFormat + "20");
            if (this.SelectedSize == 20)
            {
                control2.Attributes.Add("class", this.SelectedSizeCss);
            }
            control2.RenderControl(writer);
            WebControl control3 = new WebControl(HtmlTextWriterTag.A);
            control3.Controls.Add(new LiteralControl("40"));
            control3.Attributes.Add("href", this.urlFormat + "40");
            if (this.SelectedSize == 40)
            {
                control3.Attributes.Add("class", this.SelectedSizeCss);
            }
            control3.RenderControl(writer);
            WebControl control4 = new WebControl(HtmlTextWriterTag.A);
            control4.Controls.Add(new LiteralControl("100"));
            control4.Attributes.Add("href", this.urlFormat + "100");
            if (this.SelectedSize == 100)
            {
                control4.Attributes.Add("class", this.SelectedSizeCss);
            }
            control4.RenderControl(writer);
        }

        [Browsable(false)]
        public int SelectedSize
        {
            get
            {
                int result = 10;
                if (!string.IsNullOrEmpty(this.Context.Request.QueryString["pagesize"]))
                {
                    int.TryParse(this.Context.Request.QueryString["pagesize"], out result);
                }
                if (result <= 0)
                {
                    return 10;
                }
                return result;
            }
        }

        public string SelectedSizeCss { get; set; }
    }
}

