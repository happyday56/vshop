namespace Hidistro.ControlPanel.Sales
{
    using Hidistro.ControlPanel.Promotions;
    using Hidistro.ControlPanel.Store;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.Promotions;
    using Hidistro.Entities.Store;
    using Hidistro.SqlDal;
    using Hidistro.SqlDal.Orders;
    using System;
    using System.Data;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Transactions;

    public static class RefundHelper
    {
        public static bool AddRefund(RefundInfo refundInfo)
        {
            return new RefundDao().AddRefund(refundInfo);
        }

        public static bool CloseTransaction(OrderInfo order)
        {
            ManagerHelper.CheckPrivilege(Privilege.EditOrders);
            order.OrderStatus = OrderStatus.Closed;
            bool flag = new OrderDao().UpdateOrder(order, null);
            if (order.GroupBuyId > 0)
            {
                GroupBuyInfo groupBuy = GroupBuyHelper.GetGroupBuy(order.GroupBuyId);
                groupBuy.SoldCount -= order.GetGroupBuyProductQuantity();
                GroupBuyHelper.UpdateGroupBuy(groupBuy);
            }
            if (flag)
            {
                EventLogs.WriteOperationLog(Privilege.EditOrders, string.Format(CultureInfo.InvariantCulture, "关闭了订单“{0}”", new object[] { order.OrderId }));
            }
            return flag;
        }

        public static bool DelRefundApply(string[] ReturnsIds, out int count)
        {
            ManagerHelper.CheckPrivilege(Privilege.OrderRefundApply);
            bool flag = true;
            count = 0;
            foreach (string str in ReturnsIds)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    if (new RefundDao().DelRefundApply(int.Parse(str)))
                    {
                        count++;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public static bool EnsureRefund(string orderId, string Operator, string adminRemark, int refundType, bool accept)
        {
            RefundDao dao = new RefundDao();
            RefundInfo byOrderId = dao.GetByOrderId(orderId);
            byOrderId.Operator = Operator;
            byOrderId.AdminRemark = adminRemark;
            byOrderId.HandleTime = DateTime.Now;
            byOrderId.HandleStatus = accept ? RefundInfo.Handlestatus.Refunded : RefundInfo.Handlestatus.Refused;
            byOrderId.OrderId = orderId;
            OrderInfo orderInfo = OrderHelper.GetOrderInfo(orderId);
            using (TransactionScope scope = new TransactionScope())
            {
                OrderHelper.SetOrderState(orderId, accept ? OrderStatus.Refunded : OrderStatus.BuyerAlreadyPaid);
                dao.UpdateByOrderId(byOrderId);
                if (orderInfo.GroupBuyId > 0)
                {
                    GroupBuyHelper.RefreshGroupFinishBuyState(orderInfo.GroupBuyId);
                }
                scope.Complete();
            }
            return true;
        }

        public static void GetRefundType(string orderId, out int refundType, out string remark)
        {
            new RefundDao().GetRefundType(orderId, out refundType, out remark);
        }

        public static void GetRefundTypeFromReturn(string orderId, out int refundType, out string remark)
        {
            new RefundDao().GetRefundTypeFromReturn(orderId, out refundType, out remark);
        }

        public static DbQueryResult GetReturnOrderAll(ReturnsApplyQuery returnsapplyquery)
        {
            return new RefundDao().GetReturnOrderAll(returnsapplyquery);
        }

        public static DbQueryResult GetReturnOrderDataAll(ReturnsApplyQuery returnsapplyquery)
        {
            return new RefundDao().GetReturnOrderDataAll(returnsapplyquery);
        }

        public static DataTable GetOrderProductByOrderId(string orderId)
        {
            return new RefundDao().GetOrderProductByOrderId(orderId);
        }

        public static bool InsertOrderRefund(RefundInfo refundInfo)
        {
            return new RefundDao().InsertOrderRefund(refundInfo);
        }

        public static bool UpdateByAuditReturnsId(RefundInfo refundInfo)
        {
            return new RefundDao().UpdateByAuditReturnsId(refundInfo);
        }

        public static bool UpdateByReturnsId(RefundInfo refundInfo)
        {
            return new RefundDao().UpdateByReturnsId(refundInfo);
        }

        public static bool UpdateOrderGoodStatu(string orderid, string skuid, int OrderItemsStatus)
        {
            return new RefundDao().UpdateOrderGoodStatu(orderid, skuid, OrderItemsStatus);
        }

        public static bool UpdateOrderGoodStatuReturn(string orderid, string skuid, int OrderItemsStatus)
        {
            return new RefundDao().UpdateOrderGoodStatuReturn(orderid, skuid, OrderItemsStatus);
        }

        public static bool UpdateRefundOrderStock(string Stock, string SkuId)
        {
            return new RefundDao().UpdateRefundOrderStock(Stock, SkuId);
        }

        public static RefundInfo GetRefundInfoByOrderId(string orderId)
        {
            return new RefundDao().GetByOrderId(orderId);
        }

    }
}

