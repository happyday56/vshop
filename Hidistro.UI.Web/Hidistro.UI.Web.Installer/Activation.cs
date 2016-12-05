using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Installer
{
	public class Activation : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnInstall;
		protected System.Web.UI.HtmlControls.HtmlForm form1;
		protected System.Web.UI.WebControls.Label lblErrMessage;
		protected System.Web.UI.HtmlControls.HtmlInputText txtcode;
		protected void btnInstall_Click(object sender, System.EventArgs e)
		{
			string str = this.txtcode.Value;
			if (!string.IsNullOrEmpty(str) && this.CheckCode(str))
			{
				base.Response.Redirect("Install.aspx");
			}
			else
			{
				this.lblErrMessage.Text = "对不起，您的激活码错误！";
			}
		}
		internal bool CheckCode(string code)
		{
			bool result;
			if (!string.IsNullOrEmpty(code) && code.Length == 6)
			{
				string path = System.Web.HttpContext.Current.Server.MapPath("~/config/code.db3");
				try
				{
					using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
					{
						string str2 = reader.ReadToEnd();
						reader.Close();
						result = str2.Contains(code);
						return result;
					}
				}
				catch
				{
				}
			}
			result = false;
			return result;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}
	}
}
