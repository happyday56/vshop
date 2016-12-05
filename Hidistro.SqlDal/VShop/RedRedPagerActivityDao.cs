namespace Hidistro.SqlDal.VShop
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class RedRedPagerActivityDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool CreateRedPagerActivity(RedPagerActivityInfo redpaperactivity)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO vshop_RedPagerActivity(Name,CategoryId,MinOrderAmount,MaxGetTimes,ItemAmountLimit,ExpiryDays,OrderAmountCanUse) VALUES(@Name,@CategoryId,@MinOrderAmount,@MaxGetTimes,@ItemAmountLimit,@ExpiryDays,@OrderAmountCanUse);select @@identity");
            this.database.AddInParameter(sqlStringCommand, "Name", DbType.String, redpaperactivity.Name);
            this.database.AddInParameter(sqlStringCommand, "CategoryId", DbType.Int32, redpaperactivity.CategoryId);
            this.database.AddInParameter(sqlStringCommand, "MinOrderAmount", DbType.Decimal, redpaperactivity.MinOrderAmount);
            this.database.AddInParameter(sqlStringCommand, "MaxGetTimes", DbType.Int32, redpaperactivity.MaxGetTimes);
            if (redpaperactivity.OrderAmountCanUse <= redpaperactivity.ItemAmountLimit)
            {
                redpaperactivity.OrderAmountCanUse = redpaperactivity.ItemAmountLimit;
            }
            this.database.AddInParameter(sqlStringCommand, "ItemAmountLimit", DbType.Decimal, redpaperactivity.ItemAmountLimit);
            this.database.AddInParameter(sqlStringCommand, "ExpiryDays", DbType.Int32, redpaperactivity.ExpiryDays);
            this.database.AddInParameter(sqlStringCommand, "OrderAmountCanUse", DbType.Decimal, redpaperactivity.OrderAmountCanUse);
            return (int.Parse(this.database.ExecuteScalar(sqlStringCommand).ToString()) > 0);
        }

        public bool DelRedPagerActivity(int redpaperactivityid)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("delete  vshop_RedPagerActivity  where RedPagerActivityId=@RedPagerActivityId");
            this.database.AddInParameter(sqlStringCommand, "RedPagerActivityId", DbType.Int32, redpaperactivityid);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public DbQueryResult GetRedPagerActivity(RedPagerActivityQuery query)
        {
            StringBuilder builder = new StringBuilder();
            if (query.RedPagerActivityId > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" RedPagerActivityId = {0}", query.RedPagerActivityId);
            }
            if (!string.IsNullOrEmpty(query.Name))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" Name LIKE '%{0}%'", DataHelper.CleanSearchString(query.Name));
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vshop_RedPagerActivity", "RedPagerActivityId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public RedPagerActivityInfo GetRedPagerActivityInfo(int redpaperactivityid)
        {
            if (redpaperactivityid <= 0)
            {
                return null;
            }
            RedPagerActivityInfo info = null;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM vshop_RedPagerActivity where RedPagerActivityId={0}", redpaperactivityid));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = DataMapper.PopulateRedPagerActivityInfo(reader);
                }
            }
            return info;
        }

        public DbQueryResult GetRedPagerActivityRequest(RedPagerActivityQuery query)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(query.Name))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" Name LIKE '%{0}%'", DataHelper.CleanSearchString(query.Name));
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vshop_RedPagerActivity ", "RedPagerActivityId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public bool IsExistsMinOrderAmount(int redpageractivityid, decimal minorderamount)
        {
            bool flag = false;
            string query = "select top 1 RedPagerActivityId from vshop_RedPagerActivity where MinOrderAmount=" + minorderamount;
            if (redpageractivityid > 0)
            {
                query = query + " and RedPagerActivityId<>" + redpageractivityid;
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            IDataReader reader = this.database.ExecuteReader(sqlStringCommand);
            if (reader.Read())
            {
                flag = true;
            }
            reader.Close();
            return flag;
        }

        public bool SetIsOpen(int redpaperactivityid, bool isopen)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE vshop_RedPagerActivity set IsOpen=@IsOpen where RedPagerActivityId=@RedPagerActivityId");
            this.database.AddInParameter(sqlStringCommand, "RedPagerActivityId", DbType.Int32, redpaperactivityid);
            this.database.AddInParameter(sqlStringCommand, "IsOpen", DbType.Boolean, isopen);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateRedPagerActivity(RedPagerActivityInfo redpaperactivity)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE vshop_RedPagerActivity SET Name=@Name,CategoryId=@CategoryId,MinOrderAmount=@MinOrderAmount,MaxGetTimes=@MaxGetTimes,ItemAmountLimit=@ItemAmountLimit,ExpiryDays=@ExpiryDays,OrderAmountCanUse=@OrderAmountCanUse WHERE RedPagerActivityId=@RedPagerActivityId");
            this.database.AddInParameter(sqlStringCommand, "RedPagerActivityId", DbType.Int32, redpaperactivity.RedPagerActivityId);
            this.database.AddInParameter(sqlStringCommand, "Name", DbType.String, redpaperactivity.Name);
            this.database.AddInParameter(sqlStringCommand, "CategoryId", DbType.Int32, redpaperactivity.CategoryId);
            this.database.AddInParameter(sqlStringCommand, "MinOrderAmount", DbType.Decimal, redpaperactivity.MinOrderAmount);
            this.database.AddInParameter(sqlStringCommand, "MaxGetTimes", DbType.Decimal, redpaperactivity.MaxGetTimes);
            this.database.AddInParameter(sqlStringCommand, "ItemAmountLimit", DbType.Decimal, redpaperactivity.ItemAmountLimit);
            this.database.AddInParameter(sqlStringCommand, "ExpiryDays", DbType.Decimal, redpaperactivity.ExpiryDays);
            if (redpaperactivity.OrderAmountCanUse <= redpaperactivity.ItemAmountLimit)
            {
                redpaperactivity.OrderAmountCanUse = redpaperactivity.ItemAmountLimit;
            }
            this.database.AddInParameter(sqlStringCommand, "OrderAmountCanUse", DbType.Decimal, redpaperactivity.OrderAmountCanUse);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

