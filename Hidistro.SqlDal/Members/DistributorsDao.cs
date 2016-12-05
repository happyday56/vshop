namespace Hidistro.SqlDal.Members
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;
    using System.Text.RegularExpressions;
    using NewLife.Log;

    public class DistributorsDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool AddBalanceDrawRequest(BalanceDrawRequestInfo bdrinfo)
        {
            string query = "INSERT INTO Hishop_BalanceDrawRequest(UserId,RequestType,UserName,Amount,AccountName,CellPhone,MerchantCode,Remark,RequestTime,IsCheck, BankName, BankAddress, RegionAddress) VALUES(@UserId,@RequestType,@UserName,@Amount,@AccountName,@CellPhone,@MerchantCode,@Remark,getdate(),0, @BankName, @BankAddress, @RegionAddress)";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, bdrinfo.UserId);
            this.database.AddInParameter(sqlStringCommand, "RequestType", DbType.Int32, bdrinfo.RequesType);
            this.database.AddInParameter(sqlStringCommand, "UserName", DbType.String, bdrinfo.UserName);
            this.database.AddInParameter(sqlStringCommand, "Amount", DbType.Decimal, bdrinfo.Amount);
            this.database.AddInParameter(sqlStringCommand, "AccountName", DbType.String, bdrinfo.AccountName);
            this.database.AddInParameter(sqlStringCommand, "CellPhone", DbType.String, bdrinfo.CellPhone);
            this.database.AddInParameter(sqlStringCommand, "MerchantCode", DbType.String, bdrinfo.MerchanCade);
            this.database.AddInParameter(sqlStringCommand, "Remark", DbType.String, bdrinfo.Remark);
            this.database.AddInParameter(sqlStringCommand, "BankName", DbType.String, bdrinfo.BankName);
            this.database.AddInParameter(sqlStringCommand, "BankAddress", DbType.String, bdrinfo.BankAddress);
            this.database.AddInParameter(sqlStringCommand, "RegionAddress", DbType.String, bdrinfo.RegionAddress);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);

        }

        public void AddDistributorProducts(int productId, int distributorId)
        {
            string query = "INSERT INTO Hishop_DistributorProducts VALUES(@ProductId,@UserId)";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributorId);
            this.database.AddInParameter(sqlStringCommand, "ProductId", DbType.Int32, productId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool CreateDistributor(DistributorsInfo distributor)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO aspnet_Distributors(UserId,StoreName,Logo,BackImage,RequestAccount,GradeId,ReferralUserId,ReferralPath, ReferralOrders,ReferralBlance, ReferralRequestBalance,ReferralStatus,StoreDescription,DistributorGradeId, InvitationNum, IsBindMobile, AccumulatedIncome, AccountBalance, OrdersTotal, visticounts, goodcounts, CreateTime, DeadlineTime, IsTempStore, DecasualizationTime) VALUES(@UserId,@StoreName,@Logo,@BackImage,@RequestAccount,@GradeId,@ReferralUserId,@ReferralPath,@ReferralOrders,@ReferralBlance, @ReferralRequestBalance, @ReferralStatus,@StoreDescription,@DistributorGradeId, @InvitationNum, 0, 0, 0, 0, 0, 0, GETDATE(), DATEADD(YEAR, 1, GETDATE()), @IsTempStore, @DecasualizationTime)");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributor.UserId);
            this.database.AddInParameter(sqlStringCommand, "StoreName", DbType.String, distributor.StoreName);
            this.database.AddInParameter(sqlStringCommand, "Logo", DbType.String, distributor.Logo);
            this.database.AddInParameter(sqlStringCommand, "BackImage", DbType.String, distributor.BackImage);
            this.database.AddInParameter(sqlStringCommand, "RequestAccount", DbType.String, distributor.RequestAccount);
            this.database.AddInParameter(sqlStringCommand, "GradeId", DbType.Int64, (int)distributor.DistributorGradeId);
            this.database.AddInParameter(sqlStringCommand, "ReferralUserId", DbType.Int64, distributor.ParentUserId.Value);
            this.database.AddInParameter(sqlStringCommand, "ReferralPath", DbType.String, distributor.ReferralPath);
            this.database.AddInParameter(sqlStringCommand, "ReferralOrders", DbType.Int64, distributor.ReferralOrders);
            this.database.AddInParameter(sqlStringCommand, "ReferralBlance", DbType.Decimal, distributor.ReferralBlance);
            this.database.AddInParameter(sqlStringCommand, "ReferralRequestBalance", DbType.Decimal, distributor.ReferralRequestBalance);
            this.database.AddInParameter(sqlStringCommand, "ReferralStatus", DbType.Int64, distributor.ReferralStatus);
            this.database.AddInParameter(sqlStringCommand, "StoreDescription", DbType.String, distributor.StoreDescription);
            this.database.AddInParameter(sqlStringCommand, "DistributorGradeId", DbType.Int64, distributor.DistriGradeId);
            this.database.AddInParameter(sqlStringCommand, "InvitationNum", DbType.Int32, distributor.InvitationNum);
            this.database.AddInParameter(sqlStringCommand, "IsTempStore", DbType.Int32, distributor.IsTempStore);
            this.database.AddInParameter(sqlStringCommand, "DecasualizationTime", DbType.DateTime, distributor.DecasualizationTime);
            XTrace.WriteLine("添加新的分销商时的对应关系：《当前分销商ID：" +  distributor.UserId + " ------ 当前店铺名：" + distributor.StoreName + " ------ 上级分销商ID：" + distributor.ParentUserId.Value + "》");
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool CreateSendRedpackRecord(int serialid, int userid, string openid, int amount, string act_name, string wishing)
        {
            bool flag = true;
            int num = 0x4e20;
            int num2 = amount;
            SendRedpackRecordInfo sendredpackinfo = new SendRedpackRecordInfo {
                BalanceDrawRequestID = serialid,
                UserID = userid,
                OpenID = openid,
                ActName = act_name,
                Wishing = wishing,
                ClientIP = Globals.IPAddress
            };
            using (DbConnection connection = this.database.CreateConnection())
            {
                connection.Open();
                DbTransaction dbTran = connection.BeginTransaction();
                SendRedpackRecordDao dao = new SendRedpackRecordDao();

                try
                {
                    if (num2 <= num)
                    {
                        sendredpackinfo.Amount = amount;
                        flag = dao.AddSendRedpackRecord(sendredpackinfo, dbTran);
                        return this.UpdateSendRedpackRecord(serialid, 1, dbTran);
                    }
                    int num3 = amount % num;
                    int num4 = amount / num;
                    if (num3 > 0)
                    {
                        sendredpackinfo.Amount = num3;
                        flag = dao.AddSendRedpackRecord(sendredpackinfo, dbTran);
                    }
                    if (flag)
                    {
                        for (int i = 0; i < num4; i++)
                        {
                            sendredpackinfo.Amount = num;
                            flag = dao.AddSendRedpackRecord(sendredpackinfo, dbTran);
                            if (!flag)
                            {
                                dbTran.Rollback();
                            }
                        }
                        int num6 = num4 + ((num3 > 0) ? 1 : 0);
                        flag = this.UpdateSendRedpackRecord(serialid, num6, dbTran);
                        if (!flag)
                        {
                            dbTran.Rollback();
                        }
                        return flag;
                    }
                    dbTran.Rollback();
                    return flag;
                }
                catch
                {
                    if (dbTran.Connection != null)
                    {
                        dbTran.Rollback();
                    }
                    flag = false;
                }
                finally
                {
                    if (flag)
                    {
                        dbTran.Commit();
                    }
                    connection.Close();
                }
            }
            
            return flag;
        }

        public bool FrozenCommision(int userid, string ReferralStatus)
        {
            string query = "UPDATE aspnet_Distributors set ReferralStatus=@ReferralStatus WHERE UserId=@UserId ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "ReferralStatus", DbType.String, ReferralStatus);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userid);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public DataTable GetAllDistributorsName(string keywords)
        {
            DataTable table = new DataTable();
            string[] strArray = Regex.Split(DataHelper.CleanSearchString(keywords), @"\s+");
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" StoreName LIKE '%{0}%' OR UserName LIKE '%{0}%'", DataHelper.CleanSearchString(DataHelper.CleanSearchString(strArray[0])));
            for (int i = 1; (i < strArray.Length) && (i <= 5); i++)
            {
                builder.AppendFormat(" OR StoreName LIKE '%{0}%' OR UserName LIKE '%{0}%'", DataHelper.CleanSearchString(strArray[i]));
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT TOP 10 StoreName,UserName from vw_Hishop_DistributorsMembers WHERE " + builder.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public DataTable GetBalanceDrawRequestByExport(string storeName, string requestStartTime, string requestEndTime)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM vw_Hishop_BalanceDrawRequesDistributors ");
            sql.AppendLine("WHERE IsCheck = 0 ");
            if (!string.IsNullOrEmpty(storeName.Trim()))
            {
                sql.AppendLine("      AND StoreName LIKE '%" + storeName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(requestStartTime.Trim()))
            {
                sql.AppendLine("      AND datediff(dd,'" + requestStartTime + "',RequestTime)>=0");
            }
            if (!string.IsNullOrEmpty(requestEndTime.Trim()))
            {
                sql.AppendLine("      AND datediff(dd,'" + requestEndTime + "',RequestTime)<=0");
            }
            DbCommand sqlCommand = this.database.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public DataTable GetBalanceDrawRequestByBatch(string serialIDList)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM vw_Hishop_BalanceDrawRequesDistributors ");
            sql.AppendLine("WHERE IsCheck = 0 ");
            sql.AppendLine(" AND SerialID IN ( " + serialIDList + " ) ");

            DbCommand sqlCommand = this.database.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public DbQueryResult GetBalanceDrawRequest(BalanceDrawRequestQuery query)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(query.StoreName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" StoreName LIKE '%{0}%'", DataHelper.CleanSearchString(query.StoreName));
            }
            if (!string.IsNullOrEmpty(query.UserId))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" UserId = {0}", DataHelper.CleanSearchString(query.UserId));
            }
            if (!string.IsNullOrEmpty(query.RequestTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" convert(varchar(10),RequestTime,120)='{0}'", query.RequestTime);
            }
            if (!string.IsNullOrEmpty(query.IsCheck.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" IsCheck={0}", query.IsCheck);
            }
            if (!string.IsNullOrEmpty(query.CheckTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" convert(varchar(10),CheckTime,120)='{0}'", query.CheckTime);
            }
            if (!string.IsNullOrEmpty(query.RequestStartTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" datediff(dd,'{0}',RequestTime)>=0", query.RequestStartTime);
            }
            if (!string.IsNullOrEmpty(query.RequestEndTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("  datediff(dd,'{0}',RequestTime)<=0", query.RequestEndTime);
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_BalanceDrawRequesDistributors ", "SerialID", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public DbQueryResult GetBalanceDrawRequestTwo(BalanceDrawRequestQuery query)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(query.StoreName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" StoreName LIKE '%{0}%'", DataHelper.CleanSearchString(query.StoreName));
            }
            if (!string.IsNullOrEmpty(query.UserId))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" UserId = {0}", DataHelper.CleanSearchString(query.UserId));
            }
            if (!string.IsNullOrEmpty(query.RequestTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" convert(varchar(10),RequestTime,120)='{0}'", query.RequestTime);
            }
            if (!string.IsNullOrEmpty(query.IsCheck.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" IsCheck={0}", query.IsCheck);
            }
            if (!string.IsNullOrEmpty(query.CheckTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" convert(varchar(10),CheckTime,120)='{0}'", query.CheckTime);
            }
            if (!string.IsNullOrEmpty(query.CheckStartTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" datediff(dd,'{0}',CheckTime)>=0", query.CheckStartTime);
            }
            if (!string.IsNullOrEmpty(query.CheckEndTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("  datediff(dd,'{0}',CheckTime)<=0", query.CheckEndTime);
            }

            if (!string.IsNullOrEmpty(query.RequestStartTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" datediff(dd,'{0}',RequestTime)>=0", query.RequestStartTime);
            }
            if (!string.IsNullOrEmpty(query.RequestEndTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("  datediff(dd,'{0}',RequestTime)<=0", query.RequestEndTime);
            }
            if (!string.IsNullOrEmpty(query.Mobile.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" CellPhone='{0}'", query.Mobile);
            }
            if (query.CheckType.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" IsCheck={0}", query.CheckType.Value);
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_BalanceDrawRequesDistributors ", "SerialID", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public bool GetBalanceDrawRequestIsCheck(int serialid)
        {
            string query = "select IsCheck from Hishop_BalanceDrawRequest where SerialID=" + serialid;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return bool.Parse(this.database.ExecuteScalar(sqlStringCommand).ToString());
        }

        public DbQueryResult GetCommissions(CommissionsQuery query)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            if (query.UserId > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("UserId = {0}", query.UserId);
            }
            if (!string.IsNullOrEmpty(query.StoreName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("StoreName LIKE '%{0}%'", DataHelper.CleanSearchString(query.StoreName));
            }
            if (!string.IsNullOrEmpty(query.UserName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("OneUserName LIKE '%{0}%'", DataHelper.CleanSearchString(query.UserName));
            }
            if (!string.IsNullOrEmpty(query.OneStoreName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("OneStoreName LIKE '%{0}%'", DataHelper.CleanSearchString(query.OneStoreName));
            }
            if (!string.IsNullOrEmpty(query.OrderNum))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" OrderId = '{0}'", query.OrderNum);
            }
            if (!string.IsNullOrEmpty(query.StartTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" datediff(dd,'{0}',TradeTime)>=0", query.StartTime);
            }
            if (!string.IsNullOrEmpty(query.EndTime.ToString()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("  datediff(dd,'{0}',TradeTime)<=0", query.EndTime);
            }
            if (query.CommTypeId.HasValue)
            {
                builder.AppendFormat(" AND CommType = {0}", query.CommTypeId.Value);
            }
            if (query.OrderTypeId.HasValue)
            {
                builder.AppendFormat(" AND OrderType = {0}", query.OrderTypeId.Value);
            }
            if (query.IncomeTypeId.HasValue)
            {
                builder.AppendFormat(" AND IncomeType = {0}", query.IncomeTypeId.Value);
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_CommissionDistributors", "CommId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public DataTable GetCurrentDistributorsCommosion(int userId)
        {
            string query = string.Format("SELECT sum(OrderTotal) AS OrderTotal,sum(CommTotal) AS CommTotal from dbo.Hishop_Commissions where UserId={0} AND OrderId in (select OrderId from dbo.Hishop_Orders where ReferralUserId={0})", userId);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            DataSet set = this.database.ExecuteDataSet(sqlStringCommand);
            if ((set == null) || (set.Tables.Count <= 0))
            {
                return null;
            }
            return set.Tables[0];
        }

        public IList<DistributorGradeInfo> GetDistributorGrades()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM aspnet_DistributorGrade");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<DistributorGradeInfo>(reader);
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

        public int GetDistributorNum(DistributorGrade grade)
        {
            int num = 0;
            //string query = string.Format("SELECT COUNT(*) FROM aspnet_Members m INNER JOIN aspnet_Distributors d ON  m.UserId = d.UserId AND m.ReferralUserId = {0}", Globals.GetCurrentMemberUserId());
            string query = string.Format("SELECT COUNT(*) FROM aspnet_Distributors d WHERE d.ReferralUserId = {0}", Globals.GetCurrentMemberUserId());
            if (grade != DistributorGrade.All)
            {
                query = query + " AND d.GradeId=" + ((int)grade);
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    num = (int)reader[0];
                    reader.Close();
                }
            }
            return num;
        }

        public DbQueryResult GetDistributors(DistributorsQuery query)
        {
            StringBuilder builder = new StringBuilder();
            if (query.GradeId > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("IsTempStore = 0 AND DistributorGradeId = {0}", query.GradeId);
            }
            else if (query.GradeId == -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("IsTempStore = {0}", 1);
            }
            if (query.UserId > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("UserId = {0}", query.UserId);
            }
            if (query.ReferralStatus > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ReferralStatus = '{0}'", query.ReferralStatus);
            }
            if (query.DeadlineStatus > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("DeadlineStatus = {0}", query.DeadlineStatus);
            }
            if (!string.IsNullOrEmpty(query.StoreName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("StoreName LIKE '%{0}%'", DataHelper.CleanSearchString(query.StoreName));
            }
            if (!string.IsNullOrEmpty(query.CellPhone))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("CellPhone='{0}'", DataHelper.CleanSearchString(query.CellPhone));
            }
            if (!string.IsNullOrEmpty(query.MicroSignal))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("MicroSignal = '{0}'", DataHelper.CleanSearchString(query.MicroSignal));
            }
            if (!string.IsNullOrEmpty(query.RealName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("RealName LIKE '%{0}%'", DataHelper.CleanSearchString(query.RealName));
            }
            if (!string.IsNullOrEmpty(query.ReferralPath))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("(ReferralPath LIKE '{0}|%' OR ReferralPath LIKE '%|{0}|%' OR ReferralPath LIKE '%|{0}' OR ReferralPath='{0}')", DataHelper.CleanSearchString(query.ReferralPath));
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_DistributorsMembers", "UserId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public DbQueryResult GetDistributors2(DistributorsQuery query, decimal currLimitAmount, DateTime currTime)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" (SELECT dd.*, DATEADD(MONTH, -(DATEDIFF(MONTH, CONVERT(VARCHAR(20), '" + currTime + "', 23), CONVERT(VARCHAR(20), dd.CreateTime, 23))), CONVERT(VARCHAR(20), dd.CreateTime, 23)) AS CurrFirstTime, DATEADD(DAY, -1, DATEADD(MONTH, -(DATEDIFF(MONTH, CONVERT(VARCHAR(20), '" + currTime + "', 23), CONVERT(VARCHAR(20), dd.CreateTime, 23)) - 1), CONVERT(VARCHAR(20), dd.CreateTime, 23))) AS CurrLastTime, ISNULL(b.OrderTotal,0) AS OrderTotal, ISNULL(b.Amount,0) AS Amount, " + currLimitAmount + " AS LimitAmount, CASE WHEN ISNULL(b.OrderTotal,0) >= " + currLimitAmount + " THEN 2 ELSE 1 END AS FinishStatus FROM vw_Hishop_DistributorsMembers AS dd LEFT OUTER JOIN ( ");
            sql.AppendLine("    SELECT a.ReferralUserId AS StoreUserId, a.FirstTime, a.LastTime, SUM(ISNULL(a.OrderTotal,0)) AS OrderTotal, SUM(ISNULL(a.Amount,0)) AS Amount FROM (");
            sql.AppendLine("        SELECT o.OrderId, o.UserId, o.ReferralUserId, o.OrderDate, o.OrderStatus, o.OrderTotal, o.OrderCostPrice, o.OrderProfit, o.Amount, d.CreateTime, d.DeadlineTime");
            sql.AppendLine("               , DATEADD(MONTH, -(DATEDIFF(MONTH, CONVERT(VARCHAR(20), '" + currTime + "', 23), CONVERT(VARCHAR(20), CreateTime, 23))), CONVERT(VARCHAR(20), CreateTime, 23)) AS FirstTime");
            sql.AppendLine("               , DATEADD(DAY, -1, DATEADD(MONTH, -(DATEDIFF(MONTH, CONVERT(VARCHAR(20), '" + currTime + "', 23), CONVERT(VARCHAR(20), CreateTime, 23)) - 1), CONVERT(VARCHAR(20), CreateTime, 23))) AS LastTime ");
            sql.AppendLine("        FROM   aspnet_Distributors AS d LEFT OUTER JOIN Hishop_Orders AS o ON o.ReferralUserId = d.UserId AND o.OrderStatus IN ( 2, 3, 5 )");
            sql.AppendLine("        WHERE  d.IsTempStore = 1 AND d.ReferralStatus IN ( 0, 1 )");
            sql.AppendLine("    ) AS a ");
            sql.AppendLine("    WHERE a.OrderDate >= a.FirstTime AND a.OrderDate <= a.LastTime ");
            sql.AppendLine("    GROUP BY a.ReferralUserId, a.FirstTime, a.LastTime ");
            sql.AppendLine(") AS b ON dd.UserId = b.StoreUserId ");
            sql.AppendLine("WHERE dd.IsTempStore = 1 AND dd.ReferralStatus IN ( 0, 1 ) ) AS p");

            StringBuilder builder = new StringBuilder();
            if (query.GradeId > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("IsTempStore = 0 AND DistributorGradeId = {0}", query.GradeId);
            }
            else if (query.GradeId == -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("IsTempStore = {0}", 1);
            }
            if (query.UserId > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("UserId = {0}", query.UserId);
            }
            if (query.ReferralStatus > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("ReferralStatus = '{0}'", query.ReferralStatus);
            }
            if (query.DeadlineStatus > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("DeadlineStatus = {0}", query.DeadlineStatus);
            }
            if (query.FinishStatus > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("FinishStatus = {0}", query.FinishStatus);
            }
            if (!string.IsNullOrEmpty(query.StoreName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("StoreName LIKE '%{0}%'", DataHelper.CleanSearchString(query.StoreName));
            }
            if (!string.IsNullOrEmpty(query.CellPhone))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("CellPhone='{0}'", DataHelper.CleanSearchString(query.CellPhone));
            }
            if (!string.IsNullOrEmpty(query.MicroSignal))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("MicroSignal = '{0}'", DataHelper.CleanSearchString(query.MicroSignal));
            }
            if (!string.IsNullOrEmpty(query.RealName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("RealName LIKE '%{0}%'", DataHelper.CleanSearchString(query.RealName));
            }
            if (!string.IsNullOrEmpty(query.ReferralPath))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("(ReferralPath LIKE '{0}|%' OR ReferralPath LIKE '%|{0}|%' OR ReferralPath LIKE '%|{0}' OR ReferralPath='{0}')", DataHelper.CleanSearchString(query.ReferralPath));
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, sql.ToString(), "UserId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public DataTable GetDistributorsCommission(DistributorsQuery query)
        {
            StringBuilder builder = new StringBuilder("1=1");
            string str = "";
            if (query.GradeId > 0)
            {
                builder.AppendFormat("AND GradeId = {0}", query.GradeId);
            }
            if (!string.IsNullOrEmpty(query.ReferralPath))
            {
                builder.AppendFormat(" AND (ReferralPath LIKE '{0}|%' OR ReferralPath LIKE '%|{0}|%' OR ReferralPath LIKE '%|{0}' OR ReferralPath='{0}')", DataHelper.CleanSearchString(query.ReferralPath));
            }
            if (query.UserId > 0)
            {
                str = " UserId=" + query.UserId + " AND ";
            }
            string str2 = string.Concat(new object[] { "select TOP ", query.PageSize, " UserId,StoreName,GradeId,CreateTime,isnull((select SUM(OrderTotal) from Hishop_Commissions where ", str, " ReferralUserId=aspnet_Distributors.UserId),0) as OrderTotal,isnull((select SUM(CommTotal) from Hishop_Commissions where ", str, " ReferralUserId=aspnet_Distributors.UserId),0) as  CommTotal from aspnet_Distributors WHERE ", builder.ToString(), " order by CreateTime  desc" });
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(str2);
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public DataTable GetDistributorsCommosion(int userId)
        {
            string query = string.Format("SELECT  GradeId,COUNT(*),SUM(OrdersTotal) AS OrdersTotal,SUM(ReferralOrders) AS ReferralOrders,SUM(ReferralBlance) AS ReferralBlance,SUM(ReferralRequestBalance) AS ReferralRequestBalance FROM aspnet_Distributors WHERE ReferralPath LIKE '{0}|%' OR ReferralPath LIKE '%|{0}|%' OR ReferralPath LIKE '%|{0}' OR ReferralPath='{0}' GROUP BY GradeId", userId);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            DataSet set = this.database.ExecuteDataSet(sqlStringCommand);
            if ((set == null) || (set.Tables.Count <= 0))
            {
                return null;
            }
            return set.Tables[0];
        }

        public DataTable GetDistributorsCommosion(int userId, DistributorGrade grade)
        {
            string query = string.Format("SELECT sum(OrderTotal) AS OrderTotal,sum(CommTotal) AS CommTotal from dbo.Hishop_Commissions where UserId={0} AND ReferralUserId in (select UserId from aspnet_Distributors  WHERE (ReferralPath LIKE '{0}|%' OR ReferralPath LIKE '%|{0}|%' OR ReferralPath LIKE '%|{0}' OR ReferralPath='{0}') and GradeId={1})", userId, (int)grade);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            DataSet set = this.database.ExecuteDataSet(sqlStringCommand);
            if ((set == null) || (set.Tables.Count <= 0))
            {
                return null;
            }
            return set.Tables[0];
        }

        public int GetDownDistributorNum(string userid)
        {
            int num = 0;
            string query = string.Format("SELECT COUNT(*) FROM aspnet_Distributors where ReferralPath LIKE '{0}|%' OR ReferralPath LIKE '%|{0}|%' OR ReferralPath LIKE '%|{0}' OR ReferralPath='{0}'", userid);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    num = (int)reader[0];
                    reader.Close();
                }
            }
            return num;
        }

        public int GetDownDistributorNumReferralOrders(string userid)
        {
            int num = 0;
            string query = string.Format("SELECT isnull(sum(ReferralOrders),0) FROM aspnet_Distributors where ReferralPath LIKE '{0}|%' OR ReferralPath LIKE '%|{0}|%' OR ReferralPath LIKE '%|{0}' OR ReferralPath='{0}'", userid);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            DataSet set = this.database.ExecuteDataSet(sqlStringCommand);
            if (set.Tables[0].Rows.Count > 0)
            {
                num = int.Parse(set.Tables[0].Rows[0][0].ToString());
            }
            return num;
        }

        /// <summary>
        /// 获取分销商的所有上级分销商
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetAllParentDistributorsByUserId(int userId)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(";WITH D( UserId, ReferralUserId, UserName, RealName, StoreName, GradeName, LVL ) ");
            sql.AppendLine("AS( ");
            sql.AppendLine("    SELECT UserId, ReferralUserId, UserName, RealName, StoreName, GradeName, 0 LVL FROM vw_Hishop_DistributorDetail WHERE UserId = " + userId);
            sql.AppendLine("    UNION ALL ");
            sql.AppendLine("    SELECT dd.UserId, dd.ReferralUserId, dd.UserName, dd.RealName, dd.StoreName, dd.GradeName, LVL + 1 ");
            sql.AppendLine("    FROM vw_Hishop_DistributorDetail AS dd INNER JOIN D ON dd.UserId = D.ReferralUserId  ");
            sql.AppendLine(") ");
            sql.AppendLine("SELECT * FROM D  ");

            DbCommand command = this.database.GetSqlStringCommand(sql.ToString());
            return this.database.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// 根据用户ID获取下级人员总数及佣金总额
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="DefaultPartnerGradeId"></param>
        /// <param name="DefaultTutorGradeId"></param>
        /// <param name="DefaultStoreGradeId"></param>
        /// <returns></returns>
        public DataTable GetDistributorAllBalanceByUserId(int userId, string DefaultPartnerGradeId, string DefaultTutorGradeId, string DefaultStoreGradeId, string searchGradeId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT d.ReferralUserId, MAX(m.UserName) AS UserName, d.DistributorGradeId, CASE WHEN ( SELECT IsTempStore FROM aspnet_Distributors WHERE UserId = d.ReferralUserId ) = 1 THEN '钻石会员' ELSE MAX(dg.Name) END AS GradeName ");
            sql.AppendLine("       , CASE WHEN d.DistributorGradeId = '" + DefaultTutorGradeId + "' AND db.DistributorGradeId = '" + DefaultPartnerGradeId + "' THEN (SELECT Name FROM aspnet_DistributorGrade WHERE GradeId = '" +DefaultPartnerGradeId + "' ) ");
            sql.AppendLine("              WHEN d.DistributorGradeId = '" + DefaultStoreGradeId + "' AND db.DistributorGradeId = '" + DefaultPartnerGradeId + "' THEN (SELECT Name FROM aspnet_DistributorGrade WHERE GradeId = '" + DefaultTutorGradeId + "' ) ");
            sql.AppendLine("              WHEN d.DistributorGradeId = '" + DefaultTutorGradeId + "' AND db.DistributorGradeId = '" + DefaultTutorGradeId + "' THEN (SELECT Name FROM aspnet_DistributorGrade WHERE GradeId = '" + DefaultTutorGradeId + "' ) ");
            sql.AppendLine("              WHEN d.DistributorGradeId = '" + DefaultStoreGradeId + "' AND db.DistributorGradeId = '" + DefaultTutorGradeId + "' THEN (SELECT Name FROM aspnet_DistributorGrade WHERE GradeId = '" + DefaultTutorGradeId + "' ) ");
            sql.AppendLine("              ELSE (CASE WHEN ( SELECT IsTempStore FROM aspnet_Distributors WHERE UserId = d.ReferralUserId ) = 1 THEN '钻石会员' ELSE MAX(dg.Name) END) END AS DisplayGradeName ");
            sql.AppendLine("       , COUNT(*) AS DistriCnt, ISNULL(SUM(ISNULL(d.ReferralBlance, 0) + ISNULL(d.ReferralRequestBalance, 0)), 0) AS SumAllBalance ");
            sql.AppendLine("FROM aspnet_Distributors AS d ");
            sql.AppendLine("LEFT OUTER JOIN aspnet_Members AS m ON d.ReferralUserId = m.UserId ");
            sql.AppendLine("LEFT OUTER JOIN aspnet_DistributorGrade AS dg ON d.DistributorGradeId = dg.GradeId ");
            sql.AppendLine("LEFT OUTER JOIN aspnet_Distributors AS db ON d.ReferralUserId = db.UserId ");
            sql.AppendLine("WHERE d.ReferralUserId = " + userId );
            if (!string.IsNullOrEmpty(searchGradeId))
            {
                sql.AppendLine(" AND d.DistributorGradeId = '" + searchGradeId + "'");
            }
            sql.AppendLine("GROUP BY d.ReferralUserId, d.DistributorGradeId, db.DistributorGradeId ");
            sql.AppendLine("ORDER BY d.DistributorGradeId ASC");

            XTrace.WriteLine("----------我的团队页面数据获取SQL1：" + sql.ToString());
            
            DbCommand command = this.database.GetSqlStringCommand(sql.ToString());
            return this.database.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// 根据用户ID获取下级人员总数及佣金总额
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="DefaultPartnerGradeId"></param>
        /// <param name="DefaultTutorGradeId"></param>
        /// <param name="DefaultStoreGradeId"></param>
        /// <returns></returns>
        public DataTable GetDistributorDetailBalanceByUserId(int userId, string DefaultPartnerGradeId, string DefaultTutorGradeId, string DefaultStoreGradeId, string searchGradeId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT d.UserId, MAX(m.UserName) AS UserName, d.DistributorGradeId, CASE WHEN ( SELECT IsTempStore FROM aspnet_Distributors WHERE UserId = d.UserId ) = 1 THEN '钻石会员' ELSE MAX(dg.Name) END AS GradeName ");
            sql.AppendLine("       , CASE WHEN d.DistributorGradeId = '" + DefaultTutorGradeId + "' AND db.DistributorGradeId = '" + DefaultPartnerGradeId + "' THEN (SELECT Name FROM aspnet_DistributorGrade WHERE GradeId = '" + DefaultPartnerGradeId + "' ) ");
            sql.AppendLine("              WHEN d.DistributorGradeId = '" + DefaultStoreGradeId + "' AND db.DistributorGradeId = '" + DefaultPartnerGradeId + "' THEN (SELECT Name FROM aspnet_DistributorGrade WHERE GradeId = '" + DefaultTutorGradeId + "' ) ");
            sql.AppendLine("              WHEN d.DistributorGradeId = '" + DefaultTutorGradeId + "' AND db.DistributorGradeId = '" + DefaultTutorGradeId + "' THEN (SELECT Name FROM aspnet_DistributorGrade WHERE GradeId = '" + DefaultTutorGradeId + "' ) ");
            sql.AppendLine("              WHEN d.DistributorGradeId = '" + DefaultStoreGradeId + "' AND db.DistributorGradeId = '" + DefaultTutorGradeId + "' THEN (SELECT Name FROM aspnet_DistributorGrade WHERE GradeId = '" + DefaultTutorGradeId + "' ) ");
            sql.AppendLine("              ELSE (CASE WHEN ( SELECT IsTempStore FROM aspnet_Distributors WHERE UserId = d.UserId ) = 1 THEN '钻石会员' ELSE MAX(dg.Name) END) END AS DisplayGradeName ");
            sql.AppendLine("       , COUNT(*) AS DistriCnt, ISNULL(SUM(ISNULL(d.ReferralBlance, 0) + ISNULL(d.ReferralRequestBalance, 0)), 0) AS SumAllBalance ");
            sql.AppendLine("FROM aspnet_Distributors AS d ");
            sql.AppendLine("LEFT OUTER JOIN aspnet_Members AS m ON d.UserId = m.UserId ");
            sql.AppendLine("LEFT OUTER JOIN aspnet_DistributorGrade AS dg ON d.DistributorGradeId = dg.GradeId ");
            sql.AppendLine("LEFT OUTER JOIN aspnet_Distributors AS db ON d.UserId = db.UserId ");
            sql.AppendLine("WHERE d.ReferralUserId = " + userId);
            if (!string.IsNullOrEmpty(searchGradeId))
            {
                sql.AppendLine(" AND d.DistributorGradeId = '" + searchGradeId + "'");
            }
            sql.AppendLine("GROUP BY d.UserId, d.DistributorGradeId, db.DistributorGradeId ");
            sql.AppendLine("ORDER BY d.DistributorGradeId ASC");

            XTrace.WriteLine("----------我的团队页面数据获取SQL2：" + sql.ToString());

            DbCommand command = this.database.GetSqlStringCommand(sql.ToString());
            return this.database.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// 获取我的团队中的销售数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable GetDistributorTeamData(DistributorsQuery query)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT d.UserId, m.UserName, d.StoreName, d.DistributorGradeId, dg.Name AS DistributorGradeName, d.GradeId");
            sql.AppendLine("       , d.ReferralUserId, d.ReferralPath, d.OrdersTotal, d.ReferralOrders, d.ReferralBlance, d.ReferralRequestBalance ");
            sql.AppendLine("       , ISNULL(c.OrderTotal, 0) AS MySaleAmount, ISNULL(c.CommTotal, 0) AS MyCommissionAmount, ISNULL(cnt.MemberCnt, 0) AS MemberCnt");
            sql.AppendLine("       , ISNULL(fr.OrderTotal, 0) AS ChildOrderTotal, ISNULL(fr.CommTotal, 0) AS ChildCommTotal");
            sql.AppendLine("FROM aspnet_Distributors AS d ");
            sql.AppendLine("LEFT OUTER JOIN aspnet_DistributorGrade AS dg ON d.DistributorGradeId = dg.GradeId ");
            sql.AppendLine("LEFT OUTER JOIN aspnet_Members AS m ON d.UserId = m.UserId ");
            sql.AppendLine("LEFT OUTER JOIN (");
            sql.AppendLine("    SELECT UserId, ISNULL(SUM(OrderTotal), 0) AS OrderTotal, ISNULL(SUM(CommTotal), 0) AS CommTotal FROM Hishop_Commissions GROUP BY UserId");
            sql.AppendLine(") AS c ON d.UserId = c.UserId ");
            sql.AppendLine("LEFT OUTER JOIN (");
            sql.AppendLine("    SELECT ReferralUserId, COUNT(ReferralUserId) AS MemberCnt FROM aspnet_Distributors WHERE UserId <> ReferralUserId GROUP BY ReferralUserId");
            sql.AppendLine(") AS cnt ON d.UserId = cnt.ReferralUserId ");
            sql.AppendLine("LEFT OUTER JOIN (");
            sql.AppendLine("    SELECT ReferralUserId, ISNULL(SUM(OrderTotal), 0) AS OrderTotal, ISNULL(SUM(CommTotal), 0) AS CommTotal FROM Hishop_Commissions GROUP BY ReferralUserId");
            sql.AppendLine(") AS fr ON d.UserId = fr.ReferralUserId ");

            DbCommand command = this.database.GetSqlStringCommand(sql.ToString());
            return this.database.ExecuteDataSet(command).Tables[0];
        }

        public DataTable GetDownDistributors(DistributorsQuery query)
        {
            StringBuilder builder = new StringBuilder("1=1");
            string str = "";
            if (query.GradeId > 0)
            {
                if (query.GradeId == 2)
                {
                    builder.AppendFormat(" AND UserId IN (SELECT UserId FROM aspnet_Members WHERE ReferralUserId = {0})", DataHelper.CleanSearchString(query.ReferralPath));
                }
                if (query.GradeId == 3)
                {
                    builder.AppendFormat(" AND UserId IN (SELECT UserId FROM aspnet_Members WHERE ReferralUserId = {0}) ", DataHelper.CleanSearchString(query.ReferralPath));
                }
            }
            if (query.UserId > 0)
            {
                str = " UserId=" + query.UserId + " AND ";
            }
            string str2 = string.Concat(new object[] { "select TOP ", query.PageSize, " UserId,StoreName,GradeId,CreateTime,isnull((select SUM(OrderTotal) from Hishop_Commissions where ", str, " ReferralUserId=aspnet_Distributors.UserId),0) as OrderTotal,isnull(( SELECT SUM(d.ReferralBlance + d.ReferralRequestBalance) FROM aspnet_Members m INNER JOIN aspnet_Distributors d ON  m.UserId = d.UserId AND m.ReferralUserId = aspnet_Distributors.UserId),0) as  Blances,(SELECT COUNT(*) FROM aspnet_Members m INNER JOIN aspnet_Distributors d ON m.UserId = d.UserId AND m.ReferralUserId = aspnet_Distributors.UserId) AS GroupQty,(SELECT NAME FROM aspnet_DistributorGrade dg WHERE dg.GradeId = aspnet_Distributors.DistributorGradeId) AS GradeName from aspnet_Distributors WHERE ", builder.ToString(), " order by CreateTime  desc" });
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(str2);
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public DataTable GetDownDistributorsDetails(DistributorsQuery query)
        {
            StringBuilder builder = new StringBuilder("1=1");
            string str = "";
            if (query.GradeId > 0)
            {
                if (query.GradeId == 2)
                {
                    builder.AppendFormat(" AND UserId IN (SELECT UserId FROM aspnet_Members WHERE ReferralUserId = {0}) ", DataHelper.CleanSearchString(query.ReferralPath));
                }
                if (query.GradeId == 3)
                {
                    builder.AppendFormat(" AND UserId IN (SELECT UserId FROM aspnet_Members WHERE ReferralUserId = {0}) ", DataHelper.CleanSearchString(query.ReferralPath));
                }
            }
            if (query.UserId > 0)
            {
                str = " UserId=" + query.UserId + " AND ";
            }
            string str2 = string.Concat(new object[] { "select TOP ", query.PageSize, " UserId,StoreName,GradeId,CreateTime,isnull((select SUM(OrderTotal) from Hishop_Commissions where ", str, " ReferralUserId=aspnet_Distributors.UserId),0) as OrderTotal,isnull((select SUM(CommTotal) from Hishop_Commissions where ", str, " ReferralUserId=aspnet_Distributors.UserId),0) as  CommTotal,(SELECT COUNT(*) FROM aspnet_Members m INNER JOIN aspnet_Distributors d ON m.UserId = d.UserId AND m.ReferralUserId = aspnet_Distributors.UserId) AS GroupQty,(SELECT NAME FROM aspnet_DistributorGrade dg WHERE dg.GradeId = aspnet_Distributors.DistributorGradeId) AS GradeName, ReferralBlance + ReferralRequestBalance AS Blance from aspnet_Distributors WHERE ", builder.ToString(), " order by CreateTime  desc" });
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(str2);
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public DistributorGradeInfo GetIsDefaultDistributorGradeInfo()
        {
            DistributorGradeInfo info = null;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM aspnet_DistributorGrade where IsDefault=1 order by CommissionsLimit asc", new object[0]));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = DataMapper.PopulateDistributorGradeInfo(reader);
                }
            }
            return info;
        }

        public decimal GetUserCommissions(int userid, DateTime fromdatetime)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" State=1 ");
            if (userid > 0)
            {
                builder.Append(" and UserID=" + userid);
            }
            builder.Append(" and TradeTime>='" + fromdatetime.ToString("yyyy-MM-dd") + " 00:00:00'");
            string query = " select isnull(sum(CommTotal),0) from Hishop_Commissions where " + builder.ToString();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return decimal.Parse(this.database.ExecuteScalar(sqlStringCommand).ToString());
        }

        public decimal GetUserCommissionsByCond(int userid, int commorderstatus, DateTime fromdatetime, bool isByMonth = false)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" State=1 ");
            if (userid > 0)
            {
                builder.Append(" and UserID=" + userid);
            }
            if (commorderstatus > -1)
            {
                builder.Append(" and CommOrderStatus=" + commorderstatus);
            }
            if (isByMonth)
            {
                builder.Append(" and CONVERT(VARCHAR(7), TradeTime, 126) = '" + fromdatetime.ToString("yyyy-MM") + "'");
            }
            else
            {
                builder.Append(" and TradeTime>='" + fromdatetime.ToString("yyyy-MM-dd") + " 00:00:00'");
            }
            string query = " select isnull(sum(CommTotal),0) from Hishop_Commissions where " + builder.ToString();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return decimal.Parse(this.database.ExecuteScalar(sqlStringCommand).ToString());
        }

        public DataSet GetUserRanking(int userid)
        {
            //string query = string.Format("select d.UserId,d.Logo,d.ReferralBlance+d.ReferralRequestBalance as Blance,d.StoreName,(select count(0) from aspnet_Distributors a where (a.ReferralBlance+a.ReferralRequestBalance>(d.ReferralBlance+d.ReferralRequestBalance) or (a.ReferralBlance+a.ReferralRequestBalance=(d.ReferralBlance+d.ReferralRequestBalance) and a.UserID<d.UserID)))+1 as ccount  from aspnet_Distributors d where UserID={0};select top 10 UserId,Logo,ReferralBlance+ReferralRequestBalance as Blance,StoreName  from aspnet_Distributors order by Blance desc,userid asc;select top 10 UserId,Logo,ReferralBlance+ReferralRequestBalance as Blance,StoreName  from aspnet_Distributors where (ReferralPath like '{0}|%' or ReferralPath like '%|{0}' or ReferralPath = '{0}') order by Blance desc", userid);

            string query = string.Format("select d.UserId,d.Logo,d.ReferralBlance+d.ReferralRequestBalance as Blance,d.StoreName,(select count(0) from aspnet_Distributors a where (a.ReferralBlance+a.ReferralRequestBalance>(d.ReferralBlance+d.ReferralRequestBalance) or (a.ReferralBlance+a.ReferralRequestBalance=(d.ReferralBlance+d.ReferralRequestBalance) and a.UserID<d.UserID)))+1 as ccount  from aspnet_Distributors d where UserID={0};select UserId,Logo,ReferralBlance+ReferralRequestBalance as Blance,StoreName  from aspnet_Distributors order by Blance desc,userid asc;select UserId,Logo,ReferralBlance+ReferralRequestBalance as Blance,StoreName  from aspnet_Distributors where ReferralUserId = '{0}' order by Blance desc", userid);

            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return this.database.ExecuteDataSet(sqlStringCommand);
        }

        public int IsExiteDistributorsByStoreName(string storname)
        {
            string query = "SELECT UserId FROM aspnet_Distributors WHERE StoreName=@StoreName";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "StoreName", DbType.String, DataHelper.CleanSearchString(storname));
            object obj2 = this.database.ExecuteScalar(sqlStringCommand);
            return ((obj2 != null) ? ((int)obj2) : 0);
        }

        public bool IsExitsCommionsRequest(int userId)
        {
            bool flag = false;
            string query = "SELECT * FROM dbo.Hishop_BalanceDrawRequest WHERE IsCheck=0 AND UserId=@UserId";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool CheckOrderIsPay(int userId)
        {
            bool flag = false;
            string query = "SELECT * FROM Hishop_Orders WHERE OrderType = 2 AND OrderStatus IN ( 2, 3, 5 ) AND UserId = @UserId";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    flag = true;
                }
            }
            return flag;
        }

        public DataTable OrderIDGetCommosion(string orderid)
        {
            string query = string.Format("SELECT CommId,Userid,OrderTotal,CommTotal from Hishop_Commissions where OrderId='" + orderid + "' ", new object[0]);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            DataSet set = this.database.ExecuteDataSet(sqlStringCommand);
            if ((set == null) || (set.Tables.Count <= 0))
            {
                return null;
            }
            return set.Tables[0];
        }

        public void RemoveDistributorProducts(List<int> productIds, int distributorId)
        {
            string str = string.Join<int>(",", productIds);
            string query = "DELETE FROM Hishop_DistributorProducts WHERE UserId=@UserId AND ProductId IN (" + str + ");";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributorId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public void RemoveDistributorProductsByUserId(int distributorId)
        {
            
            string query = "DELETE FROM Hishop_DistributorProducts WHERE UserId=@UserId ;";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributorId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public string SendRedPackToBalanceDrawRequest(int serialid)
        {
            if (!SettingsManager.GetMasterSettings(false).EnableWeiXinRequest)
            {
                return "管理员后台未开启微信付款！";
            }
            string query = "select a.SerialID,a.userid,a.Amount,b.OpenID,isnull(b.OpenId,'') as OpenId from Hishop_BalanceDrawRequest a inner join aspnet_Members b on a.userid=b.userid where SerialID=@SerialID and IsCheck=0";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "SerialID", DbType.Int32, serialid);
            DataTable table = this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
            string str4 = string.Empty;
            int userid = 0;
            if (table.Rows.Count > 0)
            {
                str4 = table.Rows[0]["OpenId"].ToString();
                userid = int.Parse(table.Rows[0]["UserID"].ToString());
                decimal num2 = decimal.Parse(table.Rows[0]["Amount"].ToString()) * 100M;
                int amount = Convert.ToInt32(num2);
                if (string.IsNullOrEmpty(str4))
                {
                    return "用户未绑定微信号";
                }
                query = "select top 1 ID from vshop_SendRedpackRecord where BalanceDrawRequestID=" + table.Rows[0]["SerialID"].ToString();
                sqlStringCommand = this.database.GetSqlStringCommand(query);
                if (this.database.ExecuteDataSet(sqlStringCommand).Tables[0].Rows.Count > 0)
                {
                    return "-1";
                }
                return (this.CreateSendRedpackRecord(serialid, userid, str4, amount, "您的提现申请已成功", "恭喜您提现成功!") ? "1" : "提现操作失败");
            }
            return "该用户没有提现申请";
        }

        public bool UpdateBalanceDistributors(int UserId, decimal ReferralRequestBalance)
        {
            string query = "UPDATE aspnet_Distributors set ReferralBlance=ReferralBlance-@ReferralBlance,ReferralRequestBalance=ReferralRequestBalance+@ReferralRequestBalance, AccountBalance=AccountBalance-@AccountBalance  WHERE UserId=@UserId ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "ReferralBlance", DbType.Decimal, ReferralRequestBalance);
            this.database.AddInParameter(sqlStringCommand, "ReferralRequestBalance", DbType.Decimal, ReferralRequestBalance);
            this.database.AddInParameter(sqlStringCommand, "AccountBalance", DbType.Decimal, ReferralRequestBalance);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, UserId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateBalanceDrawRequest(int Id, string Remark)
        {
            string query = "UPDATE Hishop_BalanceDrawRequest set Remark=@Remark,IsCheck=1,CheckTime=getdate() WHERE SerialID=@SerialID ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "Remark", DbType.String, Remark);
            this.database.AddInParameter(sqlStringCommand, "SerialID", DbType.Int32, Id);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateCalculationCommissionByBuy(string UserId, string ReferralUserId, string OrderId, decimal OrderTotal, decimal resultCommTatal, string remark = "")
        {
            string query = "UPDATE aspnet_Distributors SET AccountBalance = ISNULL(AccountBalance,0) + " + resultCommTatal + " WHERE UserId = " + UserId;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateDistributorOrderTotals(string UserId,  decimal OrderTotal)
        {
            string query = "UPDATE aspnet_Distributors SET OrdersTotal = ISNULL(OrdersTotal,0) + " + OrderTotal + " WHERE UserId = " + UserId;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateCalcRecommendedIncome(string UserId, string ReferralUserId, string OrderFromStoreId, string OrderId, decimal OrderTotal, decimal resultCommTatal, int commType, string remark = "")
        {

            bool flag = false;
            string query = "SELECT * FROM Hishop_Commissions WHERE OrderId = @OrderId AND CommTotal = @CommTotal AND OrderFromStoreId = @OrderFromStoreId AND CommType = @CommType AND State = 1 ";
            DbCommand sqlCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlCommand, "OrderId", DbType.String, OrderId);
            this.database.AddInParameter(sqlCommand, "CommTotal", DbType.Decimal, resultCommTatal);
            this.database.AddInParameter(sqlCommand, "OrderFromStoreId", DbType.Int32, OrderFromStoreId);
            this.database.AddInParameter(sqlCommand, "CommType", DbType.Int32, commType);
            using (IDataReader reader = this.database.ExecuteReader(sqlCommand))
            {
                if (reader.Read())
                {
                    flag = true;
                }
            }

            if (flag)
            {
                return false;
            }

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("begin try  ");
            sql.AppendLine("  begin tran TranUpdate ");
            sql.AppendFormat(" INSERT INTO [Hishop_Commissions](UserId,ReferralUserId,OrderFromStoreId,OrderId,TradeTime,OrderTotal,CommTotal,CommType,CommRemark,State, CommOrderStatus, IncomeType)values({0}, {1}, {2}, '{3}', '{4}', {5}, {6}, {7}, '{8}', {9}, {10}, 1); ", UserId, ReferralUserId, OrderFromStoreId, OrderId, DateTime.Now.ToString(), OrderTotal, resultCommTatal, commType, remark, 1, 1);
            sql.AppendFormat("  UPDATE aspnet_Distributors set ReferralBlance = ISNULL(ReferralBlance, 0) + {0}, AccountBalance = ISNULL(AccountBalance, 0) + {1}, AccumulatedIncome = ISNULL(AccumulatedIncome, 0) + {2} WHERE UserId='{3}' ; ", resultCommTatal, resultCommTatal, resultCommTatal, UserId);
            sql.AppendFormat("  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+{0},ReferralOrders=ReferralOrders+1  WHERE UserId='{1}'; ", OrderTotal, UserId);
            sql.AppendLine("  COMMIT TRAN TranUpdate ");
            sql.AppendLine("  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ");

            //string query = "";
            //object obj2 = query + "begin try  " + "  begin tran TranUpdate";
            //obj2 = string.Concat(new object[] { obj2, " INSERT INTO   [Hishop_Commissions](UserId,ReferralUserId,OrderFromStoreId,OrderId,TradeTime,OrderTotal,CommTotal,CommType,CommRemark,State, CommOrderStatus)values(", UserId, ",", ReferralUserId, ",", OrderFromStoreId, ",'", OrderId, "','", DateTime.Now.ToString(), "',", OrderTotal, ",", resultCommTatal, ",", commType, ",'" + remark + "',1,0) ;" });
            //obj2 = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set ReferralBlance=ReferralBlance+", resultCommTatal, "  WHERE UserId=", UserId, "; " });
            //// 2015-10-28 这里是更新上级销售订单金额
            ////query = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+", OrderTotal, ",ReferralOrders=ReferralOrders+1  WHERE UserId=", ReferralUserId, "; " }) + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            //// 这里只更新自己的销售金额
            //query = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+", OrderTotal, ",ReferralOrders=ReferralOrders+1  WHERE UserId=", UserId, "; " }) + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateCalcRecommendedIncomeLTZero(string UserId, string ReferralUserId, string OrderFromStoreId, string OrderId, decimal OrderTotal, decimal resultCommTatal, int commType, string remark = "")
        {
            StringBuilder sql = new StringBuilder();
            //sql.AppendLine("begin try  ");
            //sql.AppendLine("  begin tran TranUpdate ");
            sql.AppendFormat(" INSERT INTO [Hishop_Commissions](UserId,ReferralUserId,OrderFromStoreId,OrderId,TradeTime,OrderTotal,CommTotal,CommType,CommRemark,State, CommOrderStatus, IncomeType)values({0}, {1}, {2}, '{3}', '{4}', {5}, {6}, {7}, '{8}', {9}, {10}, 1); ", UserId, ReferralUserId, OrderFromStoreId, OrderId, DateTime.Now.ToString(), OrderTotal, resultCommTatal, commType, remark, 0, 1);
            //sql.AppendFormat("  UPDATE aspnet_Distributors set ReferralBlance = ISNULL(ReferralBlance, 0) + {0}, AccountBalance = ISNULL(AccountBalance, 0) + {1}, AccumulatedIncome = ISNULL(AccumulatedIncome, 0) + {2} WHERE UserId='{3}' ; ", resultCommTatal, resultCommTatal, resultCommTatal, UserId);
            //sql.AppendFormat("  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+{0},ReferralOrders=ReferralOrders+1  WHERE UserId='{1}'; ", OrderTotal, UserId);
            //sql.AppendLine("  COMMIT TRAN TranUpdate ");
            //sql.AppendLine("  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ");

            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }


        public bool UpdateCalculationCommission(string UserId, string ReferralUserId, string OrderFromStoreId, string OrderId, decimal OrderTotal, decimal resultCommTatal, int commType, string remark = "")
        {
            bool flag = false;
            string query = "SELECT * FROM Hishop_Commissions WHERE OrderId = @OrderId AND CommTotal = @CommTotal AND OrderFromStoreId = @OrderFromStoreId AND CommType = @CommType AND State = 1 AND UserId = @UserId ";
            DbCommand sqlCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlCommand, "OrderId", DbType.String, OrderId);
            this.database.AddInParameter(sqlCommand, "CommTotal", DbType.Decimal, resultCommTatal);
            this.database.AddInParameter(sqlCommand, "OrderFromStoreId", DbType.Int32, OrderFromStoreId);
            this.database.AddInParameter(sqlCommand, "CommType", DbType.Int32, commType);
            this.database.AddInParameter(sqlCommand, "UserId", DbType.Int32, UserId);
            using (IDataReader reader = this.database.ExecuteReader(sqlCommand))
            {
                if (reader.Read())
                {
                    flag = true;
                }
            }

            if (flag)
            {
                return false;
            }

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("begin try  ");
            sql.AppendLine("  begin tran TranUpdate ");
            sql.AppendFormat(" INSERT INTO [Hishop_Commissions](UserId,ReferralUserId,OrderFromStoreId,OrderId,TradeTime,OrderTotal,CommTotal,CommType,CommRemark,State, CommOrderStatus, IncomeType)values({0}, {1}, {2}, '{3}', '{4}', {5}, {6}, {7}, '{8}', {9}, {10}, 1); ", UserId, ReferralUserId, OrderFromStoreId, OrderId, DateTime.Now.ToString(), OrderTotal, resultCommTatal, commType, remark, 1, 0);
            sql.AppendFormat("  UPDATE aspnet_Distributors set AccountBalance = ISNULL(AccountBalance, 0) + {0}, AccumulatedIncome = ISNULL(AccumulatedIncome, 0) + {1} WHERE UserId='{2}' ; ", resultCommTatal, resultCommTatal, UserId);
            sql.AppendFormat("  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+{0},ReferralOrders=ReferralOrders+1  WHERE UserId='{1}'; ", OrderTotal, UserId);
            sql.AppendLine("  COMMIT TRAN TranUpdate ");
            sql.AppendLine("  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ");


            //string query = "";
            //object obj2 = query + "begin try  " + "  begin tran TranUpdate";
            //obj2 = string.Concat(new object[] { obj2, " INSERT INTO   [Hishop_Commissions](UserId,ReferralUserId,OrderFromStoreId,OrderId,TradeTime,OrderTotal,CommTotal,CommType,CommRemark,State, CommOrderStatus)values(", UserId, ",", ReferralUserId, ",", OrderFromStoreId, ",'", OrderId, "','", DateTime.Now.ToString(), "',", OrderTotal, ",", resultCommTatal, ",", commType, ",'" + remark + "',1,0) ;" });
            //obj2 = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set ReferralBlance=ReferralBlance+", resultCommTatal, "  WHERE UserId=", UserId, "; " });
            //// 2015-10-28 这里是更新上级销售订单金额
            ////query = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+", OrderTotal, ",ReferralOrders=ReferralOrders+1  WHERE UserId=", ReferralUserId, "; " }) + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            //// 这里只更新自己的销售金额
            //query = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+", OrderTotal, ",ReferralOrders=ReferralOrders+1  WHERE UserId=", UserId, "; " }) + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateCalculationCommissionNoUpdateOrderTotal(string UserId, string ReferralUserId, string OrderFromStoreId, string OrderId, decimal OrderTotal, decimal resultCommTatal, int commType, string remark = "")
        {
            bool flag = false;
            string query = "SELECT * FROM Hishop_Commissions WHERE OrderId = @OrderId AND CommTotal = @CommTotal AND OrderFromStoreId = @OrderFromStoreId AND CommType = @CommType AND State = 1 AND UserId = @UserId ";
            DbCommand sqlCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlCommand, "OrderId", DbType.String, OrderId);
            this.database.AddInParameter(sqlCommand, "CommTotal", DbType.Decimal, resultCommTatal);
            this.database.AddInParameter(sqlCommand, "OrderFromStoreId", DbType.Int32, OrderFromStoreId);
            this.database.AddInParameter(sqlCommand, "CommType", DbType.Int32, commType);
            this.database.AddInParameter(sqlCommand, "UserId", DbType.Int32, UserId);
            using (IDataReader reader = this.database.ExecuteReader(sqlCommand))
            {
                if (reader.Read())
                {
                    flag = true;
                }
            }

            if (flag)
            {
                return false;
            }

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("begin try  ");
            sql.AppendLine("  begin tran TranUpdate ");
            sql.AppendFormat(" INSERT INTO [Hishop_Commissions](UserId,ReferralUserId,OrderFromStoreId,OrderId,TradeTime,OrderTotal,CommTotal,CommType,CommRemark,State, CommOrderStatus, IncomeType)values({0}, {1}, {2}, '{3}', '{4}', {5}, {6}, {7}, '{8}', {9}, {10}, 1); ", UserId, ReferralUserId, OrderFromStoreId, OrderId, DateTime.Now.ToString(), OrderTotal, resultCommTatal, commType, remark, 1, 0);
            sql.AppendFormat("  UPDATE aspnet_Distributors set AccountBalance = ISNULL(AccountBalance, 0) + {0}, AccumulatedIncome = ISNULL(AccumulatedIncome, 0) + {1} WHERE UserId='{2}' ; ", resultCommTatal, resultCommTatal, UserId);
            //sql.AppendFormat("  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+{0},ReferralOrders=ReferralOrders+1  WHERE UserId='{1}'; ", OrderTotal, UserId);
            sql.AppendLine("  COMMIT TRAN TranUpdate ");
            sql.AppendLine("  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ");


            //string query = "";
            //object obj2 = query + "begin try  " + "  begin tran TranUpdate";
            //obj2 = string.Concat(new object[] { obj2, " INSERT INTO   [Hishop_Commissions](UserId,ReferralUserId,OrderFromStoreId,OrderId,TradeTime,OrderTotal,CommTotal,CommType,CommRemark,State, CommOrderStatus)values(", UserId, ",", ReferralUserId, ",", OrderFromStoreId, ",'", OrderId, "','", DateTime.Now.ToString(), "',", OrderTotal, ",", resultCommTatal, ",", commType, ",'" + remark + "',1,0) ;" });
            
            //query = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set  ReferralBlance=ReferralBlance+", resultCommTatal, "  WHERE UserId=", UserId, "; " }) + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        
        public bool UpdateDistributor(DistributorsInfo distributor)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Distributors SET StoreName=@StoreName,Logo=@Logo,BackImage=@BackImage,RequestAccount=@RequestAccount,ReferralOrders=@ReferralOrders,ReferralBlance=@ReferralBlance, ReferralRequestBalance=@ReferralRequestBalance,StoreDescription=@StoreDescription,ReferralStatus=@ReferralStatus, DistributorGradeId = @DistributorGradeId, InvitationNum = @InvitationNum, IsTempStore = @IsTempStore WHERE UserId=@UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributor.UserId);
            this.database.AddInParameter(sqlStringCommand, "StoreName", DbType.String, distributor.StoreName);
            this.database.AddInParameter(sqlStringCommand, "Logo", DbType.String, distributor.Logo);
            this.database.AddInParameter(sqlStringCommand, "BackImage", DbType.String, distributor.BackImage);
            this.database.AddInParameter(sqlStringCommand, "RequestAccount", DbType.String, distributor.RequestAccount);
            this.database.AddInParameter(sqlStringCommand, "ReferralOrders", DbType.Int64, distributor.ReferralOrders);
            this.database.AddInParameter(sqlStringCommand, "ReferralStatus", DbType.Int64, distributor.ReferralStatus);
            this.database.AddInParameter(sqlStringCommand, "ReferralBlance", DbType.Decimal, distributor.ReferralBlance);
            this.database.AddInParameter(sqlStringCommand, "ReferralRequestBalance", DbType.Decimal, distributor.ReferralRequestBalance);
            this.database.AddInParameter(sqlStringCommand, "StoreDescription", DbType.String, distributor.StoreDescription);
            this.database.AddInParameter(sqlStringCommand, "DistributorGradeId", DbType.Int32, distributor.DistriGradeId);
            this.database.AddInParameter(sqlStringCommand, "InvitationNum", DbType.Int32, distributor.InvitationNum);
            this.database.AddInParameter(sqlStringCommand, "IsTempStore", DbType.Int32, distributor.IsTempStore);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateDistributorById(string requestAccount, int distributorId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Distributors SET RequestAccount=@RequestAccount WHERE UserId=@UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributorId);
            this.database.AddInParameter(sqlStringCommand, "RequestAccount", DbType.String, requestAccount);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateDistributorById(string requestAccount, string bankname, string bankaddress, string accountname, int distributorId, string regionAddress)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE aspnet_Distributors SET RequestAccount=@RequestAccount ");
            if (!string.IsNullOrEmpty(bankname))
            {
                sql.Append(", BankName=@BankName ");
            }
            if (!string.IsNullOrEmpty(bankaddress))
            {
                sql.Append(", BankAddress=@BankAddress ");
            }
            if (!string.IsNullOrEmpty(accountname))
            {
                sql.Append(", AccountName=@AccountName ");
            }
            if (!string.IsNullOrEmpty(regionAddress))
            {
                sql.Append(", RegionAddress=@RegionAddress ");
            }
            sql.Append("WHERE UserId=@UserId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql.ToString());
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributorId);
            this.database.AddInParameter(sqlStringCommand, "RequestAccount", DbType.String, requestAccount);
            if (!string.IsNullOrEmpty(bankname))
            {
                this.database.AddInParameter(sqlStringCommand, "BankName", DbType.String, bankname);
            }
            if (!string.IsNullOrEmpty(bankaddress))
            {
                this.database.AddInParameter(sqlStringCommand, "BankAddress", DbType.String, bankaddress);
            }
            if (!string.IsNullOrEmpty(accountname))
            {
                this.database.AddInParameter(sqlStringCommand, "AccountName", DbType.String, accountname);
            }
            if (!string.IsNullOrEmpty(regionAddress))
            {
                this.database.AddInParameter(sqlStringCommand, "RegionAddress", DbType.String, regionAddress);
            }
            
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateDistributorMessage(DistributorsInfo distributor)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Distributors SET StoreName=@StoreName,Logo=@Logo,BackImage=@BackImage,StoreDescription=@StoreDescription,RequestAccount=@RequestAccount WHERE UserId=@UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributor.UserId);
            this.database.AddInParameter(sqlStringCommand, "StoreName", DbType.String, distributor.StoreName);
            this.database.AddInParameter(sqlStringCommand, "Logo", DbType.String, distributor.Logo);
            this.database.AddInParameter(sqlStringCommand, "BackImage", DbType.String, distributor.BackImage);
            this.database.AddInParameter(sqlStringCommand, "StoreDescription", DbType.String, distributor.StoreDescription);
            this.database.AddInParameter(sqlStringCommand, "RequestAccount", DbType.String, distributor.RequestAccount);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateGradeId(ArrayList GradeIdList, ArrayList ReferralUserIdList)
        {
            string query = "";
            query = query + "begin try  " + "  begin tran TranUpdate";
            for (int i = 0; i < ReferralUserIdList.Count; i++)
            {
                if (!GradeIdList[i].Equals(0))
                {
                    object obj2 = query;
                    query = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors SET DistributorGradeId=", GradeIdList[i], " where UserId=", ReferralUserIdList[i], "; " });
                }
            }
            query = query + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateNotSetCalculationCommission(ArrayList UserIdList, ArrayList OrdersTotal)
        {
            string query = "";
            query = query + "begin try  " + "  begin tran TranUpdate";
            for (int i = 0; i < UserIdList.Count; i++)
            {
                object obj2 = query;
                query = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set OrdersTotal=OrdersTotal+", OrdersTotal[i], " WHERE UserId=", UserIdList[i], "; " });
            }
            query = query + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateSendRedpackRecord(int serialid, int num, DbTransaction dbTran)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update Hishop_BalanceDrawRequest set RedpackRecordNum=@RedpackRecordNum where SerialID=@SerialID");
            this.database.AddInParameter(sqlStringCommand, "RedpackRecordNum", DbType.Int32, num);
            this.database.AddInParameter(sqlStringCommand, "SerialID", DbType.Int32, serialid);
            if (dbTran != null)
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand, dbTran) > 0);
            }
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateTwoCalculationCommission(ArrayList UserIdList, string ReferralUserId, string OrderId, ArrayList OrderTotalList, ArrayList CommTatalList)
        {
            string query = "";
            query = query + "begin try  " + "  begin tran TranUpdate";
            for (int i = 0; i < UserIdList.Count; i++)
            {
                object obj2 = query;
                obj2 = string.Concat(new object[] { obj2, " INSERT INTO [Hishop_Commissions](UserId,ReferralUserId,OrderId,TradeTime,OrderTotal,CommTotal,CommType,CommRemark,State)values(", UserIdList[i], ",", ReferralUserId, ",'", OrderId, "','", DateTime.Now.ToString(), "',", OrderTotalList[i], ",", CommTatalList[i], ",1,'',1) ;" });
                obj2 = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set ReferralBlance=ReferralBlance+", CommTatalList[i], "  WHERE UserId=", UserIdList[i], "; " });
                query = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+", OrderTotalList[i], ",ReferralOrders=ReferralOrders+1  WHERE UserId=", UserIdList[i], "; " });
            }
            query = query + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateTwoDistributorsOrderNum(ArrayList useridList, ArrayList OrdersTotalList)
        {
            string query = "";
            query = query + "begin try  " + "  begin tran TranUpdate";
            for (int i = 0; i < useridList.Count; i++)
            {
                object obj2 = query;
                query = string.Concat(new object[] { obj2, "  UPDATE aspnet_Distributors set  OrdersTotal=OrdersTotal+", useridList[i], ",ReferralOrders=ReferralOrders+1  WHERE UserId=", OrdersTotalList[i], "; " });
            }
            query = query + " COMMIT TRAN TranUpdate" + "  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool IsSetCalculationCommission(string orderId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT Commid FROM Hishop_Commissions WHERE OrderId = @OrderId AND [State] = 1");
            this.database.AddInParameter(sqlStringCommand, "OrderId", DbType.String, orderId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }


        public int GetDistributorCnt()
        {
            int num = 0;
            string query = "SELECT COUNT(*) AS Cnt FROM aspnet_Distributors";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    num = (int)reader[0];
                    reader.Close();
                }
            }
            return num;
        }

        public bool UpdateIsBindMobile(int userId, int isBindMobile)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Distributors SET IsBindMobile=@IsBindMobile WHERE UserId=@UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            this.database.AddInParameter(sqlStringCommand, "IsBindMobile", DbType.Int32, isBindMobile);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public int CheckIsDistributoBuyProduct(string orderId)
        {
            int num = 0;
            string query = "SELECT COUNT(p.ProductId) AS Cnt FROM Hishop_Products AS p WHERE p.ProductId IN ( SELECT o.ProductId FROM Hishop_OrderItems AS o WHERE OrderId = '" + orderId + "' ) AND p.IsDistributorBuy = 1";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    num = (int)reader[0];
                    reader.Close();
                }
            }
            return num;
        }

        public int CheckCartIsDistributoBuyProduct(string productId)
        {
            int num = 0;
            string query = "SELECT COUNT(p.ProductId) AS Cnt FROM Hishop_Products AS p WHERE p.ProductId IN ( " + productId + " ) AND p.IsDistributorBuy = 1";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    num = (int)reader[0];
                    reader.Close();
                }
            }
            return num;
        }

        public int CheckProductIsCrossByProductId(string productId)
        {
            int num = 0;
            string query = "SELECT COUNT(p.ProductId) AS Cnt FROM Hishop_Products AS p WHERE p.ProductId IN ( " + productId + " ) AND p.IsCross = 1";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    num = (int)reader[0];
                    reader.Close();
                }
            }
            return num;
        }

        public int CheckIsFirstOrder(string referralUserId)
        {
            int num = 0;
            string query = "SELECT COUNT(o.OrderId) AS Cnt FROM Hishop_Orders AS o WHERE o.OrderType = 1 AND o.OrderStatus IN ( 2, 3, 5 ) AND o.ReferralUserId = " + referralUserId;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    num = (int)reader[0];
                    reader.Close();
                }
            }
            return num;
        }

        public bool UpdateCommissionOrderStatus(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return true;
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(sql);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);

        }


        public DataTable GetCommosionByOrderId(string orderid)
        {
            string query = string.Format("SELECT * FROM Hishop_Commissions WHERE CommOrderStatus = 0 AND OrderId = '{0}' ", orderid);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            DataSet set = this.database.ExecuteDataSet(sqlStringCommand);
            if ((set == null) || (set.Tables.Count <= 0))
            {
                return null;
            }
            return set.Tables[0];
        }

        public bool UpdateVisitCounts(int userId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("UPDATE aspnet_Distributors SET VistiCounts = ISNULL(VistiCounts, 0) + 1 WHERE UserId = {0}", userId));
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateGoodCounts(int userId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("UPDATE aspnet_Distributors SET GoodCounts = ISNULL(GoodCounts, 0) + 1 WHERE UserId = {0}", userId));
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateProductGoodCounts(int productId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("UPDATE Hishop_Products SET GoodCounts = ISNULL(GoodCounts, 0) + 1 WHERE ProductId = {0}", productId));
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }


        public decimal GetUserBalanceDrawRequesByCond(int userid, int checkstatus, DateTime fromdatetime, bool isByMonth = false)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1 = 1 ");
            if (userid > 0)
            {
                builder.Append(" and UserID=" + userid);
            }
            if (checkstatus > -1)
            {
                builder.Append(" and IsCheck = " + checkstatus);
            }
            if (isByMonth)
            {
                builder.Append(" and CONVERT(VARCHAR(7), RequestTime, 126) = '" + fromdatetime.ToString("yyyy-MM") + "'");
            }
            else
            {
                builder.Append(" and RequestTime>='" + fromdatetime.ToString("yyyy-MM-dd") + " 00:00:00'");
            }
            string query = " select isnull(sum(Amount),0) from Hishop_BalanceDrawRequest where " + builder.ToString();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return decimal.Parse(this.database.ExecuteScalar(sqlStringCommand).ToString());
        }


        public string GetOrderIdByUserIdAndProductId(int userId, string productId)
        {
            string orderId = "";
            //string query = "SELECT TOP 1 o.OrderId FROM Hishop_Orders AS o LEFT OUTER JOIN Hishop_OrderItems AS oi ON o.OrderId = oi.OrderId WHERE o.OrderStatus in ( 2, 3, 5) and o.UserId = " 
            //    + userId + " AND oi.ProductId = '" + productId + "' ORDER BY o.orderdate DESC ";
            string query = "SELECT TOP 1 o.OrderId FROM Hishop_Orders AS o LEFT OUTER JOIN Hishop_OrderItems AS oi ON o.OrderId = oi.OrderId WHERE o.OrderStatus in ( 2, 3, 5) and o.UserId = "
                + userId + " AND oi.ProductId IN ( SELECT ProductId FROM Hishop_Products WHERE IsDistributorBuy = 1 ) ORDER BY o.orderdate DESC ";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    orderId = (string)reader[0];
                    reader.Close();
                }
            }
            return orderId;
        }


        public DataTable GetCommissionByExport(string storeName, string oneUserName, string oneStoreName, string orderId, string requestStartTime, string requestEndTime)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM vw_Hishop_CommissionDistributors ");
            sql.AppendLine("WHERE State = 1 ");
            if (!string.IsNullOrEmpty(storeName.Trim()))
            {
                sql.AppendLine("      AND StoreName LIKE '%" + storeName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(oneUserName.Trim()))
            {
                sql.AppendLine("      AND OneUserName LIKE '%" + storeName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(oneStoreName.Trim()))
            {
                sql.AppendLine("      AND OneStoreName LIKE '%" + storeName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(orderId.Trim()))
            {
                sql.AppendLine("      AND OrderId = '" + orderId.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(requestStartTime.Trim()))
            {
                sql.AppendLine("      AND datediff(dd,'" + requestStartTime + "',TradeTime)>=0");
            }
            if (!string.IsNullOrEmpty(requestEndTime.Trim()))
            {
                sql.AppendLine("      AND datediff(dd,'" + requestEndTime + "',TradeTime)<=0");
            }
            DbCommand sqlCommand = this.database.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public bool DeleteBalanceDrawRequest(int serialId)
        {

            string query = "DELETE FROM Hishop_BalanceDrawRequest WHERE SerialId=@SerialId;";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "SerialId", DbType.Int32, serialId);
            return this.database.ExecuteNonQuery(sqlStringCommand) > 0;
        }


        public DbQueryResult GetDistributorIncome(DistributorIncomeQuery query)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(query.StoreName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" StoreName LIKE '%{0}%'", DataHelper.CleanSearchString(query.StoreName));
            }
            if (!string.IsNullOrEmpty(query.CellPhone))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" CellPhone LIKE '%{0}%'", DataHelper.CleanSearchString(query.CellPhone));
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_DistributorIncome ", "UserId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public DataTable GetDistributorIncomeByExport(DistributorIncomeQuery query)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM vw_Hishop_DistributorIncome ");
            sql.AppendLine("WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(query.StoreName))
            {
                sql.AppendLine("      AND StoreName LIKE '%" + query.StoreName + "%' ");
            }
            if (!string.IsNullOrEmpty(query.CellPhone))
            {
                sql.AppendLine("      AND CellPhone LIKE '%" + query.CellPhone + "%' ");
            }
            sql.AppendLine(" ORDER BY UserId ");
            DbCommand sqlCommand = this.database.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public bool UpdateDistributorReferralStatusById(int referralStatus, int distributorId)
        {
            //DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Distributors SET ReferralStatus = @ReferralStatus WHERE UserId = @UserId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Distributors SET ReferralStatus = CASE WHEN ReferralStatus = 2 THEN 1 ELSE 2 END WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributorId);
            //this.database.AddInParameter(sqlStringCommand, "ReferralStatus", DbType.Int32, referralStatus);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateDistributorDeadlineTimeById(int addYear, int distributorId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Distributors SET DeadlineTime = CASE WHEN DeadlineTime IS NULL THEN DateAdd(YEAR, " + addYear + ", CreateTime) ELSE DateAdd(YEAR, " + addYear + ", DeadlineTime) END WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributorId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateDistributorIsTempStoreAndDeadlineTimeById(int distributorId, int isTempStore, DateTime decasualizationTime, DateTime deadlineTime)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Distributors SET IsTempStore = @IsTempStore, DecasualizationTime = @DecasualizationTime, DeadlineTime = @DeadlineTime WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, distributorId);
            this.database.AddInParameter(sqlStringCommand, "IsTempStore", DbType.Int32, isTempStore);
            this.database.AddInParameter(sqlStringCommand, "DecasualizationTime", DbType.DateTime, decasualizationTime);
            this.database.AddInParameter(sqlStringCommand, "DeadlineTime", DbType.DateTime, deadlineTime);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

    }
}

