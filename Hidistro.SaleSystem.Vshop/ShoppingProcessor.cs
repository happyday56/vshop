namespace Hidistro.SaleSystem.Vshop
{
    using Hidistro.Core;
    using Hidistro.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.Promotions;
    using Hidistro.Entities.Sales;
    using Hidistro.SqlDal;
    using Hidistro.SqlDal.Commodities;
    using Hidistro.SqlDal.Members;
    using Hidistro.SqlDal.Orders;
    using Hidistro.SqlDal.Promotions;
    using Hidistro.SqlDal.Sales;
    using Hidistro.SqlDal.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    public static class ShoppingProcessor
    {
        private static object createOrderLocker = new object();

        public static decimal CalcFreight(int regionId, decimal totalWeight, ShippingModeInfo shippingModeInfo)
        {
            decimal price = 0M;
            int topRegionId = RegionHelper.GetTopRegionId(regionId);
            decimal num3 = totalWeight;
            int num4 = 1;
            if ((num3 > shippingModeInfo.Weight) && (shippingModeInfo.AddWeight.HasValue && (shippingModeInfo.AddWeight.Value > 0M)))
            {
                decimal num6 = num3 - shippingModeInfo.Weight;
                if ((num6 % shippingModeInfo.AddWeight) == 0M)
                {
                    num4 = Convert.ToInt32(Math.Truncate((decimal)((num3 - shippingModeInfo.Weight) / shippingModeInfo.AddWeight.Value)));
                }
                else
                {
                    num4 = Convert.ToInt32(Math.Truncate((decimal)((num3 - shippingModeInfo.Weight) / shippingModeInfo.AddWeight.Value))) + 1;
                }
            }
            if ((shippingModeInfo.ModeGroup == null) || (shippingModeInfo.ModeGroup.Count == 0))
            {
                if ((num3 > shippingModeInfo.Weight) && shippingModeInfo.AddPrice.HasValue)
                {
                    return ((num4 * shippingModeInfo.AddPrice.Value) + shippingModeInfo.Price);
                }
                return shippingModeInfo.Price;
            }
            int? nullable = null;
            foreach (ShippingModeGroupInfo info in shippingModeInfo.ModeGroup)
            {
                foreach (ShippingRegionInfo info2 in info.ModeRegions)
                {
                    if (topRegionId == info2.RegionId)
                    {
                        nullable = new int?(info2.GroupId);
                        break;
                    }
                }
                if (nullable.HasValue)
                {
                    if (num3 > shippingModeInfo.Weight)
                    {
                        price = (num4 * info.AddPrice) + info.Price;
                    }
                    else
                    {
                        price = info.Price;
                    }
                    break;
                }
            }
            if (nullable.HasValue)
            {
                return price;
            }
            if ((num3 > shippingModeInfo.Weight) && shippingModeInfo.AddPrice.HasValue)
            {
                return ((num4 * shippingModeInfo.AddPrice.Value) + shippingModeInfo.Price);
            }
            return shippingModeInfo.Price;
        }

        public static decimal CalcPayCharge(decimal cartMoney, PaymentModeInfo paymentModeInfo)
        {
            if (!paymentModeInfo.IsPercent)
            {
                return paymentModeInfo.Charge;
            }
            return (cartMoney * (paymentModeInfo.Charge / 100M));
        }

        private static void checkCanGroupBuy(int quantity, int groupBuyId)
        {
            GroupBuyInfo groupBuy = GroupBuyBrowser.GetGroupBuy(groupBuyId);
            if (groupBuy.Status != GroupBuyStatus.UnderWay)
            {
                throw new OrderException("当前团购状态不允许购买");
            }
            if ((groupBuy.StartDate > DateTime.Now) || (groupBuy.EndDate < DateTime.Now))
            {
                throw new OrderException("当前不在团购时间范围内");
            }
            int num = groupBuy.MaxCount - groupBuy.SoldCount;
            if (quantity > num)
            {
                throw new OrderException("剩余可购买团购数量不够");
            }
        }

        public static OrderInfo ConvertShoppingCartToOrder(ShoppingCartInfo shoppingCart, bool isCountDown, bool isSignBuy)
        {
            if (shoppingCart.LineItems.Count == 0)
            {
                return null;
            }
            OrderInfo info = new OrderInfo
            {
                Points = shoppingCart.GetPoint(),
                ReducedPromotionId = shoppingCart.ReducedPromotionId,
                ReducedPromotionName = shoppingCart.ReducedPromotionName,
                ReducedPromotionAmount = shoppingCart.ReducedPromotionAmount,
                IsReduced = shoppingCart.IsReduced,
                SentTimesPointPromotionId = shoppingCart.SentTimesPointPromotionId,
                SentTimesPointPromotionName = shoppingCart.SentTimesPointPromotionName,
                IsSendTimesPoint = shoppingCart.IsSendTimesPoint,
                TimesPoint = shoppingCart.TimesPoint,
                FreightFreePromotionId = shoppingCart.FreightFreePromotionId,
                FreightFreePromotionName = shoppingCart.FreightFreePromotionName,
                IsFreightFree = shoppingCart.IsFreightFree
            };
            string str = string.Empty;
            if (shoppingCart.LineItems.Count > 0)
            {
                foreach (ShoppingCartItemInfo info2 in shoppingCart.LineItems)
                {
                    str = str + string.Format("'{0}',", info2.SkuId);
                }
            }
            if (shoppingCart.LineItems.Count > 0)
            {
                foreach (ShoppingCartItemInfo info2 in shoppingCart.LineItems)
                {
                    LineItemInfo info3 = new LineItemInfo
                    {
                        SkuId = info2.SkuId,
                        ProductId = info2.ProductId,
                        SKU = info2.SKU,
                        Quantity = info2.Quantity,
                        ShipmentQuantity = info2.ShippQuantity,
                        ItemCostPrice = new SkuDao().GetSkuItem(info2.SkuId).CostPrice,
                        ItemListPrice = info2.MemberPrice,
                        ItemAdjustedPrice = info2.AdjustedPrice,
                        ItemDescription = info2.Name,
                        ThumbnailsUrl = info2.ThumbnailUrl40,
                        ItemWeight = info2.Weight,
                        SKUContent = info2.SkuContent,
                        PromotionId = info2.PromotionId,
                        PromotionName = info2.PromotionName,
                        MainCategoryPath = info2.MainCategoryPath
                    };
                    info.LineItems.Add(info3.SkuId, info3);
                }
            }
            info.Tax = 0.00M;
            info.InvoiceTitle = "";
            return info;
        }

        public static bool CreatOrder(OrderInfo orderInfo)
        {
            bool flag = false;
            Database database = DatabaseFactory.CreateDatabase();
            int quantity = orderInfo.LineItems.Sum<KeyValuePair<string, LineItemInfo>>((Func<KeyValuePair<string, LineItemInfo>, int>)(item => item.Value.Quantity));
            lock (createOrderLocker)
            {
                if (orderInfo.GroupBuyId > 0)
                {
                    checkCanGroupBuy(quantity, orderInfo.GroupBuyId);
                }
                using (DbConnection connection = database.CreateConnection())
                {
                    connection.Open();
                    DbTransaction dbTran = connection.BeginTransaction();

                    try
                    {
                        if (!new OrderDao().CreatOrder(orderInfo, dbTran))
                        {
                            dbTran.Rollback();
                            return false;
                        }
                        if ((orderInfo.LineItems.Count > 0) && !new LineItemDao().AddOrderLineItems(orderInfo.OrderId, orderInfo.LineItems.Values, dbTran))
                        {
                            dbTran.Rollback();
                            return false;
                        }
                        if (!string.IsNullOrEmpty(orderInfo.CouponCode) && !new CouponDao().AddCouponUseRecord(orderInfo, dbTran))
                        {
                            dbTran.Rollback();
                            return false;
                        }
                        if (orderInfo.RedPagerID > 1)
                        {
                            if ((orderInfo.RedPagerID > 0) && !new UserRedPagerDao().AddUserRedPagerRecord(orderInfo, dbTran))
                            {
                                dbTran.Rollback();
                                return false;
                            }
                        }
                        if (orderInfo.RedPagerID == 1)
                        {
                            new MemberDao().UpdateVirtualPoints(orderInfo.UserId, orderInfo.RedPagerAmount);
                            HiCache.Remove(string.Format("DataCache-Member-{0}", orderInfo.UserId));
                        }
                        if (orderInfo.GroupBuyId > 0)
                        {
                            GroupBuyDao dao = new GroupBuyDao();
                            GroupBuyInfo groupBuy = dao.GetGroupBuy(orderInfo.GroupBuyId, dbTran);
                            groupBuy.SoldCount += quantity;
                            dao.UpdateGroupBuy(groupBuy, dbTran);
                            dao.RefreshGroupBuyFinishState(orderInfo.GroupBuyId, dbTran);
                        }
                        dbTran.Commit();
                        flag = true;
                    }
                    catch
                    {
                        dbTran.Rollback();
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return flag;
        }

        public static DataTable GetCoupon(decimal orderAmount)
        {
            return new CouponDao().GetCoupon(orderAmount);
        }

        public static CouponInfo GetCoupon(string couponCode)
        {
            return new CouponDao().GetCouponDetails(couponCode);
        }

        public static OrderInfo GetOrderInfo(string orderId)
        {
            return new OrderDao().GetOrderInfo(orderId);
        }

        public static DataTable GetOrderReturnTable(int userid, string ReturnsId)
        {
            return new RefundDao().GetOrderReturnTable(userid, ReturnsId);
        }

        public static PaymentModeInfo GetPaymentMode(int modeId)
        {
            return new PaymentModeDao().GetPaymentMode(modeId);
        }

        public static IList<PaymentModeInfo> GetPaymentModes()
        {
            return new PaymentModeDao().GetPaymentModes();
        }

        public static SKUItem GetProductAndSku(MemberInfo member, int productId, string options)
        {
            return new SkuDao().GetProductAndSku(member, productId, options);
        }

        public static bool GetReturnMes(int userid, string OrderId, int ProductId, int HandleStatus)
        {
            return new RefundDao().GetReturnMes(userid, OrderId, ProductId, HandleStatus);
        }

        public static ShippingModeInfo GetShippingMode(int modeId, bool includeDetail)
        {
            return new ShippingModeDao().GetShippingMode(modeId, includeDetail);
        }

        public static IList<ShippingModeInfo> GetShippingModes()
        {
            return new ShippingModeDao().GetShippingModes();
        }

        public static bool InsertCalculationCommission(string orderid)
        {
            OrderInfo orderInfo = GetOrderInfo(orderid);
            DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(orderInfo.ReferralUserId);
            bool flag = false;
            if (userIdDistributors != null)
            {
                Dictionary<string, LineItemInfo> lineItems = orderInfo.LineItems;
                LineItemInfo info3 = new LineItemInfo();
                DataView defaultView = CategoryBrowser.GetAllCategories().DefaultView;
                string str2 = null;
                string str3 = null;
                string str4 = null;
                decimal subTotal = 0M;
                foreach (KeyValuePair<string, LineItemInfo> pair in lineItems)
                {
                    string key = pair.Key;
                    info3 = pair.Value;
                    DataTable productCategories = ProductBrowser.GetProductCategories(info3.ProductId);
                    if ((productCategories.Rows.Count > 0) && (productCategories.Rows[0][0].ToString() != "0"))
                    {
                        defaultView.RowFilter = " CategoryId=" + productCategories.Rows[0][0];
                        str2 = defaultView[0]["FirstCommission"].ToString();
                        str3 = defaultView[0]["SecondCommission"].ToString();
                        str4 = defaultView[0]["ThirdCommission"].ToString();
                        if ((!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str3)) && !string.IsNullOrEmpty(str4))
                        {
                            ArrayList referralBlanceList = new ArrayList();
                            ArrayList userIdList = new ArrayList();
                            ArrayList ordersTotalList = new ArrayList();
                            subTotal = info3.GetSubTotal();
                            if (string.IsNullOrEmpty(userIdDistributors.ReferralPath))
                            {
                                referralBlanceList.Add((decimal.Parse(str4) / 100M) * info3.GetSubTotal());
                                userIdList.Add(orderInfo.ReferralUserId);
                                ordersTotalList.Add(subTotal);
                            }
                            else
                            {
                                string[] strArray = userIdDistributors.ReferralPath.Split(new char[] { '|' });
                                if (strArray.Length == 1)
                                {
                                    referralBlanceList.Add((decimal.Parse(str3) / 100M) * info3.GetSubTotal());
                                    userIdList.Add(strArray[0]);
                                    ordersTotalList.Add(subTotal);
                                    referralBlanceList.Add((decimal.Parse(str4) / 100M) * info3.GetSubTotal());
                                    userIdList.Add(orderInfo.ReferralUserId);
                                    ordersTotalList.Add(subTotal);
                                }
                                if (strArray.Length == 2)
                                {
                                    referralBlanceList.Add((decimal.Parse(str2) / 100M) * info3.GetSubTotal());
                                    userIdList.Add(strArray[0]);
                                    ordersTotalList.Add(subTotal);
                                    referralBlanceList.Add((decimal.Parse(str3) / 100M) * info3.GetSubTotal());
                                    userIdList.Add(strArray[1]);
                                    ordersTotalList.Add(subTotal);
                                    referralBlanceList.Add((decimal.Parse(str4) / 100M) * info3.GetSubTotal());
                                    userIdList.Add(orderInfo.ReferralUserId);
                                    ordersTotalList.Add(subTotal);
                                }
                            }
                            flag = InsertCalculationCommission(userIdList, referralBlanceList, orderInfo.OrderId, ordersTotalList, orderInfo.UserId.ToString());
                        }
                    }
                }
            }
            return flag;
        }

        public static bool InsertCalculationCommission(ArrayList UserIdList, ArrayList ReferralBlanceList, string orderid, ArrayList OrdersTotalList, string userid)
        {
            return new OrderDao().InsertCalculationCommission(UserIdList, ReferralBlanceList, orderid, OrdersTotalList, userid);
        }

        public static bool InsertOrderRefund(RefundInfo refundInfo)
        {
            return new RefundDao().InsertOrderRefund(refundInfo);
        }

        public static bool UpdateAdjustCommssions(string orderId, string skuId, decimal commssionmoney, decimal adjustcommssion)
        {
            bool flag = false;
            using (DbConnection connection = DatabaseFactory.CreateDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction dbTran = connection.BeginTransaction();
                try
                {
                    OrderInfo orderInfo = GetOrderInfo(orderId);
                    if (orderId == null)
                    {
                        return false;
                    }
                    int userId = DistributorsBrower.GetCurrentDistributors().UserId;
                    if ((orderInfo.ReferralUserId != userId) || (orderInfo.OrderStatus != OrderStatus.WaitBuyerPay))
                    {
                        return false;
                    }
                    LineItemInfo lineItem = orderInfo.LineItems[skuId];
                    if ((lineItem == null) || (lineItem.ItemsCommission < adjustcommssion))
                    {
                        return false;
                    }
                    lineItem.ItemAdjustedCommssion = adjustcommssion;
                    if (!new LineItemDao().UpdateLineItem(orderId, lineItem, dbTran))
                    {
                        dbTran.Rollback();
                    }
                    if (!new OrderDao().UpdateOrder(orderInfo, dbTran))
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
                }
                finally
                {
                    connection.Close();
                }
                return flag;
            }
        }

        public static bool UpdateOrderGoodStatu(string orderid, string skuid, int OrderItemsStatus)
        {
            return new RefundDao().UpdateOrderGoodStatu(orderid, skuid, OrderItemsStatus);
        }

        public static CouponInfo UseCoupon(decimal orderAmount, string claimCode)
        {
            if (!string.IsNullOrEmpty(claimCode))
            {
                CouponInfo coupon = GetCoupon(claimCode);
                if (coupon != null)
                {
                    if (coupon.Amount.HasValue && orderAmount >= coupon.Amount)
                    {
                        return coupon;
                    }
                }
            }
            return null;
        }

        public static DataTable GetOrderBrand(string orderId)
        {
            return new OrderDao().GetOrderBrand(orderId);
        }

        public static bool AddSubOrderData(string orderId, string subOrderId, string productBrandId)
        {
            return new OrderDao().AddSubOrderData(orderId, subOrderId, productBrandId);
        }

        public static IList<OrderSubLogisticInfo> GetSubOrderLogisticByOrderId(string orderId, bool isSubOrderId = false)
        {
            return new OrderDao().GetSubOrderLogisticByOrderId(orderId, isSubOrderId);
        }

        public static IList<string> GetSubOrderByOrderId(string orderId, bool isSubOrderId = false)
        {
            return new OrderDao().GetSubOrderByOrderId(orderId, isSubOrderId);
        }

        public static DataTable GetSubOrderCVTDTByOrderId(string orderId, bool isSubOrderId = false)
        {
            return new OrderDao().GetSubOrderCVTDTByOrderId(orderId, isSubOrderId);
        }

        public static bool UpdateSubOrderExpressNo(string orderId, string subOrderId, string subExpressCompanyAbb, string subExpressCompanyName, string subShipOrderNumber)
        {
            return new OrderDao().UpdateSubOrderExpressNo(orderId, subOrderId, subExpressCompanyAbb, subExpressCompanyName, subShipOrderNumber);
        }

        
    }
}

