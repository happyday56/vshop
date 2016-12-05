using ASPNET.WebControls;
using Hidistro.ControlPanel.Promotions;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.Promotions;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Components.Validation;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Coupons)]
	public class AddCoupon : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAddCoupons;
		protected WebCalendar calendarEndDate;
		protected WebCalendar calendarStartDate;
		protected System.Web.UI.WebControls.TextBox txtAmount;
		protected System.Web.UI.WebControls.TextBox txtCouponName;
		protected System.Web.UI.WebControls.TextBox txtDiscountValue;
		protected System.Web.UI.WebControls.TextBox txtNeedPoint;
		private void btnAddCoupons_Click(object sender, System.EventArgs e)
		{
			string msg = string.Empty;
			decimal? nullable;
			decimal num;
			int num2;
			if (this.ValidateValues(out nullable, out num, out num2))
			{
				if (!this.calendarStartDate.SelectedDate.HasValue)
				{
					this.ShowMsg("请选择开始日期！", false);
				}
				else
				{
					if (!this.calendarEndDate.SelectedDate.HasValue)
					{
						this.ShowMsg("请选择结束日期！", false);
					}
					else
					{
						if (this.calendarStartDate.SelectedDate.Value.CompareTo(this.calendarEndDate.SelectedDate.Value) >= 0)
						{
							this.ShowMsg("开始日期不能晚于结束日期！", false);
						}
						else
						{
							CouponInfo target = new CouponInfo
							{
								Name = this.txtCouponName.Text,
								ClosingTime = this.calendarEndDate.SelectedDate.Value,
								StartTime = this.calendarStartDate.SelectedDate.Value,
								Amount = nullable,
								DiscountValue = num,
								NeedPoint = num2
							};
							ValidationResults results = Validation.Validate<CouponInfo>(target, new string[]
							{
								"ValCoupon"
							});
							if (!results.IsValid)
							{
								using (System.Collections.Generic.IEnumerator<ValidationResult> enumerator = ((System.Collections.Generic.IEnumerable<ValidationResult>)results).GetEnumerator())
								{
									if (enumerator.MoveNext())
									{
										ValidationResult result = enumerator.Current;
										msg += Formatter.FormatErrorMessage(result.Message);
										this.ShowMsg(msg, false);
										return;
									}
								}
							}
							string lotNumber = string.Empty;
							CouponActionStatus status = CouponHelper.CreateCoupon(target, 0, out lotNumber);
							if (status != CouponActionStatus.UnknowError)
							{
								CouponActionStatus couponActionStatus = status;
								if (couponActionStatus != CouponActionStatus.DuplicateName)
								{
									if (couponActionStatus != CouponActionStatus.CreateClaimCodeError)
									{
										this.ShowMsg("添加优惠券成功", true);
										this.RestCoupon();
									}
									else
									{
										this.ShowMsg("生成优惠券号码错误", false);
									}
								}
								else
								{
									this.ShowMsg("已经存在相同的优惠券名称", false);
								}
							}
							else
							{
								this.ShowMsg("未知错误", false);
							}
						}
					}
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnAddCoupons.Click += new System.EventHandler(this.btnAddCoupons_Click);
		}
		private void RestCoupon()
		{
			this.txtCouponName.Text = string.Empty;
			this.txtAmount.Text = string.Empty;
			this.txtDiscountValue.Text = string.Empty;
		}
		private bool ValidateValues(out decimal? amount, out decimal discount, out int needPoint)
		{
			string str = string.Empty;
			amount = new decimal?(0m);
			if (!string.IsNullOrEmpty(this.txtAmount.Text.Trim()))
			{
				decimal num;
				if (decimal.TryParse(this.txtAmount.Text.Trim(), out num))
				{
					amount = new decimal?(num);
				}
				else
				{
					str += Formatter.FormatErrorMessage("满足金额必须为0-1000万之间");
				}
			}
			if (!int.TryParse(this.txtNeedPoint.Text.Trim(), out needPoint))
			{
				str += Formatter.FormatErrorMessage("兑换所需积分不能为空，大小0-10000之间");
			}
			if (!decimal.TryParse(this.txtDiscountValue.Text.Trim(), out discount))
			{
				str += Formatter.FormatErrorMessage("可抵扣金额必须在0.01-1000万之间");
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
