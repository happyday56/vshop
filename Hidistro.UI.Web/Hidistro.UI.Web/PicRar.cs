using Hidistro.Core;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
namespace Hidistro.UI.Web
{
	public class PicRar : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlForm form1;
		public string label_html = string.Empty;
		public static bool IsNumeric(string strNumeric)
		{
			Regex regex = new Regex("^\\d+$");
			return regex.Match(strNumeric).Success;
		}
		public static bool IsQueryString(string strQuery)
		{
			return PicRar.IsQueryString(strQuery, "N");
		}
		public static bool IsQueryString(string strQuery, string Q)
		{
			bool flag = false;
			bool result;
			if (Q != null)
			{
				if (!(Q == "N"))
				{
					if (Q == "S" && System.Web.HttpContext.Current.Request.QueryString[strQuery] != null && System.Web.HttpContext.Current.Request.QueryString[strQuery].ToString() != string.Empty)
					{
						flag = true;
					}
					result = flag;
					return result;
				}
				if (System.Web.HttpContext.Current.Request.QueryString[strQuery] != null && PicRar.IsNumeric(System.Web.HttpContext.Current.Request.QueryString[strQuery].ToString()))
				{
					flag = true;
				}
			}
			result = flag;
			return result;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				string virtualPath = string.Empty;
				int maxWidth = 0;
				int maxHeight = 0;
				if (PicRar.IsQueryString("P", "S"))
				{
					virtualPath = base.Request.QueryString["P"];
				}
				if (PicRar.IsQueryString("W"))
				{
					maxWidth = int.Parse(base.Request.QueryString["W"]);
				}
				if (PicRar.IsQueryString("H"))
				{
					maxHeight = int.Parse(base.Request.QueryString["H"]);
				}
				if (virtualPath != string.Empty)
				{
					PIC pic = new PIC();
					if (!virtualPath.StartsWith("/"))
					{
						virtualPath = "/" + virtualPath;
					}
					virtualPath = Globals.ApplicationPath + virtualPath;
					pic.SendSmallImage(base.Request.MapPath(virtualPath), maxHeight, maxWidth);
					string watermarkFilename = base.Request.MapPath(Globals.ApplicationPath + "/Admin/images/watermark.gif");
					System.IO.MemoryStream stream = pic.AddImageSignPic(pic.OutBMP, watermarkFilename, 9, 60, 4);
					pic.Dispose();
					base.Response.ClearContent();
					base.Response.ContentType = "image/gif";
					base.Response.BinaryWrite(stream.ToArray());
					base.Response.End();
					stream.Dispose();
				}
			}
			catch (System.Exception exception)
			{
				this.label_html = exception.Message;
			}
		}
	}
}
