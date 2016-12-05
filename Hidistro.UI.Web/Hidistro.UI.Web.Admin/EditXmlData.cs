using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
namespace Hidistro.UI.Web.Admin
{
	public class EditXmlData : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlForm form1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string str = base.Request.Form["xmlname"];
			string s = base.Request.Form["xmldata"];
			string str2 = base.Request.Form["expressname"];
			string str3 = base.Request.Form["expressid"];
			if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str3))
			{
				int result = 0;
				if (int.TryParse(str3, out result) && SalesHelper.UpdateExpressTemplate(result, str2))
				{
					System.IO.FileStream stream = new System.IO.FileStream(System.Web.HttpContext.Current.Request.MapPath(Globals.ApplicationPath + string.Format("/Storage/master/flex/{0}", str)), System.IO.FileMode.Create);
					byte[] bytes = new System.Text.UTF8Encoding().GetBytes(s);
					stream.Write(bytes, 0, bytes.Length);
					stream.Flush();
					stream.Close();
				}
			}
		}
	}
}
