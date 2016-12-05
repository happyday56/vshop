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
using Hidistro.SqlDal.Members;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin.sales
{
	public partial class Refund : AdminPage
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
				HandleStatus = RefundInfo.Handlestatus.HasTheAudit
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
            this.ShowMsg("��˳ɹ�", true);
			this.LoadReturnApplyData();
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
				string skuId = null;
				foreach (LineItemInfo value in orderInfo.LineItems.Values)
				{
					if (value.ProductId != int.Parse(this.hidProductId.Value))
					{
						continue;
					}
					skuId = value.SkuId;
				}
				if (RefundHelper.UpdateOrderGoodStatu(this.hidOrderId.Value, skuId, 3))
				{
                    this.ShowMsg("�����ɹ�", true);
					this.LoadReturnApplyData();
					return;
				}
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
			this.LoadReturnApplyData();
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

		private void LoadReturnApplyData()
		{
			ReturnsApplyQuery returnsApplyQuery = this.AGPpN9tMpJSLGMt1AaU0jgdA();
			DbQueryResult returnOrderAll = RefundHelper.GetReturnOrderAll(returnsApplyQuery);
			this.dlstRefund.DataSource = returnOrderAll.Data;
			this.dlstRefund.DataBind();
			this.pager.TotalRecords = returnOrderAll.TotalRecords;
			this.pager1.TotalRecords = returnOrderAll.TotalRecords;
			this.txtOrderId.Text = returnsApplyQuery.OrderId;
			this.ddlHandleStatus.SelectedIndex = 0;
			this.ddlHandleStatus.SelectedValue = returnsApplyQuery.HandleStatus.Value.ToString();
            this.txtStartTime.Text = returnsApplyQuery.StartTime;
            this.txtEndTime.Text = returnsApplyQuery.EndTime;
		}

		private ReturnsApplyQuery AGPpN9tMpJSLGMt1AaU0jgdA(bool isExport = false)
		{
			ReturnsApplyQuery returnsApplyQuery = new ReturnsApplyQuery();
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["OrderId"]))
			{
                returnsApplyQuery.OrderId = Globals.UrlDecode(this.Page.Request.QueryString["OrderId"]);
			}
            if (string.IsNullOrEmpty(this.Page.Request.QueryString["HandleStatus"]))
			{
				returnsApplyQuery.HandleStatus = new int?(-1);
			}
			else
			{
				int num = 0;
                if (int.TryParse(this.Page.Request.QueryString["HandleStatus"], out num))
				{
					returnsApplyQuery.HandleStatus = new int?(num);
				}
			}
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StartTime"]))
            {
                returnsApplyQuery.StartTime = Globals.UrlDecode(this.Page.Request.QueryString["StartTime"]);
            }
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["EndTime"]))
            {
                returnsApplyQuery.EndTime = Globals.UrlDecode(this.Page.Request.QueryString["EndTime"]);
            }
            if (isExport)
            {
                returnsApplyQuery.PageIndex = 1;
                returnsApplyQuery.PageSize = 20000;
            }
            else
            {
                returnsApplyQuery.PageIndex = this.pager.PageIndex;
                returnsApplyQuery.PageSize = this.pager.PageSize;
            }
			
            returnsApplyQuery.SortBy = "ApplyForTime";
			returnsApplyQuery.SortOrder = SortAction.Desc;
			return returnsApplyQuery;
		}

		private void AH8CVHyiSPoKgHss5CP(bool flag)
		{
			NameValueCollection nameValueCollection = new NameValueCollection()
			{
				{ "OrderId", this.txtOrderId.Text },
				{ "PageSize", this.pager.PageSize.ToString() },
                { "StartTime", this.txtStartTime.Text },
                { "EndTime", this.txtEndTime.Text }
			};
			if (!flag)
			{
                nameValueCollection.Add("pageIndex", this.pager.PageIndex.ToString());
			}
			if (!string.IsNullOrEmpty(this.ddlHandleStatus.SelectedValue))
			{
                nameValueCollection.Add("HandleStatus", this.ddlHandleStatus.SelectedValue);
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
			refundInfo.RefundMoney = num;
			if (num < new decimal(0))
			{
                this.ShowMsg("����Ϊ������", false);
				return;
			}
			if (!RefundHelper.UpdateByReturnsId(refundInfo))
			{
                this.ShowMsg("����ʧ�ܣ������ԡ�", false);
			}
			else
			{
				OrderInfo orderInfo = OrderHelper.GetOrderInfo(this.hidOrderId.Value);
				string skuId = null;
				int orderItemsStatus = 0;
				foreach (LineItemInfo value in orderInfo.LineItems.Values)
				{
					if (value.ProductId != int.Parse(this.hidProductId.Value))
					{
						continue;
					}
					skuId = value.SkuId;
					orderItemsStatus = (int)value.OrderItemsStatus;
				}
				if (orderItemsStatus == 7)
				{
					this.hidStatus.Value = 3.ToString();
				}
				if (RefundHelper.UpdateOrderGoodStatu(this.hidOrderId.Value, skuId, (int.Parse(this.hidStatus.Value) == 6 ? 2 : 3)))
				{
                    this.ShowMsg("�����ɹ�", true);
					this.LoadReturnApplyData();
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
				string str = null;
				foreach (LineItemInfo value in orderInfo.LineItems.Values)
				{
					if (value.ProductId != int.Parse(this.hidProductId.Value))
					{
						continue;
					}
					skuId = value.SkuId;
					str = value.Quantity.ToString();
				}
				if (RefundHelper.UpdateOrderGoodStatu(this.hidOrderId.Value, skuId, 9))
				{
					//RefundHelper.UpdateRefundOrderStock(str, skuId);
					foreach (LineItemInfo lineItemInfo in OrderHelper.GetOrderInfo(this.hidOrderId.Value).LineItems.Values)
					{
						//if (lineItemInfo.OrderItemsStatus.ToString() != OrderStatus.Refunded.ToString())
						//{
						//	continue;
						//}
                        RefundHelper.UpdateRefundOrderStock(lineItemInfo.Quantity.ToString(), lineItemInfo.SkuId);
						num1++;
					}
					if (orderInfo.LineItems.Values.Count == num1)
					{
						this.AINLPE2AqL(this.hidOrderId.Value);
					}
                    // �����˿���Ӷ�𷵻�
                    this.ProcessOrderCommission(orderInfo);
                    this.ShowMsg("�ɹ��˿�", true);
					this.LoadReturnApplyData();
					return;
				}
			}
		}

        private void ProcessOrderCommission(OrderInfo order)
        {
            // ��ȡ�������е�Ӷ������
            IList<CommissionInfo> commissionList = OrderHelper.GetCommissionByOrderId(order.OrderId);
            if (null != commissionList && commissionList.Count > 0)
            {
                XTrace.WriteLine("-----------------------------------�˻�Ӷ�𷵻���ʼ(����ID��" + order.OrderId + ")-----------------------------------");

                foreach (CommissionInfo ci in commissionList)
                {
                    if (!OrderHelper.IsExistCommissionByUser(ci))
                    {
                        OrderHelper.UpdateDeductionCommission(ci.UserId, ci.OrderId, ci.CommId, ci.CommOrderStatus, ci.CommTotal);
                    }
                }

                XTrace.WriteLine("-----------------------------------�˻�Ӷ�𷵻�����(����ID��" + order.OrderId + ")-----------------------------------");
            }
            // ��������ʹ�õ����Ʊҹ黹
            if (order.RedPagerAmount > 0)
            {
                OrderHelper.UpdateDeductionVirtualPoint(order);
            }

            // �������ϵ�����ҿۻ�
            bool updateFlag;

            // 1.�������Ϳۼ�
            if (order.StoreGiftMoney > 0)
            {
                updateFlag = new MemberDao().UpdateVirtualPoints(order.ReferralUserId, order.StoreGiftMoney);
                XTrace.WriteLine("����������׵��ۼ�����������" + order.ReferralUserId + "------" + order.StoreGiftMoney);
            }

            // 2.��Ա���Ϳۼ�
            if (order.MemberGiftMoney > 0)
            {
                updateFlag = new MemberDao().UpdateVirtualPoints(order.UserId, order.MemberGiftMoney);
                XTrace.WriteLine("��������͹���ۼ�����Ա����" + order.UserId + "------" + order.MemberGiftMoney);
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
            this.btnCreateReport.Click += new EventHandler(this.btnCreateReport_Click);

			if (!base.IsPostBack)
			{
				this.LoadReturnApplyData();
			}
		}


        private void btnCreateReport_Click(object sender, System.EventArgs e)
        {
            ReturnsApplyQuery returnsApplyQuery = this.AGPpN9tMpJSLGMt1AaU0jgdA(true);
            DbQueryResult returnOrderAll = RefundHelper.GetReturnOrderAll(returnsApplyQuery);

            StringBuilder builder = new StringBuilder();

            DataTable exportData = null;

            if (null != returnOrderAll)
            {
                exportData = (DataTable)returnOrderAll.Data;
            }

            if (null != exportData && exportData.Rows.Count > 0)
            {
                builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                builder.AppendLine("    <tr style=\"font-weight: bold; white-space: nowrap;\">");
                builder.AppendLine("        <td>�������</td>");
                builder.AppendLine("        <td>��Ա��</td>");
                builder.AppendLine("        <td>�˿���(Ԫ)</td>");
                builder.AppendLine("        <td>�������(Ԫ)</td>");
                builder.AppendLine("        <td>����״̬</td>");
                builder.AppendLine("        <td>����ʱ��</td>");
                builder.AppendLine("        <td>���뱸ע</td>");
                builder.AppendLine("        <td>����״̬</td>");
                builder.AppendLine("        <td>����ʱ��</td>");
                builder.AppendLine("        <td>�˿�ʱ��</td>");
                builder.AppendLine("        <td>����Ա��ע</td>");
                builder.AppendLine("        <td>������Ա</td>");
                builder.AppendLine("    </tr>");

                foreach (DataRow row in exportData.Rows)
                {
                    builder.AppendLine("    <tr>");
                    builder.AppendLine("        <td style=\"vnd.ms-excel.numberformat:@\">" + row["OrderId"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Username"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RefundMoney"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OrderTotal"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + OrderInfo.GetOrderStatusName((OrderStatus)row["OrderStatus"]) + "</td>");
                    builder.AppendLine("        <td>" + row["ApplyForTime"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["Comments"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + GetHandleStatusName(row["HandleStatus"].ToString()) + "</td>");
                    builder.AppendLine("        <td>" + row["HandleTime"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["RefundTime"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["AdminRemark"].ToString() + "</td>");
                    builder.AppendLine("        <td>" + row["OperatorName"].ToString() + "</td>");
                    builder.AppendLine("    </tr>");
                }

                builder.AppendLine("</table>");

                this.Page.Response.Clear();
                this.Page.Response.Buffer = false;
                this.Page.Response.Charset = "UTF-8";
                base.Response.AppendHeader("Content-Disposition", "attachment;filename=RefundData_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                this.Page.Response.ContentType = "application/ms-excel";
                this.Page.EnableViewState = false;
                this.Page.Response.Write(builder.ToString());
                this.Page.Response.End();

            }
            else
            {
                this.ShowMsg("û�е�������", true);
            }
        }

        private string GetHandleStatusName(string handleStatus)
        {
            if (handleStatus == "4")
            {
                return "δ���";
            }
            if (handleStatus == "5")
            {
                return "�����";
            }
            if (handleStatus == "6")
            {
                return "δ�˿�";
            }
            if (handleStatus == "2")
            {
                return "���˿�";
            }
            if (handleStatus == "8")
            {
                return "�ܾ��˿�";
            }
            return "��˲�ͨ��";
        }


        

	}
}