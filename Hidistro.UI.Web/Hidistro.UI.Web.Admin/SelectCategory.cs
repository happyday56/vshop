using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.AddProducts)]
	public class SelectCategory : AdminPage
	{
		private void DoCallback()
		{
			string str = base.Request.QueryString["action"];
			base.Response.Clear();
			base.Response.ContentType = "application/json";
			if (str.Equals("getlist"))
			{
				int result = 0;
				int.TryParse(base.Request.QueryString["parentCategoryId"], out result);
				System.Collections.Generic.IList<CategoryInfo> categories = (result == 0) ? CatalogHelper.GetMainCategories() : CatalogHelper.GetSubCategories(result);
				if (categories == null || categories.Count == 0)
				{
					base.Response.Write("{\"Status\":\"0\"}");
				}
				else
				{
					base.Response.Write(this.GenerateJson(categories));
				}
			}
			else
			{
				if (str.Equals("getinfo"))
				{
					int num2 = 0;
					int.TryParse(base.Request.QueryString["categoryId"], out num2);
					if (num2 <= 0)
					{
						base.Response.Write("{\"Status\":\"0\"}");
					}
					else
					{
						CategoryInfo category = CatalogHelper.GetCategory(num2);
						if (category == null)
						{
							base.Response.Write("{\"Status\":\"0\"}");
						}
						else
						{
							base.Response.Write(string.Concat(new string[]
							{
								"{\"Status\":\"OK\", \"Name\":\"",
								category.Name,
								"\", \"Path\":\"",
								category.Path,
								"\"}"
							}));
						}
					}
				}
			}
			base.Response.End();
		}
		private string GenerateJson(System.Collections.Generic.IList<CategoryInfo> categories)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			builder.Append("{");
			builder.Append("\"Status\":\"OK\",");
			builder.Append("\"Categories\":[");
			foreach (CategoryInfo info in categories)
			{
				builder.Append("{");
				builder.AppendFormat("\"CategoryId\":\"{0}\",", info.CategoryId.ToString(System.Globalization.CultureInfo.InvariantCulture));
				builder.AppendFormat("\"HasChildren\":\"{0}\",", info.HasChildren ? "true" : "false");
				builder.AppendFormat("\"CategoryName\":\"{0}\"", info.Name);
				builder.Append("},");
			}
			builder.Remove(builder.Length - 1, 1);
			builder.Append("]}");
			return builder.ToString();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request.QueryString["isCallback"]) && base.Request.QueryString["isCallback"] == "true")
			{
				this.DoCallback();
			}
		}
	}
}
