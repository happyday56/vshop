namespace Hidistro.SaleSystem.Vshop
{
    using Hidistro.Core.Entities;
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal.VShop;
    using System;
    using System.Data;

    public class UserRedPagerBrower
    {
        public static string CreateUserRedPager(UserRedPagerInfo userredpager)
        {
            return new UserRedPagerDao().CreateUserRedPager(userredpager);
        }

        public static bool DelUserRedPager(int redpageid)
        {
            return new UserRedPagerDao().DelUserRedPager(redpageid);
        }

        public static DataTable GetUserRedPager(int userid, UserRedPagerType type)
        {
            return new UserRedPagerDao().GetUserRedPager(userid, type);
        }

        public static UserRedPagerInfo GetUserRedPagerByOrderIDAndUserID(int userid, string orderid)
        {
            return new UserRedPagerDao().GetUserRedPagerByOrderIDAndUserID(userid, orderid);
        }

        public static UserRedPagerInfo GetUserRedPagerByRedPagerID(int redpagerid)
        {
            return new UserRedPagerDao().GetUserRedPagerByRedPagerID(redpagerid);
        }

        public static DataTable GetUserRedPagerCanUse(decimal orderAmount)
        {
            return new UserRedPagerDao().GetUserRedPagerCanUse(orderAmount);
        }

        public static DbQueryResult GetUserRedPagerList(UserRedPagerQuery userredpagerquery)
        {
            return new UserRedPagerDao().GetUserRedPagerList(userredpagerquery);
        }

        public static bool SetIsUsed(int redpageid, bool isused)
        {
            return new UserRedPagerDao().SetIsUsed(redpageid, isused);
        }
    }
}

