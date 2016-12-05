using Hidistro.Core;
using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
namespace Hidistro.UI.Web.Admin
{
	public class UploadFile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			System.Web.HttpFileCollection files = base.Request.Files;
			if (files.Count > 0)
			{
				string str = System.Web.HttpContext.Current.Request.MapPath(Globals.ApplicationPath + "/Storage/master/flex");
				System.Web.HttpPostedFile file = files[0];
				string str2 = System.IO.Path.GetExtension(file.FileName).ToLower();
				if (str2 != ".jpg" && str2 != ".gif" && str2 != ".jpeg" && str2 != ".png" && str2 != ".bmp")
				{
					base.Response.Write("1");
				}
				else
				{
					string s = System.DateTime.Now.ToString("yyyyMMdd") + new System.Random().Next(10000, 99999).ToString(System.Globalization.CultureInfo.InvariantCulture) + str2;
					string filename = str + "/" + s;
					try
					{
						file.SaveAs(filename);
						base.Response.Write(s);
					}
					catch
					{
						base.Response.Write("0");
					}
				}
			}
			else
			{
				base.Response.Write("2");
			}
		}
	}
}
