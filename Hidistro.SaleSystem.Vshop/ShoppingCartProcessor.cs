namespace Hidistro.SaleSystem.Vshop
{
    using Hidistro.ControlPanel.Store;
    using Hidistro.Core;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Promotions;
    using Hidistro.Entities.Sales;
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal.Commodities;
    using Hidistro.SqlDal.Sales;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class ShoppingCartProcessor
    {
        public static void AddLineItem(string skuId, int quantity, int categoryid)
        {
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (quantity <= 0)
            {
                quantity = 1;
            }
            new ShoppingCartDao().AddLineItem(currentMember, skuId, quantity, categoryid);
        }

        public static void ClearShoppingCart()
        {
            new ShoppingCartDao().ClearShoppingCart(Globals.GetCurrentMemberUserId());
        }

        public static ShoppingCartInfo GetGroupBuyShoppingCart(int groupbuyId, string productSkuId, int buyAmount)
        {
            ShoppingCartItemInfo shoppingCartItemInfo;
            ShoppingCartInfo info = new ShoppingCartInfo();
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            ShoppingCartItemInfo info3 = new ShoppingCartDao().GetCartItemInfo(currentMember, productSkuId, buyAmount);
            if (info3 == null)
            {
                return null;
            }
            GroupBuyInfo groupBuy = GroupBuyBrowser.GetGroupBuy(groupbuyId);
            if (((groupBuy == null) || (groupBuy.StartDate > DateTime.Now)) || (groupBuy.Status != GroupBuyStatus.UnderWay))
            {
                return null;
            }
            int count = groupBuy.Count;
            decimal price = groupBuy.Price;
            shoppingCartItemInfo = new ShoppingCartItemInfo();
            shoppingCartItemInfo.SkuId = info3.SkuId;
            shoppingCartItemInfo.ProductId = info3.ProductId;
            shoppingCartItemInfo.SKU = info3.SKU;
            shoppingCartItemInfo.Name = info3.Name;
            shoppingCartItemInfo.MemberPrice = shoppingCartItemInfo.AdjustedPrice = price;
            shoppingCartItemInfo.SkuContent = info3.SkuContent;
            shoppingCartItemInfo.Quantity = shoppingCartItemInfo.ShippQuantity = buyAmount;
            shoppingCartItemInfo.Weight = info3.Weight;
            shoppingCartItemInfo.ThumbnailUrl40 = info3.ThumbnailUrl40;
            shoppingCartItemInfo.ThumbnailUrl60 = info3.ThumbnailUrl60;
            shoppingCartItemInfo.ThumbnailUrl100 = info3.ThumbnailUrl100;

            info.LineItems.Add(shoppingCartItemInfo);
            return info;
        }

        public static ShoppingCartInfo GetShoppingCart()
        {
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            
            if (currentMember == null)
            {
                return null;
            }

            ShoppingCartInfo shoppingCart = new ShoppingCartDao().GetShoppingCart(currentMember);
            if (shoppingCart.LineItems.Count == 0)
            {
                return null;
            }
            return shoppingCart;
        }

        public static ShoppingCartInfo GetShoppingCart(string productSkuId, int buyAmount)
        {
            ShoppingCartInfo info = new ShoppingCartInfo();
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            ShoppingCartItemInfo item = new ShoppingCartDao().GetCartItemInfo(currentMember, productSkuId, buyAmount);
            if (item == null)
            {
                return null;
            }
            info.LineItems.Add(item);
            return info;
        }

        public static List<ShoppingCartInfo> GetShoppingCartAviti()
        {
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                return null;
            }
            List<ShoppingCartInfo> shoppingCartAviti = new ShoppingCartDao().GetShoppingCartAviti(currentMember);
            if (shoppingCartAviti.Count == 0)
            {
                return null;
            }
            return shoppingCartAviti;
        }

        public static List<ShoppingCartInfo> GetShoppingCartAviti(MemberInfo currentMember)
        {
            
            List<ShoppingCartInfo> shoppingCartAviti = new ShoppingCartDao().GetShoppingCartAviti(currentMember);
            if (shoppingCartAviti.Count == 0)
            {
                return null;
            }
            return shoppingCartAviti;
        }

        public static int GetSkuStock(string skuId)
        {
            SKUItem sku = new SkuDao().GetSkuItem(skuId);
            if (null != sku)
            {
                return sku.Stock;
            }
            return 0;
            //return new SkuDao().GetSkuItem(skuId).Stock;
        }

        public static void RemoveLineItem(string skuId)
        {
            new ShoppingCartDao().RemoveLineItem(Globals.GetCurrentMemberUserId(), skuId);
        }

        public static void UpdateLineItemQuantity(string skuId, int quantity)
        {
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (quantity <= 0)
            {
                RemoveLineItem(skuId);
            }
            new ShoppingCartDao().UpdateLineItemQuantity(currentMember, skuId, quantity);
        }


        public static decimal DiscountMoney(IList<ShoppingCartItemInfo> infoList)
        {
            decimal num = 0M;
            decimal num2 = 0M;
            decimal num3 = 0M;
            int productCategoryId = -1;

            DataTable type = ProductBrowser.GetType();
            for (int i = 0; i < type.Rows.Count; i++)
            {
                decimal num5 = 0M;
                foreach (ShoppingCartItemInfo info in infoList)
                {
                    // 旧处理
                    //if (!string.IsNullOrEmpty(info.MainCategoryPath) 
                    //    && ((int.Parse(type.Rows[i]["ActivitiesType"].ToString()) == int.Parse(info.MainCategoryPath.Split(new char[] { '|' })[0].ToString())) 
                    //         || (int.Parse(type.Rows[i]["ActivitiesType"].ToString()) == 0)))
                    //{
                    //    num5 += info.SubTotal;
                    //}

                    productCategoryId = int.Parse(info.MainCategoryPath.Split(new char[] { '|' })[0].ToString());

                    // 新处理
                    // 商品分类不为空
                    if (!string.IsNullOrEmpty(info.MainCategoryPath))
                    {
                        if((int.Parse(type.Rows[i]["ActivitiesType"].ToString()) == 0)){
                            // 全类目处理
                            // 1.获取全类目下是否有排除的分类不参与
                            DataTable dtCategory = VShopHelper.GetActivitiesTypeNoCategoryId(int.Parse(type.Rows[i]["ActivitiesType"].ToString()), productCategoryId);
                            //if (!(null != dtCategory && dtCategory.Rows.Count > 0))
                            //{
                            //    num5 += info.SubTotal;
                            //}

                            if ((null != dtCategory && dtCategory.Rows.Count > 0))
                            {
                                if (string.IsNullOrEmpty(dtCategory.Rows[0]["CategoryId"].ToString()))
                                {
                                    num5 += info.SubTotal;
                                }
                            }   

                        }
                        else if ((int.Parse(type.Rows[i]["ActivitiesType"].ToString()) == int.Parse(info.MainCategoryPath.Split(new char[] {  '|' })[0].ToString())))
                        {
                            // 非全类目处理
                            num5 += info.SubTotal;
                        }
                    }
                }
                if (num5 != 0M)
                {
                    //DataTable allFull = ProductBrowser.GetAllFull(int.Parse(type.Rows[i]["ActivitiesType"].ToString()));
                    DataTable allFull = ProductBrowser.GetAllFullByCategoryId(int.Parse(type.Rows[i]["ActivitiesType"].ToString()));
                    if (allFull.Rows.Count > 0)
                    {
                        for (int j = 0; j < allFull.Rows.Count; j++)
                        {
                            if (num5 >= decimal.Parse(allFull.Rows[allFull.Rows.Count - 1]["MeetMoney"].ToString()))
                            {
                                num2 = decimal.Parse(allFull.Rows[allFull.Rows.Count - 1]["MeetMoney"].ToString());
                                num = decimal.Parse(allFull.Rows[allFull.Rows.Count - 1]["ReductionMoney"].ToString());
                                break;
                            }
                            if (num5 <= decimal.Parse(allFull.Rows[0]["MeetMoney"].ToString()))
                            {
                                num2 = decimal.Parse(allFull.Rows[0]["MeetMoney"].ToString());
                                //num += decimal.Parse(allFull.Rows[0]["ReductionMoney"].ToString());
                                num = decimal.Parse(allFull.Rows[0]["ReductionMoney"].ToString());
                                break;
                            }
                            if (num5 >= decimal.Parse(allFull.Rows[j]["MeetMoney"].ToString()))
                            {
                                num2 = decimal.Parse(allFull.Rows[j]["MeetMoney"].ToString());
                                num = decimal.Parse(allFull.Rows[j]["ReductionMoney"].ToString());
                            }
                        }
                        if (num5 >= num2)
                        {
                            num3 += num;
                        }
                    }
                }
            }
            return num3;
        }
    }
}

