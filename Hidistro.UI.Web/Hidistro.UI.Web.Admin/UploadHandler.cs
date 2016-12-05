using Hidistro.Core;
using System;
using System.Globalization;
using System.IO;
using System.Web;
namespace Hidistro.UI.Web.Admin
{
	public class UploadHandler : System.Web.IHttpHandler
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
		private void DeleteImage()
		{
			string path = System.Web.HttpContext.Current.Request.Form["del"];
			string str2 = Globals.PhysicalPath(path);
			try
			{
				if (System.IO.File.Exists(str2))
				{
					System.IO.File.Delete(str2);
					System.Web.HttpContext.Current.Response.Write("true");
				}
			}
			catch (System.Exception)
			{
				System.Web.HttpContext.Current.Response.Write("false");
			}
		}
		public void ProcessRequest(System.Web.HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			string text = context.Request["action"];
			if (text != null)
			{
				if (text == "upload")
				{
					this.UploadImage();
					return;
				}
				if (text == "delete")
				{
					this.DeleteImage();
					return;
				}
			}
			context.Response.Write("false");
		}
		private void UploadImage()
		{
			try
			{
				System.Web.HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["Filedata"];
				string str = System.DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
				string str2 = System.Web.HttpContext.Current.Request["uploadpath"];
				string str3 = str + System.IO.Path.GetExtension(file.FileName);
				if (string.IsNullOrEmpty(str2))
				{
					str2 = Globals.GetVshopSkinPath(null) + "/images/ad/";
					str3 = "imgCustomBg" + System.IO.Path.GetExtension(file.FileName);
					string[] files = System.IO.Directory.GetFiles(Globals.MapPath(str2), "imgCustomBg.*");
					for (int i = 0; i < files.Length; i++)
					{
						string str4 = files[i];
						System.IO.File.Delete(str4);
					}
				}
				if (!System.IO.Directory.Exists(Globals.MapPath(str2)))
				{
					System.IO.Directory.CreateDirectory(Globals.MapPath(str2));
				}
				file.SaveAs(Globals.MapPath(str2 + str3));
				System.Web.HttpContext.Current.Response.Write(str2 + str3);
			}
			catch (System.Exception)
			{
				System.Web.HttpContext.Current.Response.Write("服务器错误");
				System.Web.HttpContext.Current.Response.End();
			}
		}
	}
}
