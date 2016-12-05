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

    [ParseChildren(true)]
    public class VProductList : VshopTemplatedWebControl
    {
        private int categoryId;
        private HiImage imgUrl;
        private string keyWord;
        private Literal litContent;
        private VshopTemplatedRepeater rptCategories;
        private VshopTemplatedRepeater rptProducts;
        private HtmlInputHidden txtTotalPages;

        protected override void AttachChildControls()
        {
            int num;
            int num2;
            int num3;
            int.TryParse(this.Page.Request.QueryString["categoryId"], out this.categoryId);
            this.keyWord = this.Page.Request.QueryString["keyWord"];
            if (!string.IsNullOrWhiteSpace(this.keyWord))
            {
                this.keyWord = this.keyWord.Trim();
            }
            this.imgUrl = (HiImage) this.FindControl("imgUrl");
            this.litContent = (Literal) this.FindControl("litContent");
            this.rptProducts = (VshopTemplatedRepeater) this.FindControl("rptProducts");
            this.rptCategories = (VshopTemplatedRepeater) this.FindControl("rptCategories");
            this.txtTotalPages = (HtmlInputHidden) this.FindControl("txtTotal");
            string str = this.Page.Request.QueryString["sort"];
            if (string.IsNullOrWhiteSpace(str))
            {
                str = "DisplaySequence";
            }
            string str2 = this.Page.Request.QueryString["order"];
            if (string.IsNullOrWhiteSpace(str2))
            {
                str2 = "desc";
            }
            if (!int.TryParse(this.Page.Request.QueryString["page"], out num))
            {
                num = 1;
            }
            if (!int.TryParse(this.Page.Request.QueryString["size"], out num2))
            {
                num2 = 200;
            }
            IList<CategoryInfo> maxSubCategories = CategoryBrowser.GetMaxSubCategories(this.categoryId, 0x3e8);
            this.rptCategories.DataSource = maxSubCategories;
            this.rptCategories.DataBind();
            this.rptProducts.DataSource = ProductBrowser.GetProducts(MemberProcessor.GetCurrentMember(), null, new int?(this.categoryId), this.keyWord, num, num2, out num3, str, str2);
            this.rptProducts.DataBind();
            this.txtTotalPages.SetWhenIsNotNull(num3.ToString());
            PageTitle.AddSiteNameTitle("分类搜索页");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VProductList.html";
            }
            base.OnInit(e);
        }
    }
}

