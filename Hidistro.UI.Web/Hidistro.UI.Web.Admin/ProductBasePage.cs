using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Members;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Members;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
namespace Hidistro.UI.Web.Admin
{
	public class ProductBasePage : AdminPage
	{
		protected void DoCallback()
		{
			base.Response.Clear();
			base.Response.ContentType = "application/json";
			string str = base.Request.QueryString["action"];
			if (str.Equals("getPrepareData"))
			{
				int typeId = int.Parse(base.Request.QueryString["typeId"]);
				System.Collections.Generic.IList<AttributeInfo> attributes = ProductTypeHelper.GetAttributes(typeId);
				System.Data.DataTable brandCategoriesByTypeId = ProductTypeHelper.GetBrandCategoriesByTypeId(typeId);
				if (brandCategoriesByTypeId.Rows.Count == 0)
				{
					brandCategoriesByTypeId = CatalogHelper.GetBrandCategories();
				}
				base.Response.Write(this.GenerateJsonString(attributes, brandCategoriesByTypeId));
				attributes.Clear();
			}
			else
			{
				if (str.Equals("getMemberGradeList"))
				{
					System.Collections.Generic.IList<MemberGradeInfo> memberGrades = MemberHelper.GetMemberGrades();
					if (memberGrades == null || memberGrades.Count == 0)
					{
						base.Response.Write("{\"Status\":\"0\"}");
					}
					else
					{
						System.Text.StringBuilder builder = new System.Text.StringBuilder();
						builder.Append("{\"Status\":\"OK\",\"MemberGrades\":[");
						foreach (MemberGradeInfo info in memberGrades)
						{
							builder.Append("{");
							builder.AppendFormat("\"GradeId\":\"{0}\",", info.GradeId);
							builder.AppendFormat("\"Name\":\"{0}\",", info.Name);
							builder.AppendFormat("\"Discount\":\"{0}\"", info.Discount);
							builder.Append("},");
						}
						builder.Remove(builder.Length - 1, 1);
						builder.Append("]}");
						base.Response.Write(builder.ToString());
					}
				}
			}
			base.Response.End();
		}
		protected string DownRemotePic(string productDescrip)
		{
			SettingsManager.GetMasterSettings(true);
			string str = string.Format("/Storage/master/gallery/{0}/", System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString());
			string path = System.Web.HttpContext.Current.Request.MapPath(Globals.ApplicationPath + str);
			if (!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}
			System.Collections.Generic.IList<string> outsiteLinkImgs = this.GetOutsiteLinkImgs(productDescrip);
			if (outsiteLinkImgs.Count > 0)
			{
				foreach (string str2 in outsiteLinkImgs)
				{
					WebClient client = new WebClient();
					string str3 = System.Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.InvariantCulture);
					string str4 = str2.Substring(str2.LastIndexOf('.'));
					try
					{
						client.DownloadFile(str2, path + str3 + str4);
						productDescrip = productDescrip.Replace(str2, Globals.ApplicationPath + str + str3 + str4);
					}
					catch
					{
					}
				}
			}
			return productDescrip;
		}
		private string GenerateBrandString(System.Data.DataTable tb)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			foreach (System.Data.DataRow row in tb.Rows)
			{
				builder.Append("{");
				builder.AppendFormat("\"BrandId\":\"{0}\",\"BrandName\":\"{1}\"", row["BrandID"], row["BrandName"]);
				builder.Append("},");
			}
			builder.Remove(builder.Length - 1, 1);
			return builder.ToString();
		}
		private string GenerateJsonString(System.Collections.Generic.IList<AttributeInfo> attributes, System.Data.DataTable tbBrandCategories)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			System.Text.StringBuilder builder2 = new System.Text.StringBuilder();
			System.Text.StringBuilder builder3 = new System.Text.StringBuilder();
			System.Text.StringBuilder builder4 = new System.Text.StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (attributes != null && attributes.Count > 0)
			{
				builder2.Append("\"Attributes\":[");
				builder3.Append("\"SKUs\":[");
				foreach (AttributeInfo info in attributes)
				{
					if (info.UsageMode == AttributeUseageMode.Choose)
					{
						flag2 = true;
						builder3.Append("{");
						builder3.AppendFormat("\"Name\":\"{0}\",", info.AttributeName);
						builder3.AppendFormat("\"AttributeId\":\"{0}\",", info.AttributeId.ToString(System.Globalization.CultureInfo.InvariantCulture));
						builder3.AppendFormat("\"UseAttributeImage\":\"{0}\",", info.UseAttributeImage ? 1 : 0);
						builder3.AppendFormat("\"SKUValues\":[{0}]", this.GenerateValueItems(info.AttributeValues));
						builder3.Append("},");
					}
					else
					{
						if (info.UsageMode == AttributeUseageMode.View || info.UsageMode == AttributeUseageMode.MultiView)
						{
							flag = true;
							builder2.Append("{");
							builder2.AppendFormat("\"Name\":\"{0}\",", info.AttributeName);
							builder2.AppendFormat("\"AttributeId\":\"{0}\",", info.AttributeId.ToString(System.Globalization.CultureInfo.InvariantCulture));
							builder2.AppendFormat("\"UsageMode\":\"{0}\",", ((int)info.UsageMode).ToString());
							builder2.AppendFormat("\"AttributeValues\":[{0}]", this.GenerateValueItems(info.AttributeValues));
							builder2.Append("},");
						}
					}
				}
				if (builder2.Length > 14)
				{
					builder2.Remove(builder2.Length - 1, 1);
				}
				if (builder3.Length > 8)
				{
					builder3.Remove(builder3.Length - 1, 1);
				}
				builder2.Append("]");
				builder3.Append("]");
			}
			if (tbBrandCategories != null && tbBrandCategories.Rows.Count > 0)
			{
				flag3 = true;
				builder4.AppendFormat("\"BrandCategories\":[{0}]", this.GenerateBrandString(tbBrandCategories));
			}
			builder.Append("{\"HasAttribute\":\"" + flag.ToString() + "\",");
			builder.Append("\"HasSKU\":\"" + flag2.ToString() + "\",");
			builder.Append("\"HasBrandCategory\":\"" + flag3.ToString() + "\",");
			if (flag)
			{
				builder.Append(builder2.ToString()).Append(",");
			}
			if (flag2)
			{
				builder.Append(builder3.ToString()).Append(",");
			}
			if (flag3)
			{
				builder.Append(builder4.ToString()).Append(",");
			}
			builder.Remove(builder.Length - 1, 1);
			builder.Append("}");
			return builder.ToString();
		}
		private string GenerateValueItems(System.Collections.Generic.IList<AttributeValueInfo> values)
		{
			string result;
			if (values == null || values.Count == 0)
			{
				result = string.Empty;
			}
			else
			{
				System.Text.StringBuilder builder = new System.Text.StringBuilder();
				foreach (AttributeValueInfo info in values)
				{
					builder.Append("{");
					builder.AppendFormat("\"ValueId\":\"{0}\",\"ValueStr\":\"{1}\"", info.ValueId.ToString(System.Globalization.CultureInfo.InvariantCulture), info.ValueStr);
					builder.Append("},");
				}
				builder.Remove(builder.Length - 1, 1);
				result = builder.ToString();
			}
			return result;
		}
		protected System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<int>> GetAttributes(string attributesXml)
		{
			XmlDocument document = new XmlDocument();
			System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<int>> dictionary = null;
			System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<int>> result;
			try
			{
				document.LoadXml(attributesXml);
				XmlNodeList list = document.SelectNodes("//item");
				if (list == null || list.Count == 0)
				{
					result = null;
					return result;
				}
				dictionary = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<int>>();
				foreach (XmlNode node in list)
				{
					int key = int.Parse(node.Attributes["attributeId"].Value);
					System.Collections.Generic.IList<int> list2 = new System.Collections.Generic.List<int>();
					foreach (XmlNode node2 in node.ChildNodes)
					{
						if (node2.Attributes["valueId"].Value != "")
						{
							list2.Add(int.Parse(node2.Attributes["valueId"].Value));
						}
					}
					if (list2.Count > 0)
					{
						dictionary.Add(key, list2);
					}
				}
			}
			catch
			{
			}
			result = dictionary;
			return result;
		}
		protected void GetMemberPrices(SKUItem sku, string xml)
		{
			if (!string.IsNullOrEmpty(xml))
			{
				XmlDocument document = new XmlDocument();
				document.LoadXml(xml);
				foreach (XmlNode node in document.DocumentElement.SelectNodes("//grande"))
				{
					if (!string.IsNullOrEmpty(node.Attributes["price"].Value) && node.Attributes["price"].Value.Trim().Length != 0)
					{
						sku.MemberPrices.Add(int.Parse(node.Attributes["id"].Value), decimal.Parse(node.Attributes["price"].Value.Trim()));
					}
				}
			}
		}
		private System.Collections.Generic.IList<string> GetOutsiteLinkImgs(string html)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			System.Collections.Generic.IList<string> list = new System.Collections.Generic.List<string>();
			MatchCollection matchs = new Regex("(src)[^>]*[^/].(?:jpg|bmp|gif|png)(?:\"|') ", RegexOptions.IgnoreCase).Matches(html);
			for (int i = 0; i < matchs.Count; i++)
			{
				string item = matchs[i].Value.Replace("\\", "").Replace("\"", "").Replace("'", "").Trim().Substring(4);
				if (item.ToLower(System.Globalization.CultureInfo.InvariantCulture).IndexOf(masterSettings.SiteUrl.ToLower(System.Globalization.CultureInfo.InvariantCulture)) == -1 && item.ToLower(System.Globalization.CultureInfo.InvariantCulture).IndexOf(masterSettings.SiteUrl.ToLower(System.Globalization.CultureInfo.InvariantCulture)) == -1)
				{
					list.Add(item);
				}
			}
			return list;
		}
		protected System.Collections.Generic.Dictionary<string, SKUItem> GetSkus(string skusXml)
		{
			XmlDocument document = new XmlDocument();
			System.Collections.Generic.Dictionary<string, SKUItem> dictionary = null;
			System.Collections.Generic.Dictionary<string, SKUItem> result;
			try
			{
				document.LoadXml(skusXml);
				XmlNodeList list = document.SelectNodes("//item");
				if (list == null || list.Count == 0)
				{
					result = null;
				}
				else
				{
					dictionary = new System.Collections.Generic.Dictionary<string, SKUItem>();
					foreach (XmlNode node in list)
					{
						SKUItem item = new SKUItem
						{
							SKU = node.Attributes["skuCode"].Value,
							SalePrice = decimal.Parse(node.Attributes["salePrice"].Value),
							CostPrice = (node.Attributes["costPrice"].Value.Length > 0) ? decimal.Parse(node.Attributes["costPrice"].Value) : 0m,
							Stock = int.Parse(node.Attributes["qty"].Value),
							Weight = (node.Attributes["weight"].Value.Length > 0) ? decimal.Parse(node.Attributes["weight"].Value) : 0m
						};
						string str = "";
						foreach (XmlNode node2 in node.SelectSingleNode("skuFields").ChildNodes)
						{
							str = str + node2.Attributes["valueId"].Value + "_";
							item.SkuItems.Add(int.Parse(node2.Attributes["attributeId"].Value), int.Parse(node2.Attributes["valueId"].Value));
						}
						XmlNode node3 = node.SelectSingleNode("memberPrices");
						if (node3 != null && node3.ChildNodes.Count > 0)
						{
							foreach (XmlNode node4 in node3.ChildNodes)
							{
								if (!string.IsNullOrEmpty(node4.Attributes["price"].Value) && node4.Attributes["price"].Value.Trim().Length != 0)
								{
									item.MemberPrices.Add(int.Parse(node4.Attributes["id"].Value), decimal.Parse(node4.Attributes["price"].Value.Trim()));
								}
							}
						}
						item.SkuId = str.Substring(0, str.Length - 1);
						dictionary.Add(item.SkuId, item);
					}
					result = dictionary;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
