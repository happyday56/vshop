namespace Hidistro.Jobs
{
    using Hidistro.Core.Jobs;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// 清理过期的团购活动
    /// </summary>
    public class GroupBuyJob : IJob
    {
        public void Execute(XmlNode node)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Hishop_GroupBuy SET Status = 2 WHERE Status = 1 AND EndDate <= @CurrentTime;");
            builder.Append("UPDATE Hishop_GroupBuy SET Status = 4 WHERE Status = 5 AND (select Count(1) from Hishop_Orders where GroupBuyId = Hishop_GroupBuy.GroupBuyId and OrderStatus = 6) =0;");
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(builder.ToString());
            database.AddInParameter(sqlStringCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
            database.ExecuteNonQuery(sqlStringCommand);
        }
    }
}

