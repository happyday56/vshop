namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VMyLogistics : VWeiXinOAuthTemplatedWebControl
    {
        protected override void AttachChildControls()
        {
            string str = this.Page.Request.QueryString["orderId"];
            if (string.IsNullOrEmpty(str))
            {
                base.GotoResourceNotFound("");
            }
            OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(str);
            Literal control = this.FindControl("litCompanyName") as Literal;
            Literal literal2 = this.FindControl("litLogisticsNumber") as Literal;
            Literal litExpressData = this.FindControl("litExpressData") as Literal;
            
            control.SetWhenIsNotNull(orderInfo.ExpressCompanyName);
            literal2.SetWhenIsNotNull(orderInfo.ShipOrderNumber);

            // 处理物流信息
            StringBuilder expressHref = new StringBuilder();
            string currExpress = "";

            if (!string.IsNullOrEmpty(orderInfo.ShipOrderNumber))
            {
                currExpress = Express.GetExpressData(orderInfo.ExpressCompanyAbb, orderInfo.ShipOrderNumber, 0, false);

                if (!string.IsNullOrEmpty(currExpress))
                {
                    //expressHref.AppendLine("<a href=\"javascript:DialogFrame('" + currExpress + "','查看快递单号',600,500);\">" + orderInfo.ExpressCompanyName + ":" + orderInfo.ShipOrderNumber + "</a>&nbsp;&nbsp;");
                    expressHref.AppendLine("<a href=\"" + currExpress + "\">" + orderInfo.ExpressCompanyName + ":" + orderInfo.ShipOrderNumber + "</a>&nbsp;&nbsp;");
                }
            }            

            IList<OrderSubLogisticInfo> subOrderLogisticList = ShoppingProcessor.GetSubOrderLogisticByOrderId(orderInfo.OrderId, false);
            if (null != subOrderLogisticList && subOrderLogisticList.Count > 0)
            {
                foreach (OrderSubLogisticInfo osli in subOrderLogisticList)
                {
                    if (orderInfo.ExpressCompanyAbb.EqualIgnoreCase(osli.SubExpressCompanyAbb) && orderInfo.ShipOrderNumber.EqualIgnoreCase(osli.SubShipOrderNumber))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(osli.SubShipOrderNumber))
                    {
                        currExpress = Express.GetExpressData(osli.SubExpressCompanyAbb, osli.SubShipOrderNumber, 0, false);
                        if (!string.IsNullOrEmpty(currExpress))
                        {
                            //expressHref.AppendLine("<a href=\"javascript:DialogFrame('" + currExpress + "','查看快递单号',600,500);\">" + osli.SubExpressCompanyName + ":" + osli.SubShipOrderNumber + "</a>&nbsp;&nbsp;");
                            expressHref.AppendLine("<a href=\"" + currExpress + "\">" + osli.SubExpressCompanyName + ":" + osli.SubShipOrderNumber + "</a>&nbsp;&nbsp;");
                        }
                    }                    
                }
            }

            litExpressData.Text = expressHref.ToString(); 

            PageTitle.AddSiteNameTitle("我的物流");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vMyLogistics.html";
            }
            base.OnInit(e);
        }
    }
}

