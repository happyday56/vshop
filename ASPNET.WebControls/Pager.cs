
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.WebControls
{
    public class Pager : WebControl
    {
        private string aname = string.Empty;
        private string urlFormat;
        private int pageSize = 20;

        public Pager()
        {
            this.PageIndexFormat = "pageindex";
            this.BreakCssClass = "page-break";
            this.PrevCssClass = "page-prev";
            this.CurCssClass = "page-cur";
            this.NextCssClass = "page-next";
            this.SkipPanelCssClass = "page-skip";
            this.SkipTxtCssClass = "text";
            this.SkipBtnCssClass = "button";
        }

        private int CalculateTotalPages()
        {
            if (this.TotalRecords == 0)
            {
                return 0;
            }
            int num = this.TotalRecords / this.PageSize;
            if ((this.TotalRecords % this.PageSize) > 0)
            {
                num++;
            }
            return num;
        }

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            this.urlFormat = this.Context.Request.RawUrl;

            if (this.urlFormat.IndexOf("?") >= 0)
            {
                string oldValue = this.urlFormat.Substring(this.urlFormat.IndexOf("?") + 1);

                if (!string.IsNullOrWhiteSpace(oldValue))
                {
                    string[] strArray = oldValue.Split(new char[] { Convert.ToChar("&") }, StringSplitOptions.RemoveEmptyEntries);

                    this.urlFormat = this.urlFormat.Replace(oldValue, "");

                    foreach (string str2 in strArray)
                    {
                        if (!str2.ToLower().StartsWith(this.PageIndexFormat.ToLower() + "="))
                        {
                            this.urlFormat = this.urlFormat + str2 + "&";
                        }
                    }
                }
                this.urlFormat = this.urlFormat + this.PageIndexFormat + "=";
            }
            else
            {
                this.urlFormat = this.urlFormat + "?" + this.PageIndexFormat + "=";
            }

        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.TotalRecords > 0)
            {
                int totalPages = this.CalculateTotalPages();
                this.RenderPrevious(writer);
                this.RenderPagingButtons(writer, totalPages);
                if ((totalPages > 6) && ((this.PageIndex + 2) < totalPages))
                {
                    this.RenderMore(writer);
                }
                this.RenderNext(writer);
                if (this.ShowTotalPages)
                {
                    this.RenderGotoPage(writer, totalPages);
                }
            }
        }

        private void RenderButton(HtmlTextWriter writer, int buttonIndex)
        {
            if (buttonIndex == this.PageIndex)
            {
                new LiteralControl("<span class=\"" + this.CurCssClass + "\">" + buttonIndex.ToString(CultureInfo.InvariantCulture) + "</span>").RenderControl(writer);
            }
            else
            {
                WebControl control = new WebControl(HtmlTextWriterTag.A);
                control.Controls.Add(new LiteralControl(buttonIndex.ToString(CultureInfo.InvariantCulture)));
                control.Attributes.Add("href", this.urlFormat + buttonIndex.ToString(CultureInfo.InvariantCulture) + this.Aname);
                control.RenderControl(writer);
            }
        }

        private void RenderButtonRange(HtmlTextWriter writer, int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                this.RenderButton(writer, i);
            }
        }

        private void RenderGotoPage(HtmlTextWriter writer, int totalPages)
        {
            WebControl control = new WebControl(HtmlTextWriterTag.Span);
            control.Attributes.Add("class", this.SkipPanelCssClass);
            control.Controls.Add(new LiteralControl(string.Format("第{0}/{1}页 共{2}记录", this.PageIndex, totalPages.ToString(CultureInfo.InvariantCulture), this.TotalRecords.ToString(CultureInfo.InvariantCulture))));
            WebControl child = new WebControl(HtmlTextWriterTag.Input);
            child.Attributes.Add("type", "text");
            child.Attributes.Add("class", this.SkipTxtCssClass);
            child.Attributes.Add("value", this.PageIndex.ToString(CultureInfo.InvariantCulture));
            child.Attributes.Add("size", "3");
            child.Attributes.Add("id", "txtGoto");
            control.Controls.Add(child);
            control.Controls.Add(new LiteralControl("页"));
            WebControl control3 = new WebControl(HtmlTextWriterTag.Input);
            control3.Attributes.Add("type", "button");
            control3.Attributes.Add("class", this.SkipBtnCssClass);
            control3.Attributes.Add("value", "确定");
            control3.Attributes.Add("onclick", string.Format("location.href=AppendParameter('{0}',  $.trim($('#txtGoto').val()));", this.PageIndexFormat));
            control.Controls.Add(control3);
            control.RenderControl(writer);
        }

        private void RenderMore(HtmlTextWriter writer)
        {
            WebControl control = new WebControl(HtmlTextWriterTag.Span);
            control.Attributes.Add("class", this.BreakCssClass);
            control.Controls.Add(new LiteralControl("..."));
            control.RenderControl(writer);
        }

        private void RenderNext(HtmlTextWriter writer)
        {
            if (this.HasNext)
            {
                WebControl control = new WebControl(HtmlTextWriterTag.A);
                control.Controls.Add(new LiteralControl("下一页"));
                control.Attributes.Add("class", this.NextCssClass);
                int num = this.PageIndex + 1;
                control.Attributes.Add("href", this.urlFormat + num.ToString(CultureInfo.InvariantCulture) + this.Aname);
                control.RenderControl(writer);
            }
        }

        private void RenderPagingButtons(HtmlTextWriter writer, int totalPages)
        {
            if (totalPages <= 6)
            {
                this.RenderButtonRange(writer, 1, totalPages);
            }
            else
            {
                int startIndex = this.PageIndex - 3;
                int endIndex = this.PageIndex + 2;
                if (startIndex <= 0)
                {
                    endIndex -= --startIndex;
                    startIndex = 1;
                }
                if (endIndex > totalPages)
                {
                    endIndex = totalPages;
                }
                this.RenderButtonRange(writer, startIndex, endIndex);
            }
        }

        private void RenderPrevious(HtmlTextWriter writer)
        {
            if (this.HasPrevious)
            {
                WebControl control = new WebControl(HtmlTextWriterTag.A);
                control.Controls.Add(new LiteralControl("上一页"));
                control.Attributes.Add("class", this.PrevCssClass);
                int num = this.PageIndex - 1;
                control.Attributes.Add("href", this.urlFormat + num.ToString(CultureInfo.InvariantCulture) + this.Aname);
                control.RenderControl(writer);
            }
        }

        public string Aname
        {
            get
            {
                if (!string.IsNullOrEmpty(this.aname) && !this.aname.StartsWith("#"))
                {
                    this.aname = "#" + this.aname;
                }
                return this.aname;
            }
            set
            {
                this.aname = value;
            }
        }

        public string BreakCssClass { get; set; }

        public string CurCssClass { get; set; }

        private bool HasNext
        {
            get
            {
                return (this.PageIndex < this.CalculateTotalPages());
            }
        }

        private bool HasPrevious
        {
            get
            {
                return (this.PageIndex > 1);
            }
        }

        public string NextCssClass { get; set; }

        [Browsable(false)]
        public int PageIndex
        {
            get
            {
                int result = 1;
                if (!string.IsNullOrEmpty(this.Context.Request.QueryString[this.PageIndexFormat]))
                {
                    int.TryParse(this.Context.Request.QueryString[this.PageIndexFormat], out result);
                }
                if (result <= 0)
                {
                    return 1;
                }
                int num2 = this.CalculateTotalPages();
                if ((num2 > 0) && (result > num2))
                {
                    return num2;
                }
                return result;
            }
        }

        public string PageIndexFormat { get; set; }

        [Browsable(false)]
        public int PageSize
        {
            get
            {
                int result = this.pageSize;
                if (!string.IsNullOrEmpty(this.Context.Request.QueryString["pagesize"]))
                {
                    int.TryParse(this.Context.Request.QueryString["pagesize"], out result);
                }
                //if (result <= 0)
                //{
                this.pageSize = result;
                //}
                return pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        public string PrevCssClass { get; set; }

        public bool ShowTotalPages { get; set; }

        public string SkipBtnCssClass { get; set; }

        public string SkipPanelCssClass { get; set; }

        public string SkipTxtCssClass { get; set; }

        [Browsable(false)]
        public int TotalRecords
        {
            get
            {
                if (this.ViewState["TotalRecords"] == null)
                {
                    return 0;
                }
                return (int)this.ViewState["TotalRecords"];
            }
            set
            {
                this.ViewState["TotalRecords"] = value;
            }
        }
    }
}

