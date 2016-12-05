using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.AddProducts)]
	public class AddProductComplete : AdminPage
	{
		private int categoryId;
		protected System.Web.UI.WebControls.HyperLink hlinkAddProduct;
		protected System.Web.UI.WebControls.HyperLink hlinkProductDetails;
		private int productId;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!int.TryParse(base.Request.QueryString["categoryId"], out this.categoryId))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				if (!int.TryParse(base.Request.QueryString["productId"], out this.productId))
				{
					base.GotoResourceNotFound();
				}
				else
				{
					if (!this.Page.IsPostBack)
					{
						this.hlinkProductDetails.NavigateUrl = Globals.GetAdminAbsolutePath(string.Format("../Vshop/ProductDetails.aspx?productId={0}", this.productId));
						this.hlinkAddProduct.NavigateUrl = Globals.GetAdminAbsolutePath(string.Format("/product/AddProduct.aspx?categoryId={0}", this.categoryId));
					}
				}
			}
		}
	}
}
