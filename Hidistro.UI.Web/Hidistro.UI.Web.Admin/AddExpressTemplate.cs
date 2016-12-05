using Hidistro.ControlPanel.Store;
using Hidistro.Entities;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Data;
using System.Text;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.ExpressTemplates)]
	public class AddExpressTemplate : AdminPage
	{
		protected string ems = "";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				System.Data.DataTable expressTable = ExpressHelper.GetExpressTable();
				System.Text.StringBuilder builder = new System.Text.StringBuilder();
				foreach (System.Data.DataRow row in expressTable.Rows)
				{
					builder.AppendFormat("<option value='{0}'>{0}</option>", row["Name"]);
				}
				this.ems = builder.ToString();
			}
		}
	}
}
