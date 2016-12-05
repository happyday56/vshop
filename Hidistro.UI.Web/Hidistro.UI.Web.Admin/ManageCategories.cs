using ASPNET.WebControls;
using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
    [PrivilegeCheck(Privilege.ProductCategory)]
    public class ManageCategories : AdminPage
    {
        protected System.Web.UI.WebControls.LinkButton btnOrder;
        protected System.Web.UI.WebControls.Button btnSetCommissions;
        protected Grid grdTopCategries;
        protected System.Web.UI.HtmlControls.HtmlInputText txtcategoryId;
        protected System.Web.UI.HtmlControls.HtmlInputText txtfirst;
        protected System.Web.UI.HtmlControls.HtmlInputText txtsecond;
        protected System.Web.UI.HtmlControls.HtmlInputText txtsecondtoone;
        protected System.Web.UI.HtmlControls.HtmlInputText txtsecondtothree;
        protected System.Web.UI.HtmlControls.HtmlInputText txtthird;
        protected System.Web.UI.HtmlControls.HtmlInputText txtthridtoone;
        private void BindData()
        {
            this.grdTopCategries.DataSource = CatalogHelper.GetSequenceCategories();
            this.grdTopCategries.DataBind();
        }
        private void btnOrder_Click(object sender, System.EventArgs e)
        {
            foreach (System.Web.UI.WebControls.GridViewRow row in this.grdTopCategries.Rows)
            {
                int result = 0;
                System.Web.UI.WebControls.TextBox box = (System.Web.UI.WebControls.TextBox)row.FindControl("txtSequence");
                if (int.TryParse(box.Text.Trim(), out result))
                {
                    int categoryId = (int)this.grdTopCategries.DataKeys[row.RowIndex].Value;
                    if (CatalogHelper.GetCategory(categoryId).DisplaySequence != result)
                    {
                        CatalogHelper.SwapCategorySequence(categoryId, result);
                    }
                }
            }
            HiCache.Remove("DataCache-Categories");
            this.BindData();
        }
        private void btnSetCommissions_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtcategoryId.Value) || System.Convert.ToInt32(this.txtcategoryId.Value) <= 0)
            {
                this.ShowMsg("请选择要编辑的佣金分类", false);
            }
            else
            {
                CategoryInfo categorys = this.GetCategorys();
                if (categorys != null)
                {
                    if (CatalogHelper.UpdateCategory(categorys) == CategoryActionStatus.Success)
                    {
                        this.ShowMsg("编缉商品佣金成功", true);
                        this.BindData();
                    }
                    else
                    {
                        this.ShowMsg("编缉商品分类错误,未知", false);
                    }
                }
            }
        }

        CategoryInfo GetCategorys()
        {
            #region 注释
            //CategoryInfo category = CatalogHelper.GetCategory(System.Convert.ToInt32(this.txtcategoryId.Value));

            //CategoryInfo result;

            //if (category == null)
            //{
            //    this.ShowMsg("无法获取当前分类", false);
            //    result = null;
            //}
            //else
            //{
            //    //category.FirstCommission = string.Concat(new string[]
            //    //{
            //    //    this.txtfirst.Value,
            //    //    "|",
            //    //    this.txtsecondtoone.Value,
            //    //    "|",
            //    //    this.txtthridtoone.Value
            //    //});
            //    //category.SecondCommission = this.txtsecond.Value + "|" + this.txtsecondtothree.Value;
            //    //category.ThirdCommission = this.txtthird.Value;

            //    category.FirstCommission = txtfirst.Value.Trim();
            //    category.SecondCommission = txtsecond.Value.Trim();
            //    category.ThirdCommission = txtthird.Value.Trim();

            //    bool flag = false;
            //    string[] array = category.FirstCommission.Split(new char[]
            //    {
            //        '|'
            //    });
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        string str = array[i];
            //        if (System.Convert.ToDecimal(str) < 0m || System.Convert.ToDecimal(str) > 100m)
            //        {
            //            this.ShowMsg("输入的佣金格式不正确！", false);
            //            flag = true;
            //            break;
            //        }
            //    }
            //    array = category.SecondCommission.Split(new char[]
            //    {
            //        '|'
            //    });
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        string str2 = array[i];
            //        if (System.Convert.ToDecimal(str2) <= 0m || System.Convert.ToDecimal(str2) > 100m)
            //        {
            //            this.ShowMsg("输入的佣金格式不正确！", false);
            //            flag = true;
            //            break;
            //        }
            //    }
            //    array = category.ThirdCommission.Split(new char[]
            //    {
            //        '|'
            //    });
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        string str3 = array[i];
            //        if (System.Convert.ToDecimal(str3) <= 0m || System.Convert.ToDecimal(str3) > 100m)
            //        {
            //            this.ShowMsg("输入的佣金格式不正确！", false);
            //            flag = true;
            //            break;
            //        }
            //    }
            //    if (flag)
            //    {
            //        result = null;
            //    }
            //    else
            //    {
            //        result = category;
            //    }
            //}
            //return result;
            #endregion

            CategoryInfo categoryInfo = null;
            int catetoryId = 0;

            if (!int.TryParse(txtcategoryId.Value, out catetoryId))
            {
                this.ShowMsg("无法获取当前分类", false);
            }
            else
            {
                categoryInfo = CatalogHelper.GetCategory(catetoryId);
                if (categoryInfo == null)
                {
                    this.ShowMsg("无法获取当前分类", false);
                }
                else
                {

                    decimal firstCommission = 0m;
                    decimal secondCommission = 0m;
                    decimal thirdCommission = 0m;

                    if (!decimal.TryParse(this.txtfirst.Value, out firstCommission))
                    {
                        this.ShowMsg("输入的佣金格式不正确！", false);
                        return null;
                    }

                    if (!decimal.TryParse(this.txtsecond.Value, out secondCommission))
                    {
                        this.ShowMsg("输入的佣金格式不正确！", false);
                        return null;
                    }

                    if (!decimal.TryParse(this.txtthird.Value, out thirdCommission))
                    {
                        this.ShowMsg("输入的佣金格式不正确！", false);
                        return null;
                    }

                    decimal minCommission = 1m;
                    decimal maxCommission = 100m;

                    if ((firstCommission < minCommission || firstCommission > maxCommission) || (secondCommission < minCommission || secondCommission > maxCommission) || (thirdCommission < minCommission || thirdCommission > maxCommission))
                    {
                        this.ShowMsg("输入的佣金格式不正确！", false);
                    }
                    else
                    {
                        categoryInfo.FirstCommission = this.txtfirst.Value;
                        categoryInfo.SecondCommission = this.txtsecond.Value;
                        categoryInfo.ThirdCommission = this.txtthird.Value;
                    }

                    //try
                    //{

                    //    //if (Convert.ToDecimal(categoryInfo.FirstCommission) < new decimal(1) || Convert.ToDecimal(categoryInfo.FirstCommission) > new decimal(100))
                    //    //{
                    //    //    this.ShowMsg("输入的佣金格式不正确！", false);
                    //    //}
                    //    //else if (Convert.ToDecimal(categoryInfo.SecondCommission) < new decimal(1) || Convert.ToDecimal(categoryInfo.SecondCommission) > new decimal(100))
                    //    //{
                    //    //    this.ShowMsg("输入的佣金格式不正确！", false);
                    //    //}
                    //    //else if (Convert.ToDecimal(categoryInfo.ThirdCommission) < new decimal(1) || Convert.ToDecimal(categoryInfo.ThirdCommission) > new decimal(100))
                    //    //{
                    //    //    this.ShowMsg("输入的佣金格式不正确！", false);
                    //    //}

                    //}
                    //catch //(Exception exception)
                    //{
                    //    this.ShowMsg("输入的佣金格式不正确！", false);
                    //}
                }
            }

            return categoryInfo;

        }

        private void grdTopCategries_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int rowIndex = ((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer).RowIndex;
            int categoryId = (int)this.grdTopCategries.DataKeys[rowIndex].Value;
            if (e.CommandName == "DeleteCagetory")
            {
                if (CatalogHelper.DeleteCategory(categoryId))
                {
                    this.ShowMsg("成功删除了指定的分类", true);
                }
                else
                {
                    this.ShowMsg("分类删除失败，未知错误", false);
                }
            }
            this.BindData();
        }
        private void grdTopCategries_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                int num = (int)System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Name").ToString();
                System.Web.UI.WebControls.Literal literal3 = (System.Web.UI.WebControls.Literal)e.Row.FindControl("litIsDisplayHome");

                if (num == 1)
                {
                    str = "<b>" + str + "</b>";
                }
                else
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl control = e.Row.FindControl("spShowImage") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    control.Visible = false;
                }
                for (int i = 1; i < num; i++)
                {
                    str = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str;
                }
                System.Web.UI.WebControls.Literal literal = e.Row.FindControl("lblCategoryName") as System.Web.UI.WebControls.Literal;
                literal.Text = str;
                if (literal3.Text == "1")
                {
                    literal3.Text = "首页显示";
                }
                else
                {
                    literal3.Text = "首页不显示";
                }
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.grdTopCategries.RowCommand += new System.Web.UI.WebControls.GridViewCommandEventHandler(this.grdTopCategries_RowCommand);
            this.grdTopCategries.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(this.grdTopCategries_RowDataBound);
            this.btnSetCommissions.Click += new System.EventHandler(this.btnSetCommissions_Click);
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            if (!this.Page.IsPostBack)
            {
                this.BindData();
            }
        }
    }
}
