namespace Hidistro.SaleSystem.Vshop
{
    using Hidistro.Core.Entities;
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal.VShop;
    using System;

    public class RedPagerActivityBrower
    {
        public static bool CreateRedPagerActivity(RedPagerActivityInfo redpaperactivity)
        {
            return new RedRedPagerActivityDao().CreateRedPagerActivity(redpaperactivity);
        }

        public static bool DelRedPagerActivity(int redpaperactivityid)
        {
            return new RedRedPagerActivityDao().DelRedPagerActivity(redpaperactivityid);
        }

        public static DbQueryResult GetRedPagerActivity(RedPagerActivityQuery query)
        {
            return new RedRedPagerActivityDao().GetRedPagerActivity(query);
        }

        public static RedPagerActivityInfo GetRedPagerActivityInfo(int redpaperactivityid)
        {
            return new RedRedPagerActivityDao().GetRedPagerActivityInfo(redpaperactivityid);
        }

        public static DbQueryResult GetRedPagerActivityRequest(RedPagerActivityQuery query)
        {
            return new RedRedPagerActivityDao().GetRedPagerActivityRequest(query);
        }

        public static bool IsExistsMinOrderAmount(int redpageractivityid, decimal minorderamount)
        {
            return new RedRedPagerActivityDao().IsExistsMinOrderAmount(redpageractivityid, minorderamount);
        }

        public static bool SetIsOpen(int redpaperactivityid, bool isopen)
        {
            return new RedRedPagerActivityDao().SetIsOpen(redpaperactivityid, isopen);
        }

        public static bool UpdateRedPagerActivity(RedPagerActivityInfo redpaperactivity)
        {
            return new RedRedPagerActivityDao().UpdateRedPagerActivity(redpaperactivity);
        }
    }
}

