namespace ASPNET.WebControls
{
    using System;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:Grid runat=server></{0}:Grid>")]
    public class GetableGrid : GridView
    {
        private string currentSort;
        private string currentSortBy;
        private string defaultSortIcon;
        private bool showOrderIcons = true;
        private string sortAscIcon;
        private const string SortByKey = "sortBy";
        private string sortDescIcon;
        private const string SortOrderKey = "sort";

        private HyperLink CreateHeader(string title, string sort, string sortBy, string url)
        {
            HyperLink link = new HyperLink {
                Text = title,
                NavigateUrl = url + string.Format(CultureInfo.InvariantCulture, "{0}={1}&{2}={3}", new object[] { "sortBy", sortBy, "sort", sort })
            };
            link.ControlStyle.CopyFrom(base.HeaderStyle);
            return link;
        }

        private string CreateUrl()
        {
            string str = this.Context.Request.Url.AbsolutePath + "?";
            if (this.Context.Request.QueryString.Count > 0)
            {
                for (int i = 0; i < this.Context.Request.QueryString.Count; i++)
                {
                    string strA = this.Context.Request.QueryString.Keys[i];
                    if ((string.Compare(strA, "sortBy", true) != 0) && (string.Compare(strA, "sort", true) != 0))
                    {
                        string str3 = str;
                        str = str3 + strA + "=" + this.Page.Server.UrlEncode(this.Context.Request.QueryString[strA]) + "&";
                    }
                }
            }
            return str;
        }

        protected override void OnInit(EventArgs e)
        {
            this.currentSort = this.Context.Request.QueryString["sort"];
            this.currentSortBy = this.Context.Request.QueryString["sortBy"];
            this.SortAscIcon = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.asc.gif");
            this.SortDescIcon = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.desc.gif");
            this.DefaultSortIcon = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.por_arrow_13.gif");
            base.OnInit(e);
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
            if ((e.Row.RowType == DataControlRowType.Pager) || (e.Row.RowType == DataControlRowType.Footer))
            {
                e.Row.Visible = false;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if ((this.AllowSorting && (this.Controls.Count >= 0)) && ((this.Rows.Count > 0) && this.ShowOrderIcons))
            {
                int num = 0;
                string url = this.CreateUrl();
                foreach (DataControlField field in this.Columns)
                {
                    if (!string.IsNullOrEmpty(field.SortExpression))
                    {
                        string str2;
                        Image child = new Image {
                            BorderWidth = Unit.Pixel(0),
                            Width = Unit.Pixel(13),
                            Height = Unit.Pixel(7)
                        };
                        if (field.SortExpression == this.currentSortBy)
                        {
                            if (string.Compare("desc", this.currentSort, true, CultureInfo.InvariantCulture) == 0)
                            {
                                child.ImageUrl = this.SortDescIcon;
                                str2 = "ASC";
                            }
                            else
                            {
                                child.ImageUrl = this.SortAscIcon;
                                str2 = "DESC";
                            }
                        }
                        else
                        {
                            child.ImageUrl = this.DefaultSortIcon;
                            str2 = "ASC";
                        }
                        this.Controls[0].Controls[0].Controls[num].Controls.Clear();
                        this.Controls[0].Controls[0].Controls[num].Controls.Add(child);
                        this.Controls[0].Controls[0].Controls[num].Controls.Add(new LiteralControl("&nbsp;"));
                        this.Controls[0].Controls[0].Controls[num].Controls.Add(this.CreateHeader(field.HeaderText, str2, field.SortExpression, url));
                    }
                    num++;
                }
            }
            base.Render(writer);
        }

        public override bool AllowPaging
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public override bool AllowSorting
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public string DefaultSortIcon
        {
            get
            {
                return this.defaultSortIcon;
            }
            set
            {
                this.defaultSortIcon = value;
            }
        }

        public bool ShowOrderIcons
        {
            get
            {
                return this.showOrderIcons;
            }
            set
            {
                this.showOrderIcons = value;
            }
        }

        public string SortAscIcon
        {
            get
            {
                return this.sortAscIcon;
            }
            set
            {
                this.sortAscIcon = value;
            }
        }

        public string SortDescIcon
        {
            get
            {
                return this.sortDescIcon;
            }
            set
            {
                this.sortDescIcon = value;
            }
        }

        public string SortOrder
        {
            get
            {
                return this.currentSort;
            }
        }

        public string SortOrderBy
        {
            get
            {
                return this.currentSortBy;
            }
        }
    }
}

