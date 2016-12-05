using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.Members;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Components.Validation;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.AddMemberGrade)]
	public class AddMemberGrade : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSubmitMemberRanks;
		protected YesNoRadioButtonList chkIsDefault;
		protected System.Web.UI.WebControls.TextBox txtPoint;
		protected System.Web.UI.WebControls.TextBox txtRankDesc;
		protected System.Web.UI.WebControls.TextBox txtRankName;
		protected System.Web.UI.WebControls.TextBox txtValue;
		private void btnSubmitMemberRanks_Click(object sender, System.EventArgs e)
		{
			string str = string.Empty;
			if (this.txtValue.Text.Trim().Contains("."))
			{
				this.ShowMsg("折扣必须为正整数", false);
			}
			else
			{
				MemberGradeInfo memberGrade = new MemberGradeInfo
				{
					Name = this.txtRankName.Text.Trim(),
					Description = this.txtRankDesc.Text.Trim()
				};
				int num;
				if (int.TryParse(this.txtPoint.Text.Trim(), out num))
				{
					memberGrade.Points = num;
				}
				else
				{
					str += Formatter.FormatErrorMessage("积分设置无效或不能为空，必须大于等于0的整数");
				}
				memberGrade.IsDefault = this.chkIsDefault.SelectedValue;
				int num2;
				if (int.TryParse(this.txtValue.Text, out num2))
				{
					memberGrade.Discount = num2;
				}
				else
				{
					str += Formatter.FormatErrorMessage("等级折扣设置无效或不能为空，等级折扣必须在1-10之间");
				}
				if (!string.IsNullOrEmpty(str))
				{
					this.ShowMsg(str, false);
				}
				else
				{
					if (this.ValidationMemberGrade(memberGrade))
					{
						if (MemberHelper.HasSamePointMemberGrade(memberGrade))
						{
							this.ShowMsg("已经存在相同积分的等级，每个会员等级的积分不能相同", false);
						}
						else
						{
							if (MemberHelper.CreateMemberGrade(memberGrade))
							{
								this.ShowMsg("成功添加了一个会员等级", true);
							}
							else
							{
								this.ShowMsg("添加会员等级失败", false);
							}
						}
					}
				}
			}
		}
		protected override void OnInitComplete(System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.btnSubmitMemberRanks.Click += new System.EventHandler(this.btnSubmitMemberRanks_Click);
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}
		private bool ValidationMemberGrade(MemberGradeInfo memberGrade)
		{
			ValidationResults results = Validation.Validate<MemberGradeInfo>(memberGrade, new string[]
			{
				"ValMemberGrade"
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
