namespace ASPNET.WebControls
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:Grid runat=server></{0}:Grid>")]
    public class Grid : GridView
    {
        private string defaultSortIcon;
        private bool showOrderIcons = true;
        private string sortAscIcon;
        private string sortDescIcon;

        public event ReBindDataEventHandler ReBindData;

        private void Grid_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (this.SortOrderBy == e.SortExpression)
            {
                if (this.SortOrder == "ASC")
                {
                    this.SortOrder = "DESC";
                }
                else
                {
                    this.SortOrder = "ASC";
                }
            }
            else
            {
                this.SortOrderBy = e.SortExpression;
                this.SortOrder = "ASC";
            }
            if (this.ReBindData != null)
            {
                this.ReBindData(this);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.SortOrder = "ASC";
            this.SortOrderBy = "";
            this.SortAscIcon = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.asc.gif");
            this.SortDescIcon = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.desc.gif");
            this.DefaultSortIcon = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "ASPNET.WebControls.Grid.Images.por_arrow_13.gif");
            base.Sorting += new GridViewSortEventHandler(this.Grid_Sorting);
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
            if ((base.AllowSorting && (this.Controls.Count >= 0)) && ((this.Rows.Count > 0) && this.ShowOrderIcons))
            {
                int num = 0;
                foreach (DataControlField field in this.Columns)
                {
                    if (!string.IsNullOrEmpty(field.SortExpression))
                    {
                        Image child = new Image {
                            BorderWidth = Unit.Pixel(0),
                            Width = Unit.Pixel(13),
                            Height = Unit.Pixel(7)
                        };
                        if (field.SortExpression == this.SortOrderBy)
                        {
                            if (string.Compare("desc", this.SortOrder, true, CultureInfo.InvariantCulture) == 0)
                            {
                                child.ImageUrl = this.SortDescIcon;
                            }
                            else
                            {
                                child.ImageUrl = this.SortAscIcon;
                            }
                        }
                        else
                        {
                            child.ImageUrl = this.DefaultSortIcon;
                        }
                        this.Controls[0].Controls[0].Controls[num].Controls.AddAt(1, new LiteralControl("&nbsp;"));
                        this.Controls[0].Controls[0].Controls[num].Controls.AddAt(2, child);
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
                return (string) this.ViewState["SortOrder"];
            }
            set
            {
                this.ViewState["SortOrder"] = value;
            }
        }

        public string SortOrderBy
        {
            get
            {
                return (string) this.ViewState["SortOrderBy"];
            }
            set
            {
                this.ViewState["SortOrderBy"] = value;
            }
        }

        public delegate void ReBindDataEventHandler(object sender);
    }
}

