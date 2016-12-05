using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
    public class ManageLotteryActivity : AdminPage
    {
        protected Pager pager;
        protected System.Web.UI.WebControls.Repeater rpMaterial;
        protected void BindMaterial()
        {
            LotteryActivityQuery page = new LotteryActivityQuery
            {
                PageIndex = this.pager.PageIndex,
                PageSize = this.pager.PageSize,
                SortBy = "ActivityId",
                SortOrder = SortAction.Desc
            };
            DbQueryResult lotteryTicketList = VShopHelper.GetLotteryTicketList(page);
            this.rpMaterial.DataSource = lotteryTicketList.Data;
            this.rpMaterial.DataBind();
            this.pager.TotalRecords = lotteryTicketList.TotalRecords;
        }
        public string GetUrl(object activityId)
        {
            return string.Concat(new object[]
			{
				"http://",
				Globals.DomainName,
				"/Vshop/Ticket.aspx?id=",
				activityId
			});
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindMaterial();
            }
        }
        protected void rpMaterial_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                if (VShopHelper.DelteLotteryTicket(System.Convert.ToInt32(e.CommandArgument)))
                {
                    this.ShowMsg("删除成功", true);
                    this.BindMaterial();
                }
                else
                {
                    this.ShowMsg("删除失败", false);
                }
            }
            else
            {
                if (e.CommandName == "start")
                {
                    LotteryTicketInfo lotteryTicket = VShopHelper.GetLotteryTicket(System.Convert.ToInt32(e.CommandArgument));
                    if (lotteryTicket.OpenTime > System.DateTime.Now)
                    {
                        lotteryTicket.OpenTime = System.DateTime.Now;
                        VShopHelper.UpdateLotteryTicket(lotteryTicket);
                        base.Response.Write("<script>location.reload();</script>");
                    }
                }
            }
        }
    }
}
