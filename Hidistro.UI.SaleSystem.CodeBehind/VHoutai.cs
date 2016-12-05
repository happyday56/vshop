namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VHoutai : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litBackground;
        private Literal litStoreName;
        private Literal litRealName;

        protected override void AttachChildControls()
        {
            this.litBackground = (Literal)this.FindControl("litBackground");
            this.litStoreName = (Literal)this.FindControl("litStoreName");
            this.litRealName = (Literal)this.FindControl("litRealName");
            var currentMember = MemberProcessor.GetCurrentMember();
            int currentMemberUserId = Globals.GetCurrentMemberUserId();
            if (currentMember != null)
            {

                DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(currentMemberUserId);
                if (userIdDistributors != null)
                {
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    this.litStoreName.Text = userIdDistributors.StoreName;
                    this.litRealName.Text = userIdDistributors.UserName;
                    string logo = "";
                    if (!string.IsNullOrEmpty(userIdDistributors.Logo))
                    {
                        logo = userIdDistributors.Logo;
                    }
                    else if (!string.IsNullOrEmpty(masterSettings.DistributorLogoPic))
                    {
                        logo = masterSettings.DistributorLogoPic.Split(new char[] { '|' })[0];
                    }

                    this.litBackground.Text = string.Format("<img src=\"{0}\" />", logo);
                }
            }
            else
            {
                this.Page.Response.Redirect("/Vshop/Default.aspx");
            }

            PageTitle.AddSiteNameTitle("店铺管理");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vhoutai.html";
            }
            base.OnInit(e);
        }
    }
}

