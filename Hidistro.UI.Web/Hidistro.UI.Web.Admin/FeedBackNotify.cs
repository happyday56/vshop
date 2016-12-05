using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Weixin.Pay;
using System;
using System.Globalization;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class FeedBackNotify : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnSearchButton;
		protected System.Web.UI.WebControls.DataList dlstPtReviews;
		protected ProductCategoriesDropDownList dropCategories;
		protected PageSize hrefPageSize;
		protected Pager pager;
		protected Pager pager1;
		protected MemberGradeDropDownList rankList;
		protected System.Web.UI.WebControls.TextBox txtSearchText;
		protected System.Web.UI.WebControls.TextBox txtSKU;
		private void BindPtReview()
		{
			string msgType = "";
			switch (this.rankList.SelectedIndex)
			{
			case 1:
				msgType = "未处理";
				break;
			case 2:
				msgType = "已处理";
				break;
			}
			DbQueryResult result = VShopHelper.GetFeedBacks(this.pager.PageIndex, this.pager.PageSize, msgType);
			this.dlstPtReviews.DataSource = result.Data;
			this.dlstPtReviews.DataBind();
			this.pager.TotalRecords = result.TotalRecords;
			this.pager1.TotalRecords = result.TotalRecords;
		}
		private void btnSearchButton_Click(object sender, System.EventArgs e)
		{
			this.BindPtReview();
		}
		private void dlstPtReviews_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			int id = System.Convert.ToInt32(e.CommandArgument, System.Globalization.CultureInfo.InvariantCulture);
			if (e.CommandName == "Delete")
			{
				if (VShopHelper.DeleteFeedBack(id))
				{
					this.ShowMsg("删除成功", true);
					this.BindPtReview();
				}
				else
				{
					this.ShowMsg("删除失败", false);
				}
			}
			else
			{
				FeedBackInfo feedBack = VShopHelper.GetFeedBack(id);
				if (feedBack != null)
				{
					SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
					PayAccount account = new PayAccount(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret, masterSettings.WeixinPartnerID, masterSettings.WeixinPartnerKey, masterSettings.WeixinPaySignKey);
					NotifyClient client = new NotifyClient(account);
					if (client.UpdateFeedback(feedBack.FeedBackId, feedBack.OpenId))
					{
						VShopHelper.UpdateFeedBackMsgType(feedBack.FeedBackId, "已处理");
						this.ShowMsg("处理成功", true);
						this.BindPtReview();
					}
					else
					{
						this.ShowMsg("处理失败", false);
					}
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.dlstPtReviews.DeleteCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dlstPtReviews_DeleteCommand);
			this.dlstPtReviews.UpdateCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dlstPtReviews_DeleteCommand);
			this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
			if (!base.IsPostBack)
			{
				this.BindPtReview();
			}
		}
	}
}
