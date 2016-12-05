using ASPNET.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using Hidistro.Entities.Sales;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.sales
{
	public class AddShippingTemplate : AdminPage
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
		protected System.Web.UI.WebControls.Button btnCreate;
		protected Grid grdRegion;
		protected RegionArea regionArea;
		protected Script Script1;
		protected Hidistro.UI.Common.Controls.Style Style1;
		protected System.Web.UI.WebControls.TextBox txtAddPrice;
		protected System.Web.UI.WebControls.TextBox txtAddRegionPrice;
		protected System.Web.UI.WebControls.TextBox txtAddWeight;
		protected System.Web.UI.WebControls.TextBox txtModeName;
		protected System.Web.UI.WebControls.TextBox txtPrice;
		protected System.Web.UI.HtmlControls.HtmlInputText txtRegion;
		protected System.Web.UI.WebControls.TextBox txtRegion_Id;
		protected System.Web.UI.WebControls.TextBox txtRegionPrice;
		protected System.Web.UI.WebControls.TextBox txtWeight;
		private System.Collections.Generic.IList<AddShippingTemplate.Region> RegionList
		{
			get
			{
				if (this.ViewState["Region"] == null)
				{
					this.ViewState["Region"] = new System.Collections.Generic.List<AddShippingTemplate.Region>();
				}
				return (System.Collections.Generic.IList<AddShippingTemplate.Region>)this.ViewState["Region"];
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
				AddShippingTemplate.Region item = new AddShippingTemplate.Region
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
		private void btnCreate_Click(object sender, System.EventArgs e)
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
					AddPrice = nullable2
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
				if (SalesHelper.CreateShippingTemplate(shippingMode))
				{
					if (!string.IsNullOrEmpty(this.Page.Request.QueryString["source"]) && this.Page.Request.QueryString["source"] == "add")
					{
						this.CloseWindow();
					}
					else
					{
						this.ClearControlValue();
						this.ShowMsg("成功添加了一个配送方式模板", true);
					}
				}
				else
				{
					this.ShowMsg("您添加的地区有重复", false);
				}
			}
		}
		private void ClearControlValue()
		{
			this.txtAddPrice.Text = string.Empty;
			this.txtAddRegionPrice.Text = string.Empty;
			this.txtAddWeight.Text = string.Empty;
			this.txtModeName.Text = string.Empty;
			this.txtPrice.Text = string.Empty;
			this.txtRegion.Value = string.Empty;
			this.txtRegion_Id.Text = string.Empty;
			this.txtRegionPrice.Text = string.Empty;
			this.txtWeight.Text = string.Empty;
		}
		private void grdRegion_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			this.RegionList.RemoveAt(e.RowIndex);
			this.BindRegion();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.grdRegion.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdRegion_RowDeleting);
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
				str += Formatter.FormatErrorMessage("默认起步价必须为非负数字,限制在1000万以内");
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
			string.IsNullOrEmpty(str);
			return true;
		}
	}
}
