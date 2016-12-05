namespace Hidistro.SqlDal.Sales
{
    using Hidistro.Entities;
    using Hidistro.Entities.Sales;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class ShipperDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool AddShipper(ShippersInfo shipper)
        {
            string query = string.Empty;
            if (shipper.IsDefault)
            {
                query = query + "UPDATE Hishop_Shippers SET IsDefault = 0";
            }
            query = query + " INSERT INTO Hishop_Shippers (IsDefault, ShipperTag, ShipperName, RegionId, Address, CellPhone, TelPhone, Zipcode, Remark) VALUES (@IsDefault, @ShipperTag, @ShipperName, @RegionId, @Address, @CellPhone, @TelPhone, @Zipcode, @Remark)";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "IsDefault", DbType.Boolean, shipper.IsDefault);
            this.database.AddInParameter(sqlStringCommand, "ShipperTag", DbType.String, shipper.ShipperTag);
            this.database.AddInParameter(sqlStringCommand, "ShipperName", DbType.String, shipper.ShipperName);
            this.database.AddInParameter(sqlStringCommand, "RegionId", DbType.Int32, shipper.RegionId);
            this.database.AddInParameter(sqlStringCommand, "Address", DbType.String, shipper.Address);
            this.database.AddInParameter(sqlStringCommand, "CellPhone", DbType.String, shipper.CellPhone);
            this.database.AddInParameter(sqlStringCommand, "TelPhone", DbType.String, shipper.TelPhone);
            this.database.AddInParameter(sqlStringCommand, "Zipcode", DbType.String, shipper.Zipcode);
            this.database.AddInParameter(sqlStringCommand, "Remark", DbType.String, shipper.Remark);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool DeleteShipper(int shipperId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_Shippers WHERE ShipperId = @ShipperId");
            this.database.AddInParameter(sqlStringCommand, "ShipperId", DbType.Int32, shipperId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public ShippersInfo GetShipper(int shipperId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_Shippers WHERE ShipperId = @ShipperId");
            this.database.AddInParameter(sqlStringCommand, "ShipperId", DbType.Int32, shipperId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<ShippersInfo>(reader);
            }
        }

        public IList<ShippersInfo> GetShippers(bool includeDistributor)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_Shippers");
            if (!includeDistributor)
            {
                sqlStringCommand.CommandText = sqlStringCommand.CommandText + " WHERE DistributorUserId = 0";
            }
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<ShippersInfo>(reader);
            }
        }

        public void SetDefalutShipper(int shipperId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_Shippers SET IsDefault = 0;UPDATE Hishop_Shippers SET IsDefault = 1 WHERE ShipperId = @ShipperId");
            this.database.AddInParameter(sqlStringCommand, "ShipperId", DbType.Int32, shipperId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool UpdateShipper(ShippersInfo shipper)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_Shippers SET ShipperTag = @ShipperTag, ShipperName = @ShipperName, RegionId = @RegionId, Address = @Address, CellPhone = @CellPhone, TelPhone = @TelPhone, Zipcode = @Zipcode, Remark =@Remark WHERE ShipperId = @ShipperId");
            this.database.AddInParameter(sqlStringCommand, "ShipperTag", DbType.String, shipper.ShipperTag);
            this.database.AddInParameter(sqlStringCommand, "ShipperName", DbType.String, shipper.ShipperName);
            this.database.AddInParameter(sqlStringCommand, "RegionId", DbType.Int32, shipper.RegionId);
            this.database.AddInParameter(sqlStringCommand, "Address", DbType.String, shipper.Address);
            this.database.AddInParameter(sqlStringCommand, "CellPhone", DbType.String, shipper.CellPhone);
            this.database.AddInParameter(sqlStringCommand, "TelPhone", DbType.String, shipper.TelPhone);
            this.database.AddInParameter(sqlStringCommand, "Zipcode", DbType.String, shipper.Zipcode);
            this.database.AddInParameter(sqlStringCommand, "Remark", DbType.String, shipper.Remark);
            this.database.AddInParameter(sqlStringCommand, "ShipperId", DbType.Int32, shipper.ShipperId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

