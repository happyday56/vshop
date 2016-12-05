using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ASPNET.WebControls;
using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Sales;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities;
using Hidistro.Entities.Members;
using Hidistro.Entities.Orders;
using Hidistro.Messages;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System.Data;


namespace Hidistro.UI.Web.Admin.sales
{
	public partial class Returnsapply : AdminPage
	{

		private void btnAuditAcceptRefund_Click(object obj, EventArgs eventArg)
		{
			decimal num = new decimal(0);
			RefundInfo refundInfo = new RefundInfo()
			{
				RefundId = int.Parse(this.hidReturnsId.Value),
				AdminRemark = this.txtAdminRemark.Text.Trim(),
				HandleTime = DateTime.Now,
				AuditTime = DateTime.Now.ToString(),
				HandleStatus = RefundInfo.Handlestatus.NoRefund
			};
			if (!decimal.TryParse(this.hidAuditM.Value, out num))
			{
				this.ShowMsg("����Ľ���ʽ����ȷ", false);
				return;
			}
			refundInfo.RefundMoney = num;
			if (num < new decimal(0))
			{
				this.ShowMsg("����Ϊ������", false);
				return;
			}
			if (!RefundHelper.UpdateByAuditReturnsId(refundInfo))
			{
				this.ShowMsg("���ʧ�ܣ������ԡ�", false);
				return;
			}
			this.ShowMsg("oVs4aBBin1I=", true);
			this.LoadReturnsApplyData();
		}

		private void btnAuditRefuseRefund_Click(object obj, EventArgs eventArg)
		{
			decimal num = new decimal(0);
			RefundInfo refundInfo = new RefundInfo()
			{
				RefundId = int.Parse(this.hidReturnsId.Value),
				AdminRemark = this.txtAdminRemark.Text.Trim(),
				HandleTime = DateTime.Now,
				HandleStatus = RefundInfo.Handlestatus.AuditNotThrough,
				Operator = Globals.GetCurrentManagerUserId().ToString()
			};
			if (!decimal.TryParse(this.hidAuditM.Value, out num))
			{
				this.ShowMsg("����Ľ���ʽ����ȷ", false);
				return;
			}
			refundInfo.RefundMoney = num;
			if (num < new decimal(0))
			{
				this.ShowMsg("����Ϊ������", false);
				return;
			}
			if (!RefundHelper.UpdateByAuditReturnsId(refundInfo))
			{
				this.ShowMsg("����ʧ�ܣ������ԡ�", false);
			}
			else
			{
				OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.hidOrderId.Value);
                // 2015-11-27 ��Ӿܾ��˻���ɶ���״̬Ϊ������
				//string skuId = null;
				//foreach (LineItemInfo value in orderInfo.LineItems.Values)
				//{
				//	if (value.ProductId != int.Parse(this.hidProductId.Value))
				//	{
				//		continue;
				//	}
				//	skuId = value.SkuId;
				//}
				//if (RefundHelper.UpdateOrderGoodStatu(this.hidOrderId.Value, skuId, 3))
				//{
                    // 2015-11-27 ��Ӿܾ��˻���ɶ���״̬Ϊ������
                    OrderHelper.SetOrderState(this.hidOrderId.Value, OrderStatus.BuyerAlreadyPaid);
					this.ShowMsg("�����ɹ�", true);
					this.LoadReturnsApplyData();
					return;
				//}
			}
		}

		private void lkbtnDeleteCheck_Click(object obj, EventArgs eventArg)
		{
			int num;
			string item = "";
			if (!string.IsNullOrEmpty(base.Request["CheckBoxGroup"]))
			{
				item = base.Request["CheckBoxGroup"];
			}
			if (item.Length <= 0)
			{
				this.ShowMsg("��ѡҪɾ�����˿����뵥", false);
				return;
			}
			string str = "�ɹ�ɾ����{0}���˿����뵥";
			char[] chrArray = new char[] { ',' };
			str = (!RefundHelper.DelRefundApply(item.Split(chrArray), out num) ? string.Concat(string.Format(str, num), ",����������벻��ɾ��") : string.Format(str, num));
			this.LoadReturnsApplyData();
			this.ShowMsg(str, true);
		}

		private void btnSearchButton_Click(object obj, EventArgs eventArg)
		{
			this.AH8CVHyiSPoKgHss5CP(true);
		}

		private void dlstRefund_ItemDataBound(object obj, DataListItemEventArgs dataListItemEventArg)
		{
			if (dataListItemEventArg.Item.ItemType == ListItemType.Item || dataListItemEventArg.Item.ItemType == ListItemType.AlternatingItem)
			{
				HtmlAnchor htmlAnchor = (HtmlAnchor)dataListItemEventArg.Item.FindControl("lkbtnCheckRefund");
				Label label = (Label)dataListItemEventArg.Item.FindControl("lblHandleStatus");
				if (label.Text == "4")
				{
					label.Text = "δ���";
					return;
				}
				if (label.Text == "5")
				{
					label.Text = "�����";
					return;
				}
				if (label.Text == "6")
				{
					label.Text = "δ�˿�";
					return;
				}
				if (label.Text == "2")
				{
					label.Text = "���˿�";
					return;
				}
				if (label.Text == "8")
				{
					label.Text = "�ܾ��˿�";
					return;
				}
				label.Text = "��˲�ͨ��";

			}
		}

