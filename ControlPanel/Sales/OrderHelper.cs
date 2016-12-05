namespace Hidistro.ControlPanel.Sales
{
    using Hidistro.ControlPanel.Members;
    using Hidistro.ControlPanel.Promotions;
    using Hidistro.ControlPanel.Store;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.Promotions;
    using Hidistro.Entities.Store;
    using Hidistro.Messages;
    using Hidistro.SqlDal.Commodities;
    using Hidistro.SqlDal.Members;
    using Hidistro.SqlDal.Orders;
    using Hidistro.SqlDal.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using NewLife.Log;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;

    public static class OrderHelper
    {
        public static bool CloseTransaction(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.EditOrders);
            if (!order.CheckAction(OrderActions.SELLER_CLOSE))
            {
                return false;
            }
            order.OrderStatus = OrderStatus.Closed;
            bool flag = new OrderDao().UpdateOrder(order, null);
            if (order.GroupBuyId > 0)
            {
                GroupBuyInfo groupBuy = GroupBuyHelper.GetGroupBuy(order.GroupBuyId);
                groupBuy.SoldCount -= order.GetGroupBuyProductQuantity();
                GroupBuyHelper.UpdateGroupBuy(groupBuy);
            }
            if (order.RedPagerID == 1)
            {
                new MemberDao().AddUserVirtualPoints(order.UserId, order.RedPagerAmount);
            }
            string itemStr = "'" + string.Join("','", order.LineItems.Keys) + "'";
            new OrderDao().UpdateItemsStatus(order.OrderId, 4, itemStr);
            if (flag)
            {
                EventLogs.WriteOperationLog(Privilege.EditOrders, string.Format(CultureInfo.InvariantCulture, "关闭了订单“{0}”", new object[] { order.OrderId }));
            }
            return flag;
        }

        public static bool ConfirmOrderFinish(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.EditOrders);
            bool flag = false;
            if (order.CheckAction(OrderActions.SELLER_FINISH_TRADE))
            {
                order.OrderStatus = OrderStatus.Finished;
                order.FinishDate = new DateTime?(DateTime.Now);
                flag = new OrderDao().UpdateOrder(order, null);
                if (flag)
                {
                    EventLogs.WriteOperationLog(Privilege.EditOrders, string.Format(CultureInfo.InvariantCulture, "完成编号为\"{0}\"的订单", new object[] { order.OrderId }));
                }
            }
            return flag;
        }

        public static bool ConfirmPay(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.CofimOrderPay);
            bool flag = false;
            if (order.CheckAction(OrderActions.SELLER_CONFIRM_PAY))
            {
                OrderDao dao = new OrderDao();
                order.OrderStatus = OrderStatus.BuyerAlreadyPaid;
                order.PayDate = new DateTime?(DateTime.Now);
                flag = dao.UpdateOrder(order, null);
                string str = "";
                if (!flag)
                {
                    return flag;
                }
                dao.UpdatePayOrderStock(order.OrderId);
                foreach (LineItemInfo info in order.LineItems.Values)
                {
                    ProductDao dao2 = new ProductDao();
                    str = str + "'" + info.SkuId + "',";
                    ProductInfo productDetails = dao2.GetProductDetails(info.ProductId);
                    productDetails.SaleCounts += info.Quantity;
                    productDetails.ShowSaleCounts += info.Quantity;
                    dao2.UpdateProduct(productDetails, null);
                }
                UpdateUserAccount(order);
                if (!string.IsNullOrEmpty(str))
                {
                    dao.UpdateItemsStatus(order.OrderId, 2, str.Substring(0, str.Length - 1));
                }
                if (!string.IsNullOrEmpty(order.ActivitiesId))
                {
                    new ActivitiesDao().UpdateActivitiesTakeEffect(order.ActivitiesId);
                }
                Messenger.OrderPayment(new MemberDao().GetMember(order.UserId), order.OrderId, order.GetTotal());
                XTrace.WriteLine("支付微信通知2");
                EventLogs.WriteOperationLog(Privilege.CofimOrderPay, string.Format(CultureInfo.InvariantCulture, "确认收款编号为\"{0}\"的订单", new object[] { order.OrderId }));
            }
            return flag;
        }

        public static bool DelDebitNote(string[] noteIds, out int count)
        {
            ManagerHelper.CheckPrivilege(Privilege.DeleteOrder);
            bool flag = true;
            count = 0;
            foreach (string str in noteIds)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    flag &= new DebitNoteDao().DelDebitNote(str);
                    if (flag)
                    {
                        count++;
                    }
                }
            }
            return flag;
        }

        public static bool DeleteLineItem(string sku, OrderInfo order)
        {
            bool flag;
            ManagerHelper.CheckPrivilege(Privilege.EditOrders);
            using (DbConnection connection = DatabaseFactory.CreateDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction dbTran = connection.BeginTransaction();
                try
                {
                    order.LineItems.Remove(sku);
                    if (!new LineItemDao().DeleteLineItem(sku, order.OrderId, dbTran))
                    {
                        dbTran.Rollback();
                        return false;
                    }
                    if (!new OrderDao().UpdateOrder(order, dbTran))
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
            if (flag)
            {
                EventLogs.WriteOperationLog(Privilege.EditOrders, string.Format(CultureInfo.InvariantCulture, "删除了订单号为\"{0}\"的订单商品", new object[] { order.OrderId }));
            }
            return flag;
        }

        public static int DeleteOrders(string orderIds)
        {
            ManagerHelper.CheckPrivilege(Privilege.DeleteOrder);
            int num = new OrderDao().DeleteOrders(orderIds);
            if (num > 0)
            {
                EventLogs.WriteOperationLog(Privilege.DeleteOrder, string.Format(CultureInfo.InvariantCulture, "删除了编号为\"{0}\"的订单", new object[] { orderIds }));
            }
            return num;
        }

        public static bool DelSendNote(string[] noteIds, out int count)
        {
            ManagerHelper.CheckPrivilege(Privilege.DeleteOrder);
            bool flag = true;
            count = 0;
            foreach (string str in noteIds)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    flag &= new SendNoteDao().DelSendNote(str);
                    if (flag)
                    {
                        count++;
                    }
                }
            }
            return flag;
        }

        public static DbQueryResult GetAllDebitNote(DebitNoteQuery query)
        {
            return new DebitNoteDao().GetAllDebitNote(query);
        }

        public static DbQueryResult GetAllSendNote(RefundApplyQuery query)
        {
            return new SendNoteDao().GetAllSendNote(query);
        }

        private static string getEMSLastNum(string emsno)
        {
            List<char> list = emsno.ToList<char>();
            char ch = list[2];
            int num = int.Parse(ch.ToString()) * 8;
            char ch2 = list[3];
            num += int.Parse(ch2.ToString()) * 6;
            char ch3 = list[4];
            num += int.Parse(ch3.ToString()) * 4;
            char ch4 = list[5];
            num += int.Parse(ch4.ToString()) * 2;
            char ch5 = list[6];
            num += int.Parse(ch5.ToString()) * 3;
            char ch6 = list[7];
            num += int.Parse(ch6.ToString()) * 5;
            char ch7 = list[8];
            num += int.Parse(ch7.ToString()) * 9;
            char ch8 = list[9];
            num += int.Parse(ch8.ToString()) * 7;
            num = 11 - (num % 11);
            switch (num)
            {
                case 10:
                    num = 0;
                    break;

                case 11:
                    num = 5;
                    break;
            }
            return num.ToString();
        }

        private static string getEMSNext(string emsno)
        {
            long num = Convert.ToInt64(emsno.Substring(2, 8));
            if (num < 0x5f5e0ffL)
            {
                num += 1L;
            }
            string str = num.ToString().PadLeft(8, '0');
            string str2 = emsno.Substring(0, 2) + str + emsno.Substring(10, 1);
            return (emsno.Substring(0, 2) + str + getEMSLastNum(str2) + emsno.Substring(11, 2));
        }

        private static string GetNextExpress(string ExpressCom, string strno)
        {
            switch (ExpressCom.ToLower())
            {
                case "ems":
                    return getEMSNext(strno);

                case "顺丰快递":
                case "shunfeng":
                    return getSFNext(strno);

                case "宅急送":
                case "zhaijisong":
                    return getZJSNext(strno);
            }
            long num = long.Parse(strno) + 1L;
            return num.ToString();
        }

        public static DataSet GetOrderGoods(string orderIds)
        {
            return new OrderDao().GetOrderGoods(orderIds);
        }

        public static OrderInfo GetOrderInfo(string orderId)
        {
            return new OrderDao().GetOrderInfo(orderId);
        }

        public static DbQueryResult GetOrders(OrderQuery query)
        {
            return new OrderDao().GetOrders(query);
        }

        public static DataSet GetOrdersAndLines(string orderIds)
        {
            return new OrderDao().GetOrdersAndLines(orderIds);
        }

        public static DataSet GetProductGoods(string orderIds)
        {
            return new OrderDao().GetProductGoods(orderIds);
        }

        public static DataTable GetSendGoodsOrders(string orderIds)
        {
            return new OrderDao().GetSendGoodsOrders(orderIds);
        }

        private static string getSFNext(string sfno)
        {
            int[] numArray = new int[12];
            int[] numArray2 = new int[12];
            List<char> list = sfno.ToList<char>();
            string str = sfno.Substring(0, 11);
            string source = string.Empty;
            if (sfno.Substring(0, 1) == "0")
            {
                source = "0" + ((Convert.ToInt64(str) + 1L)).ToString();
            }
            else
            {
                source = (Convert.ToInt64(str) + 1L).ToString();
            }
            for (int i = 0; i < 12; i++)
            {
                numArray[i] = int.Parse(list[i].ToString());
            }
            source.ToList<char>();
            for (int j = 0; j < 11; j++)
            {
                numArray2[j] = int.Parse(source[j].ToString());
            }
            if (((numArray2[8] - numArray[8]) == 1) && ((numArray[8] % 2) == 1))
            {
                if ((numArray[11] - 8) >= 0)
                {
                    numArray2[11] = numArray[11] - 8;
                }
                else
                {
                    numArray2[11] = (numArray[11] - 8) + 10;
                }
            }
            else if (((numArray2[8] - numArray[8]) == 1) && ((numArray[8] % 2) == 0))
            {
                if ((numArray[11] - 7) >= 0)
                {
                    numArray2[11] = numArray[11] - 7;
                }
                else
                {
                    numArray2[11] = (numArray[11] - 7) + 10;
                }
            }
            else if (((numArray[9] == 3) || (numArray[9] == 6)) && (numArray[10] == 9))
            {
                if ((numArray[11] - 5) >= 0)
                {
                    numArray2[11] = numArray[11] - 5;
                }
                else
                {
                    numArray2[11] = (numArray[11] - 5) + 10;
                }
            }
            else if (numArray[10] == 9)
            {
                if ((numArray[11] - 4) >= 0)
                {
                    numArray2[11] = numArray[11] - 4;
                }
                else
                {
                    numArray2[11] = (numArray[11] - 4) + 10;
                }
            }
            else if ((numArray[11] - 1) >= 0)
            {
                numArray2[11] = numArray[11] - 1;
            }
            else
            {
                numArray2[11] = (numArray[11] - 1) + 10;
            }
            return (source + numArray2[11].ToString());
        }

        public static int GetSkuStock(string skuId)
        {
            return new SkuDao().GetSkuItem(skuId).Stock;
        }

        private static string getZJSNext(string zjsno)
        {
            long num = Convert.ToInt64(zjsno) + 11L;
            if ((num % 10L) > 6L)
            {
                num -= 7L;
            }
            return num.ToString().PadLeft(zjsno.Length, '0');
        }

        public static bool MondifyAddress(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.EditOrders);
            if (!order.CheckAction(OrderActions.MASTER_SELLER_MODIFY_DELIVER_ADDRESS))
            {
                return false;
            }
            bool flag = new OrderDao().UpdateOrder(order, null);
            if (flag)
            {
                EventLogs.WriteOperationLog(Privilege.EditOrders, string.Format(CultureInfo.InvariantCulture, "修改了订单“{0}”的收货地址", new object[] { order.OrderId }));
            }
            return flag;
        }

        private static void ReducedPoint(OrderInfo order, MemberInfo member)
        {
            PointDetailInfo pointDetailInfo = new PointDetailInfo();

            pointDetailInfo.OrderId = order.OrderId;
            pointDetailInfo.UserId = member.UserId;
            pointDetailInfo.TradeDate = DateTime.Now;
            pointDetailInfo.TradeType = PointTradeType.Refund;
            pointDetailInfo.Reduced = new int?(order.Points);
            pointDetailInfo.Points = member.Points - pointDetailInfo.Reduced.Value;

            new PointDetailDao().AddPointDetail(pointDetailInfo);
        }

        public static bool SaveDebitNote(DebitNoteInfo note)
        {
            return new DebitNoteDao().SaveDebitNote(note);
        }

        public static bool SaveRemark(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.RemarkOrder);
            bool flag = new OrderDao().UpdateOrder(order, null);
            if (flag)
            {
                EventLogs.WriteOperationLog(Privilege.RemarkOrder, string.Format(CultureInfo.InvariantCulture, "对订单“{0}”进行了备注", new object[] { order.OrderId }));
            }
            return flag;
        }

        public static bool SaveSendNote(SendNoteInfo note)
        {
            return new SendNoteDao().SaveSendNote(note);
        }

        public static bool SendGoods(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.OrderSendGoods);
            bool flag = false;
            if (order.CheckAction(OrderActions.SELLER_SEND_GOODS))
            {
                OrderDao dao = new OrderDao();
                order.OrderStatus = OrderStatus.SellerAlreadySent;
                order.ShippingDate = new DateTime?(DateTime.Now);
                flag = dao.UpdateOrder(order, null);
                string str = "";
                if (!flag)
                {
                    return flag;
                }
                foreach (LineItemInfo info in order.LineItems.Values)
                {
                    if ((info.OrderItemsStatus == OrderStatus.BuyerAlreadyPaid) || (info.OrderItemsStatus == OrderStatus.WaitBuyerPay))
                    {
                        str = str + "'" + info.SkuId + "',";
                    }
                }
                if (!string.IsNullOrEmpty(str))
                {
                    dao.UpdateItemsStatus(order.OrderId, 3, str.Substring(0, str.Length - 1));
                }
                if (order.Gateway.ToLower() == "hishop.plugins.payment.podrequest")
                {
                    dao.UpdatePayOrderStock(order.OrderId);
                    foreach (LineItemInfo info2 in order.LineItems.Values)
                    {
                        str = str + info2.SkuId + ",";
                        ProductDao dao2 = new ProductDao();
                        ProductInfo productDetails = dao2.GetProductDetails(info2.ProductId);
                        productDetails.SaleCounts += info2.Quantity;
                        productDetails.ShowSaleCounts += info2.Quantity;
                        dao2.UpdateProduct(productDetails, null);
                    }
                    UpdateUserAccount(order);
                }
                EventLogs.WriteOperationLog(Privilege.OrderSendGoods, string.Format(CultureInfo.InvariantCulture, "发货编号为\"{0}\"的订单", new object[] { order.OrderId }));
            }
            return flag;
        }

        public static bool SetOrderExpressComputerpe(string purchaseOrderIds, string expressCompanyName, string expressCompanyAbb)
        {
            return new OrderDao().SetOrderExpressComputerpe(purchaseOrderIds, expressCompanyName, expressCompanyAbb);
        }

        public static void SetOrderPrinted(string[] orderIds)
        {
            foreach (string str in orderIds)
            {
                OrderInfo orderInfo = new OrderDao().GetOrderInfo(str);
                orderInfo.IsPrinted = true;
                new OrderDao().UpdateOrder(orderInfo, null);
            }
        }

        public static long GetMaxPrintBatch()
        {
            return new OrderDao().GetMaxPrintBatch();
        }

        public static bool UpdateSetPrintBatch(string orderId, long printBatch, DateTime printBatchTime, bool isPrinted)
        {
            return new OrderDao().UpdateSetPrintBatch(orderId, printBatch, printBatchTime, isPrinted);
        }

        public static bool SetOrderShipNumber(string orderId, string startNumber)
        {
            OrderInfo orderInfo = new OrderDao().GetOrderInfo(orderId);
            orderInfo.ShipOrderNumber = startNumber;
            return new OrderDao().UpdateOrder(orderInfo, null);
        }

        public static void SetOrderShipNumber(string[] orderIds, string startNumber, string ExpressCom = "")
        {
            string strno = startNumber;
            OrderDao dao = new OrderDao();
            for (int i = 0; i < orderIds.Length; i++)
            {
                if (i != 0)
                {
                    strno = GetNextExpress(ExpressCom, strno);
                }
                else
                {
                    GetNextExpress(ExpressCom, strno);
                }
                dao.EditOrderShipNumber(orderIds[i], strno);
            }
        }

        public static bool SetOrderShippingMode(string orderIds, int realShippingModeId, string realModeName)
        {
            return new OrderDao().SetOrderShippingMode(orderIds, realShippingModeId, realModeName);
        }

        public static void SetOrderState(string orderId, OrderStatus orderStatus)
        {
            OrderDao dao = new OrderDao();
            OrderInfo orderInfo = dao.GetOrderInfo(orderId);
            orderInfo.OrderStatus = orderStatus;
            dao.UpdateOrder(orderInfo, null);
        }

        public static bool UpdateLineItem(string sku, OrderInfo order, int quantity)
        {
            bool flag;
            ManagerHelper.CheckPrivilege(Privilege.EditOrders);
            using (DbConnection connection = DatabaseFactory.CreateDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction dbTran = connection.BeginTransaction();
                try
                {
                    order.LineItems[sku].Quantity = quantity;
                    order.LineItems[sku].ShipmentQuantity = quantity;
                    order.LineItems[sku].ItemAdjustedPrice = order.LineItems[sku].ItemListPrice;
                    if (!new LineItemDao().UpdateLineItem(order.OrderId, order.LineItems[sku], dbTran))
                    {
                        dbTran.Rollback();
                        return false;
                    }
                    if (!new OrderDao().UpdateOrder(order, dbTran))
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
            if (flag)
            {
                EventLogs.WriteOperationLog(Privilege.EditOrders, string.Format(CultureInfo.InvariantCulture, "修改了订单号为\"{0}\"的订单商品数量", new object[] { order.OrderId }));
            }
            return flag;
        }

        public static bool UpdateOrderAmount(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.EditOrders);
            bool flag = false;
            if (order.CheckAction(OrderActions.SELLER_MODIFY_TRADE))
            {
                flag = new OrderDao().UpdateOrder(order, null);
                if (flag)
                {
                    EventLogs.WriteOperationLog(Privilege.EditOrders, string.Format(CultureInfo.InvariantCulture, "修改了编号为\"{0}\"订单的金额", new object[] { order.OrderId }));
                }
            }
            return flag;
        }

        public static bool UpdateOrderPaymentType(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.EditOrders);
            if (!order.CheckAction(OrderActions.MASTER_SELLER_MODIFY_PAYMENT_MODE))
            {
                return false;
            }
            bool flag = new OrderDao().UpdateOrder(order, null);
            if (flag)
            {
                EventLogs.WriteOperationLog(Privilege.EditOrders, string.Format(CultureInfo.InvariantCulture, "修改了订单“{0}”的支付方式", new object[] { order.OrderId }));
            }
            return flag;
        }

        public static bool UpdateOrderShippingMode(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.EditOrders);
            if (!order.CheckAction(OrderActions.MASTER_SELLER_MODIFY_SHIPPING_MODE))
            {
                return false;
            }
            bool flag = new OrderDao().UpdateOrder(order, null);
            if (flag)
            {
                EventLogs.WriteOperationLog(Privilege.EditOrders, string.Format(CultureInfo.InvariantCulture, "修改了订单“{0}”的配送方式", new object[] { order.OrderId }));
            }
            return flag;
        }

        public static IList<CommissionInfo> GetCommissionByOrderId(string orderId)
        {
            return new OrderDao().GetCommissionByOrderId(orderId);
        }

        public static bool UpdateDeductionCommission(int userId, string orderId, int commId, int commOrderStatus, decimal commTotal)
        {
            return new OrderDao().UpdateDeductionCommission(userId, orderId, commId, commOrderStatus, commTotal);
        }

        public static bool IsExistCommissionByUser(CommissionInfo ci)
        {
            return new OrderDao().IsExistCommissionByUser(ci);
        }

        public static bool UpdateDeductionVirtualPoint(OrderInfo order)
        {
            return new OrderDao().UpdateDeductionVirtualPoint(order);
        }

        private static void UpdateUserAccount(OrderInfo order)
        {
            MemberInfo member = new MemberDao().GetMember(order.UserId);
            if (member != null)
            {
                MemberDao dao = new MemberDao();
                PointDetailInfo point = new PointDetailInfo
                {
                    OrderId = order.OrderId,
                    UserId = member.UserId,
                    TradeDate = DateTime.Now,
                    TradeType = PointTradeType.Bounty,
                    Increased = new int?(order.Points),
                    Points = order.Points + member.Points
                };
                if ((point.Points > 0x7fffffff) || (point.Points < 0))
                {
                    point.Points = 0x7fffffff;
                }
                PointDetailDao dao2 = new PointDetailDao();
                dao2.AddPointDetail(point);
                member.Expenditure += order.GetTotal();
                member.OrderNumber++;
                dao.Update(member);
                int historyPoint = dao2.GetHistoryPoint(member.UserId);
                List<MemberGradeInfo> memberGrades = new MemberGradeDao().GetMemberGrades() as List<MemberGradeInfo>;
                MemberGradeInfo memberGrade = MemberHelper.GetMemberGrade(member.GradeId);
                if ((memberGrade == null) || (memberGrade.Points <= historyPoint))
                {
                    foreach (MemberGradeInfo info4 in from item in memberGrades
                                                      orderby item.Points descending
                                                      select item)
                    {
                        if (member.GradeId == info4.GradeId)
                        {
                            break;
                        }
                        if (info4.Points <= historyPoint)
                        {
                            member.GradeId = info4.GradeId;
                            dao.Update(member);
                            break;
                        }
                    }
                }
            }
        }

        public static DataTable GetExportOrderSubLogistic(string orderIds)
        {
            return new OrderDao().GetExportOrderSubLogistic(orderIds);
        }


    }
}

