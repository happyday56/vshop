namespace Hidistro.SqlDal.Sales
{
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Sales;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ShoppingCartDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public void AddLineItem(MemberInfo member, string skuId, int quantity, int categoryid)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("ss_ShoppingCart_AddLineItem");
            this.database.AddInParameter(storedProcCommand, "UserId", DbType.Int32, member.UserId);
            this.database.AddInParameter(storedProcCommand, "SkuId", DbType.String, skuId);
            this.database.AddInParameter(storedProcCommand, "Quantity", DbType.Int32, quantity);
            this.database.AddInParameter(storedProcCommand, "CategoryId", DbType.Int32, categoryid);
            this.database.ExecuteNonQuery(storedProcCommand);
        }

        public void ClearShoppingCart(int userId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_ShoppingCarts WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public DataTable GetAllFull(int ActivitiesType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ReductionMoney,ActivitiesId,ActivitiesName,MeetMoney,ActivitiesType from Hishop_Activities where datediff(dd,GETDATE(),StartTime)<=0 and datediff(dd,GETDATE(),EndTIme)>=0 and (ActivitiesType=0 or ActivitiesType=" + ActivitiesType + ")  order by MeetMoney asc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public ShoppingCartItemInfo GetCartItemInfo(MemberInfo member, string skuId, int quantity)
        {
            ShoppingCartItemInfo info = null;
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("ss_ShoppingCart_GetItemInfo");
            this.database.AddInParameter(storedProcCommand, "Quantity", DbType.Int32, quantity);
            this.database.AddInParameter(storedProcCommand, "UserId", DbType.Int32, (member != null) ? member.UserId : 0);
            this.database.AddInParameter(storedProcCommand, "SkuId", DbType.String, skuId);
            this.database.AddInParameter(storedProcCommand, "GradeId", DbType.Int32, (member != null) ? member.GradeId : 0);
            using (IDataReader reader = this.database.ExecuteReader(storedProcCommand))
            {
                if (!reader.Read())
                {
                    return info;
                }

                info = new ShoppingCartItemInfo();
                info.SkuId = skuId;
                info.Quantity = info.ShippQuantity = quantity;
                info.MainCategoryPath = reader["MainCategoryPath"].ToString();
                info.ProductId = (int)reader["ProductId"];

                if (reader["SKU"] != DBNull.Value)
                {
                    info.SKU = (string)reader["SKU"];
                }
                info.Name = (string)reader["ProductName"];
                if (DBNull.Value != reader["Weight"])
                {
                    info.Weight = (int)reader["Weight"];
                }
                if (DBNull.Value != reader["SalePrice"])
                {
                    info.MemberPrice = info.AdjustedPrice = (decimal)reader["SalePrice"];
                }
                
                if (DBNull.Value != reader["ThumbnailUrl40"])
                {
                    info.ThumbnailUrl40 = reader["ThumbnailUrl40"].ToString();
                }
                if (DBNull.Value != reader["ThumbnailUrl60"])
                {
                    info.ThumbnailUrl60 = reader["ThumbnailUrl60"].ToString();
                }
                if (DBNull.Value != reader["ThumbnailUrl100"])
                {
                    info.ThumbnailUrl100 = reader["ThumbnailUrl100"].ToString();
                }
                if (DBNull.Value != reader["IsfreeShipping"])
                {
                    info.IsfreeShipping = Convert.ToBoolean(reader["IsfreeShipping"]);
                }
                if (DBNull.Value != reader["VirtualPointRate"])
                {
                    info.VirtualPointRate = (decimal)reader["VirtualPointRate"];
                }
                else
                {
                    info.VirtualPointRate = 0;
                }
                string str = string.Empty;
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        if (!((((reader["AttributeName"] == DBNull.Value) || string.IsNullOrEmpty((string)reader["AttributeName"])) || (reader["ValueStr"] == DBNull.Value)) || string.IsNullOrEmpty((string)reader["ValueStr"])))
                        {
                            object obj2 = str;
                            str = string.Concat(new object[] { obj2, reader["AttributeName"], "：", reader["ValueStr"], "; " });
                        }
                    }
                }
                info.SkuContent = str;
            }
            return info;
        }

        public DataTable GetShopping(string CategoryId, MemberInfo member)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "select * from Hishop_ShoppingCarts where CategoryId=", CategoryId, " and UserId = ", member.UserId }));
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public ShoppingCartInfo GetShoppingCart(MemberInfo member)
        {
            ShoppingCartInfo info = new ShoppingCartInfo();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_ShoppingCarts WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, member.UserId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    ShoppingCartItemInfo item = this.GetCartItemInfo(member, (string)reader["SkuId"], (int)reader["Quantity"]);
                    if (item != null)
                    {
                        info.LineItems.Add(item);
                    }
                }
            }
            return info;
        }

        public List<ShoppingCartInfo> GetShoppingCartAviti(MemberInfo member)
        {
            List<ShoppingCartInfo> list = new List<ShoppingCartInfo>();
            DataTable shoppingCategoryId = this.GetShoppingCategoryId();
            DataTable shopping = new DataTable();
            for (int i = 0; i < shoppingCategoryId.Rows.Count; i++)
            {
                ShoppingCartInfo item = new ShoppingCartInfo
                {
                    CategoryId = int.Parse(shoppingCategoryId.Rows[i]["CategoryId"].ToString())
                };
                shopping = this.GetShopping(item.CategoryId.ToString(), member);
                for (int j = 0; j < shopping.Rows.Count; j++)
                {
                    ShoppingCartItemInfo info2 = this.GetCartItemInfo(member, shopping.Rows[j]["SkuId"].ToString(), int.Parse(shopping.Rows[j]["Quantity"].ToString()));
                    if (info2 != null)
                    {
                        item.LineItems.Add(info2);
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public List<ShoppingCartInfo> GetShoppingCartAvitiByUserId(MemberInfo member)
        {
            List<ShoppingCartInfo> list = new List<ShoppingCartInfo>();
            DataTable shoppingCategoryId = this.GetShoppingCategoryId();
            DataTable shopping = new DataTable();
            for (int i = 0; i < shoppingCategoryId.Rows.Count; i++)
            {
                ShoppingCartInfo item = new ShoppingCartInfo
                {
                    CategoryId = int.Parse(shoppingCategoryId.Rows[i]["CategoryId"].ToString())
                };
                shopping = this.GetShopping(item.CategoryId.ToString(), member);
                for (int j = 0; j < shopping.Rows.Count; j++)
                {
                    ShoppingCartItemInfo info2 = this.GetCartItemInfo(member, shopping.Rows[j]["SkuId"].ToString(), int.Parse(shopping.Rows[j]["Quantity"].ToString()));
                    if (info2 != null)
                    {
                        item.LineItems.Add(info2);
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public DataTable GetShoppingCategoryId()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select distinct CategoryId from Hishop_ShoppingCarts ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        public void RemoveLineItem(int userId, string skuId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_ShoppingCarts WHERE UserId = @UserId AND SkuId = @SkuId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            this.database.AddInParameter(sqlStringCommand, "SkuId", DbType.String, skuId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public void UpdateLineItemQuantity(MemberInfo member, string skuId, int quantity)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_ShoppingCarts SET Quantity = @Quantity WHERE UserId = @UserId AND SkuId = @SkuId");
            this.database.AddInParameter(sqlStringCommand, "Quantity", DbType.Int32, quantity);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, member.UserId);
            this.database.AddInParameter(sqlStringCommand, "SkuId", DbType.String, skuId);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }
    }
}