		private void LoadReturnsApplyData()
		{
            ReturnsApplyQuery returnsApplyQuery = this.LoadParamater();
			//DbQueryResult returnOrderAll = RefundHelper.GetReturnOrderAll(returnsApplyQuery);
            DbQueryResult returnOrderAll = RefundHelper.GetReturnOrderDataAll(returnsApplyQuery);
            
			this.dlstRefund.DataSource = returnOrderAll.Data;
			this.dlstRefund.DataBind();
			this.pager.TotalRecords = returnOrderAll.TotalRecords;
			this.pager1.TotalRecords = returnOrderAll.TotalRecords;
			this.txtOrderId.Text = returnsApplyQuery.OrderId;
			this.ddlHandleStatus.SelectedIndex = 0;
			this.ddlHandleStatus.SelectedValue = returnsApplyQuery.HandleStatus.Value.ToString();
            this.ddlOrderType.SelectedIndex = 0;
            if (returnsApplyQuery.OrderTypeId.HasValue)
            {
                this.ddlOrderType.SelectedValue = returnsApplyQuery.OrderTypeId.Value.ToString();
            }
            
            this.ddlOrderStatus.SelectedIndex = 0;
            if (returnsApplyQuery.OrderStatusId.HasValue)
            {
                this.ddlOrderStatus.SelectedValue = returnsApplyQuery.OrderStatusId.Value.ToString();
            }
            if (returnsApplyQuery.ApplyForStartTime.HasValue)
            {
                this.calendarStart.SelectedDate = returnsApplyQuery.ApplyForStartTime.Value;
            }
            if (returnsApplyQuery.ApplyForEndTime.HasValue)
            {
                this.calendarEnd.SelectedDate = returnsApplyQuery.ApplyForEndTime.Value;
            }
            
            
		}

		private ReturnsApplyQuery LoadParamater()
		{
			ReturnsApplyQuery returnsApplyQuery = new ReturnsApplyQuery();
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderId"]))
			{
				returnsApplyQuery.OrderId = Globals.UrlDecode(this.Page.Request.QueryString["OrderId"]);
			}
			if (string.IsNullOrEmpty(this.Page.Request.QueryString["HandleStatus"]))
			{
				returnsApplyQuery.HandleStatus = new int?(-2);
			}
			else
			{
				int num = 0;
				if (int.TryParse(this.Page.Request.QueryString["HandleStatus"], out num))
				{
					returnsApplyQuery.HandleStatus = new int?(num);
				}
			}

            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ApplyForStartTime"]))
            {
                returnsApplyQuery.ApplyForStartTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["ApplyForStartTime"]));
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ApplyForEndTime"]))
            {
                returnsApplyQuery.ApplyForEndTime = new System.DateTime?(System.DateTime.Parse(this.Page.Request.QueryString["ApplyForEndTime"]));
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderType"]))
            {
                int num1 = 0;
                if (int.TryParse(this.Page.Request.QueryString["OrderType"], out num1))
                {
                    returnsApplyQuery.OrderTypeId = num1;
                }
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderStatus"]))
            {
                int num2 = 0;
                if (int.TryParse(this.Page.Request.QueryString["OrderStatus"], out num2))
                {
                    returnsApplyQuery.OrderStatusId = num2;
                }
            }

			returnsApplyQuery.PageIndex = this.pager.PageIndex;
			returnsApplyQuery.PageSize = this.pager.PageSize;
			returnsApplyQuery.SortBy = "ApplyForTime";
			returnsApplyQuery.SortOrder = SortAction.Desc;
			return returnsApplyQuery;
		}

		private void AH8CVHyiSPoKgHss5CP(bool flag)
		{
			NameValueCollection nameValueCollection = new NameValueCollection()
			{
				{ "OrderId", this.txtOrderId.Text },
				{ "PageSize", this.pager.PageSize.ToString() }
			};
			if (!flag)
			{
				nameValueCollection.Add("pageIndex", this.pager.PageIndex.ToString());
			}
			if (!string.IsNullOrEmpty(this.ddlHandleStatus.SelectedValue))
			{
				nameValueCollection.Add("HandleStatus", this.ddlHandleStatus.SelectedValue);
			}
            if (!string.IsNullOrEmpty(this.ddlOrderType.SelectedValue))
            {
                nameValueCollection.Add("OrderType", this.ddlOrderType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.ddlOrderStatus.SelectedValue))
            {
                nameValueCollection.Add("OrderStatus", this.ddlOrderStatus.SelectedValue);
            }
            if (this.calendarStart.SelectedDate.HasValue)
            {
                nameValueCollection.Add("ApplyForStartTime", this.calendarStart.SelectedDate.ToString());
            }
            if (this.calendarEnd.SelectedDate.HasValue)
            {
                nameValueCollection.Add("ApplyForEndTime", this.calendarEnd.SelectedDate.ToString());
            }

			base.ReloadPage(nameValueCollection);
		}

		private void AINLPE2AqL(string str)
		{
			OrderInfo orderInfo = OrderHelper.GetOrderInfo(str);
			orderInfo.CloseReason = "�ͻ�Ҫ���˻�(��)��";
			if (RefundHelper.CloseTransaction(orderInfo))
			{
				orderInfo.OnClosed();
				MemberInfo member = MemberHelper.GetMember(orderInfo.UserId);
				Messenger.OrderClosed(member, orderInfo, orderInfo.CloseReason);
			}
		}

		private void btnRefuseRefund_Click(object obj, EventArgs eventArg)
		{
			decimal num = new decimal(0);
			RefundInfo refundInfo = new RefundInfo()
			{
				RefundId = int.Parse(this.hidReturnsId.Value),
				AdminRemark = this.hidAdminRemark.Value.Trim(),
				HandleTime = DateTime.Now,
				HandleStatus = RefundInfo.Handlestatus.RefuseRefunded,
				Operator = Globals.GetCurrentManagerUserId().ToString()
			};
			if (!decimal.TryParse(this.hidRefundM.Value, out num))
			{
				this.ShowMsg("����Ľ���ʽ����ȷ", false);
				return;
			}
			if (num < new decimal(0))
			{
				this.ShowMsg("����Ϊ������", false);
				return;
			}
			refundInfo.RefundMoney = num;
			if (!RefundHelper.UpdateByReturnsId(refundInfo))
			{
				this.ShowMsg("����ʧ�ܣ������ԡ�", false);
			}
			else
			{
				OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.hidOrderId.Value);
				string skuId = null;
				foreach (LineItemInfo value in orderInfo.LineItems.Values)
				{
					if (value.ProductId != int.Parse(this.hidProductId.Value))
					{
						continue;
					}
					skuId = value.SkuId;
				}
				if (RefundHelper.UpdateOrderGoodStatu(this.hidOrderId.Value, skuId, (int.Parse(this.hidStatus.Value) == 6 ? 2 : 3)))
				{
					this.ShowMsg("�����ɹ�", true);
					this.LoadReturnsApplyData();
					return;
				}
			}
		}

