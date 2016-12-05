using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Xml;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.ExpressTemplates)]
	public class EditExpressTemplate : AdminPage
	{
		protected string ems = "";
		protected string height = "";
		protected string width = "";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			int result = 0;
			string str = this.Page.Request.QueryString["ExpressName"];
			string str2 = this.Page.Request.QueryString["XmlFile"];
			if (!int.TryParse(this.Page.Request.QueryString["ExpressId"], out result))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2) || !str2.EndsWith(".xml"))
				{
					base.GotoResourceNotFound();
				}
				else
				{
					if (!base.IsPostBack)
					{
						System.Data.DataTable expressTable = ExpressHelper.GetExpressTable();
						XmlDocument document = new XmlDocument();
						document.Load(System.Web.HttpContext.Current.Request.MapPath(Globals.ApplicationPath + string.Format("/Storage/master/flex/{0}", str2)));
						string innerText = document.SelectSingleNode("/printer/size").InnerText;
						this.width = innerText.Split(new char[]
						{
							':'
						})[0];
						this.height = innerText.Split(new char[]
						{
							':'
						})[1];
						System.Text.StringBuilder builder = new System.Text.StringBuilder();
						foreach (System.Data.DataRow row in expressTable.Rows)
						{
							builder.AppendFormat("<option value='{0}' {1}>{0}</option>", row["Name"], row["Name"].Equals(str) ? "selected" : "");
						}
						this.ems = builder.ToString();
					}
				}
			}
		}
	}
}
