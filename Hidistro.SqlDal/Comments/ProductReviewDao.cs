namespace Hidistro.SqlDal.Comments
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Comments;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ProductReviewDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public int DeleteProductReview(long reviewId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_ProductReviews WHERE ReviewId = @ReviewId");
            this.database.AddInParameter(sqlStringCommand, "ReviewId", DbType.Int64, reviewId);
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public ProductReviewInfo GetProductReview(int reviewId)
        {
            ProductReviewInfo info = new ProductReviewInfo();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_ProductReviews WHERE ReviewId=@ReviewId");
            this.database.AddInParameter(sqlStringCommand, "ReviewId", DbType.Int32, reviewId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                //if (reader.Read())
                //{
                //    info = ReaderConvert.ReaderToModel<ProductReviewInfo>(reader);
                //}
                return ReaderConvert.ReaderToModel<ProductReviewInfo>(reader);
            }
            //return info;
        }

        public DbQueryResult GetProductReviews(ProductReviewQuery reviewQuery)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" ProductName LIKE '%{0}%'", DataHelper.CleanSearchString(reviewQuery.Keywords));
            if (!string.IsNullOrEmpty(reviewQuery.ProductCode))
            {
                builder.AppendFormat(" AND ProductCode LIKE '%{0}%'", DataHelper.CleanSearchString(reviewQuery.ProductCode));
            }
            if (reviewQuery.productId > 0)
            {
                builder.AppendFormat(" AND ProductId = {0}", reviewQuery.productId);
            }
            if (reviewQuery.CategoryId.HasValue)
            {
                builder.AppendFormat(" AND (CategoryId = {0}", reviewQuery.CategoryId.Value);
                builder.AppendFormat(" OR  CategoryId IN (SELECT CategoryId FROM Hishop_Categories WHERE Path LIKE (SELECT Path FROM Hishop_Categories WHERE CategoryId = {0}) + '%'))", reviewQuery.CategoryId.Value);
            }
            return DataHelper.PagingByRownumber(reviewQuery.PageIndex, reviewQuery.PageSize, reviewQuery.SortBy, reviewQuery.SortOrder, reviewQuery.IsCount, "vw_Hishop_ProductReviews", "ProductId", builder.ToString(), "*");
        }

        public int GetProductReviewsCount(int productId)
        {
            StringBuilder builder = new StringBuilder("SELECT count(1) FROM Hishop_ProductReviews WHERE ProductId =" + productId);
            return (int) this.database.ExecuteScalar(CommandType.Text, builder.ToString());
        }

        public bool InsertProductReview(ProductReviewInfo review)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO Hishop_ProductReviews (ProductId, UserId, ReviewText, UserName, UserEmail, ReviewDate) VALUES(@ProductId, @UserId, @ReviewText, @UserName, @UserEmail, @ReviewDate)");
            this.database.AddInParameter(sqlStringCommand, "ProductId", DbType.Int32, review.ProductId);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, review.UserId);
            this.database.AddInParameter(sqlStringCommand, "ReviewText", DbType.String, review.ReviewText);
            this.database.AddInParameter(sqlStringCommand, "UserName", DbType.String, review.UserName);
            this.database.AddInParameter(sqlStringCommand, "UserEmail", DbType.String, review.UserEmail);
            this.database.AddInParameter(sqlStringCommand, "ReviewDate", DbType.DateTime, DateTime.Now);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public void LoadProductReview(int productId, int userId, out int buyNum, out int reviewNum)
        {
            buyNum = 0;
            reviewNum = 0;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT COUNT(*) FROM Hishop_ProductReviews WHERE ProductId=@ProductId AND UserId = @UserId SELECT ISNULL(SUM(Quantity), 0) FROM Hishop_OrderItems WHERE ProductId=@ProductId AND OrderId IN" + string.Format(" (SELECT OrderId FROM Hishop_Orders WHERE UserId = @UserId AND OrderStatus = {0})", 5));
            this.database.AddInParameter(sqlStringCommand, "ProductId", DbType.Int32, productId);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    reviewNum = (int) reader[0];
                }
                reader.NextResult();
                if (reader.Read())
                {
                    buyNum = (int) reader[0];
                }
            }
        }

        public bool ReplyProductReview(ProductReviewInfo productReview)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_ProductReviews SET ReplyText = @ReplyText, ReplyDate = @ReplyDate, ReplyUserId = @ReplyUserId WHERE ReviewId = @ReviewId");
            this.database.AddInParameter(sqlStringCommand, "ReplyText", DbType.String, productReview.ReplyText);
            this.database.AddInParameter(sqlStringCommand, "ReplyDate", DbType.DateTime, productReview.ReplyDate);
            this.database.AddInParameter(sqlStringCommand, "ReplyUserId", DbType.Int32, productReview.ReplyUserId);
            this.database.AddInParameter(sqlStringCommand, "ReviewId", DbType.Int32, productReview.ReviewId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
            
        }
    }
}