		protected void btnAcceptRefund_Click(object sender, EventArgs e)
		{
			decimal num = new decimal(0);
			int num1 = 0;
			RefundInfo refundInfo = new RefundInfo()
			{
				RefundId = int.Parse(this.hidReturnsId.Value),
				AdminRemark = this.hidAdminRemark.Value.Trim(),
				HandleTime = DateTime.Now,
				RefundTime = DateTime.Now.ToString(),
				HandleStatus = RefundInfo.Handlestatus.Refunded,
				Operator = Globals.GetCurrentManagerUserId().ToString()
			};
			if (!decimal.TryParse(this.hidRefundM.Value, out num))
			{
				this.ShowMsg("����Ľ���ʽ����ȷ", false);
				return;
			}
			refundInfo.RefundMoney = num;
			if (num < new decimal(0))
			{
				this.ShowMsg("����Ϊ������", false);
				return;
			}
			if (!RefundHelper.UpdateByReturnsId(refundInfo))
			{
				this.ShowMsg("�˿�ʧ�ܣ������ԡ�", false);
			}
			else
			{
				OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.hidOrderId.Value);
				string skuId = null;
				foreach (LineItemInfo value in orderInfo.LineItems.Values)
				{
					if (value.ProductId != int.Parse(this.hidProductId.Value))
					{
						continue;
					}
					skuId = value.SkuId;
				}
				if (RefundHelper.UpdateOrderGoodStatu(this.hidOrderId.Value, skuId, 9))
				{
					foreach (LineItemInfo lineItemInfo in OrderHelper.GetOrderInfo(this.hidOrderId.Value).LineItems.Values)
					{
						if (lineItemInfo.OrderItemsStatus.ToString() != OrderStatus.Refunded.ToString())
						{
							continue;
						}
						num1++;
					}
					if (orderInfo.LineItems.Values.Count == num1)
					{
						this.AINLPE2AqL(this.hidOrderId.Value);
					}
					this.ShowMsg("�ɹ��˿�", true);
					this.LoadReturnsApplyData();
					return;
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.dlstRefund.ItemDataBound += new DataListItemEventHandler(this.dlstRefund_ItemDataBound);
			this.btnSearchButton.Click += new EventHandler(this.btnSearchButton_Click);
			this.lkbtnDeleteCheck.Click += new EventHandler(this.lkbtnDeleteCheck_Click);
			this.btnAcceptRefund.Click += new EventHandler(this.btnAcceptRefund_Click);
			this.btnRefuseRefund.Click += new EventHandler(this.btnRefuseRefund_Click);
			this.btnAuditAcceptRefund.Click += new EventHandler(this.btnAuditAcceptRefund_Click);
			this.btnAuditRefuseRefund.Click += new EventHandler(this.btnAuditRefuseRefund_Click);
			if (!base.IsPostBack)
			{
				this.LoadReturnsApplyData();
			}
		}
	}
}