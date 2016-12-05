using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.IO;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class AddDistributorBackgroundPic : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.HiddenField hidpic;
		protected System.Web.UI.WebControls.HiddenField hidpicdel;
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			masterSettings.DistributorBackgroundPic = this.hidpic.Value;
			SettingsManager.Save(masterSettings);
			if (!string.IsNullOrEmpty(this.hidpicdel.Value))
			{
				string[] array = this.hidpicdel.Value.Split(new char[]
				{
					'|'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string str = array[i];
					string path = str;
					path = base.Server.MapPath(path);
					if (System.IO.File.Exists(path))
					{
						System.IO.File.Delete(path);
					}
				}
			}
			this.hidpicdel.Value = "";
			this.ShowMsg("修改成功", true);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			if (!base.IsPostBack)
			{
				SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
				this.hidpic.Value = masterSettings.DistributorBackgroundPic;
			}
		}
	}
}
