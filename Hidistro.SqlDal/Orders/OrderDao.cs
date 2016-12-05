namespace Hidistro.SqlDal.Orders
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Orders;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;
    using System.Text;
    using NewLife.Log;
    using System.Collections.Generic;
    using Hidistro.Entities.Members;
    using Hidistro.SqlDal.Members;

    public class OrderDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool CheckRefund(string orderId, string Operator, string adminRemark, int refundType, bool accept)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Hishop_Orders SET OrderStatus = @OrderStatus WHERE OrderId = @OrderId;");
            builder.Append(" update Hishop_OrderRefund set Operator=@Operator,AdminRemark=@AdminRemark,HandleStatus=@HandleStatus,HandleTime=@HandleTime where HandleStatus=0 and OrderId = @OrderId;");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            if (accept)
            {
                this.database.AddInParameter(sqlStringCommand, "OrderStatus", DbType.Int32, 9);
                this.database.AddInParameter(sqlStringCommand, "HandleStatus", DbType.Int32, 1);
            }
            else
            {
                this.database.AddInParameter(sqlStringCommand, "OrderStatus", DbType.Int32, 2);
                this.database.AddInParameter(sqlStringCommand, "HandleStatus", DbType.Int32, 2);
            }
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            this.database.AddInParameter(sqlStringCommand, "Operator", DbType.String, Operator);
            this.database.AddInParameter(sqlStringCommand, "AdminRemark", DbType.String, adminRemark);
            this.database.AddInParameter(sqlStringCommand, "HandleTime", DbType.DateTime, DateTime.Now);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool CreatOrder(OrderInfo orderInfo, DbTransaction dbTran)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("ss_CreateOrder");
            this.database.AddInParameter(storedProcCommand, "OrderId", DbType.String, orderInfo.OrderId);
            this.database.AddInParameter(storedProcCommand, "OrderDate", DbType.DateTime, orderInfo.OrderDate);
            this.database.AddInParameter(storedProcCommand, "UserId", DbType.Int32, orderInfo.UserId);
            this.database.AddInParameter(storedProcCommand, "UserName", DbType.String, orderInfo.Username);
            this.database.AddInParameter(storedProcCommand, "Wangwang", DbType.String, orderInfo.Wangwang);
            this.database.AddInParameter(storedProcCommand, "RealName", DbType.String, orderInfo.RealName);
            this.database.AddInParameter(storedProcCommand, "EmailAddress", DbType.String, orderInfo.EmailAddress);
            this.database.AddInParameter(storedProcCommand, "Remark", DbType.String, orderInfo.Remark);
            this.database.AddInParameter(storedProcCommand, "AdjustedDiscount", DbType.Currency, orderInfo.AdjustedDiscount);
            this.database.AddInParameter(storedProcCommand, "OrderStatus", DbType.Int32, (int) orderInfo.OrderStatus);
            this.database.AddInParameter(storedProcCommand, "ShippingRegion", DbType.String, orderInfo.ShippingRegion);
            this.database.AddInParameter(storedProcCommand, "Address", DbType.String, orderInfo.Address);
            this.database.AddInParameter(storedProcCommand, "ZipCode", DbType.String, orderInfo.ZipCode);
            this.database.AddInParameter(storedProcCommand, "ShipTo", DbType.String, orderInfo.ShipTo);
            this.database.AddInParameter(storedProcCommand, "TelPhone", DbType.String, orderInfo.TelPhone);
            this.database.AddInParameter(storedProcCommand, "CellPhone", DbType.String, orderInfo.CellPhone);
            this.database.AddInParameter(storedProcCommand, "ShipToDate", DbType.String, orderInfo.ShipToDate);
            this.database.AddInParameter(storedProcCommand, "ShippingModeId", DbType.Int32, orderInfo.ShippingModeId);
            this.database.AddInParameter(storedProcCommand, "ModeName", DbType.String, orderInfo.ModeName);
            this.database.AddInParameter(storedProcCommand, "RegionId", DbType.Int32, orderInfo.RegionId);
            this.database.AddInParameter(storedProcCommand, "Freight", DbType.Currency, orderInfo.Freight);
            this.database.AddInParameter(storedProcCommand, "AdjustedFreight", DbType.Currency, orderInfo.AdjustedFreight);
            this.database.AddInParameter(storedProcCommand, "ShipOrderNumber", DbType.String, orderInfo.ShipOrderNumber);
            this.database.AddInParameter(storedProcCommand, "Weight", DbType.Int32, orderInfo.Weight);
            this.database.AddInParameter(storedProcCommand, "ExpressCompanyName", DbType.String, orderInfo.ExpressCompanyName);
            this.database.AddInParameter(storedProcCommand, "ExpressCompanyAbb", DbType.String, orderInfo.ExpressCompanyAbb);
            this.database.AddInParameter(storedProcCommand, "PaymentTypeId", DbType.Int32, orderInfo.PaymentTypeId);
            this.database.AddInParameter(storedProcCommand, "PaymentType", DbType.String, orderInfo.PaymentType);
            this.database.AddInParameter(storedProcCommand, "PayCharge", DbType.Currency, orderInfo.PayCharge);
            this.database.AddInParameter(storedProcCommand, "RefundStatus", DbType.Int32, (int) orderInfo.RefundStatus);
            this.database.AddInParameter(storedProcCommand, "Gateway", DbType.String, orderInfo.Gateway);
            this.database.AddInParameter(storedProcCommand, "OrderTotal", DbType.Currency, orderInfo.GetTotal());
            this.database.AddInParameter(storedProcCommand, "OrderPoint", DbType.Int32, orderInfo.Points);
            this.database.AddInParameter(storedProcCommand, "OrderCostPrice", DbType.Currency, orderInfo.GetCostPrice());
            this.database.AddInParameter(storedProcCommand, "OrderProfit", DbType.Currency, orderInfo.GetProfit());
            this.database.AddInParameter(storedProcCommand, "Amount", DbType.Currency, orderInfo.GetAmount());
            this.database.AddInParameter(storedProcCommand, "ReducedPromotionId", DbType.Int32, orderInfo.ReducedPromotionId);
            this.database.AddInParameter(storedProcCommand, "ReducedPromotionName", DbType.String, orderInfo.ReducedPromotionName);
            this.database.AddInParameter(storedProcCommand, "ReducedPromotionAmount", DbType.Currency, orderInfo.ReducedPromotionAmount);
            this.database.AddInParameter(storedProcCommand, "IsReduced", DbType.Boolean, orderInfo.IsReduced);
            this.database.AddInParameter(storedProcCommand, "SentTimesPointPromotionId", DbType.Int32, orderInfo.SentTimesPointPromotionId);
            this.database.AddInParameter(storedProcCommand, "SentTimesPointPromotionName", DbType.String, orderInfo.SentTimesPointPromotionName);
            this.database.AddInParameter(storedProcCommand, "TimesPoint", DbType.Currency, orderInfo.TimesPoint);
            this.database.AddInParameter(storedProcCommand, "IsSendTimesPoint", DbType.Boolean, orderInfo.IsSendTimesPoint);
            this.database.AddInParameter(storedProcCommand, "FreightFreePromotionId", DbType.Int32, orderInfo.FreightFreePromotionId);
            this.database.AddInParameter(storedProcCommand, "FreightFreePromotionName", DbType.String, orderInfo.FreightFreePromotionName);
            this.database.AddInParameter(storedProcCommand, "IsFreightFree", DbType.Boolean, orderInfo.IsFreightFree);
            this.database.AddInParameter(storedProcCommand, "CouponName", DbType.String, orderInfo.CouponName);
            this.database.AddInParameter(storedProcCommand, "CouponCode", DbType.String, orderInfo.CouponCode);
            this.database.AddInParameter(storedProcCommand, "CouponAmount", DbType.Currency, orderInfo.CouponAmount);
            this.database.AddInParameter(storedProcCommand, "CouponValue", DbType.Currency, orderInfo.CouponValue);
            this.database.AddInParameter(storedProcCommand, "RedPagerActivityName", DbType.String, orderInfo.RedPagerActivityName);
            this.database.AddInParameter(storedProcCommand, "RedPagerID", DbType.String, orderInfo.RedPagerID);
            this.database.AddInParameter(storedProcCommand, "RedPagerOrderAmountCanUse", DbType.Currency, orderInfo.RedPagerOrderAmountCanUse);
            this.database.AddInParameter(storedProcCommand, "RedPagerAmount", DbType.Currency, orderInfo.RedPagerAmount);
            if (orderInfo.GroupBuyId > 0)
            {
                this.database.AddInParameter(storedProcCommand, "GroupBuyId", DbType.Int32, orderInfo.GroupBuyId);
                this.database.AddInParameter(storedProcCommand, "NeedPrice", DbType.Currency, orderInfo.NeedPrice);
                this.database.AddInParameter(storedProcCommand, "GroupBuyStatus", DbType.Int32, 1);
            }
            else
            {
                this.database.AddInParameter(storedProcCommand, "GroupBuyId", DbType.Int32, DBNull.Value);
                this.database.AddInParameter(storedProcCommand, "NeedPrice", DbType.Currency, DBNull.Value);
                this.database.AddInParameter(storedProcCommand, "GroupBuyStatus", DbType.Int32, DBNull.Value);
            }
            if (orderInfo.CountDownBuyId > 0)
            {
                this.database.AddInParameter(storedProcCommand, "CountDownBuyId ", DbType.Int32, orderInfo.CountDownBuyId);
            }
            else
            {
                this.database.AddInParameter(storedProcCommand, "CountDownBuyId ", DbType.Int32, DBNull.Value);
            }
            if (orderInfo.BundlingID > 0)
            {
                this.database.AddInParameter(storedProcCommand, "BundlingID ", DbType.Int32, orderInfo.BundlingID);
                this.database.AddInParameter(storedProcCommand, "BundlingPrice", DbType.Currency, orderInfo.BundlingPrice);
            }
            else
            {
                this.database.AddInParameter(storedProcCommand, "BundlingID ", DbType.Int32, DBNull.Value);
                this.database.AddInParameter(storedProcCommand, "BundlingPrice", DbType.Currency, DBNull.Value);
            }
            this.database.AddInParameter(storedProcCommand, "Tax", DbType.Currency, orderInfo.Tax);
            this.database.AddInParameter(storedProcCommand, "InvoiceTitle", DbType.String, orderInfo.InvoiceTitle);
            this.database.AddInParameter(storedProcCommand, "ReferralUserId", DbType.Int32, orderInfo.ReferralUserId);
            this.database.AddInParameter(storedProcCommand, "DiscountAmount", DbType.Decimal, orderInfo.DiscountAmount);
            this.database.AddInParameter(storedProcCommand, "ActivitiesId", DbType.String, orderInfo.ActivitiesId);
            this.database.AddInParameter(storedProcCommand, "ActivitiesName", DbType.String, orderInfo.ActivitiesName);
            this.database.AddInParameter(storedProcCommand, "FirstCommission", DbType.Decimal, orderInfo.FirstCommission);
            this.database.AddInParameter(storedProcCommand, "SecondCommission", DbType.Decimal, orderInfo.SecondCommission);
            this.database.AddInParameter(storedProcCommand, "ThirdCommission", DbType.Decimal, orderInfo.ThirdCommission);
            this.database.AddInParameter(storedProcCommand, "OrderType", DbType.Int32, orderInfo.OrderType);
            this.database.AddInParameter(storedProcCommand, "MemberVPGiftId", DbType.Int32, orderInfo.MemberVPGiftId);
            this.database.AddInParameter(storedProcCommand, "MemberVPGiftDetailId", DbType.Int32, orderInfo.MemberVPGiftDetailId);
            this.database.AddInParameter(storedProcCommand, "MemberGiftMoney", DbType.Decimal, orderInfo.MemberGiftMoney);
            this.database.AddInParameter(storedProcCommand, "StoreVPGiftId", DbType.Int32, orderInfo.StoreVPGiftId);
            this.database.AddInParameter(storedProcCommand, "StoreVPGiftDetailId", DbType.Int32, orderInfo.StoreVPGiftDetailId);
            this.database.AddInParameter(storedProcCommand, "StoreGiftMoney", DbType.Decimal, orderInfo.StoreGiftMoney);
            this.database.AddInParameter(storedProcCommand, "IdCard", DbType.String, orderInfo.IdCard);
            return (this.database.ExecuteNonQuery(storedProcCommand, dbTran) == 1);
        }

        public int DeleteOrders(string orderIds)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("DELETE FROM Hishop_Orders WHERE OrderId IN({0})", orderIds));
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool EditOrderShipNumber(string orderId, string shipNumber)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_Orders SET ShipOrderNumber=@ShipOrderNumber WHERE OrderId =@OrderId");
            this.database.AddInParameter(sqlStringCommand, "ShipOrderNumber", DbType.String, shipNumber);
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public decimal GetCommissionByOrderId(string orderId, int userId)
        {
            string query = "select CommTotal from Hishop_Commissions WHERE OrderId=@OrderId AND UserId=@UserId";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            object obj2 = this.database.ExecuteScalar(sqlStringCommand);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return 0M;
            }
            return (decimal) obj2;
        }

        public DataSet GetDistributorOrder(OrderQuery query)
        {
            string str = string.Empty;
            if (query.Status != OrderStatus.All)
            {
                str = str + " AND OrderStatus=" + ((int) query.Status);
            }
            string str2 = "SELECT OrderId, OrderDate, OrderStatus,PaymentTypeId, OrderTotal,Gateway,ISNULL(FirstCommission, 0) AS FirstCommission,ISNULL(SecondCommission, 0) AS SecondCommission,ISNULL(ThirdCommission, 0) AS ThirdCommission, Freight, AdjustedFreight FROM Hishop_Orders o WHERE ReferralUserId = @UserId";
            str2 = (str2 + str + " ORDER BY OrderDate DESC") + " SELECT OrderId,SkuId, ThumbnailsUrl, ItemDescription, SKUContent, SKU, ProductId,Quantity,ISNULL(ItemListPrice, 0) AS ItemListPrice,ISNULL(ItemAdjustedCommssion, 0) AS ItemAdjustedCommssion,OrderItemsStatus,ISNULL(ItemsCommission, 0) AS ItemsCommission FROM Hishop_OrderItems WHERE OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE ReferralUserId = @UserId" + str + ")";
            XTrace.WriteLine("获取分销商店铺订单SQL：－－－" + str2);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(str2);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, query.UserId);
            DataSet set = this.database.ExecuteDataSet(sqlStringCommand);
            DataColumn parentColumn = set.Tables[0].Columns["OrderId"];
            DataColumn childColumn = set.Tables[1].Columns["OrderId"];
            DataRelation relation = new DataRelation("OrderItems", parentColumn, childColumn);
            set.Relations.Add(relation);
            return set;
        }

        public int GetDistributorOrderCount(OrderQuery query)
        {
            string str = string.Empty;
            switch (query.Status)
            {
                case OrderStatus.Finished:
                    str = str + " AND OrderStatus=" + ((int) query.Status);
                    break;

                case OrderStatus.Today:
                {
                    string str2 = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                    str = str + " AND OrderDate>='" + str2 + "'";
                    break;
                }
            }
            string str3 = "SELECT COUNT(*)  FROM Hishop_Orders o WHERE ReferralUserId = @ReferralUserId";
            str3 = str3 + str;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(str3);
            sqlStringCommand.CommandType = CommandType.Text;
            this.database.AddInParameter(sqlStringCommand, "ReferralUserId", DbType.Int32, query.UserId);
            return (int) this.database.ExecuteScalar(sqlStringCommand);
        }

        public DataSet GetOrderGoods(string orderIds)
        {
            this.database = DatabaseFactory.CreateDatabase();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT OrderId, ItemDescription AS ProductName, SKU, SKUContent, ShipmentQuantity,");
            builder.Append(" (SELECT Stock FROM Hishop_SKUs WHERE SkuId = oi.SkuId) + oi.ShipmentQuantity AS Stock, (SELECT Remark FROM Hishop_Orders WHERE OrderId = oi.OrderId) AS Remark");
            builder.Append(" FROM Hishop_OrderItems oi WHERE OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE OrderStatus = 2 or (OrderStatus = 1 AND Gateway='hishop.plugins.payment.podrequest'))");
            builder.Append(" AND (OrderItemsStatus=2 OR OrderItemsStatus=1)");
            builder.AppendFormat(" AND OrderId IN ({0}) ORDER BY OrderId;", orderIds);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand);
        }

        public OrderInfo GetOrderInfo(string orderId)
        {
            OrderInfo info = null;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_Orders Where OrderId = @OrderId;  SELECT * FROM Hishop_OrderItems Where OrderId = @OrderId ");
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = DataMapper.PopulateOrder(reader);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    info.LineItems.Add((string) reader["SkuId"], DataMapper.PopulateLineItem(reader));
                }
            }
            return info;
        }

        public DbQueryResult GetOrders(OrderQuery query)
        {
            StringBuilder builder = new StringBuilder("1=1");
            if (query.Type.HasValue)
            {
                if (((OrderQuery.OrderType) query.Type.Value) == OrderQuery.OrderType.GroupBuy)
                {
                    builder.Append(" And GroupBuyId > 0 ");
                }
                else
                {
                    builder.Append(" And GroupBuyId is null ");
                }
            }
            if ((query.OrderId != string.Empty) && (query.OrderId != null))
            {
                builder.AppendFormat(" AND OrderId = '{0}'", DataHelper.CleanSearchString(query.OrderId));
            }
            if (query.UserId.HasValue)
            {
                builder.AppendFormat(" AND UserId = '{0}'", query.UserId.Value);
            }
            if (query.PaymentType.HasValue)
            {
                builder.AppendFormat(" AND PaymentTypeId = '{0}'", query.PaymentType.Value);
            }
            if (query.GroupBuyId.HasValue)
            {
                builder.AppendFormat(" AND GroupBuyId = {0}", query.GroupBuyId.Value);
            }
            if (!string.IsNullOrEmpty(query.ProductName))
            {
                builder.AppendFormat(" AND OrderId IN (SELECT OrderId FROM Hishop_OrderItems WHERE ItemDescription LIKE '%{0}%')", DataHelper.CleanSearchString(query.ProductName));
            }
            if (!string.IsNullOrEmpty(query.ShipTo))
            {
                builder.AppendFormat(" AND ShipTo LIKE '%{0}%'", DataHelper.CleanSearchString(query.ShipTo));
            }
            if (query.RegionId.HasValue)
            {
                builder.AppendFormat(" AND ShippingRegion like '%{0}%'", DataHelper.CleanSearchString(RegionHelper.GetFullRegion(query.RegionId.Value, "，")));
            }
            if (!string.IsNullOrEmpty(query.UserName))
            {
                builder.AppendFormat(" AND  UserName  = '{0}' ", DataHelper.CleanSearchString(query.UserName));
            }
            if (query.Status == OrderStatus.History)
            {
                builder.AppendFormat(" AND OrderStatus != {0} AND OrderStatus != {1} AND OrderStatus != {2} AND OrderDate < '{3}'", new object[] { 1, 4, 9, DateTime.Now.AddMonths(-3) });
            }
            else if (query.Status == OrderStatus.BuyerAlreadyPaid)
            {
                builder.AppendFormat(" AND (OrderStatus = {0} OR (OrderStatus = 1 AND Gateway = 'hishop.plugins.payment.podrequest'))", (int) query.Status);
            }
            else if (query.Status != OrderStatus.All)
            {
                builder.AppendFormat(" AND OrderStatus = {0}", (int) query.Status);
            }
            if (query.OrderStatus.HasValue)
            {
                builder.AppendFormat(" AND OrderStatus = {0}", query.OrderStatus.Value);
            }
            if (query.StartDate.HasValue)
            {
                builder.AppendFormat(" AND datediff(dd,'{0}',OrderDate)>=0", DataHelper.GetSafeDateTimeFormat(query.StartDate.Value));
            }
            if (query.EndDate.HasValue)
            {
                builder.AppendFormat(" AND datediff(dd,'{0}',OrderDate)<=0", DataHelper.GetSafeDateTimeFormat(query.EndDate.Value));
            }
            if (query.ShippingModeId.HasValue)
            {
                builder.AppendFormat(" AND ShippingModeId = {0}", query.ShippingModeId.Value);
            }
            if (query.IsPrinted.HasValue)
            {
                builder.AppendFormat(" AND ISNULL(IsPrinted, 0)={0}", query.IsPrinted.Value);
            }
            if (query.IsCrossOrder.HasValue)
            {
                builder.AppendFormat(" AND ISNULL(IsCrossOrder, 0)={0}", query.IsCrossOrder.Value);
            }
            if (query.OrderCategory.HasValue)
            {
                builder.AppendFormat(" AND ISNULL(OrderType, 0)={0}", query.OrderCategory.Value);
            }
            if (query.ShippingModeId > 0)
            {
                builder.AppendFormat(" AND ShippingModeId={0}", query.ShippingModeId);
            }
            if (query.BrandId.HasValue)
            {
                builder.AppendFormat(" AND OrderId IN ( SELECT OrderId FROM Hishop_OrderItems WHERE ProductId IN ( SELECT ProductId FROM hishop_products WHERE BrandId = {0} ) )", query.BrandId.Value);
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_Order", "OrderId", builder.ToString(), "*");
        }

        public DataSet GetOrdersAndLines(string orderIds)
        {
            this.database = DatabaseFactory.CreateDatabase();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM Hishop_Orders WHERE OrderStatus > 0 AND OrderStatus < 4 AND OrderId IN ({0}) order by OrderDate desc ", orderIds);
            builder.AppendFormat(" SELECT * FROM Hishop_OrderItems WHERE OrderId IN ({0});", orderIds);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand);
        }

        public DataSet GetProductGoods(string orderIds)
        {
            this.database = DatabaseFactory.CreateDatabase();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ItemDescription AS ProductName, SKU, SKUContent, sum(ShipmentQuantity) as ShipmentQuantity,");
            builder.Append(" (SELECT Stock FROM Hishop_SKUs WHERE SkuId = oi.SkuId) + sum(ShipmentQuantity) AS Stock FROM Hishop_OrderItems oi");
            builder.Append(" WHERE OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE OrderStatus = 2 or (OrderStatus = 1 AND Gateway='hishop.plugins.payment.podrequest'))");
            builder.Append(" AND OrderItemsStatus=2");
            builder.AppendFormat(" AND OrderId in ({0}) GROUP BY ItemDescription, SkuId, SKU, SKUContent;", orderIds);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand);
        }

        public string GetReplaceComments(string orderId)
        {
            string query = "select Comments from Hishop_OrderReplace where HandleStatus=0 and OrderId='" + orderId + "'";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            object obj2 = this.database.ExecuteScalar(sqlStringCommand);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return "";
            }
            return obj2.ToString();
        }

        public DataTable GetSendGoodsOrders(string orderIds)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM Hishop_Orders WHERE (OrderStatus = 2 OR (OrderStatus = 1 AND Gateway = 'hishop.plugins.payment.podrequest')) AND OrderId IN ({0}) order by OrderDate desc", orderIds));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public DataSet GetUserOrder(int userId, OrderQuery query)
        {
            string str = string.Empty;
            if (query.Status == OrderStatus.WaitBuyerPay)
            {
                str = str + " AND OrderStatus = 1 AND Gateway <> 'hishop.plugins.payment.podrequest'";
            }
            else if (query.Status == OrderStatus.SellerAlreadySent)
            {
                str = str + " AND (OrderStatus = 2 OR OrderStatus = 3 OR (OrderStatus = 1 AND Gateway = 'hishop.plugins.payment.podrequest'))";
            }
            string str2 = "SELECT OrderId, OrderDate, OrderStatus,PaymentTypeId, OrderTotal, Gateway,(SELECT count(0) FROM vshop_OrderRedPager WHERE OrderId = o.OrderId and ExpiryDays<getdate() and AlreadyGetTimes<MaxGetTimes) as HasRedPage,(SELECT SUM(Quantity) FROM Hishop_OrderItems WHERE OrderId = o.OrderId) as ProductSum FROM Hishop_Orders o WHERE UserId = @UserId";
            str2 = (str2 + str + " ORDER BY OrderDate DESC") + " SELECT OrderId, ThumbnailsUrl, ItemDescription, SKUContent, SKU,OrderItemsStatus, ProductId,Quantity FROM Hishop_OrderItems WHERE OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE UserId = @UserId" + str + ")";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(str2);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            DataSet set = this.database.ExecuteDataSet(sqlStringCommand);
            DataColumn parentColumn = set.Tables[0].Columns["OrderId"];
            DataColumn childColumn = set.Tables[1].Columns["OrderId"];
            DataRelation relation = new DataRelation("OrderItems", parentColumn, childColumn);
            set.Relations.Add(relation);
            return set;
        }

        public int GetUserOrderCount(int userId, OrderQuery query)
        {
            string str = string.Empty;
            if (query.Status == OrderStatus.WaitBuyerPay)
            {
                str = str + " AND OrderStatus = 1 AND Gateway <> 'hishop.plugins.payment.podrequest'";
            }
            else if (query.Status == OrderStatus.SellerAlreadySent)
            {
                str = str + " AND (OrderStatus = 2 OR OrderStatus = 3 OR (OrderStatus = 1 AND Gateway = 'hishop.plugins.payment.podrequest'))";
            }
            string str2 = "SELECT COUNT(1)  FROM Hishop_Orders o WHERE UserId = @UserId";
            str2 = str2 + str;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(str2);
            sqlStringCommand.CommandType = CommandType.Text;
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            return (int) this.database.ExecuteScalar(sqlStringCommand);
        }

        public DataSet GetUserOrderReturn(int userId, OrderQuery query)
        {
            string str = string.Empty + " AND (OrderStatus = 2 OR OrderStatus = 3)";
            //string str = string.Empty + " AND (OrderStatus = 2)";
            string str2 = "SELECT OrderId, OrderDate, OrderStatus,PaymentTypeId, OrderTotal, (SELECT SUM(Quantity) FROM Hishop_OrderItems WHERE OrderId = o.OrderId) as ProductSum FROM Hishop_Orders o WHERE UserId = @UserId";
            str2 = (str2 + str + " ORDER BY OrderDate DESC") + " SELECT OrderId, ThumbnailsUrl,Quantity, ItemDescription,OrderItemsStatus, SKUContent, SKU, ProductId FROM Hishop_OrderItems WHERE (OrderItemsStatus=2 OR OrderItemsStatus=3) AND OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE UserId = @UserId" + str + ")";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(str2);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            DataSet set = this.database.ExecuteDataSet(sqlStringCommand);
            DataColumn parentColumn = set.Tables[0].Columns["OrderId"];
            DataColumn childColumn = set.Tables[1].Columns["OrderId"];
            DataRelation relation = new DataRelation("OrderItems", parentColumn, childColumn);
            set.Relations.Add(relation);
            return set;
        }

        public int GetUserOrderReturnCount(int userId)
        {
            object obj2 = string.Empty;
            string str = string.Concat(new object[] { obj2, " AND (OrderItemsStatus = ", 6, " OR OrderItemsStatus =", 7, ")" });
            string query = "SELECT COUNT(*) FROM Hishop_OrderItems WHERE OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE UserId=@UserId)";
            query = query + str;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            return (int) this.database.ExecuteScalar(sqlStringCommand);
        }

        public bool InsertCalculationCommission(ArrayList UserIdList, ArrayList ReferralBlanceList, string orderid, ArrayList OrdersTotalList, string userid)
        {
            string query = "";
            query = query + "begin try  " + "  begin tran TranUpdate";
            for (int i = 0; i < UserIdList.Count; i++)
            {
                object obj2 = query;
                query = string.Concat(new object[] { obj2, " INSERT INTO [Hishop_Commissions]([UserId],[ReferralUserId],[OrderId],[OrderTotal],[CommTotal],[CommType],[State])VALUES(", UserIdList[i], ",", userid, ",'", orderid, "',", OrdersTotalList[i], ",", ReferralBlanceList[i], ",1,0);" });
            }
            query = query + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool SetOrderExpressComputerpe(string orderIds, string expressCompanyName, string expressCompanyAbb)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("UPDATE Hishop_Orders SET ExpressCompanyName=@ExpressCompanyName,ExpressCompanyAbb=@ExpressCompanyAbb WHERE (OrderStatus = 2 OR (OrderStatus = 1 AND Gateway='hishop.plugins.payment.podrequest')) AND OrderId IN ({0})", orderIds));
            this.database.AddInParameter(sqlStringCommand, "ExpressCompanyName", DbType.String, expressCompanyName);
            this.database.AddInParameter(sqlStringCommand, "ExpressCompanyAbb", DbType.String, expressCompanyAbb);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool SetOrderShippingMode(string orderIds, int realShippingModeId, string realModeName)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("UPDATE Hishop_Orders SET RealShippingModeId=@RealShippingModeId,RealModeName=@RealModeName WHERE (OrderStatus = 2 OR (OrderStatus = 1 AND Gateway='hishop.plugins.payment.podrequest')) AND OrderId IN ({0})", orderIds));
            this.database.AddInParameter(sqlStringCommand, "RealShippingModeId", DbType.Int32, realShippingModeId);
            this.database.AddInParameter(sqlStringCommand, "RealModeName", DbType.String, realModeName);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateCommossionByOrderId(string orderId, decimal adjustcommssion, int userId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("Update Hishop_Commissions Set CommTotal =CommTotal-@AdjustCommssion Where OrderId =@OrderId AND UserId=@UserId");
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            this.database.AddInParameter(sqlStringCommand, "AdjustCommssion", DbType.Decimal, adjustcommssion);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int64, userId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) >= 1);
        }

        public void UpdateItemsStatus(string orderId, int status, string ItemStr)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("Update Hishop_OrderItems Set OrderItemsStatus=@OrderItemsStatus Where OrderId =@OrderId and SkuId IN (" + ItemStr + ")");
            this.database.AddInParameter(sqlStringCommand, "OrderItemsStatus", DbType.Int32, status);
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool UpdateOrder(OrderInfo order, DbTransaction dbTran = null)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_Orders SET  OrderStatus = @OrderStatus, CloseReason=@CloseReason, PayDate = @PayDate, ShippingDate=@ShippingDate, FinishDate = @FinishDate, RegionId = @RegionId, ShippingRegion = @ShippingRegion, Address = @Address, ZipCode = @ZipCode,ShipTo = @ShipTo, TelPhone = @TelPhone, CellPhone = @CellPhone, ShippingModeId=@ShippingModeId ,ModeName=@ModeName, RealShippingModeId = @RealShippingModeId, RealModeName = @RealModeName, ShipOrderNumber = @ShipOrderNumber,  ExpressCompanyName = @ExpressCompanyName,ExpressCompanyAbb = @ExpressCompanyAbb, PaymentTypeId=@PaymentTypeId,PaymentType=@PaymentType, Gateway = @Gateway, ManagerMark=@ManagerMark,ManagerRemark=@ManagerRemark,IsPrinted=@IsPrinted, OrderTotal = @OrderTotal, OrderProfit=@OrderProfit,Amount=@Amount,OrderCostPrice=@OrderCostPrice, AdjustedFreight = @AdjustedFreight, PayCharge = @PayCharge, AdjustedDiscount=@AdjustedDiscount,OrderPoint=@OrderPoint,GatewayOrderId=@GatewayOrderId WHERE OrderId = @OrderId");
            this.database.AddInParameter(sqlStringCommand, "OrderStatus", DbType.Int32, (int) order.OrderStatus);
            this.database.AddInParameter(sqlStringCommand, "CloseReason", DbType.String, order.CloseReason);
            this.database.AddInParameter(sqlStringCommand, "PayDate", DbType.DateTime, order.PayDate);
            this.database.AddInParameter(sqlStringCommand, "ShippingDate", DbType.DateTime, order.ShippingDate);
            this.database.AddInParameter(sqlStringCommand, "FinishDate", DbType.DateTime, order.FinishDate);
            this.database.AddInParameter(sqlStringCommand, "RegionId", DbType.String, order.RegionId);
            this.database.AddInParameter(sqlStringCommand, "ShippingRegion", DbType.String, order.ShippingRegion);
            this.database.AddInParameter(sqlStringCommand, "Address", DbType.String, order.Address);
            this.database.AddInParameter(sqlStringCommand, "ZipCode", DbType.String, order.ZipCode);
            this.database.AddInParameter(sqlStringCommand, "ShipTo", DbType.String, order.ShipTo);
            this.database.AddInParameter(sqlStringCommand, "TelPhone", DbType.String, order.TelPhone);
            this.database.AddInParameter(sqlStringCommand, "CellPhone", DbType.String, order.CellPhone);
            this.database.AddInParameter(sqlStringCommand, "ShippingModeId", DbType.Int32, order.ShippingModeId);
            this.database.AddInParameter(sqlStringCommand, "ModeName", DbType.String, order.ModeName);
            this.database.AddInParameter(sqlStringCommand, "RealShippingModeId", DbType.Int32, order.RealShippingModeId);
            this.database.AddInParameter(sqlStringCommand, "RealModeName", DbType.String, order.RealModeName);
            this.database.AddInParameter(sqlStringCommand, "ShipOrderNumber", DbType.String, order.ShipOrderNumber);
            this.database.AddInParameter(sqlStringCommand, "ExpressCompanyName", DbType.String, order.ExpressCompanyName);
            this.database.AddInParameter(sqlStringCommand, "ExpressCompanyAbb", DbType.String, order.ExpressCompanyAbb);
            this.database.AddInParameter(sqlStringCommand, "PaymentTypeId", DbType.Int32, order.PaymentTypeId);
            this.database.AddInParameter(sqlStringCommand, "PaymentType", DbType.String, order.PaymentType);
            this.database.AddInParameter(sqlStringCommand, "Gateway", DbType.String, order.Gateway);
            this.database.AddInParameter(sqlStringCommand, "ManagerMark", DbType.Int32, order.ManagerMark);
            this.database.AddInParameter(sqlStringCommand, "ManagerRemark", DbType.String, order.ManagerRemark);
            this.database.AddInParameter(sqlStringCommand, "IsPrinted", DbType.Boolean, order.IsPrinted);
            this.database.AddInParameter(sqlStringCommand, "OrderTotal", DbType.Currency, order.GetTotal());
            this.database.AddInParameter(sqlStringCommand, "OrderProfit", DbType.Currency, order.GetProfit());
            this.database.AddInParameter(sqlStringCommand, "Amount", DbType.Currency, order.GetAmount());
            this.database.AddInParameter(sqlStringCommand, "OrderCostPrice", DbType.Currency, order.GetCostPrice());
            this.database.AddInParameter(sqlStringCommand, "AdjustedFreight", DbType.Currency, order.AdjustedFreight);
            this.database.AddInParameter(sqlStringCommand, "PayCharge", DbType.Currency, order.PayCharge);
            this.database.AddInParameter(sqlStringCommand, "AdjustedDiscount", DbType.Currency, order.AdjustedDiscount);
            this.database.AddInParameter(sqlStringCommand, "OrderPoint", DbType.Int32, order.Points);
            this.database.AddInParameter(sqlStringCommand, "GatewayOrderId", DbType.String, order.GatewayOrderId);
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, order.OrderId);
            if (dbTran != null)
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand, dbTran) > 0);
            }
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public void UpdatePayOrderStock(string orderId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("Update Hishop_SKUs Set Stock = CASE WHEN (Stock - (SELECT SUM(oi.ShipmentQuantity) FROM Hishop_OrderItems oi Where oi.SkuId =Hishop_SKUs.SkuId AND OrderId =@OrderId))<=0 Then 0 ELSE Stock - (SELECT SUM(oi.ShipmentQuantity) FROM Hishop_OrderItems oi  Where oi.SkuId =Hishop_SKUs.SkuId AND OrderId =@OrderId) END WHERE Hishop_SKUs.SkuId  IN (Select SkuId FROM Hishop_OrderItems Where OrderId =@OrderId)");
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool UpdateRefundOrderStock(string orderId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("Update Hishop_SKUs Set Stock = Stock + (SELECT SUM(oi.ShipmentQuantity) FROM Hishop_OrderItems oi  Where oi.SkuId =Hishop_SKUs.SkuId AND OrderId =@OrderId) WHERE Hishop_SKUs.SkuId  IN (Select SkuId FROM Hishop_OrderItems Where OrderId =@OrderId)");
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) >= 1);
        }

        /// <summary>
        /// 更新订单的直接销售佣金、上级销售佣金、上上级销售佣金
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="FirstCommission"></param>
        /// <param name="SecondCommission"></param>
        /// <param name="ThirdCommission"></param>
        /// <returns></returns>
        public bool UpdateOrderCommission(string orderId, decimal FirstCommission, decimal SecondCommission, decimal ThirdCommission, int FirstUserId, int SecondUserId, int ThirdUserId, decimal ManagerCommission)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("Update Hishop_Orders Set FirstCommission = @FirstCommission, SecondCommission = @SecondCommission, ThirdCommission = @ThirdCommission, FirstUserId = @FirstUserId, SecondUserId = @SecondUserId, ThirdUserId = @ThirdUserId, ManagerCommission = @ManagerCommission Where OrderId = @OrderId");
            this.database.AddInParameter(sqlStringCommand, "FirstCommission", DbType.Decimal, FirstCommission);
            this.database.AddInParameter(sqlStringCommand, "SecondCommission", DbType.Decimal, SecondCommission);
            this.database.AddInParameter(sqlStringCommand, "ThirdCommission", DbType.Decimal, ThirdCommission);
            this.database.AddInParameter(sqlStringCommand, "FirstUserId", DbType.Int32, FirstUserId);
            this.database.AddInParameter(sqlStringCommand, "SecondUserId", DbType.Int32, SecondUserId);
            this.database.AddInParameter(sqlStringCommand, "ThirdUserId", DbType.Int32, ThirdUserId);
            this.database.AddInParameter(sqlStringCommand, "ManagerCommission", DbType.Decimal, ManagerCommission);
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) >= 1);
        }

        public IList<CommissionInfo> GetCommissionByOrderId(string orderId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_Commissions WHERE IncomeType = 1 AND OrderId = '" + orderId + "'");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<CommissionInfo>(reader);
            }
        }

        public DistributorsInfo GetDistributorInfo(int distributorId)
        {
            if (distributorId <= 0)
            {
                return null;
            }
            DistributorsInfo info = null;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM aspnet_Distributors where UserId={0}", distributorId));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = DataMapper.PopulateDistributorInfo(reader);
                }
            }
            return info;
        }


        public bool UpdateDeductionCommission(int userId, string orderId, int commId, int commOrderStatus, decimal commTotal)
        {
            DistributorsInfo di = this.GetDistributorInfo(userId);
            if (null != di)
            {
                XTrace.WriteLine("退货时佣金返还前分销商账户余额(" + orderId + ")(" + userId + ")：--累计收益--" + di.AccumulatedIncome + " --账户余额--" + di.AccountBalance + " --可提现余额--" + di.ReferralBlance);
            }

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("BEGIN TRY ");
            sql.AppendLine("    BEGIN TRAN TranUpdate ");
            if (commOrderStatus == 1)
            {
                sql.AppendLine("        UPDATE aspnet_Distributors SET AccumulatedIncome = AccumulatedIncome - " + commTotal + ", AccountBalance = AccountBalance - " + commTotal + ", ReferralBlance = ReferralBlance - " + commTotal + " WHERE UserId = " + userId + " ");
            }
            else
            {
                sql.AppendLine("        UPDATE aspnet_Distributors SET AccumulatedIncome = AccumulatedIncome - " + commTotal + ", AccountBalance = AccountBalance - " + commTotal + " WHERE UserId = " + userId + " ");
            }
            //sql.AppendLine("        UPDATE Hishop_Commissions SET State = 0 WHERE CommId = " + commId + " ");
            sql.AppendLine("        INSERT INTO Hishop_Commissions( UserId, ReferralUserId, OrderId, TradeTime, OrderTotal, CommTotal, CommType, State, CommRemark, OrderFromStoreId, CommOrderStatus, IncomeType ) ");
            sql.AppendLine("        SELECT      UserId, ReferralUserId, OrderId, GETDATE(), OrderTotal, (0 - CommTotal), CommType, State, CommRemark, OrderFromStoreId, CommOrderStatus, 2 ");
            sql.AppendLine("        FROM        Hishop_Commissions WHERE CommId = " + commId + " ");
            sql.AppendLine("    COMMIT TRAN TranUpdate ");
            sql.AppendLine("END TRY ");
            sql.AppendLine("BEGIN CATCH ");
            sql.AppendLine("    ROLLBACK TRAN TranUpdate ");
            sql.AppendLine("END CATCH ");

            XTrace.WriteLine("退货时佣金返还SQL(" + orderId + ")(" + userId + ")：\r\n" + sql.ToString());

            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);

        }

        public bool UpdateDeductionVirtualPoint(OrderInfo order)
        {
            MemberInfo member = new MemberDao().GetMember(order.UserId);
            if (null != member)
            {
                XTrace.WriteLine("退货时虚似币返还前分销商账户余额(" + order.OrderId + ")(" + order.UserId + ")：--虚似币--" + member.VirtualPoints );
            }

            StringBuilder sql = new StringBuilder();
            //sql.AppendLine("BEGIN TRY ");
            //sql.AppendLine("    BEGIN TRAN TranUpdate ");
            sql.AppendLine("        UPDATE aspnet_Members SET VirtualPoints = ISNULL(VirtualPoints, 0) + " + order.RedPagerAmount + " WHERE UserId = " + order.UserId + " ");
            //sql.AppendLine("        UPDATE Hishop_Commissions SET State = 0 WHERE CommId = " + commId + " ");
            //sql.AppendLine("    COMMIT TRAN TranUpdate ");
            //sql.AppendLine("END TRY ");
            //sql.AppendLine("BEGIN CATCH ");
            //sql.AppendLine("    ROLLBACK TRAN TranUpdate ");
            //sql.AppendLine("END CATCH ");

            XTrace.WriteLine("退货时虚似币返还SQL(" + order.OrderId + ")(" + order.UserId + ")：\r\n" + sql.ToString());

            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public long GetMaxPrintBatch()
        {
            DbCommand sqlCommand = this.database.GetSqlStringCommand("SELECT MAX(ISNULL(PrintBatch, 0)) + 1 AS PrintBatch FROM Hishop_Orders");
            long ret = 0;
            Int64.TryParse(this.database.ExecuteScalar(sqlCommand).ToString(), out ret);
            return ret;

        }


        public bool UpdateSetPrintBatch(string orderId, long printBatch, DateTime printBatchTime, bool isPrinted)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("Update Hishop_Orders Set IsPrinted = @IsPrinted, PrintBatch = @PrintBatch, PrintBatchDate = @PrintBatchDate Where OrderId = @OrderId");
            this.database.AddInParameter(sqlStringCommand, "IsPrinted", DbType.Boolean, isPrinted);
            this.database.AddInParameter(sqlStringCommand, "PrintBatch", DbType.Int64, printBatch);
            this.database.AddInParameter(sqlStringCommand, "PrintBatchDate", DbType.DateTime, printBatchTime);
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) >= 1);
        }

        public DataTable GetOrderBrand(string orderId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM vw_Hishop_OrderBrand ");
            if (!string.IsNullOrEmpty(orderId))
            {
                sql.AppendLine(" WHERE OrderId = '" + orderId + "' ");
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public bool AddSubOrderData(string orderId, string subOrderId, string productBrandId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO Hishop_OrderSubLogistic(SubOrderId,OrderId,ProductBrandId,SubExpressCompanyAbb,SubExpressCompanyName,SubShipOrderNumber) VALUES(@SubOrderId,@OrderId,@ProductBrandId,'', '', '')");
            this.database.AddInParameter(sqlStringCommand, "SubOrderId", DbType.String, subOrderId);
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            this.database.AddInParameter(sqlStringCommand, "ProductBrandId", DbType.String, productBrandId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public IList<OrderSubLogisticInfo> GetSubOrderLogisticByOrderId(string orderId, bool isSubOrderId = false)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM Hishop_OrderSubLogistic ");
            if (isSubOrderId)
            {
                sql.AppendLine("WHERE ProductBrandId <> 0 AND SubOrderId = '" + orderId + "'");
            }
            else
            {
                sql.AppendLine("WHERE ProductBrandId <> 0 AND OrderId = '" + orderId + "'");
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<OrderSubLogisticInfo>(reader);
            }
        }

        public IList<string> GetSubOrderByOrderId(string orderId, bool isSubOrderId = false)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM Hishop_OrderSubLogistic AS osl LEFT OUTER JOIN Hishop_BrandCategories AS bc ON osl.ProductBrandId = bc.BrandId ");
            if (isSubOrderId)
            {
                sql.AppendLine("WHERE osl.ProductBrandId <> 0 AND osl.SubOrderId = '" + orderId + "'");
            }
            else
            {
                sql.AppendLine("WHERE osl.ProductBrandId <> 0 AND osl.OrderId = '" + orderId + "'");
            }
            IList<string> list = new List<string>();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    list.Add((string)reader["SubOrderId"]);
                }
            }
            return list;
        }

        public DataTable GetSubOrderCVTDTByOrderId(string orderId, bool isSubOrderId = false)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM Hishop_OrderSubLogistic AS osl LEFT OUTER JOIN Hishop_BrandCategories AS bc ON osl.ProductBrandId = bc.BrandId ");
            if (isSubOrderId)
            {
                sql.AppendLine("WHERE osl.ProductBrandId <> 0 AND osl.SubOrderId = '" + orderId + "'");
            }
            else
            {
                sql.AppendLine("WHERE osl.ProductBrandId <> 0 AND osl.OrderId = '" + orderId + "'");
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }

        }

        public bool UpdateSubOrderExpressNo(string orderId, string subOrderId, string subExpressCompanyAbb, string subExpressCompanyName, string subShipOrderNumber)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("Update Hishop_OrderSubLogistic Set SubExpressCompanyAbb = @SubExpressCompanyAbb, SubExpressCompanyName = @SubExpressCompanyName, SubShipOrderNumber = @SubShipOrderNumber Where OrderId = @OrderId AND SubOrderId = @SubOrderId ");
            this.database.AddInParameter(sqlStringCommand, "SubExpressCompanyAbb", DbType.String, subExpressCompanyAbb);
            this.database.AddInParameter(sqlStringCommand, "SubExpressCompanyName", DbType.String, subExpressCompanyName);
            this.database.AddInParameter(sqlStringCommand, "SubShipOrderNumber", DbType.String, subShipOrderNumber);
            this.database.AddInParameter(sqlStringCommand, "SubOrderId", DbType.String, subOrderId);
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) >= 1);
        }


        public DataTable GetExportOrderSubLogistic(string orderIds)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM vw_Hishop_OrderSubLogistic ");
            sql.AppendLine("WHERE OrderId IN ( " + orderIds + " ) ");
            sql.AppendLine("ORDER BY ProductBrandId ASC, OrderId ASC, SubOrderId ASC ");

            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public bool IsExistCommissionByUser(CommissionInfo ci)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT Commid FROM Hishop_Commissions WHERE OrderId = @OrderId AND [State] = 1 AND IncomeType = 2 AND UserId = @UserId AND CommTotal = @CommTotal AND CommType = @CommType AND OrderFromStoreId = @OrderFromStoreId ");
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, ci.OrderId);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, ci.UserId);
            this.database.AddInParameter(sqlStringCommand, "CommTotal", DbType.Decimal, (0 - ci.CommTotal));
            this.database.AddInParameter(sqlStringCommand, "CommType", DbType.Int32, ci.CommType);
            this.database.AddInParameter(sqlStringCommand, "OrderFromStoreId", DbType.Int32, ci.OrderFromStoreId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateOrderStatus(string orderId, int orderStatus, DateTime updateDate)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_Orders SET OrderStatus = @OrderStatus, ShippingDate = @ShippingDate, FinishDate = @FinishDate WHERE OrderId = @OrderId");
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            this.database.AddInParameter(sqlStringCommand, "OrderStatus", DbType.Int32, orderStatus);
            this.database.AddInParameter(sqlStringCommand, "ShippingDate", DbType.DateTime, updateDate);
            this.database.AddInParameter(sqlStringCommand, "FinishDate", DbType.DateTime, updateDate);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public OrderInfo GetSJOrderInfo(int userId)
        {
            OrderInfo info = null;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT TOP 1 * FROM Hishop_Orders WHERE OrderStatus IN ( 2, 3, 5 ) AND OrderType = 2 AND OrderId IN ( SELECT OrderId FROM Hishop_OrderItems WHERE ProductId IN ( SELECT ProductId FROM Hishop_Products WHERE IsDistributorBuy = 1 AND PTTypeId = 1 ) ) AND PayDate >= DATEADD(HOUR, -1, GETDATE()) AND UserId = @UserId ");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = DataMapper.PopulateOrder(reader);
                }
            }
            return info;
        }

    }
}

