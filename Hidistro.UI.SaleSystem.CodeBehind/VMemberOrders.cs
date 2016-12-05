namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VMemberOrders : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litallnum;
        private Literal litfinishnum;
        private VshopTemplatedRepeater rptOrders;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("会员订单");
            int result = 0;
            int.TryParse(HttpContext.Current.Request.QueryString.Get("status"), out result);
            OrderQuery query = new OrderQuery();
            switch (result)
            {
                case 1:
                    query.Status = OrderStatus.WaitBuyerPay;
                    break;

                case 3:
                    query.Status = OrderStatus.SellerAlreadySent;
                    break;
            }
            this.rptOrders = (VshopTemplatedRepeater) this.FindControl("rptOrders");
            this.rptOrders.DataSource = MemberProcessor.GetUserOrder(Globals.GetCurrentMemberUserId(), query);
            this.rptOrders.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VMemberOrders.html";
            }
            base.OnInit(e);
        }
    }
}

