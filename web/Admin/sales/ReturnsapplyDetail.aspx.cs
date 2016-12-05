using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hidistro.ControlPanel.Sales;
using Hidistro.Core.Entities;
using Hidistro.Entities.Orders;
using Hidistro.UI.ControlPanel.Utility;


namespace Hidistro.UI.Web.Admin.sales
{
	public partial class ReturnsapplyDetail : AdminPage
	{
		private int m_ReturnsId;


		private void btnSubmit_Click(object obj, EventArgs eventArg)
		{
            this.Page.Response.Redirect("Default.aspx");
		}

		private void LoadReturnsApplyData()
		{
			ReturnsApplyQuery returnsApplyQuery = new ReturnsApplyQuery()
			{
				PageIndex = 1,
				PageSize = 1,
                ReturnsId = base.Request.QueryString["ReturnsId"]
			};
            DbQueryResult returnOrderAll = RefundHelper.GetReturnOrderDataAll(returnsApplyQuery);
			if (returnOrderAll.Data == null)
			{
                this.ShowMsg("退款单信息不存在！", false);
				return;
			}
			DataTable data = (DataTable)returnOrderAll.Data;
            this.litOrderid.Text = data.Rows[0]["OrderId"].ToString();
            this.litUserName.Text = data.Rows[0]["Username"].ToString();
            this.litRefundMoney.Text = data.Rows[0]["RefundMoney"].ToString();
            this.litApplyForTime.Text = data.Rows[0]["ApplyForTime"].ToString();
            this.litComments.Text = data.Rows[0]["Comments"].ToString();
            this.litHandleStatus.Text = this.GetStatus(int.Parse(data.Rows[0]["HandleStatus"].ToString()));
            this.litHandleTime.Text = data.Rows[0]["HandleTime"].ToString();
            this.litAdminRemark.Text = data.Rows[0]["AdminRemark"].ToString();
            this.litAccount.Text = data.Rows[0]["Account"].ToString();
            //this.litProductName.Text = data.Rows[0]["ProductName"].ToString();
            this.litAuditTime.Text = data.Rows[0]["AuditTime"].ToString();
            this.litRefundTime.Text = data.Rows[0]["RefundTime"].ToString();
            this.litOperator.Text = data.Rows[0]["OperatorName"].ToString();

            this.lblAmount.Text = data.Rows[0]["Amount"].ToString();
            this.lblDiscountAmount.Text = data.Rows[0]["DiscountAmount"].ToString();
            this.lblRedPagerAmount.Text = data.Rows[0]["RedPagerAmount"].ToString();
            this.lblVirtualPointAmount.Text = data.Rows[0]["VirtualPointAmount"].ToString();
            this.lblOrderTotal.Text = data.Rows[0]["OrderTotal"].ToString();
            this.lblOrderTypeName.Text = data.Rows[0]["OrderTypeName"].ToString();
            this.lblOrderStatusName.Text = data.Rows[0]["OrderStatusName"].ToString();
            this.lblIsCrossOrder.Text = data.Rows[0]["IsCrossOrder"].ToString() == "1" ? "是" : "否";

            // 处理显示商品信息
            DataTable productDetail = RefundHelper.GetOrderProductByOrderId(data.Rows[0]["OrderId"].ToString());
            reProductDetail.DataSource = productDetail;
            reProductDetail.DataBind();

		}

		public string GetStatus(int status)
		{
			string str = null;
			switch (status)
			{
				case 2:
				{
                    str = "已退款";
					return str;
				}
				case 3:
				{
					return str;
				}
				case 4:
				{
                    str = "未审核";
					return str;
				}
				case 5:
				{
                    str = "已审核";
					return str;
				}
				case 6:
				{
                    str = "未退款";
					return str;
				}
				case 7:
				{
                    str = "审核未通过";
					return str;
				}
				case 8:
				{
                    str = "拒绝退款";
					return str;
				}
				default:
				{
					return str;
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
			if (!base.IsPostBack)
			{
                if (int.TryParse(this.Page.Request.QueryString["ReturnsId"], out this.m_ReturnsId))
				{
					this.LoadReturnsApplyData();
					return;
				}
                this.Page.Response.Redirect("Default.aspx");
			}
		}
	}
}