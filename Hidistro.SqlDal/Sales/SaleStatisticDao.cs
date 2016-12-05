namespace Hidistro.SqlDal.Sales
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.Sales;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SaleStatisticDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        private static string BuildMemberStatisticsQuery(SaleStatisticsQuery query)
        {
            if (null == query)
            {
                throw new ArgumentNullException("query");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT UserId, UserName ");
            if (query.StartDate.HasValue || query.EndDate.HasValue)
            {
                builder.AppendFormat(",  ( select isnull(SUM(OrderTotal),0) from Hishop_Orders where OrderStatus != {0} AND OrderStatus != {1} AND OrderStatus != {2}", 1, 4, 9);
                if (query.StartDate.HasValue)
                {
                    builder.AppendFormat(" and OrderDate>='{0}'", DataHelper.GetSafeDateTimeFormat(query.StartDate.Value));
                }
                if (query.EndDate.HasValue)
                {
                    builder.AppendFormat(" and OrderDate<='{0}'", DataHelper.GetSafeDateTimeFormat(query.EndDate.Value));
                }
                builder.Append(" and userId = aspnet_Members.UserId) as SaleTotals");
                builder.AppendFormat(",(select Count(OrderId) from Hishop_Orders where OrderStatus != {0} AND OrderStatus != {1} AND OrderStatus != {2}", 1, 4, 9);
                if (query.StartDate.HasValue)
                {
                    builder.AppendFormat(" and OrderDate>='{0}'", DataHelper.GetSafeDateTimeFormat(query.StartDate.Value));
                }
                if (query.EndDate.HasValue)
                {
                    builder.AppendFormat(" and OrderDate<='{0}'", DataHelper.GetSafeDateTimeFormat(query.EndDate.Value));
                }
                builder.Append(" and userId = aspnet_Members.UserId) as OrderCount ");
            }
            else
            {
                builder.Append(",ISNULL(Expenditure,0) as SaleTotals,ISNULL(OrderNumber,0) as OrderCount ");
            }
            builder.Append(" from aspnet_Members where Expenditure > 0");
            if (query.StartDate.HasValue || query.EndDate.HasValue)
            {
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                builder.AppendFormat(" ORDER BY {0} {1}", DataHelper.CleanSearchString(query.SortBy), query.SortOrder.ToString());
            }
            return builder.ToString();
        }

        private static string BuildOrdersQuery(OrderQuery query)
        {
            if (null == query)
            {
                throw new ArgumentNullException("query");
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT OrderId FROM Hishop_Orders WHERE 1 = 1 ", new object[0]);
            if ((query.OrderId != string.Empty) && (query.OrderId != null))
            {
                builder.AppendFormat(" AND OrderId = '{0}'", DataHelper.CleanSearchString(query.OrderId));
            }
            else
            {
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
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                builder.AppendFormat(" ORDER BY {0} {1}", DataHelper.CleanSearchString(query.SortBy), query.SortOrder.ToString());
            }
            return builder.ToString();
        }

        private static string BuildProductSaleQuery(SaleStatisticsQuery query)
        {
            if (null == query)
            {
                throw new ArgumentNullException("query");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ProductId, SUM(o.Quantity) AS ProductSaleCounts, SUM(o.ItemAdjustedPrice * o.Quantity) AS ProductSaleTotals,");
            builder.Append("  (SUM(o.ItemAdjustedPrice * o.Quantity) - SUM(o.CostPrice * o.ShipmentQuantity) )AS ProductProfitsTotals ");
            builder.AppendFormat(" FROM Hishop_OrderItems o  WHERE 0=0 ", new object[0]);
            builder.AppendFormat(" AND OrderId IN (SELECT  OrderId FROM Hishop_Orders WHERE OrderStatus != {0} AND OrderStatus != {1} AND OrderStatus != {2})", 1, 4, 9);
            if (query.StartDate.HasValue)
            {
                builder.AppendFormat(" AND OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE OrderDate >= '{0}')", DataHelper.GetSafeDateTimeFormat(query.StartDate.Value));
            }
            if (query.EndDate.HasValue)
            {
                builder.AppendFormat(" AND OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE OrderDate <= '{0}')", DataHelper.GetSafeDateTimeFormat(query.EndDate.Value));
            }
            builder.Append(" GROUP BY ProductId HAVING ProductId IN");
            builder.Append(" (SELECT ProductId FROM Hishop_Products)");
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                builder.AppendFormat(" ORDER BY {0} {1}", DataHelper.CleanSearchString(query.SortBy), query.SortOrder.ToString());
            }
            return builder.ToString();
        }

        private static string BuildProductVisitAndBuyStatisticsQuery(SaleStatisticsQuery query)
        {
            if (null == query)
            {
                throw new ArgumentNullException("query");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ProductId,(SaleCounts*100/(case when VistiCounts=0 then 1 else VistiCounts end)) as BuyPercentage");
            builder.Append(" FROM Hishop_products where SaleCounts>0");
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                builder.AppendFormat(" ORDER BY {0} {1}", DataHelper.CleanSearchString(query.SortBy), query.SortOrder.ToString());
            }
            return builder.ToString();
        }

        private static string BuildRegionsUserQuery(Pagination page)
        {
            if (null == page)
            {
                throw new ArgumentNullException("page");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT r.RegionId, r.RegionName, SUM(au.UserCount) AS Usercounts,");
            builder.Append(" (SELECT (SELECT SUM(COUNT) FROM aspnet_Members)) AS AllUserCounts ");
            builder.Append(" FROM vw_Allregion_Members au, Hishop_Regions r ");
            builder.Append(" WHERE (r.AreaId IS NOT NULL) AND ((au.path LIKE r.path + LTRIM(RTRIM(STR(r.RegionId))) + ',%') OR au.RegionId = r.RegionId)");
            builder.Append(" group by r.RegionId, r.RegionName ");
            builder.Append(" UNION SELECT 0, '0', sum(au.Usercount) AS Usercounts,");
            builder.Append(" (SELECT (SELECT count(*) FROM aspnet_Members)) AS AllUserCounts ");
            builder.Append(" FROM vw_Allregion_Members au, Hishop_Regions r  ");
            builder.Append(" WHERE au.regionid IS NULL OR au.regionid = 0 group by r.RegionId, r.RegionName");
            if (!string.IsNullOrEmpty(page.SortBy))
            {
                builder.AppendFormat(" ORDER BY {0} {1}", DataHelper.CleanSearchString(page.SortBy), page.SortOrder.ToString());
            }
            return builder.ToString();
        }

        private static string BuildUserOrderQuery(OrderQuery query)
        {
            if (null == query)
            {
                throw new ArgumentNullException("query");
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT OrderId FROM Hishop_Orders WHERE OrderStatus != {0} AND OrderStatus != {1} AND OrderStatus != {2}", 1, 4, 9);
            if (!string.IsNullOrEmpty(query.OrderId))
            {
                builder.AppendFormat(" AND OrderId = '{0}'", DataHelper.CleanSearchString(query.OrderId));
                return builder.ToString();
            }
            if (!string.IsNullOrEmpty(query.UserName))
            {
                builder.AppendFormat(" AND UserName LIKE '%{0}%'", DataHelper.CleanSearchString(query.UserName));
            }
            if (!string.IsNullOrEmpty(query.ShipTo))
            {
                builder.AppendFormat(" AND ShipTo LIKE '%{0}%'", DataHelper.CleanSearchString(query.ShipTo));
            }
            if (query.StartDate.HasValue)
            {
                builder.AppendFormat(" AND  OrderDate >= '{0}'", DataHelper.GetSafeDateTimeFormat(query.StartDate.Value));
            }
            if (query.EndDate.HasValue)
            {
                builder.AppendFormat(" AND  OrderDate <= '{0}'", DataHelper.GetSafeDateTimeFormat(query.EndDate.Value));
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                builder.AppendFormat(" ORDER BY {0} {1}", DataHelper.CleanSearchString(query.SortBy), query.SortOrder.ToString());
            }
            return builder.ToString();
        }

        public DataTable GetMemberStatistics(SaleStatisticsQuery query, out int totalProductSales)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_MemberStatistics_Get");
            this.database.AddInParameter(storedProcCommand, "PageIndex", DbType.Int32, query.PageIndex);
            this.database.AddInParameter(storedProcCommand, "PageSize", DbType.Int32, query.PageSize);
            this.database.AddInParameter(storedProcCommand, "IsCount", DbType.Boolean, query.IsCount);
            this.database.AddInParameter(storedProcCommand, "sqlPopulate", DbType.String, BuildMemberStatisticsQuery(query));
            this.database.AddOutParameter(storedProcCommand, "TotalProductSales", DbType.Int32, 4);
            DataTable table = null;
            using (IDataReader reader = this.database.ExecuteReader(storedProcCommand))
            {
                table = DataHelper.ConverDataReaderToDataTable(reader);
            }
            totalProductSales = (int) this.database.GetParameterValue(storedProcCommand, "TotalProductSales");
            return table;
        }

        public DataTable GetMemberStatisticsNoPage(SaleStatisticsQuery query)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(BuildMemberStatisticsQuery(query));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public DataTable GetProductSales(SaleStatisticsQuery productSale, out int totalProductSales)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_ProductSales_Get");
            this.database.AddInParameter(storedProcCommand, "PageIndex", DbType.Int32, productSale.PageIndex);
            this.database.AddInParameter(storedProcCommand, "PageSize", DbType.Int32, productSale.PageSize);
            this.database.AddInParameter(storedProcCommand, "IsCount", DbType.Boolean, productSale.IsCount);
            this.database.AddInParameter(storedProcCommand, "sqlPopulate", DbType.String, BuildProductSaleQuery(productSale));
            this.database.AddOutParameter(storedProcCommand, "TotalProductSales", DbType.Int32, 4);
            DataTable table = null;
            using (IDataReader reader = this.database.ExecuteReader(storedProcCommand))
            {
                table = DataHelper.ConverDataReaderToDataTable(reader);
            }
            totalProductSales = (int) this.database.GetParameterValue(storedProcCommand, "TotalProductSales");
            return table;
        }

        public DataTable GetProductSalesNoPage(SaleStatisticsQuery productSale, out int totalProductSales)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_ProductSalesNoPage_Get");
            this.database.AddInParameter(storedProcCommand, "sqlPopulate", DbType.String, BuildProductSaleQuery(productSale));
            this.database.AddOutParameter(storedProcCommand, "TotalProductSales", DbType.Int32, 4);
            DataTable table = null;
            using (IDataReader reader = this.database.ExecuteReader(storedProcCommand))
            {
                table = DataHelper.ConverDataReaderToDataTable(reader);
            }
            totalProductSales = (int) this.database.GetParameterValue(storedProcCommand, "TotalProductSales");
            return table;
        }

        public DataTable GetProductVisitAndBuyStatistics(SaleStatisticsQuery query, out int totalProductSales)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_ProductVisitAndBuyStatistics_Get");
            this.database.AddInParameter(storedProcCommand, "PageIndex", DbType.Int32, query.PageIndex);
            this.database.AddInParameter(storedProcCommand, "PageSize", DbType.Int32, query.PageSize);
            this.database.AddInParameter(storedProcCommand, "sqlPopulate", DbType.String, BuildProductVisitAndBuyStatisticsQuery(query));
            this.database.AddOutParameter(storedProcCommand, "TotalProductSales", DbType.Int32, 4);
            DataTable table = null;
            using (IDataReader reader = this.database.ExecuteReader(storedProcCommand))
            {
                table = DataHelper.ConverDataReaderToDataTable(reader);
            }
            totalProductSales = (int) this.database.GetParameterValue(storedProcCommand, "TotalProductSales");
            return table;
        }

        public DataTable GetProductVisitAndBuyStatisticsNoPage(SaleStatisticsQuery query, out int totalProductSales)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(" ");
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ProductName,VistiCounts,SaleCounts as BuyCount ,(SaleCounts/(case when VistiCounts=0 then 1 else VistiCounts end))*100 as BuyPercentage ");
            builder.Append("FROM Hishop_Products WHERE SaleCounts>0 ORDER BY BuyPercentage DESC;");
            builder.Append("SELECT COUNT(*) as TotalProductSales FROM Hishop_Products WHERE SaleCounts>0;");
            sqlStringCommand.CommandText = builder.ToString();
            DataTable table = null;
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    table = DataHelper.ConverDataReaderToDataTable(reader);
                }
                if (reader.NextResult() && reader.Read())
                {
                    totalProductSales = (int) reader["TotalProductSales"];
                    return table;
                }
                totalProductSales = 0;
            }
            return table;
        }

        public DbQueryResult GetSaleOrderLineItemsStatistics(SaleStatisticsQuery query)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            if (query.OrderStartDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("OrderDate >= '{0}'", query.OrderStartDate.Value);
            }
            if (query.OrderEndDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("OrderDate <= '{0}'", query.OrderEndDate.Value.AddDays(1.0));
            }

            if (query.PayStartDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("PayDate >= '{0}'", query.PayStartDate.Value);
            }
            if (query.PayEndDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("PayDate <= '{0}'", query.PayEndDate.Value.AddDays(1.0));
            }

            if (query.ShippingStartDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ShippingDate >= '{0}'", query.ShippingStartDate.Value);
            }
            if (query.ShippingEndDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ShippingDate <= '{0}'", query.ShippingEndDate.Value.AddDays(1.0));
            }

            if (builder.Length > 0)
            {
                builder.Append(" AND ");
            }
            builder.AppendFormat("OrderStatus != {0} AND OrderStatus != {1} AND OrderStatus != {2}", 1, 4, 9);
            if (!string.IsNullOrEmpty(query.OrderId))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" OrderId = '{0}'", query.OrderId);
            }
            if (query.OrderTypeId.HasValue)
            {
                builder.AppendFormat(" AND OrderType = {0}", query.OrderTypeId.Value);
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_SaleDetails", "OrderId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public DbQueryResult GetSaleOrderLineItemsStatisticsNoPage(SaleStatisticsQuery query)
        {
            DbQueryResult result = new DbQueryResult();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM vw_Hishop_SaleDetails WHERE 1=1");
            if (query.StartDate.HasValue)
            {
                sqlStringCommand.CommandText = sqlStringCommand.CommandText + string.Format(" AND OrderDate >= '{0}'", query.StartDate);
            }
            if (query.EndDate.HasValue)
            {
                sqlStringCommand.CommandText = sqlStringCommand.CommandText + string.Format(" AND OrderDate <= '{0}'", query.EndDate.Value.AddDays(1.0));
            }
            sqlStringCommand.CommandText = sqlStringCommand.CommandText + string.Format("AND OrderStatus != {0} AND OrderStatus != {1} AND OrderStatus != {2}", 1, 4, 9);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                result.Data = DataHelper.ConverDataReaderToDataTable(reader);
            }
            return result;
        }

        public DbQueryResult GetSaleTargets()
        {
            DbQueryResult result = new DbQueryResult();
            string query = string.Empty;
            query = string.Format("select (select Count(OrderId) from Hishop_orders WHERE OrderStatus != {0} AND OrderStatus != {1}  AND OrderStatus != {2}) as OrderNumb,", 1, 4, 9) + string.Format("(select isnull(sum(OrderTotal),0) - isnull(sum(RefundAmount),0) from hishop_orders WHERE OrderStatus != {0} AND OrderStatus != {1} AND OrderStatus != {2}) as OrderPrice, ", 1, 4, 9) + " (select COUNT(*) from aspnet_Members) as UserNumb,  (select count(*) from aspnet_Members where UserID in (select userid from Hishop_orders)) as UserOrderedNumb,  ISNULL((select sum(VistiCounts) from Hishop_products),0) as ProductVisitNumb ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                result.Data = DataHelper.ConverDataReaderToDataTable(reader);
            }
            return result;
        }

        public StatisticsInfo GetStatistics()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT  (SELECT COUNT(OrderId) FROM Hishop_Orders WHERE OrderStatus = 2 OR (OrderStatus = 1 AND Gateway = 'hishop.plugins.payment.podrequest')) AS orderNumbWaitConsignment, (select count(GroupBuyId) from Hishop_GroupBuy where Status = 5) as groupBuyNumWaitRefund,  isnull((select sum(OrderTotal)-isnull(sum(RefundAmount),0) from hishop_orders where (OrderStatus<>1 AND OrderStatus<>4 AND OrderStatus<>9)   and OrderDate>='" + DataHelper.GetSafeDateTimeFormat(DateTime.Now.Date) + "'),0) as orderPriceToday, isnull((select sum(OrderProfit) from Hishop_Orders where  (OrderStatus<>1 AND OrderStatus<>4 AND OrderStatus<>9)  and OrderDate>='" + DataHelper.GetSafeDateTimeFormat(DateTime.Now.Date) + "'),0) as orderProfitToday, (select count(*) from aspnet_Members where CreateDate>='" + DataHelper.GetSafeDateTimeFormat(DateTime.Now.Date) + "' ) as userNewAddToday,(select count(*) from Hishop_Orders where datediff(dd,getdate(),OrderDate)=0 and (OrderStatus<>1 AND OrderStatus<>4 AND OrderStatus<>9)) as todayFinishOrder,(select count(*) from Hishop_Orders where datediff(dd,getdate()-1,OrderDate)=0 and (OrderStatus<>1 AND OrderStatus<>4 AND OrderStatus<>9)) as yesterdayFinishOrder, isnull((select sum(OrderTotal)-isnull(sum(RefundAmount),0) from hishop_orders where (OrderStatus<>1 AND OrderStatus<>4 AND OrderStatus<>9)   and datediff(dd,getdate()-1,OrderDate)=0),0) as orderPriceYesterDay,(select count(*) from aspnet_Members where datediff(dd,getdate()-1,CreateDate)=0) as userNewAddYesterToday,(select count(*) from aspnet_Members) as TotalMembers,(select count(*) from Hishop_Products where SaleStatus!=0) as TotalProducts,(select count(*) from aspnet_Members where datediff(dd,getdate(),VipCardDate)=0 and VipCardNumber IS NOT NULL) as TodayVipCardNumber,(select count(*) from aspnet_Members where datediff(dd,getdate()-1,VipCardDate)=0 and VipCardNumber IS NOT NULL) as YesterTodayVipCardNumber, isnull((select sum(OrderTotal)-isnull(sum(RefundAmount),0) from hishop_orders where (OrderStatus<>1 AND OrderStatus<>4 AND OrderStatus<>9)   and datediff(dd,OrderDate,getdate())<=30),0) as orderPriceMonth");
            StatisticsInfo info = new StatisticsInfo();
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info.OrderNumbWaitConsignment = (int) reader["orderNumbWaitConsignment"];
                    info.OrderPriceToday = (decimal) reader["orderProfitToday"];
                    info.OrderProfitToday = (decimal) reader["orderPriceToday"];
                    info.UserNewAddToday = (int) reader["userNewAddToday"];
                    info.TodayFinishOrder = (int) reader["todayFinishOrder"];
                    info.YesterdayFinishOrder = (int) reader["yesterdayFinishOrder"];
                    info.UserNewAddYesterToday = (int) reader["userNewAddYesterToday"];
                    info.TotalMembers = (int) reader["TotalMembers"];
                    info.TotalProducts = (int) reader["TotalProducts"];
                    info.TodayVipCardNumber = (int) reader["TodayVipCardNumber"];
                    info.YesterTodayVipCardNumber = (int) reader["YesterTodayVipCardNumber"];
                    info.OrderPriceMonth = (decimal) reader["OrderPriceMonth"];
                    info.GroupBuyNumWaitRefund = (int) reader["groupBuyNumWaitRefund"];
                    info.OrderPriceYesterday = (decimal) reader["orderPriceYesterDay"];
                }
            }
            return info;
        }

        public OrderStatisticsInfo GetUserOrders(OrderQuery userOrder)
        {
            OrderStatisticsInfo info = new OrderStatisticsInfo();
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_OrderStatistics_Get");
            this.database.AddInParameter(storedProcCommand, "PageIndex", DbType.Int32, userOrder.PageIndex);
            this.database.AddInParameter(storedProcCommand, "PageSize", DbType.Int32, userOrder.PageSize);
            this.database.AddInParameter(storedProcCommand, "IsCount", DbType.Boolean, userOrder.IsCount);
            this.database.AddInParameter(storedProcCommand, "sqlPopulate", DbType.String, BuildUserOrderQuery(userOrder));
            this.database.AddOutParameter(storedProcCommand, "TotalUserOrders", DbType.Int32, 4);
            using (IDataReader reader = this.database.ExecuteReader(storedProcCommand))
            {
                info.OrderTbl = DataHelper.ConverDataReaderToDataTable(reader);
                if (reader.NextResult())
                {
                    reader.Read();
                    if (reader["OrderTotal"] != DBNull.Value)
                    {
                        info.TotalOfPage += (decimal) reader["OrderTotal"];
                    }
                    if (reader["Profits"] != DBNull.Value)
                    {
                        info.ProfitsOfPage += (decimal) reader["Profits"];
                    }
                }
                if (reader.NextResult())
                {
                    reader.Read();
                    if (reader["OrderTotal"] != DBNull.Value)
                    {
                        info.TotalOfSearch += (decimal) reader["OrderTotal"];
                    }
                    if (reader["Profits"] != DBNull.Value)
                    {
                        info.ProfitsOfSearch += (decimal) reader["Profits"];
                    }
                }
            }
            info.TotalCount = (int) this.database.GetParameterValue(storedProcCommand, "TotaluserOrders");
            return info;
        }

        public OrderStatisticsInfo GetUserOrdersNoPage(OrderQuery userOrder)
        {
            OrderStatisticsInfo info = new OrderStatisticsInfo();
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_OrderStatisticsNoPage_Get");
            this.database.AddInParameter(storedProcCommand, "sqlPopulate", DbType.String, BuildUserOrderQuery(userOrder));
            this.database.AddOutParameter(storedProcCommand, "TotalUserOrders", DbType.Int32, 4);
            using (IDataReader reader = this.database.ExecuteReader(storedProcCommand))
            {
                info.OrderTbl = DataHelper.ConverDataReaderToDataTable(reader);
                if (reader.NextResult())
                {
                    reader.Read();
                    if (reader["OrderTotal"] != DBNull.Value)
                    {
                        info.TotalOfSearch += (decimal) reader["OrderTotal"];
                    }
                    if (reader["Profits"] != DBNull.Value)
                    {
                        info.ProfitsOfSearch += (decimal) reader["Profits"];
                    }
                }
            }
            info.TotalCount = (int) this.database.GetParameterValue(storedProcCommand, "TotaluserOrders");
            return info;
        }

        public IList<UserStatisticsInfo> GetUserStatistics(Pagination page, out int totalRegionsUsers)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT TopRegionId as RegionId,COUNT(UserId) as UserCounts,(select count(*) from aspnet_Members) as AllUserCounts FROM aspnet_Members  GROUP BY TopRegionId ");
            IList<UserStatisticsInfo> list = new List<UserStatisticsInfo>();
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                UserStatisticsInfo item = null;
                while (reader.Read())
                {
                    item = DataMapper.PopulateUserStatistics(reader);
                    list.Add(item);
                }
                if (item != null)
                {
                    totalRegionsUsers = int.Parse(item.AllUserCounts.ToString());
                    return list;
                }
                totalRegionsUsers = 0;
            }
            return list;
        }

        public DataTable SearchSaleStatisticsData(SearchStatisticsQuery query)
        {
            StringBuilder builder = new StringBuilder();

            if (query.SearchType == 1)
            {
                // 按天
                builder.AppendLine("SELECT * FROM vw_Hishop_SaleStatisticsData ");
                builder.AppendLine("WHERE OrderYear = " + query.Year + " AND OrderMonth = " + query.Month + " ");
                builder.AppendLine(" ORDER BY OrderDate DESC ");
            }
            else if (query.SearchType == 2)
            {
                // 按月
                builder.AppendLine("SELECT   a.OrderYear, a.OrderMonth, SUM(a.Amount) AS Amount, SUM(a.OrderTotal) AS OrderTotal, SUM(a.OrderCostPrice) AS OrderCostPrice, SUM(a.OrderProfit) AS OrderProfit, SUM(a.DiscountAmount) AS DiscountAmount, SUM(a.RedPagerAmount) AS RedPagerAmount, SUM(a.CommTotal) AS CommTotal, SUM(a.GrossProfit) AS GrossProfit, SUM(a.StoreCnt) AS StoreCnt, SUM(a.MemberCnt) AS MemberCnt, SUM(a.TmpStoreCnt) AS TmpStoreCnt ");
                builder.AppendLine("FROM     vw_Hishop_SaleStatisticsData AS a ");
                builder.AppendLine("WHERE a.OrderYear = " + query.Year + " ");
                builder.AppendLine("GROUP BY a.OrderYear, a.OrderMonth ");
                builder.AppendLine("ORDER BY a.OrderYear DESC, a.OrderMonth DESC ");
            }
            else if (query.SearchType == 3)
            {
                // 按年
                builder.AppendLine("SELECT   a.OrderYear, SUM(a.Amount) AS Amount, SUM(a.OrderTotal) AS OrderTotal, SUM(a.OrderCostPrice) AS OrderCostPrice, SUM(a.OrderProfit) AS OrderProfit, SUM(a.DiscountAmount) AS DiscountAmount, SUM(a.RedPagerAmount) AS RedPagerAmount, SUM(a.CommTotal) AS CommTotal, SUM(a.GrossProfit) AS GrossProfit, SUM(a.StoreCnt) AS StoreCnt, SUM(a.MemberCnt) AS MemberCnt, SUM(a.TmpStoreCnt) AS TmpStoreCnt ");
                builder.AppendLine("FROM     vw_Hishop_SaleStatisticsData AS a ");
                builder.AppendLine("GROUP BY a.OrderYear ");
                builder.AppendLine("ORDER BY a.OrderYear DESC");
            }


            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public DbQueryResult SearchSaleReturnsStatisticsData(SaleStatisticsQuery query)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            if (query.StartDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ApplyForTime >= '{0}'", query.StartDate.Value);
            }
            if (query.EndDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ApplyForTime <= '{0}'", query.EndDate.Value.AddDays(1.0));
            }
            if (builder.Length > 0)
            {
                builder.Append(" AND ");
            }
            builder.AppendFormat("ReturnStatus = {0} ", query.ReturnStatus);
            if (!string.IsNullOrEmpty(query.OrderId))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" OrderId = '{0}'", query.OrderId);
            }
            if (query.OrderTypeId.HasValue)
            {
                builder.AppendFormat(" AND OrderType = {0}", query.OrderTypeId.Value);
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_SaleReturnsStatisticsData", "OrderId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public DbQueryResult SearchSaleReturnsOrderStatisticsData(SaleStatisticsQuery query)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            if (query.StartDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ApplyForTime >= '{0}'", query.StartDate.Value);
            }
            if (query.EndDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ApplyForTime <= '{0}'", query.EndDate.Value.AddDays(1.0));
            }
            if (builder.Length > 0)
            {
                builder.Append(" AND ");
            }
            builder.AppendFormat("ReturnStatus = {0} ", query.ReturnStatus);
            if (!string.IsNullOrEmpty(query.OrderId))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" OrderId = '{0}'", query.OrderId);
            }
            if (query.OrderTypeId.HasValue)
            {
                builder.AppendFormat(" AND OrderType = {0}", query.OrderTypeId.Value);
            }
            if (query.OrderStatus.HasValue)
            {
                builder.AppendFormat(" AND OrderStatus = {0}", query.OrderStatus.Value);
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_SaleReturnsOrderStatisticsData", "OrderId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }


        public DbQueryResult SearchSaleOrderStatisticsData(SaleStatisticsQuery query)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            if (query.OrderStartDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("OrderDate >= '{0}'", query.OrderStartDate.Value);
            }
            if (query.OrderEndDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("OrderDate <= '{0}'", query.OrderEndDate.Value.AddDays(1.0));
            }

            if (query.PayStartDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("PayDate >= '{0}'", query.PayStartDate.Value);
            }
            if (query.PayEndDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("PayDate <= '{0}'", query.PayEndDate.Value.AddDays(1.0));
            }

            if (query.ShippingStartDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ShippingDate >= '{0}'", query.ShippingStartDate.Value);
            }
            if (query.ShippingEndDate.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ShippingDate <= '{0}'", query.ShippingEndDate.Value.AddDays(1.0));
            }

            if (!string.IsNullOrEmpty(query.OrderId))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" OrderId = '{0}'", query.OrderId);
            }
            if (query.OrderTypeId.HasValue)
            {
                builder.AppendFormat(" AND OrderType = {0}", query.OrderTypeId.Value);
            }
            if (query.OrderStatus.HasValue)
            {
                builder.AppendFormat(" AND OrderStatus = {0}", query.OrderStatus.Value);
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_SaleOrderStatisticsData", "OrderId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public DataTable SearchDistributorSaleStatisticsData(SearchStatisticsQuery query)
        {
            StringBuilder builder = new StringBuilder();

            if (query.SearchType == 1)
            {
                // 按天
                builder.AppendLine("SELECT * FROM vw_Hishop_StatisticsDistributorData AS a ");
                builder.AppendLine("WHERE a.OrderYear = " + query.Year + " AND a.OrderMonth = " + query.Month + " ");
                if (query.StartDate.HasValue)
                {
                    builder.AppendLine("AND a.OrderDate >= '" + query.StartDate.Value.AddDays(-1) + "' ");
                }
                if (query.EndDate.HasValue)
                {
                    builder.AppendLine("AND a.OrderDate < '" + query.EndDate.Value + "' ");
                }
                if (!string.IsNullOrEmpty(query.StoreName))
                {
                    builder.AppendLine("AND a.StoreName LIKE '%" + query.StoreName + "%' ");
                }
                if (!string.IsNullOrEmpty(query.UserName))
                {
                    builder.AppendLine("AND a.UserName LIKE '%" + query.UserName + "%' ");
                }
                if (query.GradeId.HasValue && query.GradeId > 0)
                {
                    builder.AppendLine("AND a.DistributorGradeId = " + query.GradeId + " ");
                }
                builder.AppendLine("ORDER BY UserId ASC, OrderDate DESC ");
            }
            else if (query.SearchType == 2)
            {
                // 按月
                builder.AppendLine("SELECT   a.UserId, MAX(a.StoreName) AS StoreName, MAX(a.UserName) AS UserName, MAX(a.RealName) AS RealName, MAX(a.DistributorGradeName) AS DistributorGradeName, MAX(a.DistributorGradeId) AS DistributorGradeId, a.OrderYear, a.OrderMonth, SUM(a.Amount) AS Amount, SUM(a.OrderTotal) AS OrderTotal, SUM(a.OrderCostPrice) AS OrderCostPrice, SUM(a.OrderProfit) AS OrderProfit, SUM(a.DiscountAmount) AS DiscountAmount, SUM(a.RedPagerAmount) AS RedPagerAmount, SUM(a.CommTotal) AS CommTotal, SUM(a.NormalIncome) AS NormalIncome, SUM(a.RecommendIncome) AS RecommendIncome, SUM(a.GrossProfit) AS GrossProfit, SUM(a.StoreCnt) AS StoreCnt ");
                builder.AppendLine("FROM     vw_Hishop_StatisticsDistributorData AS a ");
                builder.AppendLine("WHERE a.OrderYear = " + query.Year + " ");
                if (query.StartDate.HasValue)
                {
                    builder.AppendLine("AND a.OrderDate >= '" + query.StartDate.Value.AddDays(-1) + "' ");
                }
                if (query.EndDate.HasValue)
                {
                    builder.AppendLine("AND a.OrderDate < '" + query.EndDate.Value + "' ");
                }
                if (!string.IsNullOrEmpty(query.StoreName))
                {
                    builder.AppendLine("AND a.StoreName LIKE '%" + query.StoreName + "%' ");
                }
                if (!string.IsNullOrEmpty(query.UserName))
                {
                    builder.AppendLine("AND a.UserName LIKE '%" + query.UserName + "%' ");
                }
                if (query.GradeId.HasValue && query.GradeId > 0)
                {
                    builder.AppendLine("AND a.DistributorGradeId = " + query.GradeId + " ");
                }
                builder.AppendLine("GROUP BY a.UserId, a.OrderYear, a.OrderMonth ");
                builder.AppendLine("ORDER BY a.OrderYear DESC, MAX(a.DistributorGradeId) DESC, a.UserId ASC, a.OrderMonth DESC ");
            }
            else if (query.SearchType == 3)
            {
                // 按年
                builder.AppendLine("SELECT   a.UserId, MAX(a.StoreName) AS StoreName, MAX(a.UserName) AS UserName, MAX(a.RealName) AS RealName, MAX(a.DistributorGradeName) AS DistributorGradeName, MAX(a.DistributorGradeId) AS DistributorGradeId, a.OrderYear, SUM(a.Amount) AS Amount, SUM(a.OrderTotal) AS OrderTotal, SUM(a.OrderCostPrice) AS OrderCostPrice, SUM(a.OrderProfit) AS OrderProfit, SUM(a.DiscountAmount) AS DiscountAmount, SUM(a.RedPagerAmount) AS RedPagerAmount, SUM(a.CommTotal) AS CommTotal, SUM(a.NormalIncome) AS NormalIncome, SUM(a.RecommendIncome) AS RecommendIncome, SUM(a.GrossProfit) AS GrossProfit, SUM(a.StoreCnt) AS StoreCnt ");
                builder.AppendLine("FROM     vw_Hishop_StatisticsDistributorData AS a ");
                builder.AppendLine("WHERE a.OrderYear = " + query.Year + " ");
                if (query.StartDate.HasValue)
                {
                    builder.AppendLine("AND a.OrderDate >= '" + query.StartDate.Value.AddDays(-1) + "' ");
                }
                if (query.EndDate.HasValue)
                {
                    builder.AppendLine("AND a.OrderDate < '" + query.EndDate.Value + "' ");
                }
                if (!string.IsNullOrEmpty(query.StoreName))
                {
                    builder.AppendLine("AND a.StoreName LIKE '%" + query.StoreName + "%' ");
                }
                if (!string.IsNullOrEmpty(query.UserName))
                {
                    builder.AppendLine("AND a.UserName LIKE '%" + query.UserName + "%' ");
                }
                if (query.GradeId.HasValue && query.GradeId > 0)
                {
                    builder.AppendLine("AND a.DistributorGradeId = " + query.GradeId + " ");
                }
                builder.AppendLine("GROUP BY a.UserId, a.OrderYear ");
                builder.AppendLine("ORDER BY a.OrderYear DESC, MAX(a.DistributorGradeId) DESC, a.UserId ASC");
            }


            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }
    }
}

