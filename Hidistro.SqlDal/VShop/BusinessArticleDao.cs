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
    public class BusinessArticleDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public int AddBusinessArticle(BusinessArticleInfo baInfo)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Vshop_BusinessArticle(Title, IconUrl, Summary, ArtContent,AddedDate,ReviewCnt,PublishName)");
            builder.Append(" values (@Title,@IconUrl,@Summary,@ArtContent,@AddedDate,@ReviewCnt,@PublishName);");
            builder.Append(" select @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Title", DbType.String, baInfo.Title);
            this.database.AddInParameter(sqlStringCommand, "IconUrl", DbType.String, baInfo.IconUrl);
            this.database.AddInParameter(sqlStringCommand, "Summary", DbType.String, baInfo.Summary);
            this.database.AddInParameter(sqlStringCommand, "ArtContent", DbType.String, baInfo.ArtContent);
            this.database.AddInParameter(sqlStringCommand, "AddedDate", DbType.DateTime, baInfo.AddedDate);
            this.database.AddInParameter(sqlStringCommand, "ReviewCnt", DbType.Int32, baInfo.ReviewCnt);
            this.database.AddInParameter(sqlStringCommand, "PublishName", DbType.String, baInfo.PublishName);
            
            int.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out num);
            return num;
        }

        public bool UpdateBusinessArticle(BusinessArticleInfo baInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Vshop_BusinessArticle set ");
            builder.Append("Title=@Title,");
            builder.Append("IconUrl=@IconUrl,");
            builder.Append("Summary=@Summary,");
            builder.Append("ArtContent=@ArtContent,");
            builder.Append("AddedDate=@AddedDate,");
            builder.Append("PublishName=@PublishName");
            builder.Append(" where ArticleId=@ArticleId ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ArticleId", DbType.Int32, baInfo.ArticleId);
            this.database.AddInParameter(sqlStringCommand, "Title", DbType.String, baInfo.Title);
            this.database.AddInParameter(sqlStringCommand, "IconUrl", DbType.String, baInfo.IconUrl);
            this.database.AddInParameter(sqlStringCommand, "Summary", DbType.String, baInfo.Summary);
            this.database.AddInParameter(sqlStringCommand, "ArtContent", DbType.String, baInfo.ArtContent);
            this.database.AddInParameter(sqlStringCommand, "AddedDate", DbType.DateTime, baInfo.AddedDate);
            this.database.AddInParameter(sqlStringCommand, "PublishName", DbType.String, baInfo.PublishName);
            
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool DeleteBusinessArticle(int ArticleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Vshop_BusinessArticle where ArticleId=@ArticleId ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ArticleId", DbType.Int32, ArticleId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public int DeleteBusinessArticle(IList<int> BusinessArticles)
        {
            int num = 0;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Vshop_BusinessArticle WHERE ArticleId=@ArticleId");
            this.database.AddInParameter(sqlStringCommand, "TopicId", DbType.Int32);
            foreach (int num2 in BusinessArticles)
            {
                this.database.SetParameterValue(sqlStringCommand, "ArticleId", num2);
                this.database.ExecuteNonQuery(sqlStringCommand);
                num++;
            }
            return num;
        }

        public DbQueryResult GetBusinessArticleList(BusinessArticleQuery page)
        {
            return DataHelper.PagingByRownumber(page.PageIndex, page.PageSize, page.SortBy, page.SortOrder, page.IsCount, "Vshop_BusinessArticle", "ArticleId", "", "*");
        }

        public IList<BusinessArticleInfo> GetBusinessArticles()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("select * from Vshop_BusinessArticle order by DisplaySequence asc,ArticleId desc");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<BusinessArticleInfo>(reader);
            }
        }

        public BusinessArticleInfo GetBusinessArticle(int ArticleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Vshop_BusinessArticle where ArticleId=@ArticleId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ArticleId", DbType.Int32, ArticleId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<BusinessArticleInfo>(reader);
            }
        }

        public bool SwapBusinessArticleSequence(int ArticleId, int displaysequence)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update Vshop_BusinessArticle  set DisplaySequence=@DisplaySequence where ArticleId=@ArticleId");
            this.database.AddInParameter(sqlStringCommand, "@DisplaySequence", DbType.Int32, displaysequence);
            this.database.AddInParameter(sqlStringCommand, "@ArticleId", DbType.Int32, ArticleId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateBusinessArticleVisitCounts(int articleId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("UPDATE Vshop_BusinessArticle SET ReviewCnt = ReviewCnt + 1 WHERE ArticleId = {0}", articleId));
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public DataTable GetBusinessArticlesDT()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Vshop_BusinessArticle order by AddedDate DESC, ArticleId DESC");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

    }
}
