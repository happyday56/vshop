namespace Hidistro.Entities
{
    using Hidistro.Core.Enums;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.Promotions;
    using Hidistro.Entities.Sales;
    using Hidistro.Entities.Store;
    using Hidistro.Entities.VShop;
    using System;
    using System.Data;

    public static class DataMapper
    {
        public static CategoryInfo ConvertDataRowToProductCategory(DataRow row)
        {
            CategoryInfo info = new CategoryInfo {
                CategoryId = (int) row["CategoryId"],
                Name = (string) row["Name"],
                DisplaySequence = (int) row["DisplaySequence"]
            };
            if (row["IconUrl"] != DBNull.Value)
            {
                info.IconUrl = (string) row["IconUrl"];
            }
            if (row["ParentCategoryId"] != DBNull.Value)
            {
                info.ParentCategoryId = new int?((int) row["ParentCategoryId"]);
            }
            info.Depth = (int) row["Depth"];
            info.Path = (string) row["Path"];
            if (row["RewriteName"] != DBNull.Value)
            {
                info.RewriteName = (string) row["RewriteName"];
            }
            info.HasChildren = (bool) row["HasChildren"];
            if (row["FirstCommission"] != DBNull.Value)
            {
                info.FirstCommission = row["FirstCommission"].ToString();
            }
            if (row["SecondCommission"] != DBNull.Value)
            {
                info.SecondCommission = row["SecondCommission"].ToString();
            }
            if (row["ThirdCommission"] != DBNull.Value)
            {
                info.ThirdCommission = row["ThirdCommission"].ToString();
            }
            if (DBNull.Value != row["IsDisplayHome"])
            {
                info.IsDisplayHome = (int)row["IsDisplayHome"];
            }
            return info;
        }

        public static CouponInfo PopulateCoupon(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            CouponInfo info = new CouponInfo {
                CouponId = (int) reader["CouponId"],
                Name = (string) reader["Name"],
                StartTime = (DateTime) reader["StartTime"],
                ClosingTime = (DateTime) reader["ClosingTime"]
            };
            if (reader["Description"] != DBNull.Value)
            {
                info.Description = (string) reader["Description"];
            }
            if (reader["Amount"] != DBNull.Value)
            {
                info.Amount = new decimal?((decimal) reader["Amount"]);
            }
            info.DiscountValue = (decimal) reader["DiscountValue"];
            info.SentCount = (int) reader["SentCount"];
            info.UsedCount = (int) reader["UsedCount"];
            info.NeedPoint = (int) reader["NeedPoint"];
            return info;
        }

        public static DistributorGradeInfo PopulateDistributorGradeInfo(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            DistributorGradeInfo info = new DistributorGradeInfo {
                GradeId = (int) reader["GradeId"],
                Name = (string) reader["Name"]
            };

            if (reader["Description"] != DBNull.Value) info.Description = (string)reader["Description"];
            if (reader["CommissionsLimit"] != DBNull.Value) info.CommissionsLimit = (decimal)reader["CommissionsLimit"];
            if (reader["FirstCommissionRise"] != DBNull.Value) info.FirstCommissionRise = (decimal)reader["FirstCommissionRise"];
            if (reader["SecondCommissionRise"] != DBNull.Value) info.SecondCommissionRise = (decimal)reader["SecondCommissionRise"];
            if (reader["ThirdCommissionRise"] != DBNull.Value) info.ThirdCommissionRise = (decimal)reader["ThirdCommissionRise"];
            if (reader["IsDefault"] != DBNull.Value) info.IsDefault = (bool)reader["IsDefault"];

            if (reader["RecommendedIncome"] != DBNull.Value) info.RecommendedIncome = (decimal)reader["RecommendedIncome"];
            if (reader["AdditionalFees"] != DBNull.Value) info.AdditionalFees = (decimal)reader["AdditionalFees"];

            info.Ico = (string) reader["Ico"];
            return info;
        }

        public static DistributorsInfo PopulateDistributorInfo(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            DistributorsInfo info = new DistributorsInfo {
                UserId = (int) reader["UserId"],
                StoreName = (string) reader["StoreName"]
            };
            if (reader["RequestAccount"] != DBNull.Value) info.RequestAccount = (string)reader["RequestAccount"];
            if (reader["Logo"] != DBNull.Value) info.Logo = (string)reader["Logo"];
            info.BackImage = (string) reader["BackImage"];
            if (reader["AccountTime"] != DBNull.Value) info.AccountTime = new DateTime?((DateTime)reader["AccountTime"]);
            if (reader["GradeId"] != DBNull.Value)
            {
                info.DistributorGradeId = (DistributorGrade) Enum.Parse(typeof(DistributorGrade), reader["GradeId"].ToString(), true);
            }
            info.OrdersTotal = (decimal) reader["OrdersTotal"];
            if (reader["ReferralPath"] != DBNull.Value) info.ReferralPath = (string)reader["ReferralPath"];
            info.ReferralUserId = (int) reader["ReferralUserId"];
            info.ReferralOrders = (int) reader["ReferralOrders"];
            info.ReferralBlance = (decimal) reader["ReferralBlance"];
            info.ReferralRequestBalance = (decimal) reader["ReferralRequestBalance"];
            info.CreateTime = (DateTime) reader["CreateTime"];
            info.ReferralStatus = (int) reader["ReferralStatus"];
            if (reader["StoreDescription"] != DBNull.Value) info.StoreDescription = (string)reader["StoreDescription"];
            if (reader["DistributorGradeId"] != DBNull.Value) info.DistriGradeId = (int)reader["DistributorGradeId"];
            if (reader["BankName"] != DBNull.Value)
            {
                info.BankName = (String)reader["BankName"];
            }
            if (reader["BankAddress"] != DBNull.Value)
            {
                info.BankAddress = (String)reader["BankAddress"];
            }
            if (reader["AccountName"] != DBNull.Value)
            {
                info.AccountName = (String)reader["AccountName"];
            }
            if (DBNull.Value != reader["InvitationNum"])
            {
                info.InvitationNum = (int)reader["InvitationNum"];
            }
            if (DBNull.Value != reader["AccountBalance"])
            {
                info.AccountBalance = (decimal)reader["AccountBalance"];
            }
            else
            {
                info.AccountBalance = 0;
            }
            if (DBNull.Value != reader["IsBindMobile"])
            {
                info.IsBindMobile = (int)reader["IsBindMobile"];
            }
            else
            {
                info.IsBindMobile = 0;
            }
            if (DBNull.Value != reader["AccumulatedIncome"])
            {
                info.AccumulatedIncome = (decimal)reader["AccumulatedIncome"];
            }
            else
            {
                info.AccumulatedIncome = 0;
            }
            if (DBNull.Value != reader["VistiCounts"])
            {
                info.VistiCounts = (int)reader["VistiCounts"];
            }
            if (DBNull.Value != reader["GoodCounts"])
            {
                info.GoodCounts = (int)reader["GoodCounts"];
            }
            if (DBNull.Value != reader["RegionAddress"])
            {
                info.RegionAddress = (string)reader["RegionAddress"];
            }
            if (DBNull.Value != reader["IsTempStore"])
            {
                info.IsTempStore = (int)reader["IsTempStore"];
            }
            if (DBNull.Value != reader["DeadlineTime"])
            {
                info.DeadlineTime = (DateTime)reader["DeadlineTime"];
            }
            if (DBNull.Value != reader["DecasualizationTime"])
            {
                info.DecasualizationTime = (DateTime)reader["DecasualizationTime"];
            }

            return info;
        }

        public static GroupBuyInfo PopulateGroupBuy(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            GroupBuyInfo info = new GroupBuyInfo {
                GroupBuyId = (int) reader["GroupBuyId"],
                ProductId = (int) reader["ProductId"]
            };
            if (DBNull.Value != reader["NeedPrice"])
            {
                info.NeedPrice = (decimal) reader["NeedPrice"];
            }
            info.MaxCount = (int) reader["MaxCount"];
            info.StartDate = (DateTime) reader["StartDate"];
            info.EndDate = (DateTime) reader["EndDate"];
            if (DBNull.Value != reader["Content"])
            {
                info.Content = (string) reader["Content"];
            }
            info.Status = (GroupBuyStatus) ((int) reader["Status"]);
            info.Price = (decimal) reader["Price"];
            info.Count = (int) reader["Count"];
            info.SoldCount = (int) reader["SoldCount"];
            return info;
        }

        public static LineItemInfo PopulateLineItem(IDataRecord reader)
        {
            if (null == reader)
            {
                return null;
            }
            LineItemInfo info = new LineItemInfo {
                SkuId = (string) reader["SkuId"],
                ProductId = (int) reader["ProductId"]
            };
            if (reader["SKU"] != DBNull.Value)
            {
                info.SKU = (string) reader["SKU"];
            }
            info.Quantity = (int) reader["Quantity"];
            info.ShipmentQuantity = (int) reader["ShipmentQuantity"];
            info.ItemCostPrice = (decimal) reader["CostPrice"];
            info.ItemListPrice = (decimal) reader["ItemListPrice"];
            info.ItemAdjustedPrice = (decimal) reader["ItemAdjustedPrice"];
            info.ItemDescription = (string) reader["ItemDescription"];
            info.OrderItemsStatus = (OrderStatus) Enum.Parse(typeof(OrderStatus), reader["OrderItemsStatus"].ToString());
            if (DBNull.Value != reader["ItemsCommission"])
            {
                info.ItemsCommission = (decimal)reader["ItemsCommission"];
            }
            else
            {
                info.ItemsCommission = 0;
            }            
            if (DBNull.Value != reader["ItemAdjustedCommssion"])
            {
                info.ItemAdjustedCommssion = (decimal)reader["ItemAdjustedCommssion"];
            }
            else
            {
                info.ItemAdjustedCommssion = 0;
            }
            if (DBNull.Value != reader["SecondItemsCommission"])
            {
                info.SecondItemsCommission = (decimal)reader["SecondItemsCommission"];
            }
            else
            {
                info.SecondItemsCommission = 0;
            }
            if (DBNull.Value != reader["ThirdItemsCommission"])
            {
                info.ThirdItemsCommission = (decimal)reader["ThirdItemsCommission"];
            }
            else
            {
                info.ThirdItemsCommission = 0;
            }
            if (reader["ThumbnailsUrl"] != DBNull.Value)
            {
                info.ThumbnailsUrl = (string) reader["ThumbnailsUrl"];
            }
            info.ItemWeight = (decimal) reader["Weight"];
            if (DBNull.Value != reader["SKUContent"])
            {
                info.SKUContent = (string) reader["SKUContent"];
            }
            if (DBNull.Value != reader["PromotionId"])
            {
                info.PromotionId = (int) reader["PromotionId"];
            }
            if (DBNull.Value != reader["PromotionName"])
            {
                info.PromotionName = (string) reader["PromotionName"];
            }
            if (DBNull.Value != reader["FirstItemUserId"])
            {
                info.FirstItemUserId = (int)reader["FirstItemUserId"];
            }
            if (DBNull.Value != reader["SecondItemUserId"])
            {
                info.SecondItemUserId = (int)reader["SecondItemUserId"];
            }
            if (DBNull.Value != reader["ThirdItemUserId"])
            {
                info.ThirdItemUserId = (int)reader["ThirdItemUserId"];
            }
            if (DBNull.Value != reader["ManagerItemCommission"])
            {
                info.ManagerItemCommission = (decimal)reader["ManagerItemCommission"];
            }
            return info;
        }

        public static MemberClientSet PopulateMemberClientSet(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            MemberClientSet set = new MemberClientSet {
                ClientTypeId = (int) reader["ClientTypeId"]
            };
            if (DateTime.Compare((DateTime) reader["StartTime"], Convert.ToDateTime("1900-01-01")) != 0)
            {
                set.StartTime = new DateTime?((DateTime) reader["StartTime"]);
            }
            if (DateTime.Compare((DateTime) reader["EndTime"], Convert.ToDateTime("1900-01-01")) != 0)
            {
                set.EndTime = new DateTime?((DateTime) reader["EndTime"]);
            }
            set.LastDay = (int) reader["LastDay"];
            if (reader["ClientChar"] != DBNull.Value)
            {
                set.ClientChar = (string) reader["ClientChar"];
            }
            set.ClientValue = (decimal) reader["ClientValue"];
            return set;
        }

        public static OrderInfo PopulateOrder(IDataRecord reader)
        {
            if (reader == null)
            {
                return null;
            }
            OrderInfo info = new OrderInfo {
                OrderId = (string) reader["OrderId"]
            };
            if (DBNull.Value != reader["GatewayOrderId"])
            {
                info.GatewayOrderId = (string) reader["GatewayOrderId"];
            }
            if (DBNull.Value != reader["Remark"])
            {
                info.Remark = (string) reader["Remark"];
            }
            if (DBNull.Value != reader["ManagerMark"])
            {
                info.ManagerMark = new OrderMark?((OrderMark) reader["ManagerMark"]);
            }
            if (DBNull.Value != reader["ManagerRemark"])
            {
                info.ManagerRemark = (string) reader["ManagerRemark"];
            }
            if (DBNull.Value != reader["AdjustedDiscount"])
            {
                info.AdjustedDiscount = (decimal) reader["AdjustedDiscount"];
            }
            if (DBNull.Value != reader["OrderStatus"])
            {
                info.OrderStatus = (OrderStatus) reader["OrderStatus"];
            }
            if (DBNull.Value != reader["CloseReason"])
            {
                info.CloseReason = (string) reader["CloseReason"];
            }
            if (DBNull.Value != reader["OrderPoint"])
            {
                info.Points = (int) reader["OrderPoint"];
            }
            info.OrderDate = (DateTime) reader["OrderDate"];
            if (DBNull.Value != reader["PayDate"])
            {
                info.PayDate = new DateTime?((DateTime) reader["PayDate"]);
            }
            if (DBNull.Value != reader["ShippingDate"])
            {
                info.ShippingDate = new DateTime?((DateTime) reader["ShippingDate"]);
            }
            if (DBNull.Value != reader["FinishDate"])
            {
                info.FinishDate = new DateTime?((DateTime) reader["FinishDate"]);
            }
            info.UserId = (int) reader["UserId"];
            info.Username = (string) reader["Username"];
            info.ReferralUserId = (int) reader["ReferralUserId"];
            if (DBNull.Value != reader["EmailAddress"])
            {
                info.EmailAddress = (string) reader["EmailAddress"];
            }
            if (DBNull.Value != reader["RealName"])
            {
                info.RealName = (string) reader["RealName"];
            }
            if (DBNull.Value != reader["QQ"])
            {
                info.QQ = (string) reader["QQ"];
            }
            if (DBNull.Value != reader["Wangwang"])
            {
                info.Wangwang = (string) reader["Wangwang"];
            }
            if (DBNull.Value != reader["MSN"])
            {
                info.MSN = (string) reader["MSN"];
            }
            if (DBNull.Value != reader["ShippingRegion"])
            {
                info.ShippingRegion = (string) reader["ShippingRegion"];
            }
            if (DBNull.Value != reader["Address"])
            {
                info.Address = (string) reader["Address"];
            }
            if (DBNull.Value != reader["ZipCode"])
            {
                info.ZipCode = (string) reader["ZipCode"];
            }
            if (DBNull.Value != reader["ShipTo"])
            {
                info.ShipTo = (string) reader["ShipTo"];
            }
            if (DBNull.Value != reader["TelPhone"])
            {
                info.TelPhone = (string) reader["TelPhone"];
            }
            if (DBNull.Value != reader["CellPhone"])
            {
                info.CellPhone = (string) reader["CellPhone"];
            }
            if (DBNull.Value != reader["ShipToDate"])
            {
                info.ShipToDate = (string) reader["ShipToDate"];
            }
            if (DBNull.Value != reader["ShippingModeId"])
            {
                info.ShippingModeId = (int) reader["ShippingModeId"];
            }
            if (DBNull.Value != reader["ModeName"])
            {
                info.ModeName = (string) reader["ModeName"];
            }
            if (DBNull.Value != reader["RealShippingModeId"])
            {
                info.RealShippingModeId = (int) reader["RealShippingModeId"];
            }
            if (DBNull.Value != reader["RealModeName"])
            {
                info.RealModeName = (string) reader["RealModeName"];
            }
            if (DBNull.Value != reader["RegionId"])
            {
                info.RegionId = (int) reader["RegionId"];
            }
            if (DBNull.Value != reader["Freight"])
            {
                info.Freight = (decimal) reader["Freight"];
            }
            if (DBNull.Value != reader["AdjustedFreight"])
            {
                info.AdjustedFreight = (decimal) reader["AdjustedFreight"];
            }
            if (DBNull.Value != reader["ShipOrderNumber"])
            {
                info.ShipOrderNumber = (string) reader["ShipOrderNumber"];
            }
            if (DBNull.Value != reader["ExpressCompanyName"])
            {
                info.ExpressCompanyName = (string) reader["ExpressCompanyName"];
            }
            if (DBNull.Value != reader["ExpressCompanyAbb"])
            {
                info.ExpressCompanyAbb = (string) reader["ExpressCompanyAbb"];
            }
            if (DBNull.Value != reader["PaymentTypeId"])
            {
                info.PaymentTypeId = (int) reader["PaymentTypeId"];
            }
            if (DBNull.Value != reader["PaymentType"])
            {
                info.PaymentType = (string) reader["PaymentType"];
            }
            if (DBNull.Value != reader["PayCharge"])
            {
                info.PayCharge = (decimal) reader["PayCharge"];
            }
            if (DBNull.Value != reader["RefundStatus"])
            {
                info.RefundStatus = (RefundStatus) reader["RefundStatus"];
            }
            if (DBNull.Value != reader["RefundAmount"])
            {
                info.RefundAmount = (decimal) reader["RefundAmount"];
            }
            if (DBNull.Value != reader["RefundRemark"])
            {
                info.RefundRemark = (string) reader["RefundRemark"];
            }
            if (DBNull.Value != reader["Gateway"])
            {
                info.Gateway = (string) reader["Gateway"];
            }
            if (DBNull.Value != reader["ReducedPromotionId"])
            {
                info.ReducedPromotionId = (int) reader["ReducedPromotionId"];
            }
            if (DBNull.Value != reader["ReducedPromotionName"])
            {
                info.ReducedPromotionName = (string) reader["ReducedPromotionName"];
            }
            if (DBNull.Value != reader["ReducedPromotionAmount"])
            {
                info.ReducedPromotionAmount = (decimal) reader["ReducedPromotionAmount"];
            }
            if (DBNull.Value != reader["IsReduced"])
            {
                info.IsReduced = (bool) reader["IsReduced"];
            }
            if (DBNull.Value != reader["SentTimesPointPromotionId"])
            {
                info.SentTimesPointPromotionId = (int) reader["SentTimesPointPromotionId"];
            }
            if (DBNull.Value != reader["SentTimesPointPromotionName"])
            {
                info.SentTimesPointPromotionName = (string) reader["SentTimesPointPromotionName"];
            }
            if (DBNull.Value != reader["IsSendTimesPoint"])
            {
                info.IsSendTimesPoint = (bool) reader["IsSendTimesPoint"];
            }
            if (DBNull.Value != reader["TimesPoint"])
            {
                info.TimesPoint = (decimal) reader["TimesPoint"];
            }
            if (DBNull.Value != reader["FreightFreePromotionId"])
            {
                info.FreightFreePromotionId = (int) reader["FreightFreePromotionId"];
            }
            if (DBNull.Value != reader["FreightFreePromotionName"])
            {
                info.FreightFreePromotionName = (string) reader["FreightFreePromotionName"];
            }
            if (DBNull.Value != reader["IsFreightFree"])
            {
                info.IsFreightFree = (bool) reader["IsFreightFree"];
            }
            if (DBNull.Value != reader["DiscountAmount"])
            {
                info.DiscountAmount = (decimal) reader["DiscountAmount"];
            }
            if (DBNull.Value != reader["CouponName"])
            {
                info.CouponName = (string) reader["CouponName"];
            }
            if (DBNull.Value != reader["CouponCode"])
            {
                info.CouponCode = (string) reader["CouponCode"];
            }
            if (DBNull.Value != reader["CouponAmount"])
            {
                info.CouponAmount = (decimal) reader["CouponAmount"];
            }
            if (DBNull.Value != reader["CouponValue"])
            {
                info.CouponValue = (decimal) reader["CouponValue"];
            }
            if (DBNull.Value != reader["RedPagerActivityName"])
            {
                info.RedPagerActivityName = (string) reader["RedPagerActivityName"];
            }
            if (DBNull.Value != reader["RedPagerID"])
            {
                info.RedPagerID = new int?((int) reader["RedPagerID"]);
            }
            if (DBNull.Value != reader["RedPagerOrderAmountCanUse"])
            {
                info.RedPagerOrderAmountCanUse = (decimal) reader["RedPagerOrderAmountCanUse"];
            }
            if (DBNull.Value != reader["RedPagerAmount"])
            {
                info.RedPagerAmount = (decimal) reader["RedPagerAmount"];
            }
            if (DBNull.Value != reader["GroupBuyId"])
            {
                info.GroupBuyId = (int) reader["GroupBuyId"];
            }
            if (DBNull.Value != reader["CountDownBuyId"])
            {
                info.CountDownBuyId = (int) reader["CountDownBuyId"];
            }
            if (DBNull.Value != reader["Bundlingid"])
            {
                info.BundlingID = (int) reader["Bundlingid"];
            }
            if (DBNull.Value != reader["BundlingPrice"])
            {
                info.BundlingPrice = (decimal) reader["BundlingPrice"];
            }
            if (DBNull.Value != reader["NeedPrice"])
            {
                info.NeedPrice = (decimal) reader["NeedPrice"];
            }
            if (DBNull.Value != reader["GroupBuyStatus"])
            {
                info.GroupBuyStatus = (GroupBuyStatus) reader["GroupBuyStatus"];
            }
            if (DBNull.Value != reader["Tax"])
            {
                info.Tax = (decimal) reader["Tax"];
            }
            else
            {
                info.Tax = 0M;
            }
            if (DBNull.Value != reader["InvoiceTitle"])
            {
                info.InvoiceTitle = (string) reader["InvoiceTitle"];
            }
            else
            {
                info.InvoiceTitle = "";
            }
            if (!((DBNull.Value == reader["ReferralUserId"]) || string.IsNullOrEmpty(reader["ReferralUserId"].ToString())))
            {
                info.ReferralUserId = (int) reader["ReferralUserId"];
            }
            if (DBNull.Value != reader["FirstCommission"])
            {
                info.FirstCommission = (decimal) reader["FirstCommission"];
            }
            if (DBNull.Value != reader["SecondCommission"])
            {
                info.SecondCommission = (decimal) reader["SecondCommission"];
            }
            if (DBNull.Value != reader["ThirdCommission"])
            {
                info.ThirdCommission = (decimal) reader["ThirdCommission"];
            }
            if (DBNull.Value != reader["ActivitiesId"])
            {
                info.ActivitiesId = (string) reader["ActivitiesId"];
            }
            if (DBNull.Value != reader["ActivitiesName"])
            {
                info.ActivitiesName = (string) reader["ActivitiesName"];
            }
            if (DBNull.Value != reader["OrderTotal"])
            {
                info.OrderTotal = (decimal)reader["OrderTotal"];
            }
            if (DBNull.Value != reader["FirstUserId"])
            {
                info.FirstUserId = (int)reader["FirstUserId"];
            }
            if (DBNull.Value != reader["SecondUserId"])
            {
                info.SecondUserId = (int)reader["SecondUserId"];
            }
            if (DBNull.Value != reader["ThirdUserId"])
            {
                info.ThirdUserId = (int)reader["ThirdUserId"];
            }
            if (DBNull.Value != reader["ManagerCommission"])
            {
                info.ManagerCommission = (decimal)reader["ManagerCommission"];
            }
            if (DBNull.Value != reader["PrintBatch"])
            {
                info.PrintBatch = (long)reader["PrintBatch"];
            }
            if (DBNull.Value != reader["PrintBatchDate"])
            {
                info.PrintBatchDate = (DateTime)reader["PrintBatchDate"];
            }
            if (DBNull.Value != reader["OrderType"])
            {
                info.OrderType = (int)reader["OrderType"];
            }
            if (DBNull.Value != reader["MemberVPGiftId"])
            {
                info.MemberVPGiftId = (int)reader["MemberVPGiftId"];
            }
            if (DBNull.Value != reader["MemberVPGiftDetailId"])
            {
                info.MemberVPGiftDetailId = (int)reader["MemberVPGiftDetailId"];
            }
            if (DBNull.Value != reader["MemberGiftMoney"])
            {
                info.MemberGiftMoney = (decimal)reader["MemberGiftMoney"];
            }
            if (DBNull.Value != reader["StoreVPGiftId"])
            {
                info.StoreVPGiftId = (int)reader["StoreVPGiftId"];
            }
            if (DBNull.Value != reader["StoreVPGiftDetailId"])
            {
                info.StoreVPGiftDetailId = (int)reader["StoreVPGiftDetailId"];
            }
            if (DBNull.Value != reader["StoreGiftMoney"])
            {
                info.StoreGiftMoney = (decimal)reader["StoreGiftMoney"];
            }
            if (DBNull.Value != reader["IdCard"])
            {
                info.IdCard = (string)reader["IdCard"];
            }

            return info;
        }

        public static OrderRedPagerInfo PopulateOrderRedPagerInfo(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            return new OrderRedPagerInfo { OrderID = (string) reader["OrderID"], RedPagerActivityId = (int) reader["RedPagerActivityId"], RedPagerActivityName = (string) reader["RedPagerActivityName"], MaxGetTimes = (int) reader["MaxGetTimes"], AlreadyGetTimes = (int) reader["AlreadyGetTimes"], ItemAmountLimit = (decimal) reader["ItemAmountLimit"], OrderAmountCanUse = (decimal) reader["OrderAmountCanUse"], ExpiryDays = (int) reader["ExpiryDays"], UserID = (int) reader["UserID"] };
        }

        public static PaymentModeInfo PopulatePayment(IDataRecord reader)
        {
            if (null == reader)
            {
                return null;
            }
            PaymentModeInfo info = new PaymentModeInfo {
                ModeId = (int) reader["ModeId"],
                Name = (string) reader["Name"],
                DisplaySequence = (int) reader["DisplaySequence"],
                IsUseInpour = (bool) reader["IsUseInpour"],
                Charge = (decimal) reader["Charge"],
                IsPercent = (bool) reader["IsPercent"]
            };
            try
            {
                info.IsUseInDistributor = (bool) reader["IsUseInDistributor"];
            }
            catch
            {
                info.IsUseInDistributor = false;
            }
            if (reader["Description"] != DBNull.Value)
            {
                info.Description = (string) reader["Description"];
            }
            if (reader["Gateway"] != DBNull.Value)
            {
                info.Gateway = (string) reader["Gateway"];
            }
            if (reader["Settings"] != DBNull.Value)
            {
                info.Settings = (string) reader["Settings"];
            }
            return info;
        }

        public static ProductInfo PopulateProduct(IDataReader reader)
        {
            if (reader == null)
            {
                return null;
            }
            ProductInfo info = new ProductInfo {
                CategoryId = (int) reader["CategoryId"],
                ProductId = (int) reader["ProductId"]
            };
            if (DBNull.Value != reader["TypeId"])
            {
                info.TypeId = new int?((int) reader["TypeId"]);
            }
            info.ProductName = (string) reader["ProductName"];
            if (DBNull.Value != reader["ProductCode"])
            {
                info.ProductCode = (string) reader["ProductCode"];
            }
            if (DBNull.Value != reader["ShortDescription"])
            {
                info.ShortDescription = (string) reader["ShortDescription"];
            }
            if (DBNull.Value != reader["Unit"])
            {
                info.Unit = (string) reader["Unit"];
            }
            if (DBNull.Value != reader["Description"])
            {
                info.Description = (string) reader["Description"];
            }
            info.SaleStatus = (ProductSaleStatus) ((int) reader["SaleStatus"]);
            info.AddedDate = (DateTime) reader["AddedDate"];
            info.VistiCounts = (int) reader["VistiCounts"];
            info.SaleCounts = (int) reader["SaleCounts"];
            info.ShowSaleCounts = (int) reader["ShowSaleCounts"];
            info.DisplaySequence = (int) reader["DisplaySequence"];
            if (DBNull.Value != reader["ImageUrl1"])
            {
                info.ImageUrl1 = (string) reader["ImageUrl1"];
            }
            if (DBNull.Value != reader["ImageUrl2"])
            {
                info.ImageUrl2 = (string) reader["ImageUrl2"];
            }
            if (DBNull.Value != reader["ImageUrl3"])
            {
                info.ImageUrl3 = (string) reader["ImageUrl3"];
            }
            if (DBNull.Value != reader["ImageUrl4"])
            {
                info.ImageUrl4 = (string) reader["ImageUrl4"];
            }
            if (DBNull.Value != reader["ImageUrl5"])
            {
                info.ImageUrl5 = (string) reader["ImageUrl5"];
            }
            if (DBNull.Value != reader["ThumbnailUrl40"])
            {
                info.ThumbnailUrl40 = (string) reader["ThumbnailUrl40"];
            }
            if (DBNull.Value != reader["ThumbnailUrl60"])
            {
                info.ThumbnailUrl60 = (string) reader["ThumbnailUrl60"];
            }
            if (DBNull.Value != reader["ThumbnailUrl100"])
            {
                info.ThumbnailUrl100 = (string) reader["ThumbnailUrl100"];
            }
            if (DBNull.Value != reader["ThumbnailUrl160"])
            {
                info.ThumbnailUrl160 = (string) reader["ThumbnailUrl160"];
            }
            if (DBNull.Value != reader["ThumbnailUrl180"])
            {
                info.ThumbnailUrl180 = (string) reader["ThumbnailUrl180"];
            }
            if (DBNull.Value != reader["ThumbnailUrl220"])
            {
                info.ThumbnailUrl220 = (string) reader["ThumbnailUrl220"];
            }
            if (DBNull.Value != reader["ThumbnailUrl310"])
            {
                info.ThumbnailUrl310 = (string) reader["ThumbnailUrl310"];
            }
            if (DBNull.Value != reader["ThumbnailUrl410"])
            {
                info.ThumbnailUrl410 = (string) reader["ThumbnailUrl410"];
            }
            if (DBNull.Value != reader["MarketPrice"])
            {
                info.MarketPrice = new decimal?((decimal) reader["MarketPrice"]);
            }
            if (DBNull.Value != reader["BrandId"])
            {
                info.BrandId = new int?((int) reader["BrandId"]);
            }
            if (reader["MainCategoryPath"] != DBNull.Value)
            {
                info.MainCategoryPath = (string) reader["MainCategoryPath"];
            }
            if (reader["ExtendCategoryPath"] != DBNull.Value)
            {
                info.ExtendCategoryPath = (string) reader["ExtendCategoryPath"];
            }
            info.HasSKU = (bool) reader["HasSKU"];
            if (reader["TaobaoProductId"] != DBNull.Value)
            {
                info.TaobaoProductId = (long) reader["TaobaoProductId"];
            }
            if (DBNull.Value != reader["ProductShortLetter"])
            {
                info.ProductShortLetter = (string)reader["ProductShortLetter"];
            }
            if (DBNull.Value != reader["ProductLetter"])
            {
                info.ProductLetter = (string)reader["ProductLetter"];
            }
            if (DBNull.Value != reader["LetterImgUrl1"])
            {
                info.LetterImgUrl1 = (string)reader["LetterImgUrl1"];
            }
            if (DBNull.Value != reader["LetterImgUrl2"])
            {
                info.LetterImgUrl2 = (string)reader["LetterImgUrl2"];
            }
            if (DBNull.Value != reader["LetterImgUrl3"])
            {
                info.LetterImgUrl3 = (string)reader["LetterImgUrl3"];
            }
            if (DBNull.Value != reader["LetterImgUrl4"])
            {
                info.LetterImgUrl4 = (string)reader["LetterImgUrl4"];
            }
            if (DBNull.Value != reader["LetterImgUrl5"])
            {
                info.LetterImgUrl5 = (string)reader["LetterImgUrl5"];
            }
            if (DBNull.Value != reader["LetterTbUrl40"])
            {
                info.LetterTbUrl40 = (string)reader["LetterTbUrl40"];
            }
            if (DBNull.Value != reader["LetterTbUrl60"])
            {
                info.LetterTbUrl60 = (string)reader["LetterTbUrl60"];
            }
            if (DBNull.Value != reader["LetterTbUrl100"])
            {
                info.LetterTbUrl100 = (string)reader["LetterTbUrl100"];
            }
            if (DBNull.Value != reader["LetterTbUrl160"])
            {
                info.LetterTbUrl160 = (string)reader["LetterTbUrl160"];
            }
            if (DBNull.Value != reader["LetterTbUrl180"])
            {
                info.LetterTbUrl180 = (string)reader["LetterTbUrl180"];
            }
            if (DBNull.Value != reader["LetterTbUrl220"])
            {
                info.LetterTbUrl220 = (string)reader["LetterTbUrl220"];
            }
            if (DBNull.Value != reader["LetterTbUrl310"])
            {
                info.LetterTbUrl310 = (string)reader["LetterTbUrl310"];
            }
            if (DBNull.Value != reader["LetterTbUrl410"])
            {
                info.LetterTbUrl410 = (string)reader["LetterTbUrl410"];
            }
            if (DBNull.Value != reader["IsDistributorBuy"])
            {
                info.IsDistributorBuy = ((int)reader["IsDistributorBuy"]);
            }
            if (DBNull.Value != reader["LetterAddDate"])
            {
                info.LetterAddDate = (DateTime)reader["LetterAddDate"];
            }
            if (DBNull.Value != reader["VirtualPointRate"])
            {
                info.VirtualPointRate = new decimal?((decimal)reader["VirtualPointRate"]);
            }
            if (DBNull.Value != reader["HomePicUrl"])
            {
                info.HomePicUrl = (string)reader["HomePicUrl"];
            }
            if (DBNull.Value != reader["IsDisplayHome"])
            {
                info.IsDisplayHome = (int)reader["IsDisplayHome"];
            }
            if (DBNull.Value != reader["AddUserId"])
            {
                info.AddUserId = (int)reader["AddUserId"];
            }
            if (DBNull.Value != reader["GoodCounts"])
            {
                info.GoodCounts = (int)reader["GoodCounts"];
            }
            else
            {
                info.GoodCounts = 0;
            }
            if (DBNull.Value != reader["IsCross"])
            {
                info.IsCross = (int)reader["IsCross"];
            }
            else
            {
                info.IsCross = 1;
            }
            if (DBNull.Value != reader["MaxCross"])
            {
                info.MaxCross = (int)reader["MaxCross"];
            }
            else
            {
                info.MaxCross = 1;
            }
            if (DBNull.Value != reader["PTTypeId"])
            {
                info.PTTypeId = (int)reader["PTTypeId"];
            }
            else
            {
                info.PTTypeId = 0;
            }
            return info;
        }

        public static RedPagerActivityInfo PopulateRedPagerActivityInfo(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            return new RedPagerActivityInfo { RedPagerActivityId = (int) reader["RedPagerActivityId"], Name = (string) reader["Name"], CategoryId = (int) reader["CategoryId"], MinOrderAmount = (decimal) reader["MinOrderAmount"], MaxGetTimes = (int) reader["MaxGetTimes"], ItemAmountLimit = (decimal) reader["ItemAmountLimit"], OrderAmountCanUse = (decimal) reader["OrderAmountCanUse"], ExpiryDays = (int) reader["ExpiryDays"], IsOpen = (bool) reader["IsOpen"] };
        }

        public static SendRedpackRecordInfo PopulateSendRedpackRecordInfo(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            SendRedpackRecordInfo info = new SendRedpackRecordInfo {
                ID = (int) reader["ID"],
                BalanceDrawRequestID = (int) reader["BalanceDrawRequestID"],
                UserID = (int) reader["UserID"],
                OpenID = (string) reader["OpenID"],
                Amount = (int) reader["Amount"],
                ActName = (string) reader["ActName"],
                Wishing = (string) reader["Wishing"],
                ClientIP = (string) reader["ClientIP"],
                IsSend = (bool) reader["IsSend"]
            };
            if (reader["SendTime"] != DBNull.Value)
            {
                info.SendTime = (DateTime) reader["SendTime"];
            }
            return info;
        }

        public static ShippingAddressInfo PopulateShippingAddress(IDataRecord reader)
        {
            if (null == reader)
            {
                return null;
            }
            ShippingAddressInfo info = new ShippingAddressInfo {
                ShippingId = (int) reader["ShippingId"],
                ShipTo = (string) reader["ShipTo"],
                RegionId = (int) reader["RegionId"],
                UserId = (int) reader["UserId"],
                Address = (string) reader["Address"],
                Zipcode = (string) reader["Zipcode"],
                IsDefault = (bool) reader["IsDefault"]
            };
            if (reader["TelPhone"] != DBNull.Value)
            {
                info.TelPhone = (string) reader["TelPhone"];
            }
            if (reader["CellPhone"] != DBNull.Value)
            {
                info.CellPhone = (string) reader["CellPhone"];
            }
            return info;
        }

        public static ShippingModeInfo PopulateShippingMode(IDataRecord reader)
        {
            if (null == reader)
            {
                return null;
            }
            ShippingModeInfo info = new ShippingModeInfo();
            if (reader["ModeId"] != DBNull.Value)
            {
                info.ModeId = (int) reader["ModeId"];
            }
            if (reader["TemplateId"] != DBNull.Value)
            {
                info.TemplateId = (int) reader["TemplateId"];
            }
            info.Name = (string) reader["Name"];
            info.TemplateName = (string) reader["TemplateName"];
            if (reader["Weight"] != DBNull.Value)
            {
                info.Weight = (decimal) reader["Weight"];
            }
            if (DBNull.Value != reader["AddWeight"])
            {
                info.AddWeight = new decimal?((decimal) reader["AddWeight"]);
            }
            if (reader["Price"] != DBNull.Value)
            {
                info.Price = (decimal) reader["Price"];
            }
            if (DBNull.Value != reader["AddPrice"])
            {
                info.AddPrice = new decimal?((decimal) reader["AddPrice"]);
            }
            if (reader["Description"] != DBNull.Value)
            {
                info.Description = (string) reader["Description"];
            }
            info.DisplaySequence = (int) reader["DisplaySequence"];
            return info;
        }

        public static ShippingModeGroupInfo PopulateShippingModeGroup(IDataRecord reader)
        {
            if (null == reader)
            {
                return null;
            }
            ShippingModeGroupInfo info = new ShippingModeGroupInfo {
                TemplateId = (int) reader["TemplateId"],
                GroupId = (int) reader["GroupId"],
                Price = (decimal) reader["Price"]
            };
            if (DBNull.Value != reader["AddPrice"])
            {
                info.AddPrice = (decimal) reader["AddPrice"];
            }
            return info;
        }

        public static ShippingRegionInfo PopulateShippingRegion(IDataRecord reader)
        {
            if (null == reader)
            {
                return null;
            }
            return new ShippingRegionInfo { TemplateId = (int) reader["TemplateId"], GroupId = (int) reader["GroupId"], RegionId = (int) reader["RegionId"] };
        }

        public static ShippingModeInfo PopulateShippingTemplate(IDataRecord reader)
        {
            if (null == reader)
            {
                return null;
            }
            ShippingModeInfo info = new ShippingModeInfo();
            if (reader["TemplateId"] != DBNull.Value)
            {
                info.TemplateId = (int) reader["TemplateId"];
            }
            info.Name = (string) reader["TemplateName"];
            info.Weight = (decimal) reader["Weight"];
            if (DBNull.Value != reader["AddWeight"])
            {
                info.AddWeight = new decimal?((decimal) reader["AddWeight"]);
            }
            info.Price = (decimal) reader["Price"];
            if (DBNull.Value != reader["AddPrice"])
            {
                info.AddPrice = new decimal?((decimal) reader["AddPrice"]);
            }
            return info;
        }

        public static SKUItem PopulateSKU(IDataReader reader)
        {
            if (reader == null)
            {
                return null;
            }
            SKUItem item = new SKUItem {
                SkuId = (string) reader["SkuId"],
                ProductId = (int) reader["ProductId"]
            };
            if (reader["SKU"] != DBNull.Value)
            {
                item.SKU = (string) reader["SKU"];
            }
            if (reader["Weight"] != DBNull.Value)
            {
                item.Weight = (decimal) reader["Weight"];
            }
            item.Stock = (int) reader["Stock"];
            if (reader["CostPrice"] != DBNull.Value)
            {
                item.CostPrice = (decimal) reader["CostPrice"];
            }
            item.SalePrice = (decimal) reader["SalePrice"];
            return item;
        }

        public static TopicInfo PopulateTopic(IDataReader reader)
        {
            TopicInfo info = new TopicInfo {
                TopicId = (int) reader["TopicId"],
                Title = (string) reader["Title"]
            };
            if (reader["IconUrl"] != DBNull.Value)
            {
                info.IconUrl = (string) reader["IconUrl"];
            }
            info.Content = (string) reader["Content"];
            info.AddedDate = (DateTime) reader["AddedDate"];
            info.IsRelease = (bool) reader["IsRelease"];
            return info;
        }

        public static BusinessArticleInfo PopulateBusinessArticle(IDataReader reader)
        {
            BusinessArticleInfo info = new BusinessArticleInfo
            {
                ArticleId = (int)reader["ArticleId"],
                Title = (String)reader["Title"]
            };
            if (DBNull.Value != reader["IconUrl"])
            {
                info.IconUrl = (String)reader["IconUrl"];
            }
            info.Summary = (string)reader["Summary"];
            info.ArtContent = (string)reader["ArtContent"];
            info.AddedDate = (DateTime)reader["AddedDate"];
            info.ReviewCnt = (int)reader["ReviewCnt"];
            info.PublishName = (string)reader["PublishName"];
            return info;
        }

        public static UserRedPagerInfo PopulateUserRedPagerInfo(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            return new UserRedPagerInfo { RedPagerID = (int) reader["RedPagerID"], Amount = (decimal) reader["Amount"], UserID = (int) reader["UserID"], OrderID = (string) reader["OrderID"], RedPagerActivityName = (string) reader["RedPagerActivityName"], OrderAmountCanUse = (decimal) reader["OrderAmountCanUse"], CreateTime = (DateTime) reader["CreateTime"], ExpiryTime = (DateTime) reader["ExpiryTime"], IsUsed = (bool) reader["IsUsed"] };
        }

        public static UserStatisticsInfo PopulateUserStatistics(IDataRecord reader)
        {
            if (null == reader)
            {
                return null;
            }
            UserStatisticsInfo info = new UserStatisticsInfo();
            if (reader["RegionId"] != DBNull.Value)
            {
                info.RegionId = (int) reader["RegionId"];
            }
            if (reader["Usercounts"] != DBNull.Value)
            {
                info.Usercounts = (int) reader["Usercounts"];
            }
            if (reader["AllUserCounts"] != DBNull.Value)
            {
                info.AllUserCounts = (int) reader["AllUserCounts"];
            }
            return info;
        }

        public static VoteInfo PopulateVote(IDataRecord reader)
        {
            VoteInfo info = new VoteInfo {
                VoteId = (long) reader["VoteId"],
                VoteName = (string) reader["VoteName"],
                IsBackup = (bool) reader["IsBackup"],
                MaxCheck = (int) reader["MaxCheck"]
            };
            if (reader["ImageUrl"] != DBNull.Value)
            {
                info.ImageUrl = (string) reader["ImageUrl"];
            }
            info.StartDate = (DateTime) reader["StartDate"];
            info.EndDate = (DateTime) reader["EndDate"];
            info.VoteCounts = (int) reader["VoteCounts"];
            if (reader["Keys"] != DBNull.Value)
            {
                info.Keys = (string) reader["Keys"];
            }
            return info;
        }
    }
}

