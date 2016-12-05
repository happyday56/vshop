namespace Hidistro.ControlPanel.Promotions
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Promotions;
    using Hidistro.SqlDal.Promotions;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Data.Common;

    public static class GroupBuyHelper
    {
        public static bool AddGroupBuy(GroupBuyInfo groupBuy)
        {
            bool flag;
            Globals.EntityCoding(groupBuy, true);
            using (DbConnection connection = DatabaseFactory.CreateDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction dbTran = connection.BeginTransaction();
                try
                {
                    if (new GroupBuyDao().AddGroupBuy(groupBuy, dbTran) <= 0)
                    {
                        dbTran.Rollback();
                        return false;
                    }
                    dbTran.Commit();
                    flag = true;
                }
                catch
                {
                    dbTran.Rollback();
                    flag = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return flag;
        }

        public static bool DeleteGroupBuy(int groupBuyId)
        {
            return new GroupBuyDao().DeleteGroupBuy(groupBuyId);
        }

        public static decimal GetCurrentPrice(int groupBuyId, int prodcutQuantity)
        {
            return new GroupBuyDao().GetGroupBuy(groupBuyId, null).Price;
        }

        public static GroupBuyInfo GetGroupBuy(int groupBuyId)
        {
            return new GroupBuyDao().GetGroupBuy(groupBuyId, null);
        }

        public static DbQueryResult GetGroupBuyList(GroupBuyQuery query)
        {
            return new GroupBuyDao().GetGroupBuyList(query);
        }

        public static int GetOrderCount(int groupBuyId)
        {
            return new GroupBuyDao().GetOrderCount(groupBuyId);
        }

        public static string GetPriceByProductId(int productId)
        {
            return new GroupBuyDao().GetPriceByProductId(productId);
        }

        public static bool ProductGroupBuyExist(int productId)
        {
            return new GroupBuyDao().ProductGroupBuyExist(productId);
        }

        public static void RefreshGroupFinishBuyState(int groupBuyId)
        {
            new GroupBuyDao().RefreshGroupBuyFinishState(groupBuyId, null);
        }

        public static bool SetGroupBuyEndUntreated(int groupBuyId)
        {
            return new GroupBuyDao().SetGroupBuyEndUntreated(groupBuyId);
        }

        public static bool SetGroupBuyStatus(int groupBuyId, GroupBuyStatus status)
        {
            return new GroupBuyDao().SetGroupBuyStatus(groupBuyId, status);
        }

        public static void SwapGroupBuySequence(int groupBuyId, int displaySequence)
        {
            new GroupBuyDao().SwapGroupBuySequence(groupBuyId, displaySequence);
        }

        public static bool UpdateGroupBuy(GroupBuyInfo groupBuy)
        {
            bool flag;
            Globals.EntityCoding(groupBuy, true);
            Database database = DatabaseFactory.CreateDatabase();
            GroupBuyDao dao = new GroupBuyDao();
            using (DbConnection connection = database.CreateConnection())
            {
                connection.Open();
                DbTransaction dbTran = connection.BeginTransaction();
                try
                {
                    if (!dao.UpdateGroupBuy(groupBuy, dbTran))
                    {
                        dbTran.Rollback();
                        return false;
                    }
                    dbTran.Commit();
                    flag = true;
                }
                catch (Exception)
                {
                    dbTran.Rollback();
                    flag = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return flag;
        }
    }
}

