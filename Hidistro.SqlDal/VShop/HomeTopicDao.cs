namespace Hidistro.SqlDal.VShop
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class HomeTopicDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool AddHomeTopic(int TopicId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO Vshop_HomeTopics(TopicId,DisplaySequence) VALUES (@TopicId,0)");
            this.database.AddInParameter(sqlStringCommand, "TopicId", DbType.Int32, TopicId);
            try
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetHomeTopics()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select a.TopicId,[Title],[IconUrl],t.DisplaySequence,[AddedDate],Keys=(SELECT Keys FROM vshop_Reply WHERE [ReplyType] = @ReplyType AND ActivityId = t.TopicId)  from Vshop_Topics a inner join  Vshop_HomeTopics t on a.TopicId=t.TopicId  where a.IsRelease=1");
            builder.Append(" order by t.DisplaySequence asc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x200);
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public bool RemoveAllHomeTopics()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Vshop_HomeTopics");
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool RemoveHomeTopic(int TopicId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Vshop_HomeTopics WHERE TopicId = @TopicId");
            this.database.AddInParameter(sqlStringCommand, "TopicId", DbType.Int32, TopicId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateHomeTopicSequence(int TopicId, int displaysequence)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update Vshop_HomeTopics  set DisplaySequence=@DisplaySequence where TopicId=@TopicId");
            this.database.AddInParameter(sqlStringCommand, "@DisplaySequence", DbType.Int32, displaysequence);
            this.database.AddInParameter(sqlStringCommand, "@TopicId", DbType.Int32, TopicId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

