using Hidistro.Core;
using Hidistro.UI.Common.Controls;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class AdminMaster : System.Web.UI.MasterPage
	{
		protected System.Web.UI.WebControls.ContentPlaceHolder contentHolder;
		protected System.Web.UI.WebControls.ContentPlaceHolder headHolder;
		protected System.Web.UI.WebControls.HyperLink hlinkAdminDefault;
		protected System.Web.UI.WebControls.HyperLink hlinkDefault;
		protected System.Web.UI.WebControls.HyperLink hlinkLogout;
		protected System.Web.UI.WebControls.HyperLink hlinkService;
		protected System.Web.UI.WebControls.Image imgLogo;
		protected System.Web.UI.WebControls.Label lblUserName;
		protected System.Web.UI.WebControls.Literal mainMenuHolder;
		protected PageTitle PageTitle1;
		protected Script Script1;
		protected Script Script2;
		protected Script Script3;
		protected Script Script4;
		protected Script Script5;
		protected Script Script6;
		protected Script Script7;
		protected System.Web.UI.WebControls.Literal subMenuHolder;
		protected System.Web.UI.HtmlControls.HtmlForm thisForm;
		protected System.Web.UI.WebControls.ContentPlaceHolder validateHolder;
		protected override void OnInit(System.EventArgs e)
		{
			base.OnInit(e);
			PageTitle.AddTitle(SettingsManager.GetMasterSettings(true).SiteName);
			foreach (System.Web.UI.Control control in this.Page.Header.Controls)
			{
				if (control is System.Web.UI.HtmlControls.HtmlLink)
				{
					System.Web.UI.HtmlControls.HtmlLink link = control as System.Web.UI.HtmlControls.HtmlLink;
					if (link.Href.StartsWith("/"))
					{
						link.Href = Globals.ApplicationPath + link.Href;
					}
					else
					{
						link.Href = Globals.ApplicationPath + "/" + link.Href;
					}
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}
	}
}
