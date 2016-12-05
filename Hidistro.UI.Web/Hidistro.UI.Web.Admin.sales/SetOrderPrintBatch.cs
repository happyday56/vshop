using Hidistro.ControlPanel.Sales;
using Hidistro.UI.ControlPanel.Utility;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin.sales
{
    public class SetOrderPrintBatch : AdminPage
    {
        protected string OrderId = string.Empty;
        protected string orderIds = string.Empty;
        protected Literal litBatchNo;

        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.Request["OrderIds"]))
                {
                    OrderId = Request["OrderIds"];

                    XTrace.WriteLine("批量打印时的订单列表：" + OrderId);

                    if (!string.IsNullOrWhiteSpace(OrderId) && OrderId.EndsWith(","))
                    {
                        OrderId = OrderId.Substring(0, OrderId.Length - 1);
                    }

                    XTrace.WriteLine("设置打印批次的订单ID：" + OrderId);

                    string[] orderIds = OrderId.Split(new char[]
                {
                    ','
                });

                    long printBatch = OrderHelper.GetMaxPrintBatch();
                    DateTime printBatchDate = DateTime.Now;

                    this.litBatchNo.Text = printBatch.ToString();

                    foreach (string str in orderIds)
                    {
                        OrderHelper.UpdateSetPrintBatch(str, printBatch, printBatchDate, true);
                    }

                }

            }

        }
    }
}
