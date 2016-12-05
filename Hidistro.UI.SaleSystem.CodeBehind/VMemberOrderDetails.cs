namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VMemberOrderDetails : VWeiXinOAuthTemplatedWebControl
    {
        private HyperLink hlinkGetRedPager;
        private Literal litActualPrice;
        private Literal litAddress;
        private Literal litBuildPrice;
        private Literal litCounponPrice;
        private Literal litDisCountPrice;
        private Literal litExemption;
        private Literal litOrderDate;
        private Literal litOrderId;
        private OrderStatusLabel litOrderStatus;
        private Literal litPayTime;
        private Literal litPhone;
        private Literal litRedPagerAmount;
        private Literal litRemark;
        private Literal litShippingCost;
        private Literal litShipTo;
        private Literal litShipToDate;
        private Literal litTotalPrice;
        private string orderId;
        private HtmlInputHidden orderStatus;
        private VshopTemplatedRepeater rptOrderProducts;
        private HtmlInputHidden txtOrderId;

        protected override void AttachChildControls()
        {
            this.orderId = this.Page.Request.QueryString["orderId"];
            this.litShipTo = (Literal) this.FindControl("litShipTo");
            this.litPhone = (Literal) this.FindControl("litPhone");
            this.litAddress = (Literal) this.FindControl("litAddress");
            this.litOrderId = (Literal) this.FindControl("litOrderId");
            this.litOrderDate = (Literal) this.FindControl("litOrderDate");
            this.litOrderStatus = (OrderStatusLabel) this.FindControl("litOrderStatus");
            this.rptOrderProducts = (VshopTemplatedRepeater) this.FindControl("rptOrderProducts");
            this.litTotalPrice = (Literal) this.FindControl("litTotalPrice");
            this.litPayTime = (Literal) this.FindControl("litPayTime");
            this.hlinkGetRedPager = (HyperLink) this.FindControl("hlinkGetRedPager");
            this.orderStatus = (HtmlInputHidden) this.FindControl("orderStatus");
            this.txtOrderId = (HtmlInputHidden) this.FindControl("txtOrderId");
            this.litRemark = (Literal) this.FindControl("litRemark");
            this.litShipToDate = (Literal) this.FindControl("litShipToDate");
            this.litShippingCost = (Literal) this.FindControl("litShippingCost");
            this.litCounponPrice = (Literal) this.FindControl("litCounponPrice");
            this.litRedPagerAmount = (Literal) this.FindControl("litRedPagerAmount");
            this.litExemption = (Literal) this.FindControl("litExemption");
            this.litBuildPrice = (Literal) this.FindControl("litBuildPrice");
            this.litDisCountPrice = (Literal) this.FindControl("litDisCountPrice");
            this.litActualPrice = (Literal) this.FindControl("litActualPrice");
            OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(this.orderId);
            if (orderInfo == null)
            {
                base.GotoResourceNotFound("此订单已不存在");
            }
            this.litShipTo.Text = orderInfo.ShipTo;
            this.litPhone.Text = orderInfo.CellPhone;
            this.litAddress.Text = orderInfo.ShippingRegion + orderInfo.Address;
            this.litOrderId.Text = this.orderId;
            this.litOrderDate.Text = orderInfo.OrderDate.ToString();
            this.litTotalPrice.SetWhenIsNotNull(orderInfo.GetAmount().ToString("F2"));
            this.litOrderStatus.OrderStatusCode = orderInfo.OrderStatus;
            this.litPayTime.SetWhenIsNotNull(orderInfo.PayDate.HasValue ? orderInfo.PayDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "");
            OrderRedPagerInfo orderRedPagerInfo = OrderRedPagerBrower.GetOrderRedPagerInfo(this.orderId);
            if ((orderRedPagerInfo != null) && (orderRedPagerInfo.MaxGetTimes > orderRedPagerInfo.AlreadyGetTimes))
            {
                this.hlinkGetRedPager.NavigateUrl = "/vshop/GetRedShare.aspx?orderid=" + this.orderId;
                this.hlinkGetRedPager.Visible = true;
            }
            this.orderStatus.SetWhenIsNotNull(((int) orderInfo.OrderStatus).ToString());
            this.txtOrderId.SetWhenIsNotNull(this.orderId.ToString());
            this.litCounponPrice.SetWhenIsNotNull(orderInfo.CouponValue.ToString("F2"));
            this.litRedPagerAmount.SetWhenIsNotNull(orderInfo.RedPagerAmount.ToString("F2"));
            this.litShippingCost.SetWhenIsNotNull(orderInfo.AdjustedFreight.ToString("F2"));
            this.litShipToDate.SetWhenIsNotNull(orderInfo.ShipToDate);
            this.litBuildPrice.SetWhenIsNotNull(orderInfo.GetAmount().ToString("F2"));
            this.litDisCountPrice.SetWhenIsNotNull(orderInfo.GetAdjustCommssion().ToString("F2"));
            this.litActualPrice.SetWhenIsNotNull(orderInfo.TotalPrice.ToString("F2"));
            this.litRemark.SetWhenIsNotNull(orderInfo.Remark);
            this.litExemption.SetWhenIsNotNull(orderInfo.DiscountAmount.ToString("F2"));
            this.rptOrderProducts.DataSource = orderInfo.LineItems.Values;
            this.rptOrderProducts.DataBind();
            PageTitle.AddSiteNameTitle("订单详情");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VMemberOrderDetails.html";
            }
            base.OnInit(e);
        }
    }
}

