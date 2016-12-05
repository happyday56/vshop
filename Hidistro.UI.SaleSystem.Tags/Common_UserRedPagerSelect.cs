namespace Hidistro.UI.SaleSystem.Tags
{
    using Hidistro.SaleSystem.Vshop;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Common_UserRedPagerSelect : WebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            DataTable userRedPagerCanUse = UserRedPagerBrower.GetUserRedPagerCanUse(this.CartTotal);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<button type=\"button\" class=\"btn btn-default dropdown-toggle\" data-toggle=\"dropdown\">请选择一个代金券<span class=\"caret\"></span></button>");
            builder.AppendLine("<ul class=\"dropdown-menu\" role=\"menu\">");
            if (userRedPagerCanUse.Rows.Count > 0)
            {
                builder.AppendLine("<li><a href=\"#\" name=\"0\" value=\"0\">暂不使用</a></li>");
            }
            foreach (DataRow row in userRedPagerCanUse.Rows)
            {
                object[] args = new object[] { row["RedPagerID"], row["RedPagerActivityName"], ((decimal) row["OrderAmountCanUse"]).ToString("F2"), ((decimal) row["Amount"]).ToString("F2") };
                builder.AppendFormat("<li><a href=\"#\" name=\"{0}\" value=\"{3}\">{1}(满{2}抵用{3})</a></li>", args).AppendLine();
            }
            builder.AppendLine("</ul>");
            writer.Write(builder.ToString());
        }

        public decimal CartTotal { get; set; }
    }
}

