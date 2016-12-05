namespace Hidistro.SqlDal.VShop
{
    using Hidistro.Entities;
    using Hidistro.Entities.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ActivityDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool DeleteActivity(int activityId)
        {
            string query = "DELETE FROM vshop_Activity WHERE ActivityId=@ActivityId; DELETE FROM vshop_Reply WHERE ActivityId = @ActivityId AND [ReplyType] = @ReplyType";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, activityId);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x100);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public ActivityInfo GetActivity(int activityId)
        {
            string query = "SELECT * FROM vshop_Activity WHERE ActivityId=@ActivityId";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, activityId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<ActivityInfo>(reader);
            }
        }

        public IList<ActivityInfo> GetAllActivity()
        {
            string query = "SELECT *, (SELECT Count(ActivityId) FROM vshop_ActivitySignUp WHERE ActivityId = a.ActivityId) AS CurrentValue FROM vshop_Activity a ORDER BY ActivityId DESC";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<ActivityInfo>(reader);
            }
        }

        public int SaveActivity(ActivityInfo activity)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO vshop_Activity(").Append("Name,Description,StartDate,EndDate,CloseRemark,Keys").Append(",MaxValue,PicUrl,Item1,Item2,Item3,Item4,Item5)").Append(" VALUES (").Append("@Name,@Description,@StartDate,@EndDate,@CloseRemark,@Keys").Append(",@MaxValue,@PicUrl,@Item1,@Item2,@Item3,@Item4,@Item5)").Append(";select @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Name", DbType.String, activity.Name);
            this.database.AddInParameter(sqlStringCommand, "Description", DbType.String, activity.Description);
            this.database.AddInParameter(sqlStringCommand, "StartDate", DbType.DateTime, activity.StartDate);
            this.database.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, activity.EndDate);
            this.database.AddInParameter(sqlStringCommand, "CloseRemark", DbType.String, activity.CloseRemark);
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, activity.Keys);
            this.database.AddInParameter(sqlStringCommand, "MaxValue", DbType.Int32, activity.MaxValue);
            this.database.AddInParameter(sqlStringCommand, "PicUrl", DbType.String, activity.PicUrl);
            this.database.AddInParameter(sqlStringCommand, "Item1", DbType.String, activity.Item1);
            this.database.AddInParameter(sqlStringCommand, "Item2", DbType.String, activity.Item2);
            this.database.AddInParameter(sqlStringCommand, "Item3", DbType.String, activity.Item3);
            this.database.AddInParameter(sqlStringCommand, "Item4", DbType.String, activity.Item4);
            this.database.AddInParameter(sqlStringCommand, "Item5", DbType.String, activity.Item5);
            int.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out num);
            return num;
        }

        public bool UpdateActivity(ActivityInfo activity)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE vshop_Activity SET ").Append("Name=@Name,").Append("Description=@Description,").Append("StartDate=@StartDate,").Append("EndDate=@EndDate,").Append("CloseRemark=@CloseRemark,").Append("Keys=@Keys,").Append("MaxValue=@MaxValue,").Append("PicUrl=@PicUrl,").Append("Item1=@Item1,").Append("Item2=@Item2,").Append("Item3=@Item3,").Append("Item4=@Item4,").Append("Item5=@Item5").Append(" WHERE ActivityId=@ActivityId").Append(";UPDATE vshop_Reply SET Keys = @Keys WHERE ActivityId = @ActivityId AND [ReplyType] = @ReplyType");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Name", DbType.String, activity.Name);
            this.database.AddInParameter(sqlStringCommand, "Description", DbType.String, activity.Description);
            this.database.AddInParameter(sqlStringCommand, "StartDate", DbType.DateTime, activity.StartDate);
            this.database.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, activity.EndDate);
            this.database.AddInParameter(sqlStringCommand, "CloseRemark", DbType.String, activity.CloseRemark);
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, activity.Keys);
            this.database.AddInParameter(sqlStringCommand, "MaxValue", DbType.Int32, activity.MaxValue);
            this.database.AddInParameter(sqlStringCommand, "PicUrl", DbType.String, activity.PicUrl);
            this.database.AddInParameter(sqlStringCommand, "Item1", DbType.String, activity.Item1);
            this.database.AddInParameter(sqlStringCommand, "Item2", DbType.String, activity.Item2);
            this.database.AddInParameter(sqlStringCommand, "Item3", DbType.String, activity.Item3);
            this.database.AddInParameter(sqlStringCommand, "Item4", DbType.String, activity.Item4);
            this.database.AddInParameter(sqlStringCommand, "Item5", DbType.String, activity.Item5);
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, activity.ActivityId);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x100);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool AttendActivity(int ActivityId, int UserId)
        {
            string query = "INSERT INTO [dbo].[Hishop_ActivitiesUsers] ([AttributeId] ,[UserId]) VALUES (@AttributeId, @UserId)";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "AttributeId", DbType.Int32, ActivityId);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, UserId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public DataTable GetActivityList(int UserId)
        {
            string query = string.Format("SELECT *,(SELECT COUNT(*) FROM Hishop_ActivitiesUsers hau WHERE hau.AttributeId = ha.ActivitiesId AND hau.UserId = {0}) IsFocus FROM [dbo].[Hishop_Activities] ha WHERE ha.StartTime <= GETDATE() AND ha.EndTIme >= GETDATE();", UserId);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }
    }
}

