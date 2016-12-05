namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Entities.Orders;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OrderItemsStatus : Literal
    {
        public string GetOrderItemStatus(OrderStatus orderitem)
        {
            string str = "";
            switch (orderitem)
            {
                case OrderStatus.BuyerAlreadyPaid:
                    return string.Concat(new object[] { AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABhACAAYwBsAGEAcwBzAD0AIgBiAHQAbgAtAGgAYQB2AGUAIgAgAGgAcgBlAGYAPQAiAFIAZQBxAHUAZQBzAHQAUgBlAHQAdQByAG4ALgBhAHMAcAB4AD8AbwByAGQAZQByAEkAZAA9AA=="), this.OrderId, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JgBQAHIAbwBkAHUAYwB0AEkAZAA9AA=="), this.ProductId, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgA+ADN194sAkD5rPAAvAGEAPgA=") });

                case OrderStatus.SellerAlreadySent:
                    return string.Concat(new object[] { AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABhACAAYwBsAGEAcwBzAD0AIgBiAHQAbgAtAGgAYQB2AGUAIgAgAGgAcgBlAGYAPQAiAFIAZQBxAHUAZQBzAHQAUgBlAHQAdQByAG4ALgBhAHMAcAB4AD8AbwByAGQAZQByAEkAZAA9AA=="), this.OrderId, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JgBQAHIAbwBkAHUAYwB0AEkAZAA9AA=="), this.ProductId, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgA+ADN194sAkCeNPAAvAGEAPgA=") });

                case OrderStatus.Closed:
                case OrderStatus.Finished:
                case OrderStatus.ApplyForReplacement:
                    return str;

                case OrderStatus.ApplyForRefund:
                    return AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABhACAAYwBsAGEAcwBzAD0AIgBiAHQAbgAtAGgAYQB2AGUAIgA+AACQPmstTjwALwBhAD4A");

                case OrderStatus.ApplyForReturns:
                    return AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABhACAAYwBsAGEAcwBzAD0AIgBiAHQAbgAtAGgAYQB2AGUAIgA+AACQJ40tTjwALwBhAD4A");

                case OrderStatus.Refunded:
                    return AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABhACAAYwBsAGEAcwBzAD0AIgBiAHQAbgAtAGgAYQB2AGUAIgA+AACQPmuMWxBiPAAvAGEAPgA=");

                case OrderStatus.Returned:
                    return AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABhACAAYwBsAGEAcwBzAD0AIgBiAHQAbgAtAGgAYQB2AGUAIgA+AACQJ42MWxBiPAAvAGEAPgA=");
            }
            return str;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Text = this.GetOrderItemStatus((OrderStatus) this.OrderStatusCode);
            base.Render(writer);
        }

        public object OrderId
        {
            get
            {
                return this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TwByAGQAZQByAEkAZAA=")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TwByAGQAZQByAEkAZAA=")] = value;
            }
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

        public object ProductId
        {
            get
            {
                return this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UAByAG8AZAB1AGMAdABJAGQA")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UAByAG8AZAB1AGMAdABJAGQA")] = value;
            }
        }
    }
}

