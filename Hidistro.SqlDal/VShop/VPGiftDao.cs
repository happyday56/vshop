using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities;
using Hidistro.Entities.VShop;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Hidistro.SqlDal.VShop
{
    public class VPGiftDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public int AddVPGift(VPGiftInfo vpGift)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Hishop_VPGift(").Append("VPGiftType, VPGiftName, VPGiftCategory, StartDate, EndDate )").Append(" VALUES (").Append("@VPGiftType, @VPGiftName, @VPGiftCategory, @StartDate, @EndDate); SELECT @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "VPGiftType", DbType.Int32, vpGift.VPGiftType);
            this.database.AddInParameter(sqlStringCommand, "VPGiftName", DbType.String, vpGift.VPGiftName);
            this.database.AddInParameter(sqlStringCommand, "VPGiftCategory", DbType.Int32, vpGift.VPGiftCategory);
            this.database.AddInParameter(sqlStringCommand, "StartDate", DbType.DateTime, vpGift.StartDate);
            this.database.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, vpGift.EndDate);
            //return this.database.ExecuteNonQuery(sqlStringCommand);
            object obj2 = this.database.ExecuteScalar(sqlStringCommand);
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return 0;
        }

        public bool DeleteVPGift(int vpGiftId)
        {
            string query = "DELETE FROM Hishop_VPGiftDetail WHERE VPGiftId=@VPGiftId; DELETE FROM Hishop_VPGift WHERE VPGiftId=@VPGiftId;";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "VPGiftId", DbType.Int32, vpGiftId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool DeleteVPGiftDetail(int vpGiftDetailId)
        {
            string query = "DELETE FROM Hishop_VPGiftDetail WHERE VPGiftDetailId=@vpGiftDetailId";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "VPGiftDetailId", DbType.Int32, vpGiftDetailId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public IList<VPGiftInfo> GetVPGiftInfo(int vpGiftId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM Hishop_VPGift WHERE VPGiftId={0}", vpGiftId));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<VPGiftInfo>(reader);
            }
        }

        public IList<VPGiftInfo> GetVPGiftInfoByType(int vpGiftType)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM Hishop_VPGift WHERE ( datediff(dd,GETDATE(),StartDate)<=0 and datediff(dd,GETDATE(),EndDate)>=0 ) AND VPGiftType={0}", vpGiftType));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<VPGiftInfo>(reader);
            }
        }

        public VPGiftInfo GetVPGiftInfoDatail(int vpGiftId)
        {
            VPGiftInfo info = new VPGiftInfo();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM Hishop_VPGift WHERE VPGiftId={0};SELECT * FROM Hishop_VPGiftDetail WHERE VPGiftId = {0} ORDER BY MeetMoney DESC", vpGiftId));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                info = ReaderConvert.ReaderToModel<VPGiftInfo>(reader);
                IList<VPGiftDetailInfo> list = new List<VPGiftDetailInfo>();
                VPGiftDetailInfo vpDetail = new VPGiftDetailInfo();
                reader.NextResult();
                while (reader.Read())
                {
                    vpDetail = new VPGiftDetailInfo();
                    vpDetail.VPGiftDetailId = (int)reader["VPGiftDetailId"];
                    vpDetail.VPGiftId = (int)reader["VPGiftId"];
                    vpDetail.MeetMoney = (decimal)reader["MeetMoney"];
                    vpDetail.GiftMoney = (decimal)reader["GiftMoney"];

                    list.Add(vpDetail);
                }
                info.VPGiftItems = list;
            }
            return info;
        }

        public IList<VPGiftDetailInfo> GetVPGiftDetailById(int vpGiftId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_VPGiftDetail WHERE VPGiftId = " + vpGiftId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<VPGiftDetailInfo>(reader);
            }
        }

        public IList<VPGiftInfo> GetAllVPGift()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_VPGift where datediff(dd,GETDATE(),StartDate)<=0 and datediff(dd,GETDATE(),EndDate)>=0 ");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<VPGiftInfo>(reader);
            }
        }

        public DbQueryResult GetVPGiftList(VPGiftQuery query)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(query.VPGiftName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" VPGiftName LIKE '%{0}%'", DataHelper.CleanSearchString(query.VPGiftName));
            }
            if (!string.IsNullOrEmpty(query.State.ToString()))
            {
                if (query.State == "1")
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }
                    builder.AppendFormat(" datediff(dd,'{0}',StartDate)<=0 and datediff(dd,'{0}',EndDate)>=0", DateTime.Now.ToShortDateString());
                }
                else if (query.State == "2")
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }
                    builder.AppendFormat(" datediff(dd,'{0}',StartDate)>0 ", DateTime.Now.ToShortDateString());
                }
                else
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }
                    builder.AppendFormat(" datediff(dd,'{0}',EndDate)<0 ", DateTime.Now.ToShortDateString());
                }
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount
                , "Hishop_VPGift ", "VPGiftId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }


        public DataTable GetType(int Types)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT VPGiftId, VPGiftType, VPGiftName, VPGiftCategory, StartDate, EndDate FROM Hishop_VPGift where datediff(dd,GETDATE(),StartDate)<=0 and datediff(dd,GETDATE(),EndDate)>=0 and VPGiftType=" + Types + " order by VPGiftType asc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public DataTable GetVPGiftType(int vpGiftId, int oldVPGiftId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Hishop_VPGift where datediff(dd,GETDATE(),StartDate)<=0 and datediff(dd,GETDATE(),EndDate)>=0 and VPGiftType = " + vpGiftId + " and VPGiftType <> " + oldVPGiftId);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public DataTable GetVPGiftDetailType(int vpGiftId, int meetMoney)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Hishop_VPGiftDetail where VPGiftId = " + vpGiftId + " and MeetMoney = " + meetMoney);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }
        
        public bool UpdateVPGift(VPGiftInfo vpGift)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Hishop_VPGift SET ").Append("VPGiftType=@VPGiftType,").Append("VPGiftName=@VPGiftName,").Append("VPGiftCategory=@VPGiftCategory,")
                .Append("StartDate=@StartDate,").Append("EndDate=@EndDate").Append(" WHERE VPGiftId=@VPGiftId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "VPGiftType", DbType.Int32, vpGift.VPGiftType);
            this.database.AddInParameter(sqlStringCommand, "VPGiftName", DbType.String, vpGift.VPGiftName);
            this.database.AddInParameter(sqlStringCommand, "VPGiftCategory", DbType.Int32, vpGift.VPGiftCategory);
            this.database.AddInParameter(sqlStringCommand, "StartDate", DbType.DateTime, vpGift.StartDate);
            this.database.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, vpGift.EndDate);
            this.database.AddInParameter(sqlStringCommand, "VPGiftId", DbType.Int32, vpGift.VPGiftId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public int AddVPGiftDetail(VPGiftDetailInfo vpGift)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Hishop_VPGiftDetail(").Append("VPGiftId, MeetMoney, GiftMoney )").Append(" VALUES (").Append("@VPGiftId, @MeetMoney, @GiftMoney); SELECT @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "VPGiftId", DbType.Int32, vpGift.VPGiftId);
            this.database.AddInParameter(sqlStringCommand, "MeetMoney", DbType.Decimal, vpGift.MeetMoney);
            this.database.AddInParameter(sqlStringCommand, "GiftMoney", DbType.Decimal, vpGift.GiftMoney);
            object obj2 = this.database.ExecuteScalar(sqlStringCommand);
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return 0;
        }


        public bool UpdateParamConfig(string configKey, string configValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE T_ParamConfig SET ").Append("ConfigValue = @ConfigValue").Append(" WHERE ConfigKey = @ConfigKey");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ConfigValue", DbType.String, configValue);
            this.database.AddInParameter(sqlStringCommand, "ConfigKey", DbType.String, configKey);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }


    }
}
