namespace Hidistro.SaleSystem.Vshop
{
    using Hidistro.Entities.Promotions;
    using Hidistro.SqlDal.Promotions;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public static class GroupBuyBrowser
    {
        public static GroupBuyInfo GetGroupBuy(int groupbuyId)
        {
            return new GroupBuyDao().GetGroupBuy(groupbuyId, null);
        }

        public static DataTable GetGroupBuyProducts(int? categoryId, string keywords, int page, int size, out int total, bool onlyUnFinished = true)
        {
            return new GroupBuyDao().GetGroupBuyProducts(categoryId, keywords, page, size, out total, onlyUnFinished);
        }
    }
}

