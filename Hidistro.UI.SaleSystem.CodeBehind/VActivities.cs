namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [ParseChildren(true)]
    public class VActivities : VshopTemplatedWebControl
    {
        private VshopTemplatedRepeater rpActivityList;

        protected override void AttachChildControls()
        {
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null)
            {
                rpActivityList = (VshopTemplatedRepeater)this.FindControl("rpActivityList");
                if (rpActivityList != null)
                {
                    rpActivityList.DataSource = VshopBrowser.GetActivityList(currentMember.UserId);
                    rpActivityList.DataBind();
                }

            }
            PageTitle.AddSiteNameTitle("参与活动");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VActivities.html";
            }
            base.OnInit(e);
        }
    }
}

