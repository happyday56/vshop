namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [ParseChildren(true)]
    public class VUserInfo : VshopTemplatedWebControl
    {
        protected override void AttachChildControls()
        {
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null)
            {
                HtmlInputText control = (HtmlInputText) this.FindControl("txtUserName");
                HtmlInputText text2 = (HtmlInputText) this.FindControl("txtRealName");
                HtmlInputText text3 = (HtmlInputText) this.FindControl("txtPhone");
                HtmlInputText text4 = (HtmlInputText) this.FindControl("txtEmail");
                control.SetWhenIsNotNull(currentMember.UserName);
                text2.SetWhenIsNotNull(currentMember.RealName);
                text3.SetWhenIsNotNull(currentMember.CellPhone);
                text4.SetWhenIsNotNull(currentMember.QQ);
            }
            PageTitle.AddSiteNameTitle("修改用户信息");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VUserInfo.html";
            }
            base.OnInit(e);
        }
    }
}

