using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.UI.ControlPanel.Utility;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace Hidistro.UI.Web.Admin
{
	public class FileCategoryJson : AdminPage
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
			base.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
			if (Globals.GetCurrentManagerUserId() == 0)
			{
				base.Response.Write(JsonMapper.ToJson(hashtable));
				base.Response.End();
			}
			else
			{
				System.Collections.Generic.List<System.Collections.Hashtable> list = new System.Collections.Generic.List<System.Collections.Hashtable>();
				hashtable["category_list"] = list;
				foreach (System.Data.DataRow row in GalleryHelper.GetPhotoCategories().Rows)
				{
					System.Collections.Hashtable item = new System.Collections.Hashtable();
					item["cId"] = row["CategoryId"];
					item["cName"] = row["CategoryName"];
					list.Add(item);
				}
				base.Response.Write(JsonMapper.ToJson(hashtable));
				base.Response.End();
			}
		}
	}
}
