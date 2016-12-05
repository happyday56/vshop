using Hidistro.ControlPanel.Sales;
using Hidistro.Entities.Orders;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
namespace Hidistro.UI.Web.Admin.sales
{
	public class BatchPrintSendOrderGoods : AdminPage
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl divContent;
		protected System.Web.UI.HtmlControls.HtmlHead Head1;
		private System.Collections.Generic.List<OrderInfo> GetPrintData(string orderIds)
		{
			System.Collections.Generic.List<OrderInfo> list = new System.Collections.Generic.List<OrderInfo>();
			string[] array = orderIds.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string str = array[i];
				OrderInfo orderInfo = OrderHelper.GetOrderInfo(str);
				list.Add(orderInfo);
			}
			return list;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string orderIds = base.Request["orderIds"].Trim(new char[]
			{
				','
			});
			if (!string.IsNullOrEmpty(base.Request["orderIds"]))
			{
				foreach (OrderInfo info in this.GetPrintData(orderIds))
				{
					System.Web.UI.HtmlControls.HtmlGenericControl child = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
					child.Attributes["class"] = "order print";
					System.Text.StringBuilder builder = new System.Text.StringBuilder("");
					builder.AppendFormat("<div class=\"info\"><div class=\"prime-info\" style=\"margin-right: 20px;\"><p><span><h3 style=\"font-weight: normal\">{0}</h3></span></p></div><ul class=\"sub-info\"><li><span>生成时间： </span>{1}</li><li><span>订单编号： </span>{2}</li></ul><br class=\"clear\" /></div>", info.ShipTo, info.OrderDate.ToString("yyyy-MM-dd HH:mm"), info.OrderId);
					builder.Append("<table><col class=\"col-0\" /><col class=\"col-1\" /><col class=\"col-2\" /><col class=\"col-3\" /><col class=\"col-4\" /><col class=\"col-5\" /><thead><tr><th>货号</th><th>商品名称</th><th>规格</th><th>数量</th><th>单价</th><th>总价</th></tr></thead><tbody>");
					System.Collections.Generic.Dictionary<string, LineItemInfo> lineItems = info.LineItems;
					if (lineItems != null)
					{
						foreach (string str2 in lineItems.Keys)
						{
							LineItemInfo info2 = lineItems[str2];
							builder.AppendFormat("<tr><td>{0}</td>", info2.SKU);
							builder.AppendFormat("<td>{0}</td>", info2.ItemDescription);
							builder.AppendFormat("<td>{0}</td>", info2.SKUContent);
							builder.AppendFormat("<td>{0}</td>", info2.ShipmentQuantity);
							builder.AppendFormat("<td>{0}</td>", System.Math.Round(info2.ItemListPrice, 2));
							builder.AppendFormat("<td>{0}</td></tr>", System.Math.Round(info2.GetSubTotal(), 2));
						}
					}
                    builder.AppendLine("</tbody></table>");
                    //builder.AppendFormat("</tbody></table><ul class=\"price\"><li><span>商品总价： </span>{0}</li><li><span>运费： </span>{1}</li>", System.Math.Round(info.GetAmount(), 2), System.Math.Round(info.AdjustedFreight, 2));
                    //decimal reducedPromotionAmount = info.ReducedPromotionAmount;
                    //if (reducedPromotionAmount > 0m)
                    //{
                    //    builder.AppendFormat("<li><span>优惠金额：</span>{0}</li>", System.Math.Round(reducedPromotionAmount, 2));
                    //}
                    //decimal payCharge = info.PayCharge;
                    //if (payCharge > 0m)
                    //{
                    //    builder.AppendFormat("<li><span>支付手续费：</span>{0}</li>", System.Math.Round(payCharge, 2));
                    //}
                    //if (!string.IsNullOrEmpty(info.CouponCode))
                    //{
                    //    decimal couponValue = info.CouponValue;
                    //    if (couponValue > 0m)
                    //    {
                    //        builder.AppendFormat("<li><span>优惠券：</span>{0}</li>", System.Math.Round(couponValue, 2));
                    //    }
                    //}
                    //decimal adjustedDiscount = info.AdjustedDiscount;
                    //if (adjustedDiscount > 0m)
                    //{
                    //    builder.AppendFormat("<li><span>管理员手工加价：</span>{0}</li>", System.Math.Round(adjustedDiscount, 2));
                    //}
                    //else
                    //{
                    //    builder.AppendFormat("<li><span>管理员手工减价：</span>{0}</li>", System.Math.Round(-adjustedDiscount, 2));
                    //}
					//builder.AppendFormat("<li><span>实付金额：</span>{0}</li></ul><br class=\"clear\" /><br><br>", System.Math.Round(info.GetTotal(), 2));
                    builder.AppendLine("<br class=\"clear\" /><br><br>");

                    builder.AppendLine("<div class=\"NextPage\"></div>");
					child.InnerHtml = builder.ToString();
					this.divContent.Controls.AddAt(0, child);
				}
			}
		}
	}
}
