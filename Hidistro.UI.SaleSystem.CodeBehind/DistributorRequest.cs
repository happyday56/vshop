namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using NewLife.Log;

    [ParseChildren(true)]
    public class DistributorRequest : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litBackImg;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("申请成为店主");

            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
            
            DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(MemberProcessor.GetCurrentMember().UserId);
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();

            this.Page.Session["stylestatus"] = "2";

            if (null != currentMember)
            {
                if ((userIdDistributors != null) && (userIdDistributors.UserId > 0))
                {
                    this.Page.Response.Redirect("DistributorCenter.aspx", true);
                }
                else
                {
                    //if (masterSettings.SiteFlag.EqualIgnoreCase("ls"))
                    //{
                        //if (!(currentMember.IsStore > 0))
                        //{
                            // 不能开店则提示
                            //base.GotoResourceNotFound("您还不是店主，请联系官方客服。");
                        //}
                    //}
                    //else
                    //{
                        if (DistributorsBrower.IsExistDistributor())
                        {
                            // 不能开店则提示
                            base.GotoResourceNotFound("您还不是店主，请联系官方客服。");
                        }
                    //}          
                }
            }
            else
            {
                base.GotoResourceNotFound("您还不是店主，请联系官方客服。");
            }

        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VDistributorRequest.html";
            }
            base.OnInit(e);
        }
    }
}

