namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using ASPNET.WebControls;
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities.Store;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VFriendsCircle : VshopTemplatedWebControl
    {
        private Pager pager;
        private VshopTemplatedRepeater refriendscircle;

        protected override void AttachChildControls()
        {
            this.refriendscircle = (VshopTemplatedRepeater) this.FindControl("refriendscircle");
            this.pager = (Pager) this.FindControl("pager");
            this.refriendscircle.ItemDataBound += new RepeaterItemEventHandler(this.refriendscircle_ItemDataBound);
            this.BindData();
            PageTitle.AddSiteNameTitle("朋友圈素材");
        }

        private void BindData()
        {
            FriendExtensionQuery entity = new FriendExtensionQuery {
                PageIndex = this.pager.PageIndex,
                PageSize = this.pager.PageSize,
                SortOrder = SortAction.Desc,
                SortBy = "ExtensionId"
            };
            Globals.EntityCoding(entity, true);
            DbQueryResult result = VshopBrowser.FriendExtensionList(entity);
            this.refriendscircle.DataSource = result.Data;
            this.refriendscircle.DataBind();
            this.pager.TotalRecords = result.TotalRecords;
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VFriendsCircle.html";
            }
            base.OnInit(e);
        }

        private void refriendscircle_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal literal = (Literal) e.Item.Controls[0].FindControl("ImgPic");
                if (!string.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "ExensionImg").ToString()))
                {
                    string[] strArray = DataBinder.Eval(e.Item.DataItem, "ExensionImg").ToString().Split(new char[] { '|' });
                    string str = "";
                    foreach (string str2 in strArray)
                    {
                        if (!string.IsNullOrEmpty(str2))
                        {
                            string str3 = str;
                            str = str3 + "<div class=\"col-xs-6\"><img src='http://" + Globals.DomainName + str2 + "' width='150' height='150'/></div>";
                        }
                    }
                    literal.Text = str;
                }
            }
        }
    }
}

