using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[AdministerCheck(true)]
	public class setShoppingScore : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.TextBox txtProductPointSet;
		protected System.Web.UI.HtmlControls.HtmlGenericControl txtProductPointSetTip;
		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			decimal num;
			if (!decimal.TryParse(this.txtProductPointSet.Text.Trim(), out num) || (this.txtProductPointSet.Text.Trim().Contains(".") && this.txtProductPointSet.Text.Trim().Substring(this.txtProductPointSet.Text.Trim().IndexOf(".") + 1).Length > 2))
			{
				this.ShowMsg("几元一积分不能为空,为非负数字,范围在0.1-10000000之间", false);
			}
			else
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				masterSettings.PointsRate = num;
				Globals.EntityCoding(masterSettings, true);
				SettingsManager.Save(masterSettings);
				this.ShowMsg("保存成功", true);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				this.txtProductPointSet.Text = masterSettings.PointsRate.ToString(System.Globalization.CultureInfo.InvariantCulture);
			}
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
		}
	}
}
