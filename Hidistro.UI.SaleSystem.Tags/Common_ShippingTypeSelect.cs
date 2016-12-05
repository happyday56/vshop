namespace Hidistro.UI.SaleSystem.Tags
{
    using Hidistro.Entities.Sales;
    using Hidistro.SaleSystem.Vshop;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Common_ShippingTypeSelect : WebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            IList<ShippingModeInfo> shippingModes = ShoppingProcessor.GetShippingModes();
            if ((shippingModes != null) && (shippingModes.Count > 0))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(" <button type=\"button\" class=\"btn btn-default dropdown-toggle\" data-toggle=\"dropdown\">请选择配送方式<span class=\"caret\"></span></button>");
                builder.AppendLine("<ul id=\"shippingTypeUl\" class=\"dropdown-menu\" role=\"menu\">");
                foreach (ShippingModeInfo info in shippingModes)
                {
                    decimal num = 0M;
                    if (this.ShoppingCartItemInfo.Count != this.ShoppingCartItemInfo.Count<Hidistro.Entities.Sales.ShoppingCartItemInfo>(a => a.IsfreeShipping))
                    {
                        num = ShoppingProcessor.CalcFreight(this.RegionId, this.Weight, info);
                    }
                    string introduced5 = info.Name + "： ￥" + num.ToString("F2");
                    builder.AppendFormat("<li><a href=\"#\" name=\"{0}\" value=\"{2}\">{1}</a></li>", info.ModeId, introduced5, num.ToString("F2")).AppendLine();
                }
                builder.AppendLine("</ul>");
                writer.Write(builder.ToString());
            }
        }

        public int RegionId { get; set; }

        public IList<Hidistro.Entities.Sales.ShoppingCartItemInfo> ShoppingCartItemInfo { get; set; }

        public decimal Weight { get; set; }
    }
}

