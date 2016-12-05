using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.ExpressTemplates)]
	public class ExpressTemplates : AdminPage
	{
		protected Grid grdExpressTemplates;
		private void BindExpressTemplates()
		{
			this.grdExpressTemplates.DataSource = SalesHelper.GetExpressTemplates();
			this.grdExpressTemplates.DataBind();
		}
		private void DeleteXmlFile(string xmlfile)
		{
			string path = System.Web.HttpContext.Current.Request.MapPath(Globals.ApplicationPath + string.Format("/Storage/master/flex/{0}", xmlfile));
			if (System.IO.File.Exists(path))
			{
				XmlDocument document = new XmlDocument();
				document.Load(path);
				XmlNode node = document.SelectSingleNode("printer/pic");
				string str2 = System.Web.HttpContext.Current.Request.MapPath(Globals.ApplicationPath + string.Format("/Storage/master/flex/{0}", node.InnerText));
				if (System.IO.File.Exists(str2))
				{
					System.IO.File.Delete(str2);
				}
				System.IO.File.Delete(path);
			}
		}
		private void grdExpressTemplates_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			if (e.CommandName == "SetYesOrNo")
			{
				System.Web.UI.WebControls.GridViewRow namingContainer = (System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer;
				int expressId = (int)this.grdExpressTemplates.DataKeys[namingContainer.RowIndex].Value;
				SalesHelper.SetExpressIsUse(expressId);
				this.BindExpressTemplates();
			}
		}
		private void grdExpressTemplates_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			int expressId = (int)this.grdExpressTemplates.DataKeys[e.RowIndex].Value;
			if (SalesHelper.DeleteExpressTemplate(expressId))
			{
				System.Web.UI.WebControls.Literal literal = this.grdExpressTemplates.Rows[e.RowIndex].FindControl("litXmlFile") as System.Web.UI.WebControls.Literal;
				this.DeleteXmlFile(literal.Text);
				this.BindExpressTemplates();
				this.ShowMsg("已经成功删除选择的快递单模板", true);
			}
			else
			{
				this.ShowMsg("删除快递单模板失败", false);
			}
		}
		protected override void OnInitComplete(System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.grdExpressTemplates.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdExpressTemplates_RowDeleting);
			this.grdExpressTemplates.RowCommand += new System.Web.UI.WebControls.GridViewCommandEventHandler(this.grdExpressTemplates_RowCommand);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BindExpressTemplates();
			}
		}
	}
}
