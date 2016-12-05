using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;
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
	public class AddShipper : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAddShipper;
		protected YesNoRadioButtonList chkIsDefault;
		protected RegionSelector ddlReggion;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.TextBox txtCellPhone;
		protected System.Web.UI.WebControls.TextBox txtRemark;
		protected System.Web.UI.WebControls.TextBox txtShipperName;
		protected System.Web.UI.WebControls.TextBox txtShipperTag;
		protected System.Web.UI.WebControls.TextBox txtTelPhone;
		protected System.Web.UI.WebControls.TextBox txtZipcode;
		private void btnAddShipper_Click(object sender, System.EventArgs e)
		{
			ShippersInfo shipper = new ShippersInfo
			{
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
				shipper.IsDefault = this.chkIsDefault.SelectedValue;
				shipper.Remark = this.txtRemark.Text.Trim();
				if (this.ValidationShipper(shipper))
				{
					if (string.IsNullOrEmpty(shipper.CellPhone) && string.IsNullOrEmpty(shipper.TelPhone))
					{
						this.ShowMsg("手机号码和电话号码必填其一", false);
					}
					else
					{
						if (SalesHelper.AddShipper(shipper))
						{
							this.ShowMsg("成功添加了一个发货信息", true);
						}
						else
						{
							this.ShowMsg("添加发货信息失败", false);
						}
					}
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.btnAddShipper.Click += new System.EventHandler(this.btnAddShipper_Click);
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
