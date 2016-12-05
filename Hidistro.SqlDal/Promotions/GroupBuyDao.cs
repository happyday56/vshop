namespace Hidistro.SqlDal.Promotions
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Promotions;
    using Hidistro.SqlDal;
    using Hidistro.SqlDal.Commodities;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    public class GroupBuyDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public int AddGroupBuy(GroupBuyInfo groupBuy, DbTransaction dbTran)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DECLARE @DisplaySequence AS INT SELECT @DisplaySequence = (CASE WHEN MAX(DisplaySequence) IS NULL THEN 1 ELSE MAX(DisplaySequence) + 1 END) FROM Hishop_GroupBuy;INSERT INTO Hishop_GroupBuy(ProductId,NeedPrice,StartDate,EndDate,MaxCount,Content,Status,DisplaySequence,Count,Price) VALUES(@ProductId,@NeedPrice,@StartDate,@EndDate,@MaxCount,@Content,@Status,@DisplaySequence,@Count,@Price); SELECT @@IDENTITY");
            this.database.AddInParameter(sqlStringCommand, "ProductId", DbType.Int32, groupBuy.ProductId);
            this.database.AddInParameter(sqlStringCommand, "NeedPrice", DbType.Currency, groupBuy.NeedPrice);
            this.database.AddInParameter(sqlStringCommand, "StartDate", DbType.DateTime, groupBuy.StartDate);
            this.database.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, groupBuy.EndDate);
            this.database.AddInParameter(sqlStringCommand, "MaxCount", DbType.Int32, groupBuy.MaxCount);
            this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, groupBuy.Content);
            this.database.AddInParameter(sqlStringCommand, "Status", DbType.Int32, 1);
            this.database.AddInParameter(sqlStringCommand, "Count", DbType.Int32, groupBuy.Count);
            this.database.AddInParameter(sqlStringCommand, "Price", DbType.Decimal, groupBuy.Price);
            object obj2 = null;
            if (dbTran != null)
            {
                obj2 = this.database.ExecuteScalar(sqlStringCommand, dbTran);
            }
            else
            {
                obj2 = this.database.ExecuteScalar(sqlStringCommand);
            }
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return 0;
        }

        public bool DeleteGroupBuy(int groupBuyId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_GroupBuy WHERE GroupBuyId=@GroupBuyId");
            this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuyId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool DeleteGroupBuyCondition(int groupBuyId, DbTransaction dbTran)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_GroupBuyCondition WHERE GroupBuyId=@GroupBuyId");
            this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuyId);
            if (dbTran != null)
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand, dbTran) > 0);
            }
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public GroupBuyInfo GetGroupBuy(int groupBuyId, DbTransaction trans = null)
        {
            GroupBuyInfo info = null;
            IDataReader reader;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT g.* ,(SELECT SUM(Quantity) FROM Hishop_OrderItems WHERE OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE GroupBuyId = @GroupBuyId AND OrderStatus <> 1 AND OrderStatus <> 4)) AS ProdcutQuantity FROM Hishop_GroupBuy g WHERE GroupBuyId=@GroupBuyId;");
            this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuyId);
            if (trans != null)
            {
                using (reader = this.database.ExecuteReader(sqlStringCommand, trans))
                {
                    if (reader.Read())
                    {
                        info = DataMapper.PopulateGroupBuy(reader);
                    }
                }
                return info;
            }
            using (reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = DataMapper.PopulateGroupBuy(reader);
                }
            }
            return info;
        }

        public DbQueryResult GetGroupBuyList(GroupBuyQuery query)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1");
            if (query.State > 0)
            {
                builder.AppendFormat(" AND Status = {0} ", query.State);
            }
            if (!string.IsNullOrEmpty(query.ProductName))
            {
                builder.AppendFormat(" AND ProductName Like '%{0}%' ", DataHelper.CleanSearchString(query.ProductName));
            }
            string selectFields = "GroupBuyId,ProductId,ProductName,MaxCount,NeedPrice,Status,SoldCount,OrderCount,ISNULL(ProdcutQuantity,0) AS ProdcutQuantity,StartDate,EndDate,DisplaySequence";
            return DataHelper.PagingByTopnotin(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_GroupBuy", "GroupBuyId", builder.ToString(), selectFields);
        }

        public DataTable GetGroupBuyProducts(int? categoryId, string keyWord, int page, int size, out int total, bool onlyUnFinished = true)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("a.GroupBuyId,a.ProductId,ProductName,ProductCode,ShortDescription,SoldCount,");
            builder.Append(" ThumbnailUrl60,ThumbnailUrl100,ThumbnailUrl160,ThumbnailUrl180,ThumbnailUrl220,ThumbnailUrl310,a.Price,b.SalePrice");
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" Hishop_GroupBuy a left join vw_Hishop_BrowseProductList b on a.ProductId = b.ProductId ");
            StringBuilder builder3 = new StringBuilder(" SaleStatus=1");
            if (onlyUnFinished)
            {
                builder3.AppendFormat(" AND  a.Status = {0}", 1);
            }
            if (categoryId.HasValue)
            {
                CategoryInfo category = new CategoryDao().GetCategory(categoryId.Value);
                if (category != null)
                {
                    builder3.AppendFormat(" AND ( MainCategoryPath LIKE '{0}|%' OR ExtendCategoryPath LIKE '{0}|%') ", category.Path);
                }
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                builder3.AppendFormat(" AND (ProductName LIKE '%{0}%' OR ProductCode LIKE '%{0}%')", keyWord);
            }
            string sortBy = "a.DisplaySequence";
            DbQueryResult result = DataHelper.PagingByRownumber(page, size, sortBy, SortAction.Desc, true, builder2.ToString(), "GroupBuyId", builder3.ToString(), builder.ToString());
            DataTable data = (DataTable) result.Data;
            total = result.TotalRecords;
            return data;
        }

        public int GetOrderCount(int groupBuyId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT SUM(Quantity) FROM Hishop_OrderItems WHERE OrderId IN (SELECT OrderId FROM Hishop_Orders WHERE GroupBuyId = @GroupBuyId AND OrderStatus <> 1 AND OrderStatus <> 4)");
            this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuyId);
            object obj2 = this.database.ExecuteScalar(sqlStringCommand);
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                return (int) obj2;
            }
            return 0;
        }

        public string GetPriceByProductId(int productId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT SalePrice FROM vw_Hishop_BrowseProductList WHERE ProductId=@ProductId");
            this.database.AddInParameter(sqlStringCommand, "ProductId", DbType.Int32, productId);
            return this.database.ExecuteScalar(sqlStringCommand).ToString();
        }

        public bool ProductGroupBuyExist(int productId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT COUNT(*) FROM Hishop_GroupBuy WHERE ProductId=@ProductId AND Status=@Status");
            this.database.AddInParameter(sqlStringCommand, "ProductId", DbType.Int32, productId);
            this.database.AddInParameter(sqlStringCommand, "Status", DbType.Int32, 1);
            return (((int) this.database.ExecuteScalar(sqlStringCommand)) > 0);
        }

        public void RefreshGroupBuyFinishState(int groupbuyId, DbTransaction trans = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Hishop_GroupBuy SET Status = 2 WHERE GroupBuyId=@groupBuyId AND Status = 1 AND (EndDate <= @CurrentTime OR soldCount >= maxCount);");
            builder.Append("UPDATE Hishop_GroupBuy SET Status = 4 WHERE GroupBuyId=@groupBuyId AND Status = 5 AND (select Count(1) from Hishop_Orders where GroupBuyId = Hishop_GroupBuy.GroupBuyId and OrderStatus = 6) =0;");
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(builder.ToString());
            database.AddInParameter(sqlStringCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
            database.AddInParameter(sqlStringCommand, "groupBuyId", DbType.Int32, groupbuyId);
            if (trans != null)
            {
                database.ExecuteNonQuery(sqlStringCommand, trans);
            }
            else
            {
                database.ExecuteNonQuery(sqlStringCommand);
            }
        }

        public bool SetGroupBuyEndUntreated(int groupBuyId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_GroupBuy SET Status=@Status,EndDate=@EndDate WHERE GroupBuyId=@GroupBuyId");
            this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuyId);
            this.database.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, DateTime.Now);
            this.database.AddInParameter(sqlStringCommand, "Status", DbType.Int32, 2);
            return (this.database.ExecuteNonQuery(sqlStringCommand) == 1);
        }

        private bool SetGroupBuyFailed(int groupBuyId)
        {
            IDataReader reader;
            StringBuilder builder = new StringBuilder();
            builder.Append("update Hishop_Orders set OrderStatus = 6 where GroupBuyId = @GroupBuyId and OrderStatus = 2;");
            builder.AppendFormat("update Hishop_Orders set OrderStatus = 4,CloseReason='{0}' where GroupBuyId = @GroupBuyId and OrderStatus = 1;", "团购失败,自动关闭");
            builder.Append("UPDATE Hishop_GroupBuy SET Status = 5,SoldCount = 0 WHERE GroupBuyId = @GroupBuyId AND (select Count(1) from Hishop_Orders where GroupBuyId = @GroupBuyId and OrderStatus = 6) >0;");
            builder.Append("UPDATE Hishop_GroupBuy SET Status = 4,SoldCount = 0 WHERE GroupBuyId = @GroupBuyId AND (select Count(1) from Hishop_Orders where GroupBuyId = @GroupBuyId and OrderStatus = 6) =0;");
            builder.Append("  select OrderId,TelPhone,CellPhone,ShippingRegion+[Address] as [Address],ShipTo from Hishop_Orders where GroupBuyId = @GroupBuyId and OrderStatus = 6; ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuyId);
            using (reader = this.database.ExecuteReader(sqlStringCommand))
            {
                RefundInfo refundInfo = new RefundInfo();
                RefundDao dao = new RefundDao();
                while (reader.Read())
                {
                    refundInfo.OrderId = reader["OrderId"].ToString();
                    refundInfo.RefundRemark = "团购失败，申请退款";
                    refundInfo.ApplyForTime = DateTime.Now;
                    refundInfo.HandleStatus = RefundInfo.Handlestatus.Applied;
                    dao.AddRefund(refundInfo);
                }
            }
            builder.Clear();
            builder.Append("select c.SkuId,c.Quantity from Hishop_GroupBuy a left join Hishop_Orders b on a.GroupBuyId = b.GroupBuyId left join Hishop_OrderItems c on b.OrderId = c.OrderId left join Hishop_SKUs d on c.SkuId = d.SkuId where a.GroupBuyId =@GroupBuyId and b.OrderStatus = 6");
            sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuyId);
            Dictionary<string, int> source = new Dictionary<string, int>();
            using (reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    Dictionary<string, int> dictionary2;
                    string str2;
                    string key = reader.GetString(0);
                    if (!source.ContainsKey(key))
                    {
                        source[key] = 0;
                    }
                    (dictionary2 = source)[str2 = key] = dictionary2[str2] + reader.GetInt32(1);
                }
            }
            builder.Clear();
            for (int i = 0; i < source.Count; i++)
            {
                builder.AppendFormat("update Hishop_SKUs set Stock=Stock+{1} where SKUId = '{0}';", source.ElementAt<KeyValuePair<string, int>>(i).Key, source.ElementAt<KeyValuePair<string, int>>(i).Value);
            }
            this.database.ExecuteNonQuery(CommandType.Text, builder.ToString());
            return true;
        }

        public bool SetGroupBuyStatus(int groupBuyId, GroupBuyStatus status)
        {
            if (status != GroupBuyStatus.Failed)
            {
                DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_GroupBuy SET Status=@Status WHERE GroupBuyId=@GroupBuyId;UPDATE Hishop_Orders SET GroupBuyStatus=@Status WHERE GroupBuyId=@GroupBuyId");
                this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuyId);
                this.database.AddInParameter(sqlStringCommand, "Status", DbType.Int32, (int) status);
                return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
            }
            return this.SetGroupBuyFailed(groupBuyId);
        }

        public void SwapGroupBuySequence(int groupBuyId, int displaySequence)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_GroupBuy SET DisplaySequence = @DisplaySequence WHERE GroupBuyId=@GroupBuyId");
            this.database.AddInParameter(sqlStringCommand, "DisplaySequence", DbType.Int32, displaySequence);
            this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuyId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool UpdateGroupBuy(GroupBuyInfo groupBuy, DbTransaction dbTran)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_GroupBuy SET ProductId=@ProductId,NeedPrice=@NeedPrice,Count=@Count,Price=@Price,StartDate=@StartDate,EndDate=@EndDate,MaxCount=@MaxCount,Content=@Content,soldCount=@soldcout WHERE GroupBuyId=@GroupBuyId");
            this.database.AddInParameter(sqlStringCommand, "GroupBuyId", DbType.Int32, groupBuy.GroupBuyId);
            this.database.AddInParameter(sqlStringCommand, "ProductId", DbType.Int32, groupBuy.ProductId);
            this.database.AddInParameter(sqlStringCommand, "NeedPrice", DbType.Currency, groupBuy.NeedPrice);
            this.database.AddInParameter(sqlStringCommand, "StartDate", DbType.DateTime, groupBuy.StartDate);
            this.database.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, groupBuy.EndDate);
            this.database.AddInParameter(sqlStringCommand, "MaxCount", DbType.Int32, groupBuy.MaxCount);
            this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, groupBuy.Content);
            this.database.AddInParameter(sqlStringCommand, "soldcout", DbType.Int32, groupBuy.SoldCount);
            this.database.AddInParameter(sqlStringCommand, "Count", DbType.Int32, groupBuy.Count);
            this.database.AddInParameter(sqlStringCommand, "Price", DbType.Double, groupBuy.Price);
            if (dbTran != null)
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand, dbTran) == 1);
            }
            return (this.database.ExecuteNonQuery(sqlStringCommand) == 1);
        }
    }
}

