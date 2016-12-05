namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VChirldrenDistributors : VWeiXinOAuthTemplatedWebControl
    {
        private Panel onedistributor;
        private VshopTemplatedRepeater rpdistributor;
                
        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("我的团队");
            this.onedistributor = (Panel)this.FindControl("onedistributor");
            this.rpdistributor = (VshopTemplatedRepeater)this.FindControl("rpdistributor");

            this.Page.Session["stylestatus"] = "1";
            
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            DistributorsInfo currentDistributors = DistributorsBrower.GetCurrentDistributors(Globals.GetCurrentMemberUserId());

            if (null != currentDistributors)
            {
                // 获取下级店铺信息
                DataTable data = DistributorsBrower.GetDistributorAllBalanceByUserId(currentDistributors.UserId, masterSettings.DefaultPartnerGradeId, masterSettings.DefaultTutorGradeId, masterSettings.DefaultStoreGradeId, "");
                this.rpdistributor.DataSource = data;
                this.rpdistributor.DataBind();
            }
           
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VChirldrenDistributors.html";
            }
            base.OnInit(e);
        }
    }
}

