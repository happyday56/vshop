namespace Hidistro.Entities.Orders
{
    using System;

    public enum OrderStatus
    {
        All = 0,
        // 申请退款
        ApplyForRefund = 6,
        // 申请换货
        ApplyForReplacement = 8,
        // 申请退货
        ApplyForReturns = 7,
        // 已付款,等待发货
        BuyerAlreadyPaid = 2,
        // 已关闭
        Closed = 4,
        // 订单已完成
        Finished = 5,
        // 历史订单
        History = 0x63,
        // 已退款
        Refunded = 9,
        // 已退货
        Returned = 10,
        // 已发货
        SellerAlreadySent = 3,
        Today = 11,
        // 待付款
        WaitBuyerPay = 1
    }
}

