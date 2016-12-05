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

    public class ActivitiesDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public int AddActivities(ActivitiesInfo activity)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Hishop_Activities(").Append("ActivitiesName,ActivitiesType,MeetMoney,ReductionMoney,StartTime,EndTIme,ActivitiesDescription,Type,CoverImg, IsDisplayHome)").Append(" VALUES (").Append("@ActivitiesName,@ActivitiesType,@MeetMoney,@ReductionMoney,@StartTime,@EndTime,@ActivitiesDescription,@Type,@CoverImg, @IsDisplayHome); SELECT @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivitiesName", DbType.String, activity.ActivitiesName);
            this.database.AddInParameter(sqlStringCommand, "ActivitiesType", DbType.Int32, activity.ActivitiesType);
            this.database.AddInParameter(sqlStringCommand, "MeetMoney", DbType.Decimal, activity.MeetMoney);
            this.database.AddInParameter(sqlStringCommand, "ReductionMoney", DbType.Decimal, activity.ReductionMoney);
            this.database.AddInParameter(sqlStringCommand, "StartTime", DbType.DateTime, activity.StartTime);
            this.database.AddInParameter(sqlStringCommand, "EndTIme", DbType.DateTime, activity.EndTIme);
            this.database.AddInParameter(sqlStringCommand, "ActivitiesDescription", DbType.String, activity.ActivitiesDescription);
            this.database.AddInParameter(sqlStringCommand, "Type", DbType.Int32, activity.Type);
            this.database.AddInParameter(sqlStringCommand, "CoverImg", DbType.String, activity.CoverImg);
            this.database.AddInParameter(sqlStringCommand, "IsDisplayHome", DbType.Int32, activity.IsDisplayHome);
            //return this.database.ExecuteNonQuery(sqlStringCommand);
            object obj2 = this.database.ExecuteScalar(sqlStringCommand);
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return 0;
        }

        public bool DeleteActivities(int ActivitiesId)
        {
            string query = "DELETE FROM Hishop_ActivityCategories WHERE ActivitiesId=@ActivitiesId; DELETE FROM Hishop_Activities WHERE ActivitiesId=@ActivitiesId;";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "ActivitiesId", DbType.Int32, ActivitiesId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public IList<ActivitiesInfo> GetActivitiesInfo(string ActivitiesId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM Hishop_Activities WHERE ActivitiesId={0}", ActivitiesId));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<ActivitiesInfo>(reader);
            }
        }

        public IList<ActivitiesInfo> GetActivitiesInfoByType(string actType)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM Hishop_Activities WHERE ( datediff(dd,GETDATE(),StartTime)<=0 and datediff(dd,GETDATE(),EndTIme)>=0 ) AND ActivitiesType={0}", actType));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<ActivitiesInfo>(reader);
            }
        }

        public ActivitiesInfo GetActivitiesInfoDatail(string ActivitiesId)
        {
            ActivitiesInfo info = new ActivitiesInfo();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM Hishop_Activities WHERE ActivitiesId={0};SELECT * FROM Hishop_ActivityCategories WHERE ActivitiesId = {0}", ActivitiesId));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                //return ReaderConvert.ReaderToList<ActivitiesInfo>(reader);
                info = ReaderConvert.ReaderToModel<ActivitiesInfo>(reader);
                IList<int> list = new List<int>();
                reader.NextResult();
                while (reader.Read())
                {
                    list.Add((int)reader["CategoryId"]);
                }
                info.ProductCategories = list;
            }
            return info;
        }

        public IList<ActivitiesInfo> GetAllActivities()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_Activities where datediff(dd,GETDATE(),StartTime)<=0 and datediff(dd,GETDATE(),EndTIme)>=0 AND IsDisplayHome = 1 ");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<ActivitiesInfo>(reader);
            }
        }

        public DbQueryResult GetActivitiesList(ActivitiesQuery query)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(query.ActivitiesName))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat(" ActivitiesName LIKE '%{0}%'", DataHelper.CleanSearchString(query.ActivitiesName));
            }
            if (!string.IsNullOrEmpty(query.State.ToString()))
            {
                if (query.State == "1")
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }
                    builder.AppendFormat(" datediff(dd,'{0}',StartTime)<=0 and datediff(dd,'{0}',EndTIme)>=0", DateTime.Now.ToShortDateString());
                }
                else if (query.State == "2")
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }
                    builder.AppendFormat(" datediff(dd,'{0}',StartTime)>0 ", DateTime.Now.ToShortDateString());
                }
                else
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }
                    builder.AppendFormat(" datediff(dd,'{0}',EndTIme)<0 ", DateTime.Now.ToShortDateString());
                }
            }
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "Hishop_Activities ", "ActivitiesId", (builder.Length > 0) ? builder.ToString() : null, "*,isnull((SELECT count(*) FROM  Hishop_ActivitiesUsers WHERE AttributeId = Hishop_Activities.ActivitiesId),0) as MerchantsCount, (SELECT Name FROM Hishop_Categories WHERE CategoryId = Hishop_Activities.ActivitiesType) AS CategoriesName");
        }

        public DbQueryResult GetActivitiesShopsList(ActivitiesQuery query)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(query.ActivitiesId))
                builder.AppendFormat(" ActivitiesId = {0}", query.ActivitiesId);

            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "vw_Hishop_ActivitiesUsers ", "UserId", (builder.Length > 0) ? builder.ToString() : null, "*");
        }

        public DataTable GetType(int Types)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ReductionMoney,ActivitiesId,ActivitiesName,MeetMoney,ActivitiesType from Hishop_Activities where datediff(dd,GETDATE(),StartTime)<=0 and datediff(dd,GETDATE(),EndTIme)>=0 and Type=" + Types + " order by MeetMoney asc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public DataTable GetActivitiesType(int activitiesType, int oldActTypeId, decimal meetMoney, decimal reductionMoney)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ReductionMoney,ActivitiesId,ActivitiesName,MeetMoney,ActivitiesType from Hishop_Activities where datediff(dd,GETDATE(),StartTime)<=0 and datediff(dd,GETDATE(),EndTIme)>=0 and MeetMoney = " + meetMoney + " and ReductionMoney = " + reductionMoney + " and ActivitiesType = " + activitiesType + " and ActivitiesType <> " + oldActTypeId + " order by MeetMoney asc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public bool UpdateActivities(ActivitiesInfo activity)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Hishop_Activities SET ").Append("ActivitiesName=@ActivitiesName,").Append("ActivitiesType=@ActivitiesType,").Append("MeetMoney=@MeetMoney,").Append("ReductionMoney=@ReductionMoney,").Append("StartTime=@StartTime,").Append("EndTIme=@EndTIme,").Append("ActivitiesDescription=@ActivitiesDescription,").Append("CoverImg=@CoverImg").Append(", IsDisplayHome = @IsDisplayHome").Append(" WHERE ActivitiesId=@ActivitiesId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ActivitiesName", DbType.String, activity.ActivitiesName);
            this.database.AddInParameter(sqlStringCommand, "ActivitiesType", DbType.Int32, activity.ActivitiesType);
            this.database.AddInParameter(sqlStringCommand, "MeetMoney", DbType.Decimal, activity.MeetMoney);
            this.database.AddInParameter(sqlStringCommand, "ReductionMoney", DbType.Decimal, activity.ReductionMoney);
            this.database.AddInParameter(sqlStringCommand, "StartTime", DbType.DateTime, activity.StartTime);
            this.database.AddInParameter(sqlStringCommand, "EndTIme", DbType.DateTime, activity.EndTIme);
            this.database.AddInParameter(sqlStringCommand, "ActivitiesDescription", DbType.String, activity.ActivitiesDescription);
            this.database.AddInParameter(sqlStringCommand, "ActivitiesId", DbType.Int32, activity.ActivitiesId);
            this.database.AddInParameter(sqlStringCommand, "CoverImg", DbType.String, activity.CoverImg);
            this.database.AddInParameter(sqlStringCommand, "IsDisplayHome", DbType.Int32, activity.IsDisplayHome);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateActivitiesTakeEffect(string activity)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Hishop_Activities SET ").Append("TakeEffect=TakeEffect+1").Append(" WHERE ActivitiesId IN (" + activity + ")");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public void AddActivityCagegories(int activityId, IList<int> activityCagegories)
        {
            int ac = 0;
            DbCommand sqlCommand = this.database.GetSqlStringCommand("INSERT INTO Hishop_ActivityCategories(ActivitiesId,CategoryId) VALUES(@ActivitiesId,@CategoryId)");
            this.database.AddInParameter(sqlCommand, "ActivitiesId", DbType.Int32, activityId);
            this.database.AddInParameter(sqlCommand, "CategoryId", DbType.Int32);
            foreach (int num in activityCagegories)
            {
                this.database.SetParameterValue(sqlCommand, "CategoryId", num);
                ac = this.database.ExecuteNonQuery(sqlCommand);
            }
        }
        public bool DeleteActivityCagegories(int activityId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_ActivityCategories WHERE ActivitiesId=@ActivitiesId");
            this.database.AddInParameter(sqlStringCommand, "ActivitiesId", DbType.Int32, activityId);
            try
            {
                this.database.ExecuteNonQuery(sqlStringCommand);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public DataTable GetActivitiesTypeNoCategoryId(int activitiesType, int categoryId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT DISTINCT ac.CategoryId ");
            builder.AppendLine("FROM Hishop_Activities AS a LEFT OUTER JOIN Hishop_ActivityCategories AS ac ON a.ActivitiesId = ac.ActivitiesId AND ac.CategoryId = " + categoryId + " ");
            builder.AppendLine("WHERE ( DATEDIFF(dd,GETDATE(),a.StartTime) <= 0 AND DATEDIFF(dd,GETDATE(),a.EndTIme) >= 0 ) AND a.ActivitiesType = " + activitiesType);
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }
    }
}

