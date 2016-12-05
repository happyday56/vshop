namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VInvite : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litProduct;
        private VshopTemplatedRepeater rptInvitelist;
        private VshopTemplatedRepeater rptInviteComplatedList;
        private Literal litQRcode;
        private Literal litQRcode2;
        private Literal litD1;
        private Literal litD2;

        protected override void AttachChildControls()
        {
            //            this.Page.Response.Write("<script>AAA='" + this.Page.Request.Url.Host + ":"+this.Page.Request.Url.Port + "/vshop/invite_open.aspx?invitecode=" + "';bbb='" + Globals.ApplicationPath + "/vshop/invite_open.aspx?invitecode='</script>");
            this.rptInvitelist = (VshopTemplatedRepeater)this.FindControl("rptInvitelist");
            this.rptInviteComplatedList = (VshopTemplatedRepeater)this.FindControl("rptInviteComplatedList");
            this.litProduct = (Literal)this.FindControl("litProduct");
            this.litQRcode = (Literal)this.FindControl("litQRcode");
            this.litQRcode2 = (Literal)this.FindControl("litQRcode2");
            this.litD1 = (Literal)this.FindControl("litD1");
            this.litD2 = (Literal)this.FindControl("litD2");

            var currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null)
            {
                DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(currentMember.UserId);

                if (null != userIdDistributors && userIdDistributors.IsTempStore == 0)
                {
                    this.litD1.Text = "display:none;";
                    this.litD2.Text = "display:block;";

                    this.litQRcode.Text = "<a href=\"QRcode.aspx?PTTypeId=1&ReferralUserId=" + userIdDistributors.UserId + "\" class=\"btn\" >邀请店主</a>";
                    this.litQRcode2.Text = "<a href=\"QRcode.aspx?PTTypeId=2&ReferralUserId=" + userIdDistributors.UserId + "\" class=\"btn\" >邀请钻石会员</a>";

                    int lostNum = InviteBrowser.GetInvitationNum(currentMember.UserId);
                    DataTable disProduct = InviteBrowser.GetDistributorProduct();
                    if (disProduct != null && disProduct.Rows.Count > 0)
                    {
                        this.litProduct.Text = string.Format("{0}（<span id='loseNum' val='{1}'>{2}</span>）<input type='hidden' id='hdproduct' value='{3}'/>", disProduct.Rows[0]["ProductName"], lostNum, lostNum + "个限额", disProduct.Rows[0]["ProductId"]);
                    }

                    this.rptInvitelist.DataSource = InviteBrowser.GetInviteCodeList(currentMember.UserId);
                    this.rptInvitelist.DataBind();

                    this.rptInviteComplatedList.DataSource = InviteBrowser.GetInviteComplatedList(currentMember.UserId);
                    this.rptInviteComplatedList.DataBind();
                }
                else
                {
                    this.litD1.Text = "display:block;";
                    this.litD2.Text = "display:none;";
                }
            }
            else
            {
                this.Page.Response.Redirect("/Vshop/Default.aspx");
            }

            //this.rptProducts = (VshopTemplatedRepeater)this.FindControl("rptProducts");
            //this.litdescription = (Literal)this.FindControl("litdescription");
            //ProductQuery query = new ProductQuery();
            //int num = 0;
            //int num2 = 0;
            //if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CategoryId"]) && !string.IsNullOrEmpty(this.Page.Request.QueryString["ActivitiesId"]))
            //{
            //    if (!int.TryParse(this.Page.Request.QueryString["CategoryId"], out num))
            //    {
            //        this.Page.Response.Redirect("/Vshop/Default.aspx");
            //    }
            //    else
            //    {
            //        int.TryParse(this.Page.Request.QueryString["ActivitiesId"], out num2);
            //        query.CategoryId = new int?(num);
            //        DataTable activitie = ProductBrowser.GetActivitie(num2);
            //        if (activitie.Rows.Count > 0)
            //        {
            //            this.litdescription.Text = activitie.Rows[0]["ActivitiesDescription"].ToString();
            //            if ((this.rptProducts != null) && (query.CategoryId > 0))
            //            {
            //                query.PageSize = 20;
            //                query.PageIndex = 1;
            //                DbQueryResult homeProduct = ProductBrowser.GetHomeProduct(MemberProcessor.GetCurrentMember(), query);
            //                this.rptProducts.DataSource = homeProduct.Data;
            //                this.rptProducts.DataBind();
            //            }
            //        }
            //        else
            //        {
            //            this.Page.Response.Redirect("/Vshop/Default.aspx");
            //        }
            //    }
            //}
            PageTitle.AddSiteNameTitle("邀请");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vInvite.html";
            }
            base.OnInit(e);
        }
    }
}

