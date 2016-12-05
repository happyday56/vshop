namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Hidistro.Entities.Commodities;
    using Hidistro.ControlPanel.Store;
    using Hidistro.Core.Entities;
    using Hidistro.Core;

    [ParseChildren(true)]
    public class VBusinessArticleList : VshopTemplatedWebControl
    {
        private VshopTemplatedRepeater rptArticles;

        protected override void AttachChildControls()
        {            
            this.rptArticles = (VshopTemplatedRepeater)this.FindControl("rptArticles");

            this.rptArticles.DataSource = VShopHelper.GetBusinessArticlesDT();
            this.rptArticles.DataBind();

            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            PageTitle.AddSiteNameTitle(masterSettings.SiteName + "商学院");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VBusinessArticleList.html";
            }
            base.OnInit(e);
        }
    }
}

