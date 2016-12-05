using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using Hidistro.Entities;
using Hidistro.Entities.Sales;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.sales
{
	public class EditShippingTemplate : AdminPage
	{
		[System.Serializable]
		public class Region
		{
			private decimal regionAddPrice;
			private decimal regionPrice;
			private string regions;
			private string regionsId;
			public decimal RegionAddPrice
			{
				get
				{
					return this.regionAddPrice;
				}
				set
				{
					this.regionAddPrice = value;
				}
			}
			public decimal RegionPrice
			{
				get
				{
					return this.regionPrice;
				}
				set
				{
					this.regionPrice = value;
				}
			}
			public string Regions
			{
				get
				{
					return this.regions;
				}
				set
				{
					this.regions = value;
				}
			}
			public string RegionsId
			{
				get
				{
					return this.regionsId;
				}
				set
				{
					this.regionsId = value;
				}
			}
		}
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnUpdate;
		protected Grid grdRegion;
		protected RegionArea regionArea;
		protected Script Script1;
		protected Hidistro.UI.Common.Controls.Style Style1;
		private int templateId;
		protected System.Web.UI.WebControls.TextBox txtAddPrice;
		protected System.Web.UI.WebControls.TextBox txtAddRegionPrice;
		protected System.Web.UI.WebControls.TextBox txtAddWeight;
		protected System.Web.UI.WebControls.TextBox txtModeName;
		protected System.Web.UI.WebControls.TextBox txtPrice;
		protected System.Web.UI.HtmlControls.HtmlInputText txtRegion;
		protected System.Web.UI.WebControls.TextBox txtRegion_Id;
		protected System.Web.UI.WebControls.TextBox txtRegionPrice;
		protected System.Web.UI.WebControls.TextBox txtWeight;
		private System.Collections.Generic.IList<EditShippingTemplate.Region> RegionList
		{
			get
			{
				if (this.ViewState["Region"] == null)
				{
					this.ViewState["Region"] = new System.Collections.Generic.List<EditShippingTemplate.Region>();
				}
				return (System.Collections.Generic.IList<EditShippingTemplate.Region>)this.ViewState["Region"];
			}
		}
		private void BindControl(ShippingModeInfo modeItem)
		{
			this.txtModeName.Text = Globals.HtmlDecode(modeItem.Name);
			this.txtWeight.Text = modeItem.Weight.ToString("F2");
			this.txtAddWeight.Text = modeItem.AddWeight.Value.ToString("F2");
			if (modeItem.AddPrice.HasValue)
			{
				this.txtAddPrice.Text = modeItem.AddPrice.Value.ToString("F2");
			}
			this.txtPrice.Text = modeItem.Price.ToString("F2");
			this.RegionList.Clear();
			if (modeItem.ModeGroup != null && modeItem.ModeGroup.Count > 0)
			{
				foreach (ShippingModeGroupInfo info in modeItem.ModeGroup)
				{
					EditShippingTemplate.Region item = new EditShippingTemplate.Region
					{
						RegionPrice = decimal.Parse(info.Price.ToString("F2")),
						RegionAddPrice = decimal.Parse(info.AddPrice.ToString("F2"))
					};
					System.Text.StringBuilder builder = new System.Text.StringBuilder();
					System.Text.StringBuilder builder2 = new System.Text.StringBuilder();
					foreach (ShippingRegionInfo info2 in info.ModeRegions)
					{
						builder.Append(info2.RegionId + ",");
						builder2.Append(RegionHelper.GetFullRegion(info2.RegionId, ",") + ",");
					}
					if (!string.IsNullOrEmpty(builder.ToString()))
					{
						item.RegionsId = builder.ToString().Substring(0, builder.ToString().Length - 1);
					}
					if (!string.IsNullOrEmpty(builder2.ToString()))
					{
						item.Regions = builder2.ToString().Substring(0, builder2.ToString().Length - 1);
					}
					this.RegionList.Add(item);
				}
			}
		}
		private void BindRegion()
		{
			this.grdRegion.DataSource = this.RegionList;
			this.grdRegion.DataBind();
		}
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			decimal num;
			decimal num2;
			if (this.ValidateValues(out num, out num2))
			{
				EditShippingTemplate.Region item = new EditShippingTemplate.Region
				{
					RegionsId = this.txtRegion_Id.Text,
					Regions = this.txtRegion.Value,
					RegionPrice = num,
					RegionAddPrice = num2
				};
				this.RegionList.Add(item);
				this.BindRegion();
				this.txtRegion_Id.Text = string.Empty;
				this.txtRegion.Value = string.Empty;
				this.txtRegionPrice.Text = "0";
				this.txtAddRegionPrice.Text = "0";
			}
		}
		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			decimal num;
			decimal? nullable;
			decimal num2;
			decimal? nullable2;
			if (this.ValidateRegionValues(out num, out nullable, out num2, out nullable2))
			{
				new System.Collections.Generic.List<ShippingModeGroupInfo>();
				ShippingModeInfo shippingMode = new ShippingModeInfo
				{
					Name = Globals.HtmlEncode(this.txtModeName.Text.Trim()),
					Weight = num,
					AddWeight = nullable,
					Price = num2,
					AddPrice = nullable2,
					TemplateId = this.templateId
				};
				foreach (System.Web.UI.WebControls.GridViewRow row in this.grdRegion.Rows)
				{
					decimal result = 0m;
					decimal num3 = 0m;
					decimal.TryParse(((System.Web.UI.WebControls.TextBox)row.FindControl("txtModeRegionPrice")).Text, out result);
					decimal.TryParse(((System.Web.UI.WebControls.TextBox)row.FindControl("txtModeRegionAddPrice")).Text, out num3);
					ShippingModeGroupInfo item = new ShippingModeGroupInfo
					{
						Price = result,
						AddPrice = num3
					};
					System.Web.UI.WebControls.TextBox box = (System.Web.UI.WebControls.TextBox)this.grdRegion.Rows[row.RowIndex].FindControl("txtRegionvalue_Id");
					if (!string.IsNullOrEmpty(box.Text))
					{
						string[] array = box.Text.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string str = array[i];
							ShippingRegionInfo info3 = new ShippingRegionInfo
							{
								RegionId = System.Convert.ToInt32(str.Trim())
							};
							item.ModeRegions.Add(info3);
						}
					}
					shippingMode.ModeGroup.Add(item);
				}
				if (SalesHelper.UpdateShippingTemplate(shippingMode))
				{
					this.Page.Response.Redirect("EditShippingTemplate.aspx?TemplateId=" + shippingMode.TemplateId + "&isUpdate=true");
				}
				else
				{
					this.ShowMsg("您添加的地区有重复", false);
				}
			}
		}
		private void grdRegion_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			this.RegionList.RemoveAt(e.RowIndex);
			this.BindRegion();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!int.TryParse(this.Page.Request.QueryString["TemplateId"], out this.templateId))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
				this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
				this.grdRegion.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdRegion_RowDeleting);
				if (!this.Page.IsPostBack)
				{
					if (this.Page.Request.QueryString["isUpdate"] != null && this.Page.Request.QueryString["isUpdate"] == "true")
					{
						this.ShowMsg("成功修改了一个配送方式", true);
					}
					ShippingModeInfo shippingTemplate = SalesHelper.GetShippingTemplate(this.templateId, true);
					if (shippingTemplate == null)
					{
						base.GotoResourceNotFound();
					}
					else
					{
						this.BindControl(shippingTemplate);
						this.BindRegion();
					}
				}
			}
		}
		private bool ValidateRegionValues(out decimal weight, out decimal? addWeight, out decimal price, out decimal? addPrice)
		{
			string str = string.Empty;
			addWeight = new decimal?(0m);
			addPrice = new decimal?(0m);
			if (!decimal.TryParse(this.txtWeight.Text.Trim(), out weight))
			{
				str += Formatter.FormatErrorMessage("起步重量不能为空,必须为正整数,限制在100千克以内");
			}
			if (!string.IsNullOrEmpty(this.txtAddWeight.Text.Trim()))
			{
				decimal num;
				if (decimal.TryParse(this.txtAddWeight.Text.Trim(), out num))
				{
					addWeight = new decimal?(num);
				}
				else
				{
					str += Formatter.FormatErrorMessage("加价重量不能为空,必须为正整数,限制在100千克以内");
				}
			}
			if (!decimal.TryParse(this.txtPrice.Text.Trim(), out price))
			{
				str += Formatter.FormatErrorMessage("默认起步价不能为空,必须为非负数字,限制在1000万以内");
			}
			if (!string.IsNullOrEmpty(this.txtAddPrice.Text.Trim()))
			{
				decimal num2;
				if (decimal.TryParse(this.txtAddPrice.Text.Trim(), out num2))
				{
					addPrice = new decimal?(num2);
				}
				else
				{
					str += Formatter.FormatErrorMessage("默认加价必须为非负数字,限制在1000万以内");
				}
			}
			bool result;
			if (!string.IsNullOrEmpty(str))
			{
				this.ShowMsg(str, false);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}
		private bool ValidateValues(out decimal regionPrice, out decimal addRegionPrice)
		{
			string str = string.Empty;
			if (string.IsNullOrEmpty(this.txtRegion_Id.Text))
			{
				str += Formatter.FormatErrorMessage("到达地不能为空");
			}
			if (string.IsNullOrEmpty(this.txtRegionPrice.Text))
			{
				str += Formatter.FormatErrorMessage("起步价不能为空");
				regionPrice = 0m;
			}
			else
			{
				if (!decimal.TryParse(this.txtRegionPrice.Text.Trim(), out regionPrice))
				{
					str += Formatter.FormatErrorMessage("起步价只能为非负数字");
				}
				else
				{
					if (decimal.Parse(this.txtRegionPrice.Text.Trim()) > 10000000m)
					{
						str += Formatter.FormatErrorMessage("起步价限制在1000万以内");
					}
				}
			}
			if (string.IsNullOrEmpty(this.txtAddRegionPrice.Text))
			{
				str += Formatter.FormatErrorMessage("加价不能为空");
				addRegionPrice = 0m;
			}
			else
			{
				if (!decimal.TryParse(this.txtAddRegionPrice.Text.Trim(), out addRegionPrice))
				{
					str += Formatter.FormatErrorMessage("加价只能为非负数字");
				}
				else
				{
					if (decimal.Parse(this.txtAddRegionPrice.Text.Trim()) > 10000000m)
					{
						str += Formatter.FormatErrorMessage("加价限制在1000万以内");
					}
				}
			}
			bool result;
			if (!string.IsNullOrEmpty(str))
			{
				this.ShowMsg(str, false);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}
	}
}
