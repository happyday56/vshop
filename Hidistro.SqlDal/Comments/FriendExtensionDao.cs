namespace Hidistro.SqlDal.Comments
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Store;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class FriendExtensionDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public int DeleteFriendExtension(int ExtensionId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_FriendExtension WHERE ExtensionId = @ExtensionId");
            this.database.AddInParameter(sqlStringCommand, "ExtensionId", DbType.Int64, ExtensionId);
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public DbQueryResult FriendExtensionList(FriendExtensionQuery Query)
        {
            StringBuilder builder = new StringBuilder();
            if (Query.ExtensionId > 0)
            {
                builder.AppendFormat(" ExtensionId = {0}", Query.ExtensionId);
            }
            return DataHelper.PagingByRownumber(Query.PageIndex, Query.PageSize, Query.SortBy, Query.SortOrder, Query.IsCount, "Hishop_FriendExtension", "ExtensionId", builder.ToString(), "*");
        }

        public bool InsertFriendExtension(FriendExtensionInfo review)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO Hishop_FriendExtension (ExensiontRemark, ExensionImg) VALUES(@ExensiontRemark, @ExensionImg)");
            this.database.AddInParameter(sqlStringCommand, "ExensiontRemark", DbType.String, review.ExensiontRemark);
            this.database.AddInParameter(sqlStringCommand, "ExensionImg", DbType.String, review.ExensionImg);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateFriendExtension(FriendExtensionInfo review)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_FriendExtension set ExensiontRemark=@ExensiontRemark, ExensionImg=@ExensionImg WHERE ExtensionId = @ExtensionId  ");
            this.database.AddInParameter(sqlStringCommand, "ExensiontRemark", DbType.String, review.ExensiontRemark);
            this.database.AddInParameter(sqlStringCommand, "ExensionImg", DbType.String, review.ExensionImg);
            this.database.AddInParameter(sqlStringCommand, "ExtensionId", DbType.Int64, review.ExtensionId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateFriendExtensionImg(FriendExtensionInfo review)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_FriendExtension set  ExensionImg=@ExensionImg WHERE ExtensionId = @ExtensionId  ");
            this.database.AddInParameter(sqlStringCommand, "ExensionImg", DbType.String, review.ExensionImg);
            this.database.AddInParameter(sqlStringCommand, "ExtensionId", DbType.Int64, review.ExtensionId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

