namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [ParseChildren(true)]
    public class VMyRedPager : VWeiXinOAuthTemplatedWebControl
    {
        private VshopTemplatedRepeater rptRedPagerList;
        private HtmlInputHidden txtTotal;

        protected override void AttachChildControls()
        {
            int num2;
            int num3;
            string url = this.Page.Request.QueryString["returnUrl"];
            if (!string.IsNullOrWhiteSpace(this.Page.Request.QueryString["returnUrl"]))
            {
                this.Page.Response.Redirect(url);
            }
            string str2 = this.Page.Request.QueryString["status"];
            if (string.IsNullOrEmpty(str2))
            {
                str2 = "1";
            }
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            int num = 0;
            int.TryParse(str2, out num);
            this.rptRedPagerList = (VshopTemplatedRepeater) this.FindControl("rptRedPagerList");
            this.txtTotal = (HtmlInputHidden) this.FindControl("txtTotal");
            if (!int.TryParse(this.Page.Request.QueryString["page"], out num2))
            {
                num2 = 1;
            }
            if (!int.TryParse(this.Page.Request.QueryString["size"], out num3))
            {
                num3 = 20;
            }
            UserRedPagerQuery userredpagerquery = new UserRedPagerQuery {
                UserID = currentMember.UserId,
                IsCount = true,
                PageIndex = num2,
                PageSize = num3,
                SortBy = "RedPagerID",
                SortOrder = SortAction.Desc,
                Type = (UserRedPagerType) num
            };
            DbQueryResult userRedPagerList = UserRedPagerBrower.GetUserRedPagerList(userredpagerquery);
            this.rptRedPagerList.DataSource = userRedPagerList.Data;
            this.rptRedPagerList.DataBind();
            this.txtTotal.SetWhenIsNotNull(userRedPagerList.TotalRecords.ToString());
            PageTitle.AddSiteNameTitle("我的代金券");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vMyRedPager.html";
            }
            base.OnInit(e);
        }
    }
}

