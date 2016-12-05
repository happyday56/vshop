using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin
{
	[AdministerCheck(true)]
	public partial class setSharePromotion : AdminPage
	{


		public setSharePromotion()
		{
		}

		protected void btnOK_Click(object sender, EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			masterSettings.GoodsPic = this.upgoods.UploadedImageUrl;
			masterSettings.GoodsName = this.txtGoodsName.Text;
			masterSettings.GoodsDescription = this.txtGoodsDec.Text;
			masterSettings.ShopHomePic = this.upshop.UploadedImageUrl;
			masterSettings.ShopHomeName = this.txtShopName.Text;
			masterSettings.ShopHomeDescription = this.txtShopDec.Text;
			masterSettings.ShopSpreadingCodePic = this.upSharp.UploadedImageUrl;
			masterSettings.ShopSpreadingCodeName = this.txtSharpName.Text;
			masterSettings.ShopSpreadingCodeDescription = this.txtSharpDec.Text;
			Globals.EntityCoding(masterSettings, true);
			SettingsManager.Save(masterSettings);
            this.ShowMsg("±£´æ³É¹¦", true);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				this.upgoods.UploadedImageUrl = masterSettings.GoodsPic;
				this.txtGoodsName.Text = masterSettings.GoodsName;
				this.txtGoodsDec.Text = masterSettings.GoodsDescription;
				this.upshop.UploadedImageUrl = masterSettings.ShopHomePic;
				this.txtShopName.Text = masterSettings.ShopHomeName;
				this.txtShopDec.Text = masterSettings.ShopHomeDescription;
				this.upSharp.UploadedImageUrl = masterSettings.ShopSpreadingCodePic;
				this.txtSharpName.Text = masterSettings.ShopSpreadingCodeName;
				this.txtSharpDec.Text = masterSettings.ShopSpreadingCodeDescription;
			}
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
		}
	}
}