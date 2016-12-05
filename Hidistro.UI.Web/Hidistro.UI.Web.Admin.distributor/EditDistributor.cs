using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Members;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class EditDistributor : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected System.Web.UI.WebControls.DropDownList DrStatus;
		protected System.Web.UI.WebControls.TextBox txtStoreDescription;
		protected System.Web.UI.WebControls.TextBox txtStoreName;
        protected DistributorGradeDropDownList dropDistributorGrade;

		private int userid;
		private void Bind()
		{
            dropDistributorGrade.DataBind();

			DistributorsInfo userIdDistributors = VShopHelper.GetUserIdDistributors(int.Parse(this.Page.Request.QueryString["UserId"]));
			if (userIdDistributors != null)
			{
				this.txtStoreName.Text = userIdDistributors.StoreName;
				this.txtStoreDescription.Text = userIdDistributors.StoreDescription;
				this.DrStatus.SelectedValue = userIdDistributors.ReferralStatus.ToString();
                if (userIdDistributors.IsTempStore == 1)
                {
                    this.dropDistributorGrade.SelectedValue = -1;
                }
                else
                {
                    this.dropDistributorGrade.SelectedValue = userIdDistributors.DistriGradeId;
                }                
			}
			else
			{
				this.ShowMsg("分销商不存在！", false);
			}
		}
		private void btn_Submint(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtStoreName.Text.Trim()))
			{
				this.ShowMsg("店铺店不能为空", false);
			}
			else
			{
                SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);

				DistributorsInfo userIdDistributors = VShopHelper.GetUserIdDistributors(int.Parse(this.Page.Request.QueryString["UserId"]));
				if (userIdDistributors != null)
				{
					userIdDistributors.StoreName = this.txtStoreName.Text.Trim();
					userIdDistributors.StoreDescription = this.txtStoreDescription.Text.Trim();
					userIdDistributors.ReferralStatus = int.Parse(this.DrStatus.SelectedValue);

                    int tmpGradeId = (int)this.dropDistributorGrade.SelectedValue;

                    if (tmpGradeId == -1)
                    {
                        userIdDistributors.IsTempStore = 1;
                        userIdDistributors.DistriGradeId = int.Parse(siteSettings.DefaultStoreGradeId);
                    }
                    else
                    {
                        userIdDistributors.IsTempStore = 0;
                        userIdDistributors.DistriGradeId = (int)this.dropDistributorGrade.SelectedValue;
                    }                    

                    if (userIdDistributors.DistriGradeId.ToString().Equals(siteSettings.DefaultPartnerGradeId) || userIdDistributors.DistriGradeId.ToString().Equals(siteSettings.DefaultTutorGradeId))
                    {
                        userIdDistributors.InvitationNum = siteSettings.DefaultMaxInvitationNum;
                    }
                    else
                    {
                        userIdDistributors.InvitationNum = siteSettings.DefaultMinInvitationNum;
                    }
					if (DistributorsBrower.UpdateDistributor(userIdDistributors))
					{
						this.ShowMsg("店铺店修改成功", true);
					}
					else
					{
						this.ShowMsg("店铺店修改失败", false);
					}
				}
				else
				{
					this.ShowMsg("分销商不存在！", false);
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSubmit.Click += new System.EventHandler(this.btn_Submint);
			if (!base.IsPostBack)
			{
				if (int.TryParse(this.Page.Request.QueryString["UserId"], out this.userid))
				{
					this.Bind();
				}
				else
				{
					this.Page.Response.Redirect("DistributorList.aspx");
				}
			}
		}
	}
}
