namespace Hidistro.ControlPanel.Promotions
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Promotions;
    using Hidistro.SqlDal.Promotions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class CouponHelper
    {
        public static CouponActionStatus CreateCoupon(CouponInfo coupon, int count, out string lotNumber)
        {
            Globals.EntityCoding(coupon, true);
            return new CouponDao().CreateCoupon(coupon, count, out lotNumber);
        }

        public static bool DeleteCoupon(int couponId)
        {
            return new CouponDao().DeleteCoupon(couponId);
        }

        public static CouponInfo GetCoupon(int couponId)
        {
            return new CouponDao().GetCouponDetails(couponId);
        }

        public static IList<CouponItemInfo> GetCouponItemInfos(string lotNumber)
        {
            return new CouponDao().GetCouponItemInfos(lotNumber);
        }

        public static DbQueryResult GetCouponsList(CouponItemInfoQuery query)
        {
            return new CouponDao().GetCouponsList(query);
        }

        public static DbQueryResult GetNewCoupons(Pagination page)
        {
            return new CouponDao().GetNewCoupons(page);
        }

        public static void SendClaimCodes(int couponId, IList<CouponItemInfo> listCouponItem)
        {
            foreach (CouponItemInfo info in listCouponItem)
            {
                new CouponDao().SendClaimCodes(couponId, info);
            }
        }

        public static CouponActionStatus UpdateCoupon(CouponInfo coupon)
        {
            Globals.EntityCoding(coupon, true);
            return new CouponDao().UpdateCoupon(coupon);
        }
    }
}

