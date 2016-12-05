using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.store
{
	[AdministerCheck(true)]
	public class Backup : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnBackup;
		private void btnBackup_Click(object sender, System.EventArgs e)
		{
			string str = StoreHelper.BackupData();
			if (!string.IsNullOrEmpty(str))
			{
				string fileName = System.Web.HttpContext.Current.Request.MapPath(Globals.ApplicationPath + "/Storage/data/Backup/" + str);
				System.IO.FileInfo info = new System.IO.FileInfo(fileName);
				if (StoreHelper.InserBackInfo(str, "2.2", info.Length))
				{
					this.ShowMsg("备份数据成功", true);
				}
				else
				{
					System.IO.File.Delete(fileName);
					this.ShowMsg("备份数据失败，可能是同时备份的人太多，请重试", false);
				}
			}
			else
			{
				this.ShowMsg("备份数据失败，可能是您的数据库服务器和web服务器不是同一台服务器", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
		}
	}
}
