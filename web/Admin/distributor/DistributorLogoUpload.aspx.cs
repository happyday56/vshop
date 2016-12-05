using Hidistro.Core;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Hidistro.UI.Web.Admin.distributor
{
	public partial class DistributorLogoUpload : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
            if (base.Request.QueryString["delimg"] != null)
			{
                string str = base.Server.HtmlEncode(base.Request.QueryString["delimg"]);
				str = base.Server.MapPath(str);
				if (File.Exists(str))
				{
					File.Delete(str);
				}
                base.Response.Write("0");
				base.Response.End();
			}
			Image image = null;
			Image image1 = null;
			Bitmap bitmap = null;
			Graphics graphic = null;
			MemoryStream memoryStream = null;
            int num = int.Parse(base.Request.QueryString["imgurl"]);
			try
			{
				try
				{
					if (num >= 1)
					{
                        base.Response.Write("0");
					}
					else
					{
                        HttpPostedFile item = base.Request.Files["Filedata"];
						DateTime now = DateTime.Now;
                        string str1 = now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo);
                        string str2 = "/Storage/data/DistributorLogoPic/";
						string str3 = string.Concat(str1, Path.GetExtension(item.FileName));
						item.SaveAs(Globals.MapPath(string.Concat(str2, str3)));
						base.Response.StatusCode = 200;
                        base.Response.Write(string.Concat(str1, "|/Storage/data/DistributorLogoPic/", str3));
					}
				}
				catch// (Exception exception)
				{
					base.Response.StatusCode = 500;
                    base.Response.Write("服务器错误");
					base.Response.End();
				}
			}
			finally
			{
				if (bitmap != null)
				{
					bitmap.Dispose();
				}
				if (graphic != null)
				{
					graphic.Dispose();
				}
				if (image1 != null)
				{
					image1.Dispose();
				}
				if (image != null)
				{
					image.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
				base.Response.End();
			}
		}
	
    }

}