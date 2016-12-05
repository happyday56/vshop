namespace Hidistro.SqlDal.VShop
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class LotteryActivityDao
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

        public bool DeletePrizeRecord(int RecordId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Vshop_PrizeRecord ");
            builder.Append(" where RecordId=@RecordId ");
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(builder.ToString());
            database.AddInParameter(sqlStringCommand, "RecordId", DbType.Int32, RecordId);
            return (database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool DelteLotteryActivity(int activityid, string type)
        {
            object obj2 = Enum.Parse(typeof(ReplyType), type);
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Vshop_LotteryActivity ");
            builder.Append(" where ActivityId=@ActivityId;DELETE FROM vshop_Reply WHERE ActivityId = @ActivityId AND [ReplyType] = @ReplyType");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, activityid);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, (int) obj2);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool DelteLotteryTicket(int activityId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Vshop_LotteryActivity ");
            builder.Append(" where ActivityId=@ActivityId;DELETE FROM vshop_Reply WHERE ActivityId = @ActivityId AND [ReplyType] = @ReplyType");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, activityId);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x40);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public IList<LotteryActivityInfo> GetLotteryActivityByType(LotteryActivityType type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ActivityId,ActivityName from Vshop_LotteryActivity ");
            builder.Append(" where ActivityType=@ActivityType order by ActivityId desc ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityType", DbType.Int32, (int) type);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<LotteryActivityInfo>(reader);
            }
        }

        public LotteryActivityInfo GetLotteryActivityInfo(int activityid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ActivityId,ActivityName,ActivityType,StartTime,EndTime,ActivityDesc,ActivityPic,ActivityKey,PrizeSetting,MaxNum from Vshop_LotteryActivity ");
            builder.Append(" where ActivityId=@ActivityId ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, activityid);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<LotteryActivityInfo>(reader);
            }
        }

        public DbQueryResult GetLotteryActivityList(LotteryActivityQuery page)
        {
            StringBuilder builder = new StringBuilder();
            if (page.ActivityType != ((LotteryActivityType) 0))
            {
                builder.AppendFormat("ActivityType={0}", (int) page.ActivityType);
            }
            return DataHelper.PagingByRownumber(page.PageIndex, page.PageSize, page.SortBy, page.SortOrder, page.IsCount, "Vshop_LotteryActivity", "ActivityId", builder.ToString(), "*");
        }

        public LotteryTicketInfo GetLotteryTicket(int activityid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ActivityId,ActivityName,ActivityType,StartTime,OpenTime,EndTime,ActivityDesc,ActivityPic,ActivityKey,PrizeSetting,GradeIds,MinValue,InvitationCode,IsOpened from Vshop_LotteryActivity ");
            builder.Append(" where ActivityId=@ActivityId ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, activityid);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<LotteryTicketInfo>(reader);
            }
        }

        public DbQueryResult GetLotteryTicketList(LotteryActivityQuery page)
        {
            StringBuilder builder = new StringBuilder();
            if (page.ActivityType != ((LotteryActivityType) 0))
            {
                builder.AppendFormat("ActivityType={0}", (int) page.ActivityType);
            }
            return DataHelper.PagingByRownumber(page.PageIndex, page.PageSize, page.SortBy, page.SortOrder, page.IsCount, "Vshop_LotteryActivity", "ActivityId", builder.ToString(), "*");
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
            builder.AppendFormat(" and b.IsPrize=1 order by b.PrizeTime desc", new object[0]);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return (ReaderConvert.ReaderToList<PrizeRecordInfo>(reader) as List<PrizeRecordInfo>);
            }
        }

        public int InsertLotteryActivity(LotteryActivityInfo model)
        {
            int num;
            if (model.StartTime > model.EndTime)
            {
                DateTime startTime = model.StartTime;
                model.StartTime = model.EndTime;
                model.EndTime = startTime;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Vshop_LotteryActivity(");
            builder.Append("ActivityName,ActivityType,StartTime,EndTime,ActivityDesc,ActivityPic,ActivityKey,PrizeSetting,MaxNum)");
            builder.Append(" values (");
            builder.Append("@ActivityName,@ActivityType,@StartTime,@EndTime,@ActivityDesc,@ActivityPic,@ActivityKey,@PrizeSetting,@MaxNum)");
            builder.Append(";select @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityName", DbType.String, model.ActivityName);
            this.database.AddInParameter(sqlStringCommand, "ActivityType", DbType.Int32, model.ActivityType);
            this.database.AddInParameter(sqlStringCommand, "StartTime", DbType.DateTime, model.StartTime);
            this.database.AddInParameter(sqlStringCommand, "EndTime", DbType.DateTime, model.EndTime);
            this.database.AddInParameter(sqlStringCommand, "ActivityDesc", DbType.String, model.ActivityDesc);
            this.database.AddInParameter(sqlStringCommand, "ActivityPic", DbType.String, model.ActivityPic);
            this.database.AddInParameter(sqlStringCommand, "ActivityKey", DbType.String, model.ActivityKey);
            this.database.AddInParameter(sqlStringCommand, "PrizeSetting", DbType.String, model.PrizeSetting);
            this.database.AddInParameter(sqlStringCommand, "MaxNum", DbType.Int32, model.MaxNum);
            if (!int.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out num))
            {
                return 0;
            }
            return num;
        }

        public int SaveLotteryTicket(LotteryTicketInfo info)
        {
            int num;
            if (info.StartTime > info.EndTime)
            {
                DateTime startTime = info.StartTime;
                info.StartTime = info.EndTime;
                info.EndTime = startTime;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Vshop_LotteryActivity(");
            builder.Append("ActivityName,ActivityType,StartTime,OpenTime,EndTime,ActivityDesc,ActivityPic,ActivityKey,PrizeSetting,GradeIds,MinValue,InvitationCode,IsOpened)");
            builder.Append(" values (");
            builder.Append("@ActivityName,@ActivityType,@StartTime,@OpenTime,@EndTime,@ActivityDesc,@ActivityPic,@ActivityKey,@PrizeSetting,@GradeIds,@MinValue,@InvitationCode,@IsOpened)");
            builder.Append(";select @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityName", DbType.String, info.ActivityName);
            this.database.AddInParameter(sqlStringCommand, "ActivityType", DbType.Int32, info.ActivityType);
            this.database.AddInParameter(sqlStringCommand, "StartTime", DbType.DateTime, info.StartTime);
            this.database.AddInParameter(sqlStringCommand, "OpenTime", DbType.DateTime, info.OpenTime);
            this.database.AddInParameter(sqlStringCommand, "EndTime", DbType.DateTime, info.EndTime);
            this.database.AddInParameter(sqlStringCommand, "ActivityDesc", DbType.String, info.ActivityDesc);
            this.database.AddInParameter(sqlStringCommand, "ActivityPic", DbType.String, info.ActivityPic);
            this.database.AddInParameter(sqlStringCommand, "ActivityKey", DbType.String, info.ActivityKey);
            this.database.AddInParameter(sqlStringCommand, "PrizeSetting", DbType.String, info.PrizeSetting);
            this.database.AddInParameter(sqlStringCommand, "GradeIds", DbType.String, info.GradeIds);
            this.database.AddInParameter(sqlStringCommand, "MinValue", DbType.Int32, info.MinValue);
            this.database.AddInParameter(sqlStringCommand, "InvitationCode", DbType.String, info.InvitationCode);
            this.database.AddInParameter(sqlStringCommand, "IsOpened", DbType.Boolean, info.IsOpened);
            if (!int.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out num))
            {
                return 0;
            }
            return num;
        }

        public bool UpdateLotteryActivity(LotteryActivityInfo model)
        {
            if (model.StartTime > model.EndTime)
            {
                DateTime startTime = model.StartTime;
                model.StartTime = model.EndTime;
                model.EndTime = startTime;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("update Vshop_LotteryActivity set ");
            builder.Append("ActivityName=@ActivityName,");
            builder.Append("ActivityType=@ActivityType,");
            builder.Append("StartTime=@StartTime,");
            builder.Append("EndTime=@EndTime,");
            builder.Append("ActivityDesc=@ActivityDesc,");
            builder.Append("ActivityPic=@ActivityPic,");
            builder.Append("ActivityKey=@ActivityKey,");
            builder.Append("PrizeSetting=@PrizeSetting,");
            builder.Append("MaxNum=@MaxNum ");
            builder.Append(" where ActivityId=@ActivityId ");
            builder.Append(";UPDATE vshop_Reply SET Keys = @ActivityKey WHERE ActivityId = @ActivityId  AND [ReplyType] = @ReplyType");
            string str = ((LotteryActivityType) model.ActivityType).ToString();
            object obj2 = Enum.Parse(typeof(ReplyType), str);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, model.ActivityId);
            this.database.AddInParameter(sqlStringCommand, "ActivityName", DbType.String, model.ActivityName);
            this.database.AddInParameter(sqlStringCommand, "ActivityType", DbType.Int32, model.ActivityType);
            this.database.AddInParameter(sqlStringCommand, "StartTime", DbType.DateTime, model.StartTime);
            this.database.AddInParameter(sqlStringCommand, "EndTime", DbType.DateTime, model.EndTime);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, (int) obj2);
            this.database.AddInParameter(sqlStringCommand, "ActivityDesc", DbType.String, model.ActivityDesc);
            this.database.AddInParameter(sqlStringCommand, "ActivityPic", DbType.String, model.ActivityPic);
            this.database.AddInParameter(sqlStringCommand, "ActivityKey", DbType.String, model.ActivityKey);
            this.database.AddInParameter(sqlStringCommand, "PrizeSetting", DbType.String, model.PrizeSetting);
            this.database.AddInParameter(sqlStringCommand, "MaxNum", DbType.Int32, model.MaxNum);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateLotteryTicket(LotteryTicketInfo info)
        {
            if (info.StartTime > info.EndTime)
            {
                DateTime startTime = info.StartTime;
                info.StartTime = info.EndTime;
                info.EndTime = startTime;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("update Vshop_LotteryActivity set ");
            builder.Append("ActivityName=@ActivityName,");
            builder.Append("ActivityType=@ActivityType,");
            builder.Append("StartTime=@StartTime,");
            builder.Append("OpenTime=@OpenTime,");
            builder.Append("EndTime=@EndTime,");
            builder.Append("ActivityDesc=@ActivityDesc,");
            builder.Append("ActivityPic=@ActivityPic,");
            builder.Append("ActivityKey=@ActivityKey,");
            builder.Append("PrizeSetting=@PrizeSetting,");
            builder.Append("GradeIds=@GradeIds,");
            builder.Append("MinValue=@MinValue,");
            builder.Append("InvitationCode=@InvitationCode,");
            builder.Append("IsOpened=@IsOpened");
            builder.Append(" where ActivityId=@ActivityId ");
            builder.Append(";UPDATE vshop_Reply SET Keys = @ActivityKey WHERE ActivityId = @ActivityId  AND [ReplyType] = @ReplyType");
            string str = ((LotteryActivityType) info.ActivityType).ToString();
            object obj2 = Enum.Parse(typeof(ReplyType), str);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, info.ActivityId);
            this.database.AddInParameter(sqlStringCommand, "ActivityName", DbType.String, info.ActivityName);
            this.database.AddInParameter(sqlStringCommand, "ActivityType", DbType.Int32, info.ActivityType);
            this.database.AddInParameter(sqlStringCommand, "StartTime", DbType.DateTime, info.StartTime);
            this.database.AddInParameter(sqlStringCommand, "OpenTime", DbType.DateTime, info.OpenTime);
            this.database.AddInParameter(sqlStringCommand, "EndTime", DbType.DateTime, info.EndTime);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, (int) obj2);
            this.database.AddInParameter(sqlStringCommand, "ActivityDesc", DbType.String, info.ActivityDesc);
            this.database.AddInParameter(sqlStringCommand, "ActivityPic", DbType.String, info.ActivityPic);
            this.database.AddInParameter(sqlStringCommand, "ActivityKey", DbType.String, info.ActivityKey);
            this.database.AddInParameter(sqlStringCommand, "PrizeSetting", DbType.String, info.PrizeSetting);
            this.database.AddInParameter(sqlStringCommand, "GradeIds", DbType.String, info.GradeIds);
            this.database.AddInParameter(sqlStringCommand, "MinValue", DbType.Int32, info.MinValue);
            this.database.AddInParameter(sqlStringCommand, "InvitationCode", DbType.String, info.InvitationCode);
            this.database.AddInParameter(sqlStringCommand, "IsOpened", DbType.Boolean, info.IsOpened);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

