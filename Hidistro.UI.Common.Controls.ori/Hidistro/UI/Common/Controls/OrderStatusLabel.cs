namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Entities.Orders;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OrderStatusLabel : Literal
    {
        protected override void Render(HtmlTextWriter writer)
        {
            base.Text = OrderInfo.GetOrderStatusName((OrderStatus) this.OrderStatusCode);
            base.Render(writer);
        }

        public object OrderStatusCode
        {
            get
            {
                return this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TwByAGQAZQByAFMAdABhAHQAdQBzAEMAbwBkAGUA")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TwByAGQAZQByAFMAdABhAHQAdQBzAEMAbwBkAGUA")] = value;
            }
        }
    }
}

