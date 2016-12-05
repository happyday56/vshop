namespace Hidistro.SqlDal.Sales
{
    using Hidistro.Core;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data;
    using System.Data.Common;

    public class ExpressTemplateDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool AddExpressTemplate(string expressName, string xmlFile)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO Hishop_ExpressTemplates(ExpressName, XmlFile, IsUse) VALUES(@ExpressName, @XmlFile, 1)");
            this.database.AddInParameter(sqlStringCommand, "ExpressName", DbType.String, expressName);
            this.database.AddInParameter(sqlStringCommand, "XmlFile", DbType.String, xmlFile);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool DeleteExpressTemplate(int expressId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_ExpressTemplates WHERE ExpressId = @ExpressId");
            this.database.AddInParameter(sqlStringCommand, "ExpressId", DbType.Int32, expressId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public DataTable GetExpressTemplates(bool? isUser)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_ExpressTemplates");
            if (isUser.HasValue)
            {
                sqlStringCommand.CommandText = sqlStringCommand.CommandText + string.Format(" WHERE IsUse = '{0}'", isUser);
            }
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public bool SetExpressIsUse(int expressId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_ExpressTemplates SET IsUse = ~IsUse WHERE ExpressId = @ExpressId");
            this.database.AddInParameter(sqlStringCommand, "ExpressId", DbType.Int32, expressId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateExpressTemplate(int expressId, string expressName)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_ExpressTemplates SET ExpressName = @ExpressName WHERE ExpressId = @ExpressId");
            this.database.AddInParameter(sqlStringCommand, "ExpressName", DbType.String, expressName);
            this.database.AddInParameter(sqlStringCommand, "ExpressId", DbType.Int32, expressId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

