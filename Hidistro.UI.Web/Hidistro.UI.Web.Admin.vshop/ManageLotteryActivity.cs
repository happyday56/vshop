using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class ManageLotteryActivity : AdminPage
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor addactivity;
		protected System.Web.UI.WebControls.Literal Litdesc;
		protected System.Web.UI.WebControls.Literal LitTitle;
		protected Pager pager;
		protected System.Web.UI.WebControls.Repeater rpMaterial;
		protected int type;
		protected void BindMaterial()
		{
			LotteryActivityQuery page = new LotteryActivityQuery
			{
				ActivityType = (LotteryActivityType)this.type,
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortBy = "ActivityId",
				SortOrder = SortAction.Desc
			};
			DbQueryResult lotteryActivityList = VShopHelper.GetLotteryActivityList(page);
			this.rpMaterial.DataSource = lotteryActivityList.Data;
			this.rpMaterial.DataBind();
			this.pager.TotalRecords = lotteryActivityList.TotalRecords;
		}
		public string GetUrl(object activityId)
		{
			string str = string.Empty;
			string result;
			switch (this.type)
			{
			case 1:
				result = string.Concat(new object[]
				{
					"http://",
					Globals.DomainName,
					"/Vshop/BigWheel.aspx?activityid=",
					activityId
				});
				break;
			case 2:
				result = string.Concat(new object[]
				{
					"http://",
					Globals.DomainName,
					"/Vshop/Scratch.aspx?activityid=",
					activityId
				});
				break;
			case 3:
				result = string.Concat(new object[]
				{
					"http://",
					Globals.DomainName,
					"/Vshop/SmashEgg.aspx?activityid=",
					activityId
				});
				break;
			default:
				result = str;
				break;
			}
			return result;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!int.TryParse(base.Request.QueryString["type"], out this.type))
			{
				this.ShowMsg("参数错误", false);
			}
			else
			{
				this.addactivity.HRef = "VLotteryActivity.aspx?type=" + this.type;
				switch (this.type)
				{
				case 1:
					this.addactivity.InnerText = "添加大转盘";
					this.LitTitle.Text = "大转盘活动管理";
					this.Litdesc.Text = "大转盘";
					break;
				case 2:
					this.addactivity.InnerText = "添加刮刮卡";
					this.LitTitle.Text = "刮刮卡活动管理";
					this.Litdesc.Text = "刮刮卡";
					break;
				case 3:
					this.addactivity.InnerText = "添加砸金蛋";
					this.LitTitle.Text = "砸金蛋活动管理";
					this.Litdesc.Text = "砸金蛋";
					break;
				}
				if (!this.Page.IsPostBack)
				{
					this.BindMaterial();
				}
			}
		}
		protected void rpMaterial_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "del")
			{
				if (VShopHelper.DeleteLotteryActivity(System.Convert.ToInt32(e.CommandArgument), ((LotteryActivityType)this.type).ToString()))
				{
					this.ShowMsg("删除成功", true);
					this.BindMaterial();
				}
				else
				{
					this.ShowMsg("删除失败", false);
				}
			}
		}
	}
}
