namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VOrderManager : VWeiXinOAuthTemplatedWebControl
    {

        private VshopTemplatedRepeater vshoporders;

        protected override void AttachChildControls()
        {
            this.vshoporders = (VshopTemplatedRepeater)this.FindControl("vshoporders");
            int result = 0;
            int.TryParse(HttpContext.Current.Request.QueryString.Get("status"), out result);

            var currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null)
            {
                OrderQuery query = new OrderQuery
                {
                    UserId = new int?(Globals.GetCurrentMemberUserId())
                };
                
                if (result == 1)
                {
                     query.Status = OrderStatus.WaitBuyerPay;
                }
                else if (result == 2)
                {
                    query.Status = OrderStatus.BuyerAlreadyPaid;
                }
                else if (result == 5)
                {
                    query.Status = OrderStatus.Finished;
                }
                else
                {
                    //   this.litallnum.Text = DistributorsBrower.GetDistributorOrderCount(query).ToString();
                    query.Status = OrderStatus.All;
                    //    this.litfinishnum.Text = DistributorsBrower.GetDistributorOrderCount(query).ToString();
                }
            //    this.vshoporders.ItemDataBound += new RepeaterItemEventHandler(this.vshoporders_ItemDataBound);
                var data = DistributorsBrower.GetDistributorOrder(query);
                this.vshoporders.DataSource = data;
                this.vshoporders.DataBind();



            }
            else
            {
                this.Page.Response.Redirect("/Vshop/Default.aspx");
            }

            PageTitle.AddSiteNameTitle("店铺订单");
        }

        private void vshoporders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataTable table = ((DataView)DataBinder.Eval(e.Item.DataItem, "OrderItems")).ToTable();
            //decimal num = (decimal)table.Compute("sum(ItemAdjustedCommssion)", "true");
            //decimal num2 = (decimal)table.Compute("sum(itemsCommission)", "true");
            //FormatedMoneyLabel label = (FormatedMoneyLabel)e.Item.Controls[0].FindControl("lbladjustsum");
            //label.Text = num.ToString("F2");
            //Literal literal = (Literal)e.Item.Controls[0].FindControl("litCommission");
            //literal.Text = (num2 - num).ToString("F2");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vordermanager.html";
            }
            base.OnInit(e);
        }
    }
}

