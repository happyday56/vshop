namespace Hidistro.UI.SaleSystem.Tags
{
    using Hidistro.Core.Enums;
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Common_PrizeUsers : WebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Activity != null)
            {
                PrizeQuery page = new PrizeQuery {
                    ActivityId = this.Activity.ActivityId,
                    SortOrder = SortAction.Desc,
                    SortBy = "PrizeTime"
                };
                IOrderedEnumerable<PrizeRecordInfo> source = from a in VshopBrowser.GetPrizeList(page)
                    orderby a.PrizeTime descending
                    select a;
                StringBuilder builder = new StringBuilder();
                if ((source != null) && (source.Count<PrizeRecordInfo>() > 0))
                {
                    foreach (PrizeRecordInfo info in source)
                    {
                        if (!string.IsNullOrEmpty(info.CellPhone) && !string.IsNullOrEmpty(info.RealName))
                        {
                            builder.AppendFormat("<p>{0}&nbsp;&nbsp;{1} &nbsp;&nbsp;{2}</p>", info.Prizelevel, this.ShowCellPhone(info.CellPhone), info.RealName);
                        }
                    }
                    writer.Write(builder.ToString());
                }
                else
                {
                    builder.AppendFormat("<p>暂无获奖名单！</p>", new object[0]);
                }
            }
        }

        private string ShowCellPhone(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                return Regex.Replace(phone, @"(?im)(\d{3})(\d{4})(\d{4})", "$1****$3");
            }
            return "";
        }

        public LotteryActivityInfo Activity { get; set; }
    }
}

