using ASPNET.WebControls;
using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.Members;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.ClientGroup)]
	public class ClientSet : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSaveClientSettings;
		protected WebCalendar calendarEndDate;
		protected WebCalendar calendarStartDate;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton radioactivymoney;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton radioactivyorder;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton radnewday;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton radnewtime;
		protected System.Web.UI.HtmlControls.HtmlSelect slsactivymoney;
		protected System.Web.UI.HtmlControls.HtmlSelect slsactivymoneychar;
		protected System.Web.UI.HtmlControls.HtmlSelect slsactivyorder;
		protected System.Web.UI.HtmlControls.HtmlSelect slsactivyorderchar;
		protected System.Web.UI.HtmlControls.HtmlSelect slsnewregist;
		protected System.Web.UI.HtmlControls.HtmlSelect slssleep;
		protected System.Web.UI.HtmlControls.HtmlInputText txtactivymoney;
		protected System.Web.UI.HtmlControls.HtmlInputText txtactivyorder;
		protected void btnSaveClientSettings_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.Dictionary<int, MemberClientSet> clientset = new System.Collections.Generic.Dictionary<int, MemberClientSet>();
			MemberClientSet set = new MemberClientSet();
			if (this.radnewtime.Checked)
			{
				set.ClientTypeId = 1;
				if (this.calendarStartDate.SelectedDate.HasValue)
				{
					set.StartTime = new System.DateTime?(this.calendarStartDate.SelectedDate.Value);
				}
				if (this.calendarEndDate.SelectedDate.HasValue)
				{
					set.EndTime = new System.DateTime?(this.calendarEndDate.SelectedDate.Value);
				}
			}
			else
			{
				set.ClientTypeId = 2;
				set.LastDay = int.Parse(this.slsnewregist.Value);
			}
			clientset.Add(set.ClientTypeId, set);
			set = new MemberClientSet();
			if (this.radioactivyorder.Checked)
			{
				set.ClientTypeId = 6;
				set.LastDay = int.Parse(this.slsactivyorder.Value);
				set.ClientChar = this.slsactivyorderchar.Value;
				if (!string.IsNullOrEmpty(this.txtactivyorder.Value))
				{
					set.ClientValue = decimal.Parse(this.txtactivyorder.Value);
				}
			}
			else
			{
				set.ClientTypeId = 7;
				set.LastDay = int.Parse(this.slsactivymoney.Value);
				set.ClientChar = this.slsactivymoneychar.Value;
				if (!string.IsNullOrEmpty(this.txtactivymoney.Value))
				{
					set.ClientValue = decimal.Parse(this.txtactivymoney.Value);
				}
			}
			clientset.Add(set.ClientTypeId, set);
			set = new MemberClientSet
			{
				ClientTypeId = 11,
				LastDay = int.Parse(this.slssleep.Value)
			};
			clientset.Add(set.ClientTypeId, set);
			if (MemberHelper.InsertClientSet(clientset))
			{
				this.ShowMsg("保存成功", true);
			}
			else
			{
				this.ShowMsg("保存失败", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSaveClientSettings.Click += new System.EventHandler(this.btnSaveClientSettings_Click);
			if (!base.IsPostBack)
			{
				System.Collections.Generic.Dictionary<int, MemberClientSet> memberClientSet = MemberHelper.GetMemberClientSet();
				if (memberClientSet.Count == 3)
				{
					foreach (int num in memberClientSet.Keys)
					{
						int num2 = num;
						switch (num2)
						{
						case 1:
							if (memberClientSet[num].StartTime.HasValue)
							{
								this.calendarStartDate.SelectedDate = new System.DateTime?(memberClientSet[num].StartTime.Value.Date);
							}
							if (memberClientSet[num].EndTime.HasValue)
							{
								this.calendarEndDate.SelectedDate = new System.DateTime?(memberClientSet[num].EndTime.Value.Date);
							}
							break;
						case 2:
							this.slsnewregist.Value = memberClientSet[num].LastDay.ToString();
							this.radnewday.Checked = true;
							break;
						case 3:
						case 4:
						case 5:
							break;
						case 6:
							this.slsactivyorder.Value = memberClientSet[num].LastDay.ToString();
							this.slsactivyorderchar.Value = memberClientSet[num].ClientChar.ToString();
							this.txtactivyorder.Value = memberClientSet[num].ClientValue.ToString("F0");
							break;
						case 7:
							this.slsactivymoney.Value = memberClientSet[num].LastDay.ToString();
							this.slsactivymoneychar.Value = memberClientSet[num].ClientChar.ToString();
							this.txtactivymoney.Value = memberClientSet[num].ClientValue.ToString("F2");
							this.radioactivymoney.Checked = true;
							break;
						default:
							if (num2 == 11)
							{
								this.slssleep.Value = memberClientSet[num].LastDay.ToString();
							}
							break;
						}
					}
				}
			}
		}
	}
}
