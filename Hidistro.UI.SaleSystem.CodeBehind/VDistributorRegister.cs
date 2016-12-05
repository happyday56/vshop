using Hidistro.UI.Common.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Hidistro.UI.SaleSystem.CodeBehind
{
    [ParseChildren(true), WeiXinOAuth(Common.Controls.WeiXinOAuthPage.VDistributorRegister)]
    public class VDistributorRegister : VWeiXinOAuthTemplatedWebControl
    {
        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("分销商注册");
        }

        protected override void OnInit(EventArgs e)
        {
            if (null == this.SkinName)
            {
                this.SkinName = "skin-VDistributorRegister.html";
            }
            base.OnInit(e);
        }
    }
}
