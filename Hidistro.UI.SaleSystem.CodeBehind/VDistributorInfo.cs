namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VDistributorInfo : VWeiXinOAuthTemplatedWebControl
    {
        private HtmlInputHidden hdbackimg;
        private HtmlInputHidden hdlogo;
        private HtmlImage imglogo;
        private Literal litBackImg;
        private HtmlInputText txtacctount;
        private HtmlTextArea txtdescription;
        private HtmlInputText txtstorename;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("店铺消息");
            this.imglogo = (HtmlImage) this.FindControl("imglogo");
            this.litBackImg = (Literal) this.FindControl("litBackImg");
            this.hdbackimg = (HtmlInputHidden) this.FindControl("hdbackimg");
            this.hdlogo = (HtmlInputHidden) this.FindControl("hdlogo");
            this.txtstorename = (HtmlInputText) this.FindControl("txtstorename");
            this.txtdescription = (HtmlTextArea) this.FindControl("txtdescription");
            this.txtacctount = (HtmlInputText) this.FindControl("txtacctount");
            DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(Globals.GetCurrentMemberUserId());
            if (userIdDistributors != null)
            {
                if (!string.IsNullOrEmpty(userIdDistributors.Logo))
                {
                    this.imglogo.Src = userIdDistributors.Logo;
                }
                this.hdbackimg.Value = userIdDistributors.BackImage;
                this.hdlogo.Value = userIdDistributors.Logo;
                this.txtstorename.Value = userIdDistributors.StoreName;
                this.txtdescription.Value = userIdDistributors.StoreDescription;
                this.txtacctount.Value = userIdDistributors.RequestAccount;
                int num = 0;
                if (!string.IsNullOrEmpty(userIdDistributors.BackImage))
                {
                    foreach (string str in userIdDistributors.BackImage.Split(new char[] { '|' }).ToList<string>())
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            num++;
                            if (!string.IsNullOrEmpty(str))
                            {
                                object text = this.litBackImg.Text;
                                this.litBackImg.Text = string.Concat(new object[] { text, " <div class=\"upFile clearfix\" style=\"float: left;\"><div class=\"bgImg\" ><img id=\"BakImg", num, "\" src=\"", str, "\" /></div><div class=\"adds\"><input id=\"BakFile", num, "\" name=\"BakFile", num, "\" type=\"file\"  onchange=\"return FileUpload_onselect('BakImg", num, "')\"/></div></div>" });
                            }
                        }
                    }
                }
                while (num < 4)
                {
                    num++;
                    object obj3 = this.litBackImg.Text;
                    this.litBackImg.Text = string.Concat(new object[] { obj3, "<div class=\"upFile clearfix\" style=\"float: left;\"><div class=\"bgImg\"><img id=\"BakImg", num, "\" src=\"http://fpoimg.com/100x100/product\" /></div><div class=\"adds\"><input id=\"BakFile", num, "\" name=\"BakFile", num, "\" type=\"file\" /></div></div>" });
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-DistributorInfo.html";
            }
            base.OnInit(e);
        }
    }
}

