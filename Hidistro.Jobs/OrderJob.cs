namespace Hidistro.Jobs
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Core.Jobs;
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Xml;

    /// <summary>
    /// 更新订单状态
    /// </summary>
    public class OrderJob : IJob
    {
        public void Execute(XmlNode node)
        {
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(" UPDATE Hishop_Orders SET OrderStatus=4,CloseReason='过期没付款，自动关闭' WHERE OrderStatus=1 AND OrderDate <= @OrderDate;");
            database.AddInParameter(sqlStringCommand, "OrderDate", DbType.DateTime, DateTime.Now.AddDays((double) -masterSettings.CloseOrderDays));
            database.ExecuteNonQuery(sqlStringCommand);
            string query = string.Format("SELECT OrderId FROM  Hishop_Orders WHERE  OrderStatus=3 AND ShippingDate <= '" + DateTime.Now.AddDays((double) -masterSettings.FinishOrderDays) + "'", new object[0]);
            DbCommand command = database.GetSqlStringCommand(query);
            DataTable table = database.ExecuteDataSet(command).Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                bool flag = false;
                OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(table.Rows[i][0].ToString());
                Dictionary<string, LineItemInfo> lineItems = orderInfo.LineItems;
                LineItemInfo info2 = new LineItemInfo();
                foreach (KeyValuePair<string, LineItemInfo> pair in lineItems)
                {
                    info2 = pair.Value;
                    if ((info2.OrderItemsStatus == OrderStatus.ApplyForRefund) || (info2.OrderItemsStatus == OrderStatus.ApplyForReturns))
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    DbCommand command3 = database.GetSqlStringCommand(" UPDATE Hishop_Orders SET FinishDate = getdate(), OrderStatus = 5,CloseReason='订单自动完成' WHERE OrderStatus=3 AND ShippingDate <= @ShippingDate AND OrderId=@OrderId");
                    database.AddInParameter(command3, "ShippingDate", DbType.DateTime, DateTime.Now.AddDays((double) -masterSettings.FinishOrderDays));
                    database.AddInParameter(command3, "OrderId", DbType.String, orderInfo.OrderId);
                    if (database.ExecuteNonQuery(command3) > 0)
                    {
                       // DistributorsBrower.UpdateCalculationCommission(orderInfo);
                        // 更新订单佣金的结算状态（1：表示已确认收货结算）
                        DistributorsBrower.UpdateCommissionOrderStatus(orderInfo.OrderId, 1);

                        foreach (LineItemInfo info3 in orderInfo.LineItems.Values)
                        {
                            if (info3.OrderItemsStatus.ToString() == OrderStatus.SellerAlreadySent.ToString())
                            {
                                DbCommand command4 = database.GetSqlStringCommand("update Hishop_OrderItems set OrderItemsStatus=@OrderItemsStatus where orderid=@orderid and skuid=@skuid");
                                database.AddInParameter(command4, "OrderItemsStatus", DbType.Int32, 5);
                                database.AddInParameter(command4, "skuid", DbType.String, info3.SkuId);
                                database.AddInParameter(command4, "orderid", DbType.String, orderInfo.OrderId);
                                database.ExecuteNonQuery(command4);
                            }
                        }
                    }
                }
            }
        }
    }
}

