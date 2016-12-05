namespace Hidistro.SqlDal.VShop
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class UserRedPagerDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool AddUserRedPagerRecord(OrderInfo orderinfo, DbTransaction dbTran)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update vshop_UserRedPager set IsUsed=@IsUsed,UseOrderID=@UseOrderID,UsedTime=@UsedTime WHERE RedPagerID=@RedPagerID and IsUsed!=1");
            this.database.AddInParameter(sqlStringCommand, "RedPagerID", DbType.Int32, orderinfo.RedPagerID);
            this.database.AddInParameter(sqlStringCommand, "IsUsed", DbType.Int32, 1);
            this.database.AddInParameter(sqlStringCommand, "UseOrderID", DbType.String, orderinfo.OrderId);
            this.database.AddInParameter(sqlStringCommand, "UsedTime", DbType.DateTime, DateTime.Now);
            if (dbTran != null)
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand, dbTran) > 0);
            }
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public string CreateUserRedPager(UserRedPagerInfo userredpager)
        {
            if (this.HasGetThisRedPager(userredpager.UserID, userredpager.OrderID))
            {
                return "-1";
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("if exists(select top 1 OrderID from vshop_OrderRedPager where OrderID=@OrderID and MaxGetTimes>AlreadyGetTimes) begin Update vshop_OrderRedPager set AlreadyGetTimes=AlreadyGetTimes+1 where OrderID=@OrderID;INSERT INTO vshop_UserRedPager(Amount,UserID,OrderID,RedPagerActivityName,OrderAmountCanUse,CreateTime,ExpiryTime,IsUsed) VALUES(@Amount,@UserID,@OrderID,@RedPagerActivityName,@OrderAmountCanUse,@CreateTime,@ExpiryTime,@IsUsed); select 1 end else begin select 0 end");
            this.database.AddInParameter(sqlStringCommand, "Amount", DbType.Decimal, userredpager.Amount);
            this.database.AddInParameter(sqlStringCommand, "UserID", DbType.Int32, userredpager.UserID);
            this.database.AddInParameter(sqlStringCommand, "OrderID", DbType.String, userredpager.OrderID);
            this.database.AddInParameter(sqlStringCommand, "RedPagerActivityName", DbType.String, userredpager.RedPagerActivityName);
            this.database.AddInParameter(sqlStringCommand, "OrderAmountCanUse", DbType.Decimal, userredpager.OrderAmountCanUse);
            this.database.AddInParameter(sqlStringCommand, "CreateTime", DbType.DateTime, userredpager.CreateTime);
            this.database.AddInParameter(sqlStringCommand, "ExpiryTime", DbType.DateTime, userredpager.ExpiryTime);
            this.database.AddInParameter(sqlStringCommand, "IsUsed", DbType.Boolean, userredpager.IsUsed);
            return this.database.ExecuteScalar(sqlStringCommand).ToString();
        }

        public bool DelUserRedPager(int redpageid)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("delete vshop_UserRedPager where RedPagerID=@RedPagerID");
            this.database.AddInParameter(sqlStringCommand, "RedPagerID", DbType.Int32, redpageid);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public DataTable GetUserRedPager(int userid, UserRedPagerType type)
        {
            string str = string.Empty;
            switch (type)
            {
                case UserRedPagerType.All:
                    str = " AND IsUsed=0 ";
                    break;

                case UserRedPagerType.Usable:
                    str = " AND IsUsed=0 and ExpiryTime>getdate()";
                    break;

                case UserRedPagerType.Expiry:
                    str = " AND IsUsed=0 and ExpiryTime<getdate()";
                    break;

                default:
                    str = " AND IsUsed=0 and ExpiryTime>getdate() ";
                    break;
            }
            string query = string.Format("select * FROM vshop_UserRedPager WHERE UserId={0}" + str + " ORDER BY RedPagerID DESC", userid);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public UserRedPagerInfo GetUserRedPagerByOrderIDAndUserID(int userid, string orderid)
        {
            UserRedPagerInfo info = new UserRedPagerInfo();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("select top 1 * from vshop_UserRedPager where OrderID=@OrderID and UserID=@UserID");
            this.database.AddInParameter(sqlStringCommand, "UserID", DbType.Int32, userid);
            this.database.AddInParameter(sqlStringCommand, "OrderID", DbType.String, orderid);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = DataMapper.PopulateUserRedPagerInfo(reader);
                }
            }
            return info;
        }

        public UserRedPagerInfo GetUserRedPagerByRedPagerID(int redpagerid)
        {
            if (redpagerid <= 0)
            {
                return null;
            }
            UserRedPagerInfo info = null;
            string query = string.Format("select * FROM vshop_UserRedPager WHERE redpagerid={0}", redpagerid);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = DataMapper.PopulateUserRedPagerInfo(reader);
                }
            }
            return info;
        }

        public DataTable GetUserRedPagerCanUse(decimal orderAmount)
        {
            DataTable table = new DataTable();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT RedPagerID,Amount,RedPagerActivityName,OrderAmountCanUse from vshop_UserRedPager where UserId=@UserId AND ExpiryTime>getdate() AND IsUsed=0 AND OrderAmountCanUse<=@OrderAmountCanUse");
            this.database.AddInParameter(sqlStringCommand, "OrderAmountCanUse", DbType.Decimal, orderAmount);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, Globals.GetCurrentMemberUserId());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public DbQueryResult GetUserRedPagerList(UserRedPagerQuery userredpagerquery)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" UserId={0} ", userredpagerquery.UserID);
            switch (userredpagerquery.Type)
            {
                case UserRedPagerType.All:
                    builder.Append(" AND IsUsed=0  ");
                    break;

                case UserRedPagerType.Usable:
                    builder.Append(" AND IsUsed=0 and ExpiryTime>getdate() ");
                    break;

                case UserRedPagerType.Expiry:
                    builder.Append(" AND IsUsed=0 and ExpiryTime<getdate() ");
                    break;

                default:
                    builder.Append(" AND IsUsed=0 and ExpiryTime>getdate()  ");
                    break;
            }
            return DataHelper.PagingByRownumber(userredpagerquery.PageIndex, userredpagerquery.PageSize, userredpagerquery.SortBy, userredpagerquery.SortOrder, userredpagerquery.IsCount, "vshop_UserRedPager", "RedPagerID", builder.ToString(), "*");
        }

        public bool HasGetThisRedPager(int userid, string orderid)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("select top 1 OrderID from vshop_UserRedPager where OrderID=@OrderID and UserID=@UserID");
            this.database.AddInParameter(sqlStringCommand, "UserID", DbType.Int32, userid);
            this.database.AddInParameter(sqlStringCommand, "OrderID", DbType.String, orderid);
            return (this.database.ExecuteDataSet(sqlStringCommand).Tables[0].Rows.Count > 0);
        }

        public bool SetIsUsed(int redpageid, bool isused)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE vshop_UserRedPager set IsUsed=@IsUsed where RedPagerID=@RedPagerID");
            this.database.AddInParameter(sqlStringCommand, "RedPagerID", DbType.Int32, redpageid);
            this.database.AddInParameter(sqlStringCommand, "IsUsed", DbType.Boolean, isused);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

