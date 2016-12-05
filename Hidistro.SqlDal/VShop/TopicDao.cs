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

    public class TopicDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool AddReleatesProdcutBytopicid(int topicid, int prodcutId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO Vshop_RelatedTopicProducts(Topicid, RelatedProductId,DisplaySequence) VALUES (@Topicid, @RelatedProductId,0)");
            this.database.AddInParameter(sqlStringCommand, "Topicid", DbType.Int32, topicid);
            this.database.AddInParameter(sqlStringCommand, "RelatedProductId", DbType.Int32, prodcutId);
            try
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
            }
            catch
            {
                return false;
            }
        }

        public int AddTopic(TopicInfo topicinfo)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Vshop_Topics(Title,IconUrl,Content,AddedDate,IsRelease,DisplaySequence)");
            builder.Append(" values (@Title,@IconUrl,@Content,@AddedDate,@IsRelease,@DisplaySequence);");
            builder.Append(" select @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Title", DbType.String, topicinfo.Title);
            this.database.AddInParameter(sqlStringCommand, "IconUrl", DbType.String, topicinfo.IconUrl);
            this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, topicinfo.Content);
            this.database.AddInParameter(sqlStringCommand, "AddedDate", DbType.DateTime, topicinfo.AddedDate);
            this.database.AddInParameter(sqlStringCommand, "IsRelease", DbType.Boolean, topicinfo.IsRelease);
            this.database.AddInParameter(sqlStringCommand, "DisplaySequence", DbType.Int32, topicinfo.DisplaySequence);
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, topicinfo.Keys);
            int.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out num);
            return num;
        }

        public bool DeleteTopic(int TopicId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Vshop_Topics where TopicId=@TopicId ");
            builder.Append(" DELETE FROM vshop_Reply WHERE ActivityId = @TopicId  AND [ReplyType] = @ReplyType");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "TopicId", DbType.Int32, TopicId);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x200);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public int DeleteTopics(IList<int> Topics)
        {
            int num = 0;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Vshop_Topics WHERE TopicId=@TopicId");
            this.database.AddInParameter(sqlStringCommand, "TopicId", DbType.Int32);
            foreach (int num2 in Topics)
            {
                this.database.SetParameterValue(sqlStringCommand, "TopicId", num2);
                this.database.ExecuteNonQuery(sqlStringCommand);
                num++;
            }
            return num;
        }

        public DataTable GetRelatedTopicProducts(int topicid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ProductId, ProductCode, ProductName, ThumbnailUrl40, MarketPrice, SalePrice, Stock,t.DisplaySequence from vw_Hishop_BrowseProductList p inner join  Vshop_RelatedTopicProducts t on p.productid=t.RelatedProductId where t.Topicid=@Topicid");
            builder.AppendFormat(" and SaleStatus = {0}", 1);
            builder.Append(" order by t.DisplaySequence asc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Topicid", DbType.Int32, topicid);
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public TopicInfo GetTopic(int TopicId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *, (SELECT Keys FROM vshop_Reply WHERE [ReplyType] = @ReplyType AND ActivityId = t.TopicId) AS Keys from Vshop_Topics t where TopicId=@TopicId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "TopicId", DbType.Int32, TopicId);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x200);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<TopicInfo>(reader);
            }
        }

        public DbQueryResult GetTopicList(TopicQuery page)
        {
            StringBuilder builder = new StringBuilder();
            if (!(!page.IsRelease.HasValue ? true : !page.IsRelease.Value))
            {
                builder.Append("IsRelease = 1");
            }
            else if (!(!page.IsRelease.HasValue ? true : page.IsRelease.Value))
            {
                builder.Append("IsRelease = 0");
            }
            if (page.IsincludeHomeProduct.HasValue && !page.IsincludeHomeProduct.Value)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" topicid  not in (select topicid from Vshop_HomeTopics) ");
            }
            int num = 0x200;
            return DataHelper.PagingByRownumber(page.PageIndex, page.PageSize, page.SortBy, page.SortOrder, page.IsCount, "Vshop_Topics t", "TopicId", builder.ToString(), "*, (SELECT Keys FROM vshop_Reply WHERE [ReplyType] =" + num + " AND Activityid = t.TopicId) AS Keys");
        }

        public IList<TopicInfo> GetTopics()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("select * from Vshop_Topics order by DisplaySequence asc,topicid desc");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<TopicInfo>(reader);
            }
        }

        public bool RemoveReleatesProductBytopicid(int topicid)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Vshop_RelatedTopicProducts WHERE Topicid = @Topicid");
            this.database.AddInParameter(sqlStringCommand, "Topicid", DbType.Int32, topicid);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool RemoveReleatesProductBytopicid(int topicid, int productId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Vshop_RelatedTopicProducts WHERE Topicid = @Topicid AND RelatedProductId = @RelatedProductId");
            this.database.AddInParameter(sqlStringCommand, "Topicid", DbType.Int32, topicid);
            this.database.AddInParameter(sqlStringCommand, "RelatedProductId", DbType.Int32, productId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool SwapTopicSequence(int TopicId, int displaysequence)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update Vshop_Topics  set DisplaySequence=@DisplaySequence where TopicId=@TopicId");
            this.database.AddInParameter(sqlStringCommand, "@DisplaySequence", DbType.Int32, displaysequence);
            this.database.AddInParameter(sqlStringCommand, "@TopicId", DbType.Int32, TopicId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateRelateProductSequence(int TopicId, int RelatedProductId, int displaysequence)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update Vshop_RelatedTopicProducts  set DisplaySequence=@DisplaySequence where TopicId=@TopicId and RelatedProductId=@RelatedProductId");
            this.database.AddInParameter(sqlStringCommand, "@DisplaySequence", DbType.Int32, displaysequence);
            this.database.AddInParameter(sqlStringCommand, "@TopicId", DbType.Int32, TopicId);
            this.database.AddInParameter(sqlStringCommand, "@RelatedProductId", DbType.Int32, RelatedProductId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateTopic(TopicInfo topic)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Vshop_Topics set ");
            builder.Append("Title=@Title,");
            builder.Append("IconUrl=@IconUrl,");
            builder.Append("Content=@Content,");
            builder.Append("AddedDate=@AddedDate,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("IsRelease=@IsRelease");
            builder.Append(" where TopicId=@TopicId ");
            builder.Append(" UPDATE vshop_Reply SET Keys = @Keys WHERE  ActivityId = @TopicId  AND [ReplyType] = @ReplyType");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "TopicId", DbType.Int32, topic.TopicId);
            this.database.AddInParameter(sqlStringCommand, "Title", DbType.String, topic.Title);
            this.database.AddInParameter(sqlStringCommand, "IconUrl", DbType.String, topic.IconUrl);
            this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, topic.Content);
            this.database.AddInParameter(sqlStringCommand, "AddedDate", DbType.DateTime, topic.AddedDate);
            this.database.AddInParameter(sqlStringCommand, "DisplaySequence", DbType.Int32, topic.DisplaySequence);
            this.database.AddInParameter(sqlStringCommand, "IsRelease", DbType.Boolean, topic.IsRelease);
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, topic.Keys);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x200);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

