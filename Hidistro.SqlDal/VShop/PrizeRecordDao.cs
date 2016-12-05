namespace Hidistro.SqlDal.VShop
{
    using Hidistro.Core;
    using Hidistro.Entities;
    using Hidistro.Entities.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class PrizeRecordDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public int AddPrizeRecord(PrizeRecordInfo model)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Vshop_PrizeRecord(");
            builder.Append("ActivityID,PrizeTime,UserID,PrizeName,Prizelevel,IsPrize)");
            builder.Append(" values (");
            builder.Append("@ActivityID,@PrizeTime,@UserID,@PrizeName,@Prizelevel,@IsPrize)");
            builder.Append(";select @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityID", DbType.Int32, model.ActivityID);
            this.database.AddInParameter(sqlStringCommand, "PrizeTime", DbType.DateTime, model.PrizeTime);
            this.database.AddInParameter(sqlStringCommand, "UserID", DbType.Int32, model.UserID);
            this.database.AddInParameter(sqlStringCommand, "PrizeName", DbType.String, model.PrizeName);
            this.database.AddInParameter(sqlStringCommand, "Prizelevel", DbType.String, model.Prizelevel);
            this.database.AddInParameter(sqlStringCommand, "IsPrize", DbType.Boolean, model.IsPrize);
            if (!int.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out num))
            {
                return 0;
            }
            return num;
        }

        public int GetCountBySignUp(int activityId)
        {
            string query = "select count(*) from Vshop_PrizeRecord where ActivityID=@ActivityID";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "ActivityID", DbType.Int32, activityId);
            return Convert.ToInt32(this.database.ExecuteScalar(sqlStringCommand));
        }

        public List<PrizeRecordInfo> GetPrizeList(PrizeQuery page)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ActivityName=(select  ActivityName from Vshop_LotteryActivity a where a.ActivityId=b.ActivityId),");
            builder.Append("UserName=(select UserName from aspnet_Members c where  c.UserId=b.UserId),");
            builder.Append(" b.* from Vshop_PrizeRecord b");
            if (page.ActivityId != 0)
            {
                builder.AppendFormat(" where b.ActivityId={0}", page.ActivityId);
            }
            builder.AppendFormat(" and b.IsPrize=1", new object[0]);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return (ReaderConvert.ReaderToList<PrizeRecordInfo>(reader) as List<PrizeRecordInfo>);
            }
        }

        public int GetUserPrizeCount(int ActivityId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) from Vshop_PrizeRecord where ActivityId=@ActivityId  and UserID=@UserID");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, ActivityId);
            this.database.AddInParameter(sqlStringCommand, "UserID", DbType.Int32, Globals.GetCurrentMemberUserId());
            object objA = this.database.ExecuteScalar(sqlStringCommand);
            if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
            {
                return 0;
            }
            return int.Parse(objA.ToString());
        }

        public PrizeRecordInfo GetUserPrizeRecord(int activityid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 * from Vshop_PrizeRecord where ActivityId=@ActivityId  and UserID=@UserID order by RecordId desc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, activityid);
            this.database.AddInParameter(sqlStringCommand, "UserID", DbType.Int32, Globals.GetCurrentMemberUserId());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<PrizeRecordInfo>(reader);
            }
        }

        public bool HasSignUp(int activityId, int userId)
        {
            string query = "select count(*) from Vshop_PrizeRecord where ActivityID=@ActivityID and UserID=@UserID";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "ActivityID", DbType.Int32, activityId);
            this.database.AddInParameter(sqlStringCommand, "UserID", DbType.Int32, userId);
            return (Convert.ToInt32(this.database.ExecuteScalar(sqlStringCommand)) > 0);
        }

        public bool OpenTicket(int ticketId, List<PrizeSetting> list)
        {
            if ((list == null) || (list.Count == 0))
            {
                return false;
            }
            string format = "UPDATE Vshop_PrizeRecord SET Prizelevel=@Prizelevel, PrizeName=@PrizeName WHERE RecordId IN(SELECT TOP {0} RecordId FROM Vshop_PrizeRecord WHERE ActivityID=@ActivityID AND PrizeName IS NULL ORDER BY NewID())";
            foreach (PrizeSetting setting in list)
            {
                DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format(format, setting.PrizeNum));
                this.database.AddInParameter(sqlStringCommand, "Prizelevel", DbType.String, setting.PrizeLevel);
                this.database.AddInParameter(sqlStringCommand, "PrizeName", DbType.String, setting.PrizeName);
                this.database.AddInParameter(sqlStringCommand, "ActivityID", DbType.Int32, ticketId);
                this.database.ExecuteNonQuery(sqlStringCommand);
            }
            return true;
        }

        public bool UpdatePrizeRecord(PrizeRecordInfo model)
        {
            string query = "UPDATE Vshop_PrizeRecord SET  RealName=@RealName, CellPhone=@CellPhone WHERE ActivityID=@ActivityID AND UserId=@UserId AND IsPrize = 1 AND CellPhone IS NULL AND RealName IS NULL";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "ActivityID", DbType.Int32, model.ActivityID);
            this.database.AddInParameter(sqlStringCommand, "UserID", DbType.Int32, model.UserID);
            this.database.AddInParameter(sqlStringCommand, "RealName", DbType.String, model.RealName);
            this.database.AddInParameter(sqlStringCommand, "CellPhone", DbType.String, model.CellPhone);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

