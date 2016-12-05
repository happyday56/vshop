using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using Hidistro.UI.Web.Admin.product.ascx;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.product
{
	[PrivilegeCheck(Privilege.AddProductType)]
	public class AddSpecification : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnFilish;
		protected SpecificationView specificationView;
		private void btnFilish_Click(object server, System.EventArgs e)
		{
			base.Response.Redirect(Globals.GetAdminAbsolutePath("/product/ProductTypes.aspx"), true);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			int num;
			if (!string.IsNullOrEmpty(base.Request["isCallback"]) && base.Request["isCallback"] == "true" && int.TryParse(base.Request["ValueId"], out num))
			{
				if (ProductTypeHelper.DeleteAttributeValue(num))
				{
					if (!string.IsNullOrEmpty(base.Request["ImageUrl"]))
					{
						StoreHelper.DeleteImage(base.Request["ImageUrl"]);
						base.Response.Clear();
						base.Response.ContentType = "application/json";
						base.Response.Write("{\"Status\":\"true\"}");
						base.Response.End();
					}
					else
					{
						base.Response.Clear();
						base.Response.ContentType = "application/json";
						base.Response.Write("{\"Status\":\"true\"}");
						base.Response.End();
					}
				}
				else
				{
					base.Response.Clear();
					base.Response.ContentType = "application/json";
					base.Response.Write("{\"Status\":\"false\"}");
					base.Response.End();
				}
			}
			if (!string.IsNullOrEmpty(base.Request["isAjax"]) && base.Request["isAjax"] == "true")
			{
				string str = base.Request["Mode"].ToString();
				string str2 = "false";
				string str3 = str;
				if (str3 != null)
				{
					if (!(str3 == "Add"))
					{
						if (str3 == "AddSkuItemValue")
						{
							string str4 = "参数缺少";
							int num2;
							if (int.TryParse(base.Request["AttributeId"], out num2))
							{
								str4 = "规格值名不允许为空！";
								if (!string.IsNullOrEmpty(base.Request["ValueName"].ToString()))
								{
									string str5 = Globals.HtmlEncode(base.Request["ValueName"].ToString().Replace("+", "").Replace(",", ""));
									str4 = "规格值名长度不允许超过15个字符";
									if (str5.Length < 15)
									{
										AttributeValueInfo attributeValue = new AttributeValueInfo
										{
											ValueStr = str5,
											AttributeId = num2
										};
										str4 = "添加规格值失败";
										int num3 = ProductTypeHelper.AddAttributeValue(attributeValue);
										if (num3 > 0)
										{
											str4 = num3.ToString();
											str2 = "true";
										}
									}
								}
							}
							base.Response.Clear();
							base.Response.ContentType = "application/json";
							base.Response.Write(string.Concat(new string[]
							{
								"{\"Status\":\"",
								str2,
								"\",\"msg\":\"",
								str4,
								"\"}"
							}));
							base.Response.End();
						}
					}
					else
					{
						int num2 = 0;
						string str4 = "参数缺少";
						if (int.TryParse(base.Request["AttributeId"], out num2))
						{
							str4 = "属性名称不允许为空！";
							if (!string.IsNullOrEmpty(base.Request["ValueName"].ToString()))
							{
								string str6 = Globals.HtmlEncode(base.Request["ValueName"].ToString());
								AttributeValueInfo info = new AttributeValueInfo
								{
									ValueStr = str6,
									AttributeId = num2
								};
								str4 = "添加属性值失败";
								int num4 = ProductTypeHelper.AddAttributeValue(info);
								if (num4 > 0)
								{
									str4 = num4.ToString();
									str2 = "true";
								}
							}
						}
						base.Response.Clear();
						base.Response.ContentType = "application/json";
						base.Response.Write(string.Concat(new string[]
						{
							"{\"Status\":\"",
							str2,
							"\",\"msg\":\"",
							str4,
							"\"}"
						}));
						base.Response.End();
					}
				}
			}
			this.btnFilish.Click += new System.EventHandler(this.btnFilish_Click);
		}
	}
}
