using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities;
using Hidistro.Entities.Members;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Members)]
	public class MemberDetails : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnEdit;
		private int currentUserId;
		protected System.Web.UI.WebControls.Literal lblUserLink;
		protected System.Web.UI.WebControls.Literal litAddress;
		protected System.Web.UI.WebControls.Literal litCellPhone;
		protected System.Web.UI.WebControls.Literal litCreateDate;
		protected System.Web.UI.WebControls.Literal litEmail;
		protected System.Web.UI.WebControls.Literal litGrade;
		protected System.Web.UI.WebControls.Literal litOpenId;
		protected System.Web.UI.WebControls.Literal litQQ;
		protected System.Web.UI.WebControls.Literal litRealName;
		protected System.Web.UI.WebControls.Literal litUserName;
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			base.Response.Redirect(Globals.GetAdminAbsolutePath("/member/EditMember.aspx?userId=" + this.Page.Request.QueryString["userId"]), true);
		}
		private void LoadMemberInfo()
		{
			MemberInfo member = MemberHelper.GetMember(this.currentUserId);
			if (member == null)
			{
				base.GotoResourceNotFound();
			}
			else
			{
				Uri url = System.Web.HttpContext.Current.Request.Url;
				string str = (url.Port == 80) ? string.Empty : (":" + url.Port.ToString(System.Globalization.CultureInfo.InvariantCulture));
				this.lblUserLink.Text = string.Concat(new object[]
				{
					string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}://{1}{2}", new object[]
					{
						url.Scheme,
						SettingsManager.GetMasterSettings(true).SiteUrl,
						str
					}),
					Globals.ApplicationPath,
					"/?ReferralUserId=",
					member.UserId
				});
				this.litUserName.Text = member.UserName;
				this.litGrade.Text = MemberHelper.GetMemberGrade(member.GradeId).Name;
				this.litCreateDate.Text = member.CreateDate.ToString();
				this.litRealName.Text = member.RealName;
				this.litAddress.Text = RegionHelper.GetFullRegion(member.RegionId, "") + member.Address;
				this.litQQ.Text = member.QQ;
				this.litCellPhone.Text = member.CellPhone;
				this.litEmail.Text = member.Email;
				this.litOpenId.Text = member.OpenId;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!int.TryParse(this.Page.Request.QueryString["userId"], out this.currentUserId))
			{
				base.GotoResourceNotFound();
			}
			else
			{
				this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
				if (!this.Page.IsPostBack)
				{
					this.LoadMemberInfo();
				}
			}
		}
	}
}
