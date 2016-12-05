namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.ControlPanel.Store;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Sales;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using NewLife.Log;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VShoppingCart : VWeiXinOAuthTemplatedWebControl
    {
        private HtmlAnchor aLink;
        private IList<ShoppingCartInfo> cart;
        private HtmlGenericControl divShowTotal;
        private Literal litcount;
        private Literal litExemption;
        private Literal litStoreMoney;
        private Literal littext;
        private Literal litTotal;
        private decimal ReductionMoneyALL;
        private VshopTemplatedRepeater rptCartProducts;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("购物车");

            this.rptCartProducts = (VshopTemplatedRepeater) this.FindControl("rptCartProducts");
            this.rptCartProducts.ItemDataBound += new RepeaterItemEventHandler(this.rptCartProducts_ItemDataBound);
            this.litTotal = (Literal) this.FindControl("litTotal");
            this.littext = (Literal) this.FindControl("littext");
            this.litStoreMoney = (Literal) this.FindControl("litStoreMoney");
            this.litExemption = (Literal) this.FindControl("litExemption");
            this.litcount = (Literal) this.FindControl("litcount");
            this.divShowTotal = (HtmlGenericControl) this.FindControl("divShowTotal");
            this.aLink = (HtmlAnchor) this.FindControl("aLink");
            this.Page.Session["stylestatus"] = "0";
            this.litExemption.Text = "0.00";
            this.cart = ShoppingCartProcessor.GetShoppingCartAviti();
            //MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            //if (currentMember == null)
            //{
            //    this.cart = null;
            //    // 调试代码
            //    //currentMember = new MemberInfo();
            //    //currentMember.UserId = 96;
            //    //currentMember.GradeId = 1;
            //    //this.cart = ShoppingCartProcessor.GetShoppingCartAviti(currentMember);

            //}
            //else
            //{
            //    this.cart = ShoppingCartProcessor.GetShoppingCartAviti(currentMember);
            //}
                        
            if (this.cart != null)
            {
                
                this.rptCartProducts.DataSource = this.cart;
                this.rptCartProducts.DataBind();
                int num = 0;
                for (int i = 0; i < this.cart.Count; i++)
                {
                    num += this.cart[i].LineItems.Count;
                }
                this.litcount.Text = num.ToString();
            }
            
            decimal num3 = 0M;
            decimal num9 = 0;
            if (this.cart != null)
            {
                foreach (ShoppingCartInfo info in this.cart)
                {
                    // 1.获取全类目下是否有排除的分类不参与
                    DataTable dtCategory = VShopHelper.GetActivitiesTypeNoCategoryId(0, info.CategoryId);
                    if ((null != dtCategory && dtCategory.Rows.Count > 0))
                    {
                        if (string.IsNullOrEmpty(dtCategory.Rows[0]["CategoryId"].ToString()))
                        {
                            num3 += info.GetAmount();
                        }
                        else
                        {
                            num9 += info.GetAmount();
                        }
                    }
                    else
                    {
                        num9 += info.GetAmount();
                    }
                    //num3 += info.GetAmount();
                }
            }
            
            DataTable allFull = ProductBrowser.GetAllFull(0);
            if (allFull.Rows.Count > 0)
            {
                decimal num4 = 0M;
                decimal num5 = 0M;
                string str = "";
                string str2 = "";
                string str3 = "";
                for (int j = 0; j < allFull.Rows.Count; j++)
                {
                    if (num3 >= decimal.Parse(allFull.Rows[allFull.Rows.Count - 1]["MeetMoney"].ToString()))
                    {
                        str = allFull.Rows[allFull.Rows.Count - 1]["ActivitiesName"].ToString();
                        num5 = decimal.Parse(allFull.Rows[allFull.Rows.Count - 1]["MeetMoney"].ToString());
                        num4 = decimal.Parse(allFull.Rows[allFull.Rows.Count - 1]["ReductionMoney"].ToString());
                        str3 = allFull.Rows[allFull.Rows.Count - 1]["ActivitiesType"].ToString();
                        str2 = allFull.Rows[allFull.Rows.Count - 1]["ActivitiesId"].ToString();
                        break;
                    }
                    if (num3 <= decimal.Parse(allFull.Rows[0]["MeetMoney"].ToString()))
                    {
                        str = allFull.Rows[0]["ActivitiesName"].ToString();
                        num5 = decimal.Parse(allFull.Rows[0]["MeetMoney"].ToString());
                        num4 = decimal.Parse(allFull.Rows[0]["ReductionMoney"].ToString());
                        str3 = allFull.Rows[0]["ActivitiesType"].ToString();
                        str2 = allFull.Rows[0]["ActivitiesId"].ToString();
                        break;
                    }
                    if (num3 >= decimal.Parse(allFull.Rows[j]["MeetMoney"].ToString()))
                    {
                        str = allFull.Rows[j]["ActivitiesName"].ToString();
                        num5 = decimal.Parse(allFull.Rows[j]["MeetMoney"].ToString());
                        num4 = decimal.Parse(allFull.Rows[j]["ReductionMoney"].ToString());
                        str3 = allFull.Rows[j]["ActivitiesType"].ToString();
                        str2 = allFull.Rows[j]["ActivitiesId"].ToString();
                    }
                }
                this.littext.Text = "";
                if (num3 >= num5)
                {
                    string text = this.littext.Text;
                    this.littext.Text = text + "<div id=\"cartProducts\" class=\"well shopcart\"><div class=\"price-sale\"><span class=\"title\">促销活动：</span><span class=\"purchase\"><a href=\"/Vshop/ActivityDetail.aspx?ActivitiesId=" + str2 + "&CategoryId=" + str3 + "\">" + str + "</a></span><span>已满" + num5.ToString("0") + "已减" + num4.ToString("0") + "</span></div></div>";
                    this.ReductionMoneyALL += num4;
                }
                else
                {
                    string str5 = this.littext.Text;
                    this.littext.Text = str5 + "<div id=\"cartProducts\" class=\"well shopcart\"><div class=\"price-sale\"><span class=\"title\">促销活动：</span><span class=\"purchase\"><a href=\"/Vshop/ActivityDetail.aspx?ActivitiesId=" + str2 + "&CategoryId=" + str3 + "\">" + str + "</a></span><span>满" + num5.ToString("0") + "减" + num4.ToString("0") + "</span></div></div>";
                }
                this.litExemption.Text = this.ReductionMoneyALL.ToString("0.00");
                if (num3 == 0M)
                {
                    this.littext.Text = "";
                }
            }
            this.litStoreMoney.Text = "￥" + num3.ToString("0.00");
            this.litTotal.Text = "￥" + ((num3 + num9 - this.ReductionMoneyALL)).ToString("0.00");
            XTrace.WriteLine("购物车金额：num3->" + num3 + "---num9->" + num9 + "---ReductionMoneyALL->" + this.ReductionMoneyALL);
            
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VShoppingCart.html";
            }
            base.OnInit(e);
        }

        private void rptCartProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal literal = (Literal) e.Item.Controls[0].FindControl("litpromotion");
                Literal literal1 = (Literal) e.Item.Controls[0].FindControl("litline");
                decimal num = 0M;
                foreach (ShoppingCartInfo info in this.cart)
                {
                    if (info.CategoryId == ((int) DataBinder.Eval(e.Item.DataItem, "CategoryId")))
                    {
                        num += info.GetAmount();
                    }
                }
                //DataTable allFull = ProductBrowser.GetAllFull((int) DataBinder.Eval(e.Item.DataItem, "CategoryId"));
                DataTable allFull = ProductBrowser.GetAllFullByCategoryId((int)DataBinder.Eval(e.Item.DataItem, "CategoryId"));
                string str = "";
                decimal num2 = 0M;
                decimal num3 = 0M;
                string str2 = "";
                string str3 = "";
                if (allFull.Rows.Count > 0)
                {
                    for (int i = 0; i < allFull.Rows.Count; i++)
                    {
                        if (num >= decimal.Parse(allFull.Rows[allFull.Rows.Count - 1]["MeetMoney"].ToString()))
                        {
                            str = allFull.Rows[allFull.Rows.Count - 1]["ActivitiesName"].ToString();
                            num2 = decimal.Parse(allFull.Rows[allFull.Rows.Count - 1]["MeetMoney"].ToString());
                            num3 = decimal.Parse(allFull.Rows[allFull.Rows.Count - 1]["ReductionMoney"].ToString());
                            str3 = allFull.Rows[allFull.Rows.Count - 1]["ActivitiesType"].ToString();
                            str2 = allFull.Rows[allFull.Rows.Count - 1]["ActivitiesId"].ToString();
                            break;
                        }
                        if (num <= decimal.Parse(allFull.Rows[0]["MeetMoney"].ToString()))
                        {
                            str = allFull.Rows[0]["ActivitiesName"].ToString();
                            num2 = decimal.Parse(allFull.Rows[0]["MeetMoney"].ToString());
                            num3 = decimal.Parse(allFull.Rows[0]["ReductionMoney"].ToString());
                            str3 = allFull.Rows[0]["ActivitiesType"].ToString();
                            str2 = allFull.Rows[0]["ActivitiesId"].ToString();
                            break;
                        }
                        if (num >= decimal.Parse(allFull.Rows[i]["MeetMoney"].ToString()))
                        {
                            str = allFull.Rows[i]["ActivitiesName"].ToString();
                            num2 = decimal.Parse(allFull.Rows[i]["MeetMoney"].ToString());
                            num3 = decimal.Parse(allFull.Rows[i]["ReductionMoney"].ToString());
                            str3 = allFull.Rows[i]["ActivitiesType"].ToString();
                            str2 = allFull.Rows[i]["ActivitiesId"].ToString();
                        }
                    }
                    if (allFull.Rows[0]["ActivitiesType"].ToString() != "0")
                    {
                        literal.Text = "";
                        if (num >= num2)
                        {
                            string text = literal.Text;
                            literal.Text = text + "<div class=\"price-sale\"><span class=\"title\">促销活动：</span><span class=\"purchase\"><a href=\"/Vshop/ActivityDetail.aspx?ActivitiesId=" + str2 + "&CategoryId=" + str3 + "\">" + str + "</a></span><span>已满" + num2.ToString("0") + "已减" + num3.ToString("0") + "</span></div>";
                            this.ReductionMoneyALL += num3;
                        }
                        else
                        {
                            string str5 = literal.Text;
                            literal.Text = str5 + "<div class=\"price-sale\"><span class=\"title\">促销活动：</span><span class=\"purchase\"><a href=\"/Vshop/ActivityDetail.aspx?ActivitiesId=" + str2 + "&CategoryId=" + str3 + "\">" + str + "</a></span><span>满" + num2.ToString("0") + "减" + num3.ToString("0") + "</span></div>";
                        }
                        this.litExemption.Text = this.ReductionMoneyALL.ToString("0.00");
                    }
                }
                if (num == 0M)
                {
                    literal.Text = "";
                }
            }
        }
    }
}

