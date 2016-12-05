namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VRequestReturn : VshopTemplatedWebControl
    {
        private HtmlInputHidden hidorderid;
        private HtmlInputHidden hidOrderStatus;
        private HtmlInputHidden hidproductid;
        private HtmlInputHidden hidskuid;
        private Literal litimage;
        private Literal litItemAdjustedPrice;
        private Literal litname;
        private Literal litQuantity;

        private Literal litOrderId;
        private Literal litOrderTotal;
        private Literal litProductList;

        private string orderId;
        private string ProductId;
        private VshopTemplatedRepeater rptOrderProducts;

        protected override void AttachChildControls()
        {
            this.hidOrderStatus = (HtmlInputHidden) this.FindControl("OrderStatus");
            this.hidskuid = (HtmlInputHidden) this.FindControl("skuid");
            this.hidorderid = (HtmlInputHidden) this.FindControl("orderid");
            this.hidproductid = (HtmlInputHidden) this.FindControl("productid");
            this.orderId = this.Page.Request.QueryString["orderId"].Trim();
            this.ProductId = this.Page.Request.QueryString["ProductId"].Trim();
            this.hidorderid.Value = this.orderId;
            this.hidproductid.Value = this.ProductId;
            this.litimage = (Literal) this.FindControl("litimage");
            this.litname = (Literal) this.FindControl("litname");
            this.litItemAdjustedPrice = (Literal) this.FindControl("litItemAdjustedPrice");
            this.litQuantity = (Literal) this.FindControl("litQuantity");

            this.litOrderId = (Literal)this.FindControl("litOrderId");
            this.litOrderTotal = (Literal)this.FindControl("litOrderTotal");
            this.litProductList = (Literal)this.FindControl("litProductList");

            this.rptOrderProducts = (VshopTemplatedRepeater) this.FindControl("rptOrderProducts");
            OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(this.orderId);
            this.hidOrderStatus.Value = ((int) orderInfo.OrderStatus).ToString();
            if (orderInfo == null)
            {
                base.GotoResourceNotFound("此订单已不存在");
            }
            // 新逻辑按订单退货，不按订单中的商品
            this.litOrderId.Text = orderInfo.OrderId;
            this.litOrderTotal.Text = orderInfo.OrderTotal.ToString("0.00");
            StringBuilder text = new StringBuilder();
            foreach (LineItemInfo info3 in orderInfo.LineItems.Values)
            {
                text.AppendLine("<div class=\"choose_goods_content\">");
                text.AppendLine("    <img src=\"" + info3.ThumbnailsUrl + "\">");
                text.AppendLine("    <div class=\"info\">");
                text.AppendLine("        <p>" + info3.ItemDescription + "</p>");
                text.AppendLine("        <div>");
                text.AppendLine("            ￥<e id=\"itemadjusteprice\">" + info3.ItemAdjustedPrice.ToString("0.00") + "</e><span>数量：<e id=\"quantity\">" + info3.Quantity + "</e></span>");
                text.AppendLine("        </div>");
                text.AppendLine("    </div>");
                text.AppendLine("</div>");
            }

            this.litProductList.Text = text.ToString();
            

            // 旧逻辑
            //bool flag = false;
            foreach (LineItemInfo info2 in orderInfo.LineItems.Values)
            {
                if (info2.ProductId.ToString() == this.ProductId)
                {
                    this.litimage.Text = "<image src=\"" + info2.ThumbnailsUrl + "\"></image>";
                    this.litname.Text = info2.ItemDescription;
                    this.litItemAdjustedPrice.Text = info2.ItemAdjustedPrice.ToString("0.00");
                    this.litQuantity.Text = info2.Quantity.ToString();
                    this.hidskuid.Value = info2.SkuId;
                    //flag = true;
                }
            }
            //if (!flag)
            //{
            //    base.GotoResourceNotFound("此订单商品不存在");
            //}
            PageTitle.AddSiteNameTitle("申请退货");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-VRequestReturn.html";
            }
            base.OnInit(e);
        }
    }
}

