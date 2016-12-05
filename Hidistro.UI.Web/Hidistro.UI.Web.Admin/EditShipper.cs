using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Components.Validation;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Shippers)]
	public class EditShipper : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnEditShipper;
		protected RegionSelector ddlReggion;
		private int shipperId;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.TextBox txtCellPhone;
		protected System.Web.UI.WebControls.TextBox txtRemark;
		protected System.Web.UI.WebControls.TextBox txtShipperName;
		protected System.Web.UI.WebControls.TextBox txtShipperTag;
		protected System.Web.UI.WebControls.TextBox txtTelPhone;
		protected System.Web.UI.WebControls.TextBox txtZipcode;
		private void btnEditShipper_Click(object sender, System.EventArgs e)
		{
			ShippersInfo shipper = new ShippersInfo
			{
				ShipperId = this.shipperId,
				ShipperTag = this.txtShipperTag.Text.Trim(),
				ShipperName = this.txtShipperName.Text.Trim()
			};
			if (!this.ddlReggion.GetSelectedRegionId().HasValue)
			{
				this.ShowMsg("请选择地区", false);
			}
			else
			{
				shipper.RegionId = this.ddlReggion.GetSelectedRegionId().Value;
				shipper.Address = this.txtAddress.Text.Trim();
				shipper.CellPhone = this.txtCellPhone.Text.Trim();
				shipper.TelPhone = this.txtTelPhone.Text.Trim();
				shipper.Zipcode = this.txtZipcode.Text.Trim();
				shipper.Remark = this.txtRemark.Text.Trim();
				if (this.ValidationShipper(shipper))
				{
					if (string.IsNullOrEmpty(shipper.CellPhone) && string.IsNullOrEmpty(shipper.TelPhone))
					{
						this.ShowMsg("手机号码和电话号码必填其一", false);
					}
					else
					{
						if (SalesHelper.UpdateShipper(shipper))
						{
							this.ShowMsg("成功修改了一个发货信息", true);
						}
						else
						{
							this.ShowMsg("修改发货信息失败", false);
						}
					}
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!int.TryParse(this.Page.Request.QueryString["ShipperId"], out this.shipperId))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				this.btnEditShipper.Click += new System.EventHandler(this.btnEditShipper_Click);
				if (!this.Page.IsPostBack)
				{
					ShippersInfo shipper = SalesHelper.GetShipper(this.shipperId);
					if (shipper == null)
					{
						base.GotoResourceNotFound();
					}
					else
					{
						Globals.EntityCoding(shipper, false);
						this.txtShipperTag.Text = shipper.ShipperTag;
						this.txtShipperName.Text = shipper.ShipperName;
						this.ddlReggion.SetSelectedRegionId(new int?(shipper.RegionId));
						this.txtAddress.Text = shipper.Address;
						this.txtCellPhone.Text = shipper.CellPhone;
						this.txtTelPhone.Text = shipper.TelPhone;
						this.txtZipcode.Text = shipper.Zipcode;
						this.txtRemark.Text = shipper.Remark;
					}
				}
			}
		}
		private bool ValidationShipper(ShippersInfo shipper)
		{
			ValidationResults results = Validation.Validate<ShippersInfo>(shipper, new string[]
			{
				"Valshipper"
			});
			string msg = string.Empty;
			if (!results.IsValid)
			{
				foreach (ValidationResult result in (System.Collections.Generic.IEnumerable<ValidationResult>)results)
				{
					msg += Formatter.FormatErrorMessage(result.Message);
				}
				this.ShowMsg(msg, false);
			}
			return results.IsValid;
		}
	}
}
