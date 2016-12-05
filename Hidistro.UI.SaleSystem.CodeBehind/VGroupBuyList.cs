namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Hidistro.Entities.Commodities;

    public class VGroupBuyList : VshopTemplatedWebControl
    {
        private int categoryId;
        private HiImage imgUrl;
        private string keyWord;
        private Literal litContent;
        private VshopTemplatedRepeater rptCategories;
        private VshopTemplatedRepeater rptProducts;
        private HtmlInputHidden txtTotal;

        protected override void AttachChildControls()
        {
            int num;
            int num2;
            int num3;
            int.TryParse(this.Page.Request.QueryString["categoryId"], out this.categoryId);
            this.keyWord = this.Page.Request.QueryString["keyWord"];
            this.imgUrl = (HiImage) this.FindControl("imgUrl");
            this.litContent = (Literal) this.FindControl("litContent");
            this.rptProducts = (VshopTemplatedRepeater) this.FindControl("rptGroupBuyProducts");
            this.txtTotal = (HtmlInputHidden) this.FindControl("txtTotal");
            this.rptCategories = (VshopTemplatedRepeater) this.FindControl("rptCategories");
            if (this.rptCategories != null)
            {
                IList<CategoryInfo> maxSubCategories = CategoryBrowser.GetMaxSubCategories(this.categoryId, 0x3e8);
                this.rptCategories.DataSource = maxSubCategories;
                this.rptCategories.DataBind();
            }
            if (!int.TryParse(this.Page.Request.QueryString["page"], out num))
            {
                num = 1;
            }
            if (!int.TryParse(this.Page.Request.QueryString["size"], out num2))
            {
                num2 = 10;
            }
            this.rptProducts.DataSource = GroupBuyBrowser.GetGroupBuyProducts(new int?(this.categoryId), this.keyWord, num, num2, out num3, true);
            this.rptProducts.DataBind();
            this.txtTotal.SetWhenIsNotNull(num3.ToString());
            PageTitle.AddSiteNameTitle("团购搜索页");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VGroupBuyList.html";
            }
            base.OnInit(e);
        }
    }
}

